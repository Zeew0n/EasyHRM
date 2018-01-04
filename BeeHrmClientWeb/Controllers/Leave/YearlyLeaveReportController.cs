using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.Leave_Module.Helper;
using BeeHRM.ApplicationService.Leave_Module.Serivices.Implementation;
using BeeHRM.ApplicationService.Leave_Module.Serivices.Interface;
using BeeHRM.ApplicationService.ViewModel;
using BeeHrmClientWeb.CodeBase;
using BeeHrmInterface.GlobalSelectLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Leave
{
    public class YearlyLeaveReportController : BaseController
    {
        private ILeaveApplicationServices _LeaveAddAdmin;
        private ILeaveApplicationService _LeaveApplicationService;
        private ILeaveValidateHelper _ValidateLeave;
        private IDynamicSelectList _DynamicSelectList;
        private ILeaveEarnedService _LeaveEarnedService;
        private IOfficeServices _officeService;
        private IDesignationService _designationService;
        private IEmployeeService _employeeService;

        public YearlyLeaveReportController()
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
        #region leave Yearly Report
        public ActionResult Index()
        {
            int EmpCode = Convert.ToInt32(Session["Empcode"]);
            ViewBag.dllOfficeTypeList = _officeService.GetClildOfficeListByEmpCode(Convert.ToInt32(ViewBag.EmpCode));

            ViewBag.dllDesginationList = _designationService.GetDesignationList().ToList();
            ViewBag.dllLeaveYearList = _LeaveApplicationService.GetLeaveYearSelectList();

            return View();
        }

        [HttpPost]
        public ActionResult Index(int OfficeId, int? DesgId, int LeaveYearId)
        {
            int EmpCode = Convert.ToInt32(Session["Empcode"]);
            ViewBag.dllOfficeTypeList = _officeService.GetClildOfficeListByEmpCode(Convert.ToInt32(ViewBag.EmpCode));

            ViewBag.dllLeaveYearList = _LeaveApplicationService.GetLeaveYearSelectList();
            ViewBag.dllDesginationList = _designationService.GetDesignationList().ToList();
            ViewBag.OfficeId = OfficeId;
            ViewBag.DesgId = DesgId;
            ViewBag.LeaveYearId = LeaveYearId;
            var emplist = _employeeService.EmployeeSearch(OfficeId, DesgId);
            List<MultipleLeaveYearlyReportViewModel> masterRecord = new List<MultipleLeaveYearlyReportViewModel>();
            foreach (var empdata in emplist)
            {

                var desgndata = _designationService.GetDesignationById(empdata.EmpDesgId);
                var empdbdata = _employeeService.GetEmployeeByID(empdata.EmpCode);
                LeaveYearlyReportViewModel homeleave = new LeaveYearlyReportViewModel();
                homeleave = _LeaveAddAdmin.YearlyOfficewiseLeaveReport(empdata.EmpCode, 1, LeaveYearId);

                LeaveYearlyReportViewModel sickleave = new LeaveYearlyReportViewModel();
                sickleave = _LeaveAddAdmin.YearlyOfficewiseLeaveReport(empdata.EmpCode, 2, LeaveYearId);

                LeaveYearlyReportViewModel casualleave = new LeaveYearlyReportViewModel();
                casualleave = _LeaveAddAdmin.YearlyOfficewiseLeaveReport(empdata.EmpCode, 3, LeaveYearId);

                LeaveYearlyReportViewModel compensationLeave = new LeaveYearlyReportViewModel();
                compensationLeave = _LeaveAddAdmin.YearlyOfficewiseLeaveReport(empdata.EmpCode, 9, LeaveYearId);

                MultipleLeaveYearlyReportViewModel single = new MultipleLeaveYearlyReportViewModel()
                {
                    EmpName = empdbdata.Name,
                    DesgName = desgndata.DsgName,
                    OfficeName = _officeService.GetOfficeName(OfficeId),
                    EmpCode = empdata.EmpCode,
                    H_BalanceDays = homeleave.BalanceDays,
                    H_TotalLeaveDays = homeleave.TotalLeaveDays,
                    H_TotalTaken = homeleave.TotalTaken,
                    H_PrevYearLeaveDays = homeleave.PrevYearBalance,
                    H_ThisYearLeaveDays = homeleave.ThisYearEarned,

                    S_BalanceDays = sickleave.BalanceDays,
                    S_TotalLeaveDays = sickleave.TotalLeaveDays,
                    S_TotalTaken = sickleave.TotalTaken,

                    Ca_BalanceDays = casualleave.BalanceDays,
                    Ca_TotalLeaveDays = casualleave.TotalLeaveDays,
                    Ca_TotalTaken = casualleave.TotalTaken,

                    Co_BalanceDays = compensationLeave.BalanceDays,
                    Co_TotalLeaveDays = compensationLeave.TotalLeaveDays,
                    Co_TotalTaken = compensationLeave.TotalTaken,

                };
                masterRecord.Add(single);

            }



            return View(masterRecord);
        }
        #endregion
    }
}