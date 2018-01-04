using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.Leave_Module.DTOs;
using BeeHRM.ApplicationService.Leave_Module.Helper;
using BeeHRM.ApplicationService.Leave_Module.Serivices.Implementation;
using BeeHRM.ApplicationService.Leave_Module.Serivices.Interface;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Models;
using BeeHrmClientWeb.Utilities;
using BeeHrmInterface.GlobalSelectLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Leave
{
    public class LeaveUserController : BaseController
    {
        private ILeaveApplicationServices _LeaveAddAdmin;
        private ILeaveValidateHelper _ValidateLeave;
        private IDynamicSelectList _DynamicSelectList;
        private IEmployeeService _employeeServices;
        private ILeaveApplicationService _leaveServices;
        private IUnitOfWork _unitOfWork;
        private IOfficeServices _officeService;

        public LeaveUserController()
        {
            this._officeService = new OfficeServices();
            this._LeaveAddAdmin = new LeaveApplicationServices();
            this._ValidateLeave = new LeaveValidateHelper();
            this._DynamicSelectList = new DynamicSelectList();
            _leaveServices = new LeaveApplicationService();
            _employeeServices = new EmployeeService();
            _unitOfWork = new UnitOfWork();


        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();

        }
        #region LeaveBalance
        public ActionResult LeaveBalance()
        {
            try
            {
                LeaveBalanceModel result = new LeaveBalanceModel();
                result.LeaveBalance = _LeaveAddAdmin.LeaveBalanceSearch();
                result.LeaveBalance.EmpCode = Convert.ToInt32(Session["EmpCode"]);
                result.LeaveBalanceDetails = _LeaveAddAdmin.LeaveBalanceList(result.LeaveBalance.LeaveYearId, Convert.ToInt32(result.LeaveBalance.EmpCode));
                return View(result);


            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return View();
            }
        }
        [HttpPost]
        public ActionResult LeaveBalance(LeaveBalanceModel result)
        {
            try
            {
                result.LeaveBalance.EmpCode = Convert.ToInt32(Session["EmpCode"]);
                result.LeaveBalanceDetails = _LeaveAddAdmin.LeaveBalanceList(result.LeaveBalance.LeaveYearId, result.LeaveBalance.EmpCode);
                result.LeaveBalance = _LeaveAddAdmin.LeaveBalanceSearch();
                return View(result);


            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return View(result);
            }
        }
        #endregion

        #region LeaveApply 
        public ActionResult Index()
        {
            try
            {
                LeaveApplicationModel Result = new LeaveApplicationModel();
                int EmpCode = Convert.ToInt32(Session["EmpCode"]);

                Result.LeaveApplication = _LeaveAddAdmin.LeaveApplicationSearch();


                Result.LeaveApplicationDetails = _LeaveAddAdmin.LeaveapplicationList().Where(x => x.LeaveEmpCode == EmpCode).Take(50).OrderByDescending(x => x.LeaveStartDate);

                return View(Result);
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return View();
            }

        }
        [HttpPost]
        public ActionResult Index(LeaveApplicationModel Record)
        {
            int EmpCode = Convert.ToInt32(Session["EmpCode"]);
            int LeaveYearid = Record.LeaveApplication.LeaveYearId;
            int monthNumber = Record.MonthId;
            int leaveStatus = Record.LeaveApplication.LeaveStatus;


            try
            {
                //IEnumerable<LeaveApplicationDTOs> result = _LeaveAddAdmin.LeaveapplicationList();
                Record.LeaveApplication = _LeaveAddAdmin.LeaveApplicationSearch();

                var LeaveApplicationDetails = _LeaveAddAdmin.LeaveapplicationList().
                    Where(x => x.LeaveEmpCode == EmpCode).ToList();

                if (LeaveYearid > 0)
                {
                    LeaveApplicationDetails = LeaveApplicationDetails.Where(x => x.LeaveYearId == LeaveYearid).ToList();
                }
                if (leaveStatus > 0)
                {
                    LeaveApplicationDetails = LeaveApplicationDetails.Where(x => x.LeaveStatus == leaveStatus).ToList();
                }
                if (monthNumber > 0)
                {
                    LeaveApplicationDetails = LeaveApplicationDetails.Where(x => x.LeaveStartDate.Month == monthNumber).ToList();
                }

                LeaveApplicationDetails = LeaveApplicationDetails.OrderByDescending(x => x.LeaveStartDate).ToList();

                Record.LeaveApplicationDetails = LeaveApplicationDetails;

                return View(Record);
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return View(Record);
            }

        }
        public ActionResult LeaveApplistList()
        {
            try
            {
                int EmpCode = Convert.ToInt32(Session["EmpCode"]);
                IEnumerable<LeaveBalance> Result = _LeaveAddAdmin.LeaveBalanceList(null, EmpCode);
                return View(Result);
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return View();
            }
        }
        public ActionResult Create(int levid)
        {
            string recommendType = "General";
            try
            {

                int empcode = Convert.ToInt32(Session["EmpCode"]);

                LeaveApplicationDTOs Result = _LeaveAddAdmin.CreateLeaveApplicationInformation(empcode, levid, recommendType);

                return View(Result);
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                int empcode = Convert.ToInt32(Session["EmpCode"]);
                return RedirectToAction("LeaveApplistList");
            }
        }
        [HttpPost]
        public ActionResult Create(LeaveApplicationDTOs Record, int levid)
        {
            string recommendType = "General";
            int empcode = Convert.ToInt32(Session["EmpCode"]);
            Record.ApproverList = _DynamicSelectList.GetLeaveApproverSelectList(empcode, recommendType).ToList();
            Record.RecommenderList = _DynamicSelectList.GetLeaveRecommenderSelectList(empcode, recommendType).ToList();
            try
            {
                Record.LeaveStartDate = Convert.ToDateTime(NepEngDate.NepToEng(Record.LeaveStartDateNP));
                Record.LeaveEndDate = Convert.ToDateTime(NepEngDate.NepToEng(Record.LeaveEndDateNP));

                if (Record.IsHalfDay)
                {
                    Record.LeaveEndDate = Record.LeaveStartDate;
                }

                if (ModelState.IsValid)
                {
                    Record = _ValidateLeave.ValidateLeave(Record);

                    if (Record.ErrorList != null && Record.ErrorList.Count > 0)
                    {
                        return View(Record);
                    }
                    else
                    {
                        _LeaveAddAdmin.LeaveApplicationCreate(Record);
                        Session["success"] = "Leave Created Sucessfully ";
                        return RedirectToAction("Index");
                    }
                }
                else
                {

                    Record.ApproverList = _LeaveAddAdmin.CreateLeaveApplicationInformation(empcode, Record.LeaveTypeId, recommendType).ApproverList;

                    return View(Record);
                }



            }
            catch (Exception Ex)
            {

                Session["error"] = Ex.Message;
                Record.ApproverList = _LeaveAddAdmin.CreateLeaveApplicationInformation(empcode, Record.LeaveTypeId, recommendType).ApproverList;

                return View(Record);
            }
        }
        public ActionResult LeaveDetails(int id)
        {
            try
            {
                int empcode = Convert.ToInt32(Session["EmpCode"]);
                LeaveApplicationDTOs Result = _LeaveAddAdmin.LeaveDetails(id, empcode);
                if (empcode != Result.LeaveEmpCode)
                {
                    throw new Exception("you are not authorized to access this details.");
                }
                return View(Result);
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                int empcode = Convert.ToInt32(Session["EmpCode"]);
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region ApproveRecommend
        public ActionResult LeaveRecommendList()
        {
            try
            {
                LeaveApplicationModel Result = new LeaveApplicationModel();
                int EmpCode = Convert.ToInt32(Session["EmpCode"]);
                Result.LeaveApplication = _LeaveAddAdmin.LeaveApplicationSearch();
                Result.LeaveApplicationDetails = _LeaveAddAdmin.LeaveapplicationList().Where(x => x.RecommededEmpCode == EmpCode).OrderByDescending(x => x.LeaveStartDate).Take(100); ;
                return View(Result);
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return View();
            }

        }
        [HttpPost]
        public ActionResult LeaveRecommendList(LeaveApplicationModel Record)
        {
            int EmpCode = Convert.ToInt32(Session["EmpCode"]);
            int LeaveYearid = Record.LeaveApplication.LeaveYearId;
            int monthNumber = Record.MonthId;
            int leaveStatus = Record.LeaveApplication.LeaveStatus;
            int recommandStatus = Record.LeaveApplication.RecommendStatus;
            int LeaveEempCode = Record.LeaveApplication.LeaveEmpCode;
            try
            {
                LeaveApplicationModel Result = new LeaveApplicationModel();

                Result.LeaveApplication = _LeaveAddAdmin.LeaveApplicationSearch();
                var LeaveApplicationDetails = _LeaveAddAdmin.LeaveapplicationList().Where(x => x.RecommededEmpCode == EmpCode).OrderByDescending(x => x.LeaveStartDate).ToList();
                if (LeaveEempCode > 0)
                {
                    LeaveApplicationDetails = LeaveApplicationDetails.Where(x => x.LeaveEmpCode == LeaveEempCode).ToList();
                }
                if (LeaveYearid > 0)
                {
                    LeaveApplicationDetails = LeaveApplicationDetails.Where(x => x.LeaveYearId == LeaveYearid).ToList();
                }

                if (leaveStatus > 0)
                {
                    LeaveApplicationDetails = LeaveApplicationDetails.Where(x => x.LeaveStatus == leaveStatus).ToList();
                }
                if (recommandStatus > 0)
                {
                    LeaveApplicationDetails = LeaveApplicationDetails.Where(x => x.RecommendStatus == recommandStatus).ToList();
                }
                Result.LeaveApplicationDetails = LeaveApplicationDetails;

                return View(Result);
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return View();
            }

        }
        public ActionResult LeaveRecommend(int id)
        {
            try
            {
                int empcode = Convert.ToInt32(Session["EmpCode"]);
                LeaveApplicationDTOs Result = _LeaveAddAdmin.LeaveDetails(id, empcode);
                if (Result.RecommededEmpCode != empcode)
                {
                    throw new Exception("You are not authorized to access page .");
                }
                return View(Result);
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return RedirectToAction("LeaveRecommendList");
            }
        }
        [HttpPost]
        public ActionResult LeaveRecommend(int id, LeaveApplicationDTOs Record)
        {
            int empcode = Convert.ToInt32(Session["EmpCode"]);
            try
            {
                if (ModelState.IsValid)
                {
                    Record.RecommendStatusDate = DateTime.Now;
                    _LeaveAddAdmin.LeaveApplicationUpdae(Record);
                    Session["sucess"] = "Leave recommend stataus changed sucesfully";
                    return RedirectToAction("LeaveRecommendList");
                }
                else
                {
                    LeaveApplicationDTOs Result = _LeaveAddAdmin.LeaveDetails(id, empcode);
                    Record.EmployeeDetail = Result.EmployeeDetail;
                    Record.RecommenderDetails = Result.RecommenderDetails;
                    Record.Leavetypes = Result.Leavetypes;
                    Record.ApproverDetails = Result.ApproverDetails;
                    return View(Record);
                }

            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return RedirectToAction("LeaveRecommendList");
            }
        }
        public ActionResult ApproverList()
        {
            try
            {
                LeaveApplicationModel Result = new LeaveApplicationModel();
                int EmpCode = Convert.ToInt32(Session["EmpCode"]);
                Result.LeaveApplication = _LeaveAddAdmin.LeaveApplicationSearch();

                Result.LeaveApplicationDetails = _LeaveAddAdmin.LeaveapplicationList().Where(x => x.LeaveApproverEmpCode == EmpCode && x.RecommendStatus == 2).OrderByDescending(x => x.LeaveStartDate).Take(100);
                return View(Result);
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return View();
            }
        }
        [HttpPost]
        public ActionResult ApproverList(LeaveApplicationModel Record)
        {
            int EmpCode = Convert.ToInt32(Session["EmpCode"]);
            int LeaveYearid = Record.LeaveApplication.LeaveYearId;
            int monthNumber = Record.MonthId;
            int leaveStatus = Record.LeaveApplication.LeaveStatus;
            int recommandStatus = Record.LeaveApplication.RecommendStatus;
            int LeaveEempCode = Record.LeaveApplication.LeaveEmpCode;
            try
            {
                IEnumerable<LeaveApplicationDTOs> result = _LeaveAddAdmin.LeaveapplicationList();
                Record.LeaveApplication = _LeaveAddAdmin.LeaveApplicationSearch();

                // List<int> OfficeFilterList = _officeService.MyAccessOfficeList();

                //  var LeaveApplicationDetails = _LeaveAddAdmin.LeaveapplicationList().Where(x => x.LeaveEmpCode > 0 && OfficeFilterList.Contains(x.EmployeeDetail.EmpOfficeId)).ToList();
                var LeaveApplicationDetails = _LeaveAddAdmin.LeaveapplicationList().Where(x => x.LeaveApproverEmpCode == EmpCode && x.RecommendStatus == 2).OrderByDescending(x => x.LeaveStartDate).ToList();
                if (LeaveEempCode > 0)
                {
                    LeaveApplicationDetails = LeaveApplicationDetails.Where(x => x.LeaveEmpCode == LeaveEempCode).ToList();
                }

                if (LeaveYearid > 0)
                {
                    LeaveApplicationDetails = LeaveApplicationDetails.Where(x => x.LeaveYearId == LeaveYearid).ToList();
                }
                if (leaveStatus > 0)
                {
                    LeaveApplicationDetails = LeaveApplicationDetails.Where(x => x.LeaveStatus == leaveStatus).ToList();
                }
                if (recommandStatus > 0)
                {
                    LeaveApplicationDetails = LeaveApplicationDetails.Where(x => x.RecommendStatus == recommandStatus).ToList();
                }
                if (monthNumber > 0)
                {
                    LeaveApplicationDetails = LeaveApplicationDetails.Where(x => x.LeaveStartDate.Month == monthNumber).ToList();
                }

                LeaveApplicationDetails = LeaveApplicationDetails.OrderByDescending(x => x.LeaveStartDate).ToList();

                Record.LeaveApplicationDetails = LeaveApplicationDetails;

                return View(Record);
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return View(Record);
            }

        }
        public ActionResult LeaveApprove(int id)
        {
            try
            {
                int empcode = Convert.ToInt32(Session["EmpCode"]);
                LeaveApplicationDTOs Result = _LeaveAddAdmin.LeaveDetails(id, empcode);
                if (Result.LeaveApproverEmpCode != empcode && Result.RecommendStatus != 2)
                {
                    throw new Exception("You are not authorized to access page .");
                }
                return View(Result);
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return RedirectToAction("ApproverList");
            }
        }
        [HttpPost]
        public ActionResult LeaveApprove(int id, LeaveApplicationDTOs Record)
        {
            int empcode = Convert.ToInt32(Session["EmpCode"]);
            try
            {
                if (ModelState.IsValid)
                {
                    Record.LeaveStatusDate = DateTime.Now;
                    _LeaveAddAdmin.LeaveApplicationUpdae(Record);
                    Session["sucess"] = "Leave approved sucesfully";
                    return RedirectToAction("ApproverList");
                }
                else
                {
                    LeaveApplicationDTOs Result = _LeaveAddAdmin.LeaveDetails(id, empcode);
                    Record.EmployeeDetail = Result.EmployeeDetail;
                    Record.RecommenderDetails = Result.RecommenderDetails;
                    Record.LeaveDetails = Result.LeaveDetails;
                    Record.ApproverDetails = Result.ApproverDetails;

                    return View(Record);
                }

            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return RedirectToAction("ApproverList");
            }
        }


        public ActionResult RejectLeaveApplication(int id)
        {
            int EmpCode = Convert.ToInt32(Session["EmpCode"]);
            try
            {
                _LeaveAddAdmin.RejectEmployeeLeave(id, EmpCode);
                return RedirectToAction("LeaveDetails", "LeaveUser", new { id = EmpCode });
            }
            catch (Exception Exception)
            {

                Session["error"] = Exception.Message;
            }
            return View();
        }
        public ActionResult CancelMyLeaveApplication(int id)
        {
            int EmpCode = Convert.ToInt32(Session["EmpCode"]);
            try
            {
                _LeaveAddAdmin.CancelMyLeave(id, EmpCode);
                Session["sucess"] = "You canceld your applied leave sucesfully";
                return RedirectToAction("index", "LeaveUser");
            }
            catch (Exception Exception)
            {

                Session["error"] = Exception.Message;
            }
            return View();
        }
        #endregion

        #region Yearly and Monthly Leave Balance

        public ActionResult SelfLeaveYearlyReport()
        {
            LeaveYearlyReportWithFilter Record = new LeaveYearlyReportWithFilter();
            int empCode = Convert.ToInt32(Session["EmpCode"]);
            Record.EmployeeCode = empCode;
            Record.LeaveYearId = 1;
            Record.LeaveYearId = _unitOfWork.LeaveYearRepository.All().Where(x => x.YearCurrent == true).Select(x => x.YearId).FirstOrDefault();

            Record.LeaveYears = _leaveServices.GetLeaveYearSelectList();
            Record.LeaveYearlyReport = new List<LeaveYearlyReportDTO>();
            Record.PayrollLeaveDeduction = new List<PayrollLeaveDeductionDTO>();
            Record.LeaveYearlyReport = _leaveServices.GetLeaveYearlyReport(Record);
            return View(Record);
        }

        [HttpPost]
        public ActionResult SelfLeaveYearlyReport(LeaveYearlyReportWithFilter Record)
        {

            Record.LeaveYears = _leaveServices.GetLeaveYearSelectList();
            Record.LeaveYearlyReport = new List<LeaveYearlyReportDTO>();
            Record.PayrollLeaveDeduction = new List<PayrollLeaveDeductionDTO>();
            if (ModelState.IsValid)
            {
                Record.LeaveYearlyReport = _leaveServices.GetLeaveYearlyReport(Record);
                Record.PayrollLeaveDeduction = _leaveServices.GetPayrollLeaveDeduction(Record);
            }
            else
            {
                TempData["Danger"] = "Please fill in the required field";
                return View(Record);
            }
            return View(Record);
        }

        #endregion
    }

    internal interface IFiscalService
    {
    }
}