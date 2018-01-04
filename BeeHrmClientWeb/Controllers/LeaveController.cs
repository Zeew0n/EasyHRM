using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers
{
    public class LeaveController : BaseController
    {
        private ILeaveApplicationService _leaveServices;
        private IModulServices _moduleService;
        private IOfficeServices _officeServices;
        private IEmployeeService _employeeServices;
        private INotification _notifications;
        private IReportsServices _reportServices;
        private ILeaveRuleService _leaveRuleService;
        private ILeaveRuleDetailService _leaveRuleDetailService;
        private ILeaveTypeService _leaveTypeService;
        private ILeaveAssigned _leaveAssigned;
        private ILeaveEarnedService _leaveEarnedService;
        public LeaveController()
        {
            _leaveServices = new LeaveApplicationService();
            _moduleService = new ModuleService();
            _employeeServices = new EmployeeService();
            _notifications = new NotificationService();
            _officeServices = new OfficeServices();
            _reportServices = new ReportsServices();
            _leaveRuleService = new LeaveRuleService();
            _leaveRuleDetailService = new LeaveRuleDetailService();
            _leaveTypeService = new LeaveTypeService();
            _leaveAssigned = new LeaveAssignServices();
            _leaveEarnedService = new LeaveEarnedService();





        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();
        }
        // GET: Leave
        [Route("admin/leaveapplication/list")]
        public ActionResult Index()
        {
            int? monthId = DateTime.Now.Month;
            int? year = DateTime.Now.Year;
            int roleid = Convert.ToInt32(Session["RoleId"]);

            IEnumerable<LeaveApplicationViewModel> leaveList = _leaveServices.GetLeaveApplicationsListAdmin(Convert.ToInt32(ViewBag.Empcode), monthId, year);
            ViewBag.Years = _leaveServices.GetLeaveApplicationsYear();
            return View(leaveList);
        }

        [Route("admin/leaveapplication/list")]
        [HttpPost]
        public ActionResult Index(YearViewModel lam)
        {
            int? monthId = lam.MonthId;
            int? year = lam.YearName;
            IEnumerable<LeaveApplicationViewModel> leaveList = _leaveServices
                .GetLeaveApplicationsListAdmin(Convert.ToInt32(ViewBag.Empcode), monthId, year);
            ViewBag.Years = _leaveServices.GetLeaveApplicationsYear();
            ViewBag.YVM = lam;
            return View(leaveList);
        }

        [Route("admin/Leave/Details/{id}")]
        public ActionResult LeaveDetailAdmin(int id)
        {
            LeaveApplicationViewModel lavm = _leaveServices.LeaveDetailAdmin(id);
            LeaveApplicationRecommendDetailViewModel lardVM = new LeaveApplicationRecommendDetailViewModel();
            lardVM.LeaveDetail = lavm;
            lardVM.LeaveApplier = _employeeServices.GetEmployeeDetails(lavm.LeaveEmpCode);
            return View(lardVM);
        }


        [Route("admin/leaveapplication/create")]
        public ActionResult ApplyLeave()
        {
            int roleid = Convert.ToInt32(Session["RoleId"]);
            int empCode = Convert.ToInt32(Session["EmpCode"]);
            LeaveApplicationAddViewModel lavm = new LeaveApplicationAddViewModel();
            lavm.Employees = _employeeServices.GetEmployeeList(roleid).Where(x => x.EmpCode != empCode);
            lavm.SerializedActiveYearData = JsonConvert.SerializeObject(_leaveServices.GetActiveYear());
            return View(lavm);
        }

        [Route("admin/leaveapplication/create")]
        [HttpPost]
        public ActionResult ApplyLeave(LeaveApplicationDTO leave)
        {

            int empCode = leave.LeaveEmpCode;
            int curentemp = Convert.ToInt32(Session["Empcode"]);
            int roleid = Convert.ToInt32(Session["RoleId"]);
            leave.LeaveDaysType = "F";
            leave.RecommededEmpCode = Convert.ToInt32(Session["Empcode"]);
            leave.RecommendStatus = 2;
            leave.RecommenderMessage = leave.LeaveDetails;
            leave.RecommendStatusDate = DateTime.Now;
            double leavedays = _leaveServices.LeaveDayCalculations(leave.LeaveTypeId, leave.LeaveStartDate, leave.LeaveEndDate);



            LeaveApplicationAddViewModel lavm = new LeaveApplicationAddViewModel();
            LeaveYearDTO year = _leaveServices.GetActiveYear();
            lavm.Employees = _employeeServices.GetEmployeeList(roleid).Where(x => x.EmpCode != curentemp);

            lavm.SerializedActiveYearData = JsonConvert.SerializeObject(_leaveServices.GetActiveYear());

            if (!ModelState.IsValid)
            {
                return View(lavm);
            }
            List<LeaveStatViewModel> StatList = _leaveServices.GetLeaveStatus(empCode, year.YearId).ToList();
            LeaveStatViewModel targetData = StatList.Find(x => x.LeaveTypeId == leave.LeaveTypeId);
            LeaveYearDTO currentYear = _leaveServices.GetActiveYear();
            if (leave.IsHalfDayLeave == true)
            {
                leave.LeaveEndDate = leave.LeaveStartDate;
            }

            DateTime startDate = Convert.ToDateTime(leave.LeaveStartDate);
            DateTime endDate = Convert.ToDateTime(leave.LeaveEndDate);
            if (startDate > currentYear.YearStartDate
                && startDate < currentYear.YearEndDate && endDate > currentYear.YearStartDate
                && endDate < currentYear.YearEndDate)
            {
                #region ApplyBeforeZeroDays
                if (targetData.LeaveApplyBeforeDays == 0)
                {
                    if (startDate > endDate)
                    {
                        ModelState.AddModelError("StartDateGreaterError", "The start Date is greater than the End Date.");
                        return View(lavm);
                    }
                    leave.LeaveEmpCode = empCode;
                    leave.LeaveYearId = currentYear.YearId;
                    leave.LeaveStatus = 1;
                    leave.LeaveStatusDate = DateTime.Now;
                    if (leave.IsHalfDayLeave == true)
                    {
                        leave.LeaveDays = 0.5M;
                    }
                    else
                    {
                        leave.LeaveDays = leave.LeaveEndDate.Date.Subtract(leave.LeaveStartDate.Date).Duration().Days + 1;
                    }

                    leave.LeaveAppliedDate = DateTime.Now;
                    leave.LeaveGUICode = Guid.NewGuid().ToString();
                    leave.PaidLeave = targetData.IsPayable;
                    if (leave.IsHalfDayLeave)
                    {
                        leave.LeaveDaysType = "H";
                        if (leave.LeaveDaysPart == null)
                        {
                            ModelState.AddModelError("LeaveDaysPartError", "Leave Day Part is not selected.");
                            return View(lavm);
                        }
                        if ((leave.LeaveEndDate.Date.Subtract(leave.LeaveStartDate.Date).Duration().Days + 1) / 2 > targetData.TotalRemainingDays)
                        {
                            ModelState.AddModelError("LeaveBalanceError", "You do not have enough leave balance for this leave type.");
                            return View(lavm);
                        }
                    }
                    else
                    {
                        if ((leave.LeaveEndDate.Date.Subtract(leave.LeaveStartDate.Date).Duration().Days + 1) > targetData.TotalRemainingDays)
                        {
                            ModelState.AddModelError("LeaveBalanceError", "You do not have enough leave balance for this leave type.");
                            return View(lavm);
                        }
                    }

                    LeaveApplicationDTO reLeaveApplication1 = _leaveServices.InsertLeaveApplication(leave);
                    string leaveName1 = targetData.LeaveTypeName;
                    Session["Success"] = "Your leave application of leave type \"" + leaveName1 + "\" has been inserted.";
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
                #endregion

                #region HAlfDayLeaveCondition
                else if (leave.IsHalfDayLeave == true)
                {
                    leave.LeaveEmpCode = empCode;
                    leave.LeaveYearId = currentYear.YearId;
                    leave.RecommendStatus = 1;
                    leave.RecommendStatusDate = DateTime.Now;
                    leave.LeaveStatus = 1;
                    leave.LeaveStatusDate = DateTime.Now;
                    leave.LeaveAppliedDate = DateTime.Now;
                    leave.LeaveGUICode = Guid.NewGuid().ToString();
                    leave.PaidLeave = targetData.IsPayable;
                    leave.LeaveDays = 0.5M;
                    leave.LeaveDaysType = "H";
                    if (targetData.TotalRemainingDays == 0)
                    {
                        ModelState.AddModelError("LeaveBalanceError", "You do not have enough leave balance for this leave type.");
                        return View(lavm);
                    }
                    if (leave.LeaveDaysPart == null)
                    {
                        ModelState.AddModelError("LeaveDaysPartError", "Leave Day Part is not selected.");
                        return View(lavm);
                    }
                    LeaveApplicationDTO reLeaveApplication1 = _leaveServices.InsertLeaveApplication(leave);
                    string leaveName1 = targetData.LeaveTypeName;
                    Session["Success"] = "Your leave application of leave type \"" + leaveName1 + "\" has been inserted.";
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
                #endregion


                else if (startDate > endDate)
                {
                    ModelState.AddModelError("StartDateGreaterError", "The start Date is greater than the End Date.");
                    return View(lavm);
                }
                else
                {
                    #region NormalLeaveCondition
                    leave.LeaveEmpCode = empCode;
                    leave.LeaveYearId = currentYear.YearId;
                    leave.RecommendStatus = 2;
                    leave.RecommendStatusDate = DateTime.Now;
                    leave.LeaveStatus = 1;
                    leave.LeaveStatusDate = DateTime.Now;
                    leave.LeaveDays = leave.LeaveEndDate.Date.Subtract(leave.LeaveStartDate.Date).Duration().Days + 1;
                    leave.LeaveAppliedDate = DateTime.Now;
                    leave.LeaveGUICode = Guid.NewGuid().ToString();
                    leave.PaidLeave = targetData.IsPayable;
                    if ((leave.LeaveEndDate.Date.Subtract(leave.LeaveStartDate.Date).Duration().Days + 1) > targetData.TotalRemainingDays)
                    {
                        ModelState.AddModelError("LeaveBalanceError", "You do not have enough leave balance for this leave type.");
                        return View(lavm);
                    }
                    LeaveApplicationDTO reLeaveApplication = _leaveServices.InsertLeaveApplication(leave);
                    string leaveName = targetData.LeaveTypeName;
                    Session["Success"] = "Your leave application of leave type \"" + leaveName + "\" has been inserted.";
                    ModelState.Clear();
                    return RedirectToAction("Index");
                    #endregion
                }
            }
            else
            {
                ModelState.AddModelError("DateRangeError", "The Leave start date or end date you stated does not lie in the current year.");
                return View(lavm);
            }
        }
        [Route("admin/leave/leeavebalance")]
        public JsonResult LeaveBalance(int id, int? year)
        {

            if (year == null)
            {
                LeaveYearDTO yearid = _leaveServices.GetActiveYear();
                IEnumerable<LeaveStatViewModel> lst = _leaveServices.GetLeaveStatus(id, yearid.YearId);
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int yearid = _leaveServices.GetLeaveYearId(year);
                IEnumerable<LeaveStatViewModel> lst = _leaveServices.GetLeaveStatus(id, yearid);
                return Json(lst, JsonRequestBehavior.AllowGet);
            }





        }

        [Route("admin/leave/recommender/{id}")]
        public JsonResult RecommenderList(int id)
        {

            IEnumerable<EmployeeDTO> lst = _leaveServices.GetRecommenderList(id);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [Route("admin/leave/approver/{id}")]
        public JsonResult Approverlist(int id)
        {

            IEnumerable<EmployeeDTO> lst = _leaveServices.GetApproverList(id);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }


        [Route("admin/leave/balance")]
        public ActionResult LeaveBalanceAdmin()
        {

            LeaveBalanceViewModel lbvm = new LeaveBalanceViewModel();
            int roleid = Convert.ToInt32(Session["RoleId"]);
            lbvm.EmpList = _employeeServices.GetEmployeeList(roleid);
            List<int> neapaliyear = _reportServices.GetYearList();
            List<SelectListItem> year = new List<SelectListItem>();

            foreach (int str in neapaliyear)
            {
                year.Add(new SelectListItem
                {
                    Text = str.ToString(),
                    Value = str.ToString()
                });
            }
            lbvm.YearList = year;
            return View(lbvm);
        }

        [Route("employee/leave/balance/{Empcode}")]
        public ActionResult IndividualLeaveBalance(int Empcode)
        {
            LeaveBalanceIndividual lbvm = new LeaveBalanceIndividual();
            lbvm.EmpDetail = _employeeServices.GetEmployeeDetails(Empcode);
            List<int> neapaliyear = _reportServices.GetYearList();
            List<SelectListItem> year = new List<SelectListItem>();
            LeaveYearDTO active_year = _leaveServices.GetActiveYear();
            foreach (int str in neapaliyear)
            {
                year.Add(new SelectListItem
                {
                    Text = str.ToString(),
                    Value = str.ToString()
                });
            }
            lbvm.Currentyear = active_year.YearId;
            lbvm.YearList = year;
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(Empcode);
            ViewBag.EmployeeDetail = lbvm.EmpDetail;
            return View(lbvm);
        }

        [Route("leave/assign/{empcode}")]
        public ActionResult LeaveAssignIndividual(int empcode)
        {

            LeaveYearDTO year = _leaveServices.GetActiveYear();
            EmployeeDetailsViewModel empdetail = _employeeServices.GetEmployeeDetails(empcode);
            ViewBag.EmployeeDetail = empdetail;

            int leaveAssign = _leaveServices.UpdateLeaveAssign(empcode, year.YearId);
            if (leaveAssign == 1)
            {

                int id = empdetail.LeaveRuleId;

                LeaveRuleAddViewModel lrVM = new LeaveRuleAddViewModel();
                lrVM.Empdetail = empdetail;
                lrVM.LeaveAssign = _leaveRuleService.LeaveAssignDetail(empcode).ToList();

                return View(lrVM);
            }
            else
            {
                return RedirectToAction("EmployeeLeaveAssignDetails", empcode);
            }


        }

        [HttpPost]
        [Route("leave/assign/{empcode}")]
        public ActionResult LeaveAssignIndividual(int empcode, LeaveRuleAddViewModel leave)
        {



            LeaveYearDTO year = _leaveServices.GetActiveYear();
            EmployeeDetailsViewModel empdetail = _employeeServices.GetEmployeeDetails(empcode);
            ViewBag.EmployeeDetail = empdetail;
            int leaveAssign = _leaveServices.UpdateLeaveAssign(empcode, year.YearId);
            foreach (var row in leave.LeaveAssign)
            {

                int update = _leaveAssigned.UpdateLeaveAssignedDaysInital(row.AssignedId, row.TotalAssignDay);
                if (update == 1)
                {
                    Session["Sucess"] = "Leave updated sucessfully!!";
                }
                else
                {
                    Session["Error"] = "Oops Seems you can not update leave at this time!!";
                }
            }

            return RedirectToAction("EmployeeLeaveAssignDetails", empcode);

        }



        [Route("admin/leaveassign/detail/{empcode}")]
        public ActionResult EmployeeLeaveAssignDetails(int empcode)
        {

            LeaveYearDTO yearid = _leaveServices.GetActiveYear();
            IEnumerable<LeaveStatViewModel> lst = _leaveServices.GetLeaveStatus(empcode, yearid.YearId);
            LeaveBalanceIndividual lbvm = new LeaveBalanceIndividual();
            lbvm.LeaveRuleList = _leaveRuleService.GetLeaveRulesList();
            lbvm.EmpDetail = _employeeServices.GetEmployeeDetails(empcode);
            LeaveRuleDTO name = _leaveRuleService.GetLeaveRuleById(lbvm.EmpDetail.LeaveRuleId);
            lbvm.LeaveuleName = name.LeaveRuleName;

            var leaveRuleDetailsList = _leaveRuleDetailService.GetLeaveRuleDetails(lbvm.EmpDetail.LeaveRuleId);
            ViewBag.EmployeeDetail = lbvm.EmpDetail;
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(empcode);
            lbvm.UnassignLeave = _leaveServices.UnassignedLeave(empcode, lbvm.EmpDetail.LeaveRuleId, yearid.YearId);
            lbvm.LeaveDetails = lst;

            return View(lbvm);
        }


        [Route("admin/UpdateLeaveAssign/detail/{Empcode}")]
        public ActionResult UpdateLeaveAssignDetails(LeaveRuleAddViewModel Ldvm, int Empcode)
        {
            List<LeaveAssignDetailViewModel> Data = Ldvm.UpdateResult;
            int count = Data.Count;
            foreach (var item in Ldvm.UpdateResult)
            {

                LeaveAssignedDTO lvt = new LeaveAssignedDTO();
                lvt.AssignedDays = item.AddDays;
                lvt.AssignLeaveTypeId = item.LeaveTypeId;
                lvt.AssignEmpCode = Empcode;
                lvt.LeaveTakenDays = item.AddDays;
                lvt.AssignedLeaveYearId = item.LeaveYearId;

                if (item.IsAlreadyAssigned == true && item.IsEnable == false)
                {
                    int isExist = _leaveAssigned.CheckLeaveExistence(Empcode, item.LeaveTypeId, item.LeaveYearId);
                    if (isExist == 1)
                    {
                        int isDeleted = _leaveAssigned.DeleteLeaveAssignRules(item.AssignedId);
                    }
                    else
                    {
                        Session["Error"] = "This Leave is Alredy taken by Employee.so You Can not delete this Leave";
                        return RedirectToAction("EmployeeLeaveAssignDetails", Empcode);
                    }
                }
                else

                if (item.IsAlreadyAssigned == true && item.AddDays != 0 && item.IsEnable == true)
                {
                    int result = _leaveAssigned.UpdateLeaveAssigned(lvt);
                }
                else
                     if (item.IsEnable == true && item.IsAlreadyAssigned == false)
                {

                    lvt.LeaveTakenDays = 0;
                    int result = _leaveAssigned.InsertLeaveAssigned(lvt);
                }
            }

            return RedirectToAction("EmployeeLeaveAssignDetails", Empcode);
        }

        [Route("admin/updat/employeeleaverules/")]
        public JsonResult UpdateEmployeeLeaveRules(int EmpCode, int LeaveId)
        {

            try
            {
                _employeeServices.UpdateEmployeeLeaveRuleId(EmpCode, LeaveId);
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {

                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("employee/leeavebalance")]
        public JsonResult IndividualLeaveBalance(int id, int yearid)
        {
            IEnumerable<LeaveStatViewModel> lst = _leaveServices.GetLeaveStatus(id, yearid).Where(x => x.TotalRemainingDays > 0);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }


        //public ActionResult LeaveYearlyReport()
        //{
        //    int curentemp = Convert.ToInt32(Session["Empcode"]);

        //    LeaveYearlyReportWithFilter Record = new LeaveYearlyReportWithFilter();

        //    Record.EmployeeSelectList = _leaveEarnedService.GetBrancheEmployeeSelectList(curentemp);
        //    Record.LeaveYears = _leaveServices.GetLeaveYearSelectList();
        //    Record.LeaveYearlyReport = new List<LeaveYearlyReportDTO>();
        //    Record.PayrollLeaveDeduction = new List<PayrollLeaveDeductionDTO>();
        //    return View(Record);
        //}
        //[HttpPost]
        //public ActionResult LeaveYearlyReport(LeaveYearlyReportWithFilter Record)
        //{
        //    Record.EmployeeSelectList = _employeeServices.GetEmployeeSelectList();
        //    Record.LeaveYears = _leaveServices.GetLeaveYearSelectList();
        //    Record.LeaveYearlyReport = new List<LeaveYearlyReportDTO>();
        //    Record.PayrollLeaveDeduction = new List<PayrollLeaveDeductionDTO>();
        //    if (ModelState.IsValid)
        //    {
        //        Record.LeaveYearlyReport = _leaveServices.GetLeaveYearlyReport(Record);
        //        Record.PayrollLeaveDeduction = _leaveServices.GetPayrollLeaveDeduction(Record);
        //    }
        //    else
        //    {
        //        TempData["Danger"] = "Please fill in the required field";
        //        return View(Record);
        //    }
        //    return View(Record);
        //}











    }
}


