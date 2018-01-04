using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHrmClientWeb.CodeBase;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;


namespace BeeHrmClientWeb.Controllers.Leave
{
    public class LeaveYearlyReportController : BaseController
    {
        private ILeaveApplicationService _leaveServices;
        private IModulServices _moduleService;
        private IOfficeServices _officeServices;
        private IEmployeeService _employeeServices;
        private INotification _notifications;
        private ILeaveRuleService _leaveRuleService;
        private ILeaveRuleDetailService _leaveRuleDetailService;
        private ILeaveTypeService _leaveTypeService;
        private ILeaveAssigned _leaveAssigned;
        private ILeaveEarnedService _leaveEarnedService;
        public LeaveYearlyReportController()
        {
            _leaveServices = new LeaveApplicationService();
            _moduleService = new ModuleService();
            _employeeServices = new EmployeeService();
            _notifications = new NotificationService();
            _officeServices = new OfficeServices();

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



        // GET: LeaveYearlyReport
        public ActionResult Index()
        {
            int curentemp = Convert.ToInt32(Session["Empcode"]);

            LeaveYearlyReportWithFilter Record = new LeaveYearlyReportWithFilter();

            Record.EmployeeSelectList = _leaveEarnedService.GetBrancheEmployeeSelectList(curentemp);
            Record.LeaveYears = _leaveServices.GetLeaveYearSelectList();
            Record.LeaveYearlyReport = new List<LeaveYearlyReportDTO>();
            Record.PayrollLeaveDeduction = new List<PayrollLeaveDeductionDTO>();
            return View("../Leave/LeaveYearlyReport/LeaveYearlyReport", Record);
        }
        [HttpPost]
        public ActionResult Index(LeaveYearlyReportWithFilter Record)
        {
            Record.EmployeeSelectList = _employeeServices.GetEmployeeSelectList();
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
                return View("../Leave/LeaveYearlyReport/LeaveYearlyReport", Record);
            }
            return View("../Leave/LeaveYearlyReport/LeaveYearlyReport", Record);
        }

    }
}