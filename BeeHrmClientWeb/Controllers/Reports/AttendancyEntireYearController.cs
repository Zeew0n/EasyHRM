using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using BeeHrmClientWeb.CodeBase;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Reports
{

    public class AttendancyEntireYearController : BaseController
    {
        private IAttendanceDailyService _attendanceDailyServices;

        private IDepartmentService _departmentServices;
        private IGroupService _groupServices;
        private IDesignationService _designationServices;
        private IOfficeServices _officeServices;
        private INotification _notifications;
        private IEmployeeService _employeeServices;
        private IReportsServices _reportServices;
        private IFiscalService _fiscalService;
        public AttendancyEntireYearController()
        {

            _attendanceDailyServices = new AttendanceDailyService();
            _departmentServices = new DepartmentService();
            _designationServices = new DesignationService();
            _groupServices = new GroupService();
            _officeServices = new OfficeServices();
            _employeeServices = new EmployeeService();
            _notifications = new NotificationService();
            _reportServices = new ReportsServices();
            _fiscalService = new FiscalService();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();

        }
        // GET: AttendancyEntireYear
        public ActionResult Index()
        {
            int EmpCode = Convert.ToInt32(Request["empCode"]);

            int FiscalYear = Convert.ToInt32(Request["fyId"]);

            ViewBag.FiscalsDropdown = _fiscalService.GetFiscalDropDown();

            EmployeeEditViewModel EmpDetails = _employeeServices.GetEmployeeByID(EmpCode);

            ViewBag.EmpDetails = EmpDetails;

            AttendanceEntireYearViewModel record = _reportServices.AttendanceEntireYearReport(EmpCode, FiscalYear, 4);


            ViewBag.FirstMonthSummary = _reportServices.AttendanceEntireYearReportSummary(EmpCode, FiscalYear, 4);

            ViewBag.SecondMonth = _reportServices.AttendanceEntireYearReport(EmpCode, FiscalYear, 5);
            ViewBag.SecondMonthSummary = _reportServices.AttendanceEntireYearReportSummary(EmpCode, FiscalYear, 5);

            ViewBag.ThirdMonth = _reportServices.AttendanceEntireYearReport(EmpCode, FiscalYear, 6);
            ViewBag.ThirdMonthSummary = _reportServices.AttendanceEntireYearReportSummary(EmpCode, FiscalYear, 6);

            ViewBag.FourthMonth = _reportServices.AttendanceEntireYearReport(EmpCode, FiscalYear, 7);
            ViewBag.FourthMonthSummary = _reportServices.AttendanceEntireYearReportSummary(EmpCode, FiscalYear, 7);

            ViewBag.FivethMonth = _reportServices.AttendanceEntireYearReport(EmpCode, FiscalYear, 8);
            ViewBag.FivethMonthSummary = _reportServices.AttendanceEntireYearReportSummary(EmpCode, FiscalYear, 8);

            ViewBag.SixthMonth = _reportServices.AttendanceEntireYearReport(EmpCode, FiscalYear, 9);
            ViewBag.SisthMonthSummary = _reportServices.AttendanceEntireYearReportSummary(EmpCode, FiscalYear, 9);

            ViewBag.SeventhMonth = _reportServices.AttendanceEntireYearReport(EmpCode, FiscalYear, 10);
            ViewBag.SeventhMonthSummary = _reportServices.AttendanceEntireYearReportSummary(EmpCode, FiscalYear, 10);

            ViewBag.EightthMonth = _reportServices.AttendanceEntireYearReport(EmpCode, FiscalYear, 11);
            ViewBag.EightthMonthSummary = _reportServices.AttendanceEntireYearReportSummary(EmpCode, FiscalYear, 11);

            ViewBag.NinethMonth = _reportServices.AttendanceEntireYearReport(EmpCode, FiscalYear, 12);
            ViewBag.NinethMonthSummary = _reportServices.AttendanceEntireYearReportSummary(EmpCode, FiscalYear, 12);

            ViewBag.TenthMonth = _reportServices.AttendanceEntireYearReport(EmpCode, FiscalYear, 1);
            ViewBag.TenthMonthSummary = _reportServices.AttendanceEntireYearReportSummary(EmpCode, FiscalYear, 1);

            ViewBag.EleventhMonth = _reportServices.AttendanceEntireYearReport(EmpCode, FiscalYear, 2);
            ViewBag.EleventMonthSummary = _reportServices.AttendanceEntireYearReportSummary(EmpCode, FiscalYear, 2);

            ViewBag.TwelevethMonth = _reportServices.AttendanceEntireYearReport(EmpCode, FiscalYear, 3);
            ViewBag.TwelevethMonthSummary = _reportServices.AttendanceEntireYearReportSummary(EmpCode, FiscalYear, 3);

            return View("../AttendanceReport/AttendancyEntireYear", record);
        }
    }
}