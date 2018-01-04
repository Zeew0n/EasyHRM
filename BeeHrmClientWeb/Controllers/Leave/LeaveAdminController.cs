using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.Leave_Module.DTOs;
using BeeHRM.ApplicationService.Leave_Module.Helper;
using BeeHRM.ApplicationService.Leave_Module.Serivices.Implementation;
using BeeHRM.ApplicationService.Leave_Module.Serivices.Interface;
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
    public class LeaveAdminController : BaseController
    {
        private ILeaveApplicationServices _LeaveAddAdmin;
        private ILeaveApplicationService _LeaveApplicationService;
        private ILeaveValidateHelper _ValidateLeave;
        private IDynamicSelectList _DynamicSelectList;
        private ILeaveEarnedService _LeaveEarnedService;
        private IOfficeServices _officeService;
        private IDesignationService _designationService;
        private IEmployeeService _employeeService;
        public LeaveAdminController()
        {
            this._LeaveApplicationService = new LeaveApplicationService();
            this._designationService = new DesignationService();
            this._LeaveAddAdmin = new LeaveApplicationServices();
            this._ValidateLeave = new LeaveValidateHelper();
            this._DynamicSelectList = new DynamicSelectList();
            this._LeaveEarnedService = new LeaveEarnedService();
            this._officeService = new OfficeServices();
            this._employeeService = new EmployeeService();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();

        }
        #region LeaveBalance
        public ActionResult LeaveBalance(int? Id)
        {

            if (Id == null)
            {
                Id = Convert.ToInt32(Session["EmpCode"]);
            }
            else
            {
                ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(Convert.ToInt32(Id));
            }

            try
            {
                LeaveBalanceModel result = new LeaveBalanceModel();
                result.LeaveBalance = _LeaveAddAdmin.LeaveBalanceSearch();
                result.LeaveBalance.EmpCodeList = _LeaveEarnedService.GetBrancheEmployeeSelectList(Convert.ToInt32(Session["EmpCode"]));
                if (Id >= 0)
                {
                    result.LeaveBalance.EmpCode = Convert.ToInt32(Id);
                }
                else
                {
                    result.LeaveBalance.EmpCode = Convert.ToInt32(Session["EmpCode"]);
                }
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
                result.LeaveBalanceDetails = _LeaveAddAdmin.LeaveBalanceList(result.LeaveBalance.LeaveYearId, result.LeaveBalance.EmpCode);
                result.LeaveBalance = _LeaveAddAdmin.LeaveBalanceSearch();
                result.LeaveBalance.EmpCodeList = _LeaveEarnedService.GetBrancheEmployeeSelectList(Convert.ToInt32(Session["EmpCode"]));
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

            int EmpCode = Convert.ToInt32(Session["Empcode"]);
            try
            {
                LeaveApplicationModel Result = new LeaveApplicationModel();
                Result.LeaveApplication = _LeaveAddAdmin.LeaveApplicationSearch();

                //

                List<int> OfficeFilterList = _officeService.MyAccessOfficeList();

                Result.LeaveApplicationDetails = _LeaveAddAdmin.LeaveapplicationList().Where(x => x.LeaveEmpCode > 0 && OfficeFilterList.Contains(x.EmployeeDetail.EmpOfficeId)).Take(100).ToList();

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
            int recommandStatus = Record.LeaveApplication.RecommendStatus;
            int LeaveEempCode = Record.LeaveApplication.LeaveEmpCode;
            try
            {
                IEnumerable<LeaveApplicationDTOs> result = _LeaveAddAdmin.LeaveapplicationList();
                Record.LeaveApplication = _LeaveAddAdmin.LeaveApplicationSearch();

                List<int> OfficeFilterList = _officeService.MyAccessOfficeList();

                var LeaveApplicationDetails = _LeaveAddAdmin.LeaveapplicationList().Where(x => x.LeaveEmpCode > 0 && OfficeFilterList.Contains(x.EmployeeDetail.EmpOfficeId)).ToList();
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
                ViewBag.EmpCode = LeaveEempCode;
                // Record.LeaveApplicationDetails = _LeaveAddAdmin.LeaveapplicationList().Where(x => x.RecommendStatus == Record.LeaveApplication.RecommendStatus || x.LeaveStatus == Record.LeaveApplication.LeaveStatus || x.LeaveEmpCode == Record.LeaveApplication.LeaveEmpCode);
                return View(Record);
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return View(Record);
            }

        }
        public ActionResult LeaveApplistList(int EmpCode)
        {
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(EmpCode);

            try
            {

                IEnumerable<LeaveBalance> Result = _LeaveAddAdmin.LeaveBalanceList(null, EmpCode);
                return View(Result);
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return View();
            }
        }
        public ActionResult Create(int EmpCode, int levid)
        {
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(EmpCode);
            string recommendType = "General";
            try
            {
                int empcode = Convert.ToInt32(Session["EmpCode"]);
                if (empcode == EmpCode)
                {
                    throw new Exception("User cannot create self Leve request from Admin side..");
                }

                LeaveApplicationDTOs Result = _LeaveAddAdmin.CreateLeaveApplicationInformation(EmpCode, levid, recommendType);
                Result.LeaveTypeId = levid;
                Result.LeaveEmpCode = EmpCode;
                return View(Result);
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return RedirectToAction("LeaveApplistList", new { EmpCode = EmpCode });
            }
        }
        [HttpPost]
        public ActionResult Create(LeaveApplicationDTOs Record, int EmpCode, int levid)
        {
            string recommendType = "General";
            Record.LeaveStartDate = !string.IsNullOrEmpty(Record.LeaveStartDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(Record.LeaveStartDateNP)) : Record.LeaveStartDate;
            Record.LeaveEndDate = !string.IsNullOrEmpty(Record.LeaveEndDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(Record.LeaveEndDateNP)) : Record.LeaveEndDate;

            try
            {
                if (Record.IsHalfDay == true)
                {
                    Record.LeaveEndDate = Record.LeaveStartDate;
                }


                if (ModelState.IsValid)
                {
                    LeaveApplicationDTOs data = _ValidateLeave.ValidateLeave(Record);

                    if (data.ErrorList != null && data.ErrorList.Count > 0)
                    {
                        data.RecommenderList = _DynamicSelectList.GetLeaveRecommenderSelectList(EmpCode, recommendType).ToList();
                        data.ApproverList = _LeaveAddAdmin.CreateLeaveApplicationInformation(EmpCode, levid, recommendType).ApproverList;
                        return View(data);
                    }
                    else
                    {
                        _LeaveAddAdmin.LeaveApplicationCreate(Record);
                        Session["success"] = "Leave Created Sucessfully ";
                        return RedirectToAction("LeaveApplistList", new { EmpCode = EmpCode });
                    }
                }
                else
                {



                    Record.RecommenderList = _DynamicSelectList.GetLeaveRecommenderSelectList(EmpCode, recommendType).ToList();
                    Record.ApproverList = _LeaveAddAdmin.CreateLeaveApplicationInformation(EmpCode, levid, recommendType).ApproverList;
                    return View(Record);
                }



            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                Record.RecommenderList = _DynamicSelectList.GetLeaveRecommenderSelectList(EmpCode, recommendType).ToList();
                Record.ApproverList = _LeaveAddAdmin.CreateLeaveApplicationInformation(EmpCode, levid, recommendType).ApproverList;
                return View(Record);
            }
        }

        public ActionResult LeaveDetails(int id, int empcode)
        {
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(empcode);
            try
            {
                LeaveApplicationDTOs Result = _LeaveAddAdmin.LeaveDetails(id, empcode);
                return View(Result);
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return RedirectToAction("Index");
            }
        }
        public ActionResult LeaveCancelApproved(int id, int empcode)
        {
            LeaveApplicationDTOs Result = _LeaveAddAdmin.LeaveDetails(id, empcode);
            Result.LeaveStatus = 5;
            _LeaveAddAdmin.LeaveApplicationUpdae(Result);
            Session["success"] = "Approved leave canceled sucessfully ";
            return RedirectToAction("LeaveDetails", new { Id = id, empcode = empcode });
        }

        #endregion
        #region Leavessign 
        public ActionResult LeaveAssign(int Id)
        {

            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(Id);
            try
            {
                LeaveAssignedDTOs Result = _LeaveAddAdmin.LeaveAssignCreateInfo(Id);
                Result.AssignEmpCode = Id;
                return View(Result);
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                return View();
            }
        }
        [HttpPost]
        public ActionResult LeaveAssign(int Id, LeaveAssignedDTOs data)
        {
            try
            {
                _LeaveAddAdmin.CreateLeaveAssign(data);
                //data.NepaliMonth = _LeaveAddAdmin.LeaveAssignCreateInfo(Id).NepaliMonth;
                //data = _LeaveAddAdmin.LeaveAssignCreateInfo(Id);
                return RedirectToAction("LeaveAssign", new { Id = Id });
            }
            catch (Exception Ex)
            {
                Session["error"] = Ex.Message;
                LeaveAssignedDTOs Result = _LeaveAddAdmin.LeaveAssignCreateInfo(Id);
                //data.NepaliMonth = _LeaveAddAdmin.LeaveAssignCreateInfo(Id).NepaliMonth;
                return View(Result);
            }
        }
        #endregion

    }
}