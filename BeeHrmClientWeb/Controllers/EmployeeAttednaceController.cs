using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers
{
    public class EmployeeAttednaceController : BaseController
    {
        private IAttendanceDailyService _attendanceDailyServices;

        private IDepartmentService _departmentServices;

        private IDesignationService _designationServices;
        private IModulServices _moduleService;
        private IOfficeServices _officeServices;
        private INotification _notifications;
        private IEmployeeService _employeeServices;
        public EmployeeAttednaceController()
        {
            _attendanceDailyServices = new AttendanceDailyService();
            _departmentServices = new DepartmentService();
            _designationServices = new DesignationService();

            _officeServices = new OfficeServices();
            _moduleService = new ModuleService();
            _employeeServices = new EmployeeService();
            _notifications = new NotificationService();
            // ViewBag.TopMenuList = _moduleService.GetModuleParents(0);
            //ViewBag.SideBar = _moduleService.GetModuleParents(2);

        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();

        }
        // GET: EmployeeAttednace

        public ActionResult Index()
        {

            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to view or update attendance details";
                return PartialView("_partialviewNotFound");
            }

            int EmpCode = Convert.ToInt32(Session["EmpCode"]);
            ViewBag.emplist = _employeeServices.GetEmployeeList(EmpCode);
            return View();



        }



        [HttpPost]
        public ActionResult Index(DailyAttendanceFilterViewModel atd)
        {
            atd.startdate = !String.IsNullOrEmpty(atd.startdateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(atd.startdateNP)) : atd.startdate;
            atd.Enddate = !String.IsNullOrEmpty(atd.EnddateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(atd.EnddateNP)) : atd.Enddate;

            try
            {
                int EmpCode = Convert.ToInt32(Session["EmpCode"]);
                ViewBag.emplist = _employeeServices.GetEmployeeList(EmpCode);
                IEnumerable<DailyAttendanceFilterViewModel> item = _attendanceDailyServices.GetAttendanceByRangeAndID(atd.EmpSearchCode, atd.startdate, atd.Enddate);
                var list = _employeeServices.GetEmployeeDetails(atd.EmpSearchCode);
                ViewBag.Department = list.Department;
                ViewBag.code = atd.EmpSearchCode;
                ViewBag.sdate = atd.startdateNP;
                ViewBag.edate = atd.EnddateNP;
                ViewBag.Name = list.Name;
                ViewBag.office = list.OfficeName;
                ViewBag.Desg = list.Designation;
                ViewBag.EmployeeType = list.Group;
                ViewBag.Shift = list.Shift;
                ViewBag.level = list.Level;
                ViewBag.image = list.PhotoName;

                return View(item);
            }
            catch (Exception Ex)
            {
                ViewBag.Error = "No Data Found !!" + Ex.Message;
                return View();
            }
        }
    }
}