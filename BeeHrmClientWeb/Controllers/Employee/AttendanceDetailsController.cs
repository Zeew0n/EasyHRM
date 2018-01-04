using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Employee
{
    public class AttendanceDetailsController : BaseController
    {
        private IAttendanceDailyService _attendanceDailyServices;

        private IDepartmentService _departmentServices;

        private IDesignationService _designationServices;
        private IModulServices _moduleService;
        private IOfficeServices _officeServices;
        private INotification _notifications;
        private IEmployeeService _employeeServices;
        public AttendanceDetailsController()
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
        // GET: AttendaceDetails


        public ActionResult Index(int id)
        {

            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to view or update attendance details";
                return PartialView("_partialviewNotFound");
            }

            try
            {
                ViewBag.usercode = id;
                var item = _attendanceDailyServices.GetAttendanceByRangeAndID(id, null, null);
                var list = _employeeServices.GetEmployeeDetails(id);
                ViewBag.Department = list.Department;
                ViewBag.Name = list.Name;
                ViewBag.office = list.OfficeName;
                ViewBag.Desg = list.Designation;
                ViewBag.EmployeeType = list.Group;
                ViewBag.Shift = list.Shift;
                ViewBag.level = list.Level;
                ViewBag.image = list.PhotoName;
                ViewBag.Empcode = list.Code;
                ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(ViewBag.Empcode);

                return View("../Employee/AttendanceDetails/Index", item);

            }
            catch (Exception Ex)
            {
                ViewBag.Error = "No Data found or No employee found with this employeeCode";

                return View("../Employee/AttendanceDetails/Index");
            }

        }


        [HttpPost]
        public ActionResult Index(DailyAttendanceFilterViewModel atd, int id)
        {
            atd.startdate = Convert.ToDateTime(NepEngDate.NepToEng(atd.startdateNP));
            atd.Enddate = Convert.ToDateTime(NepEngDate.NepToEng(atd.EnddateNP));
            ViewBag.sdate = atd.startdateNP;
            ViewBag.edate = atd.EnddateNP;
            try
            {
                IEnumerable<DailyAttendanceFilterViewModel> item = _attendanceDailyServices.GetAttendanceByRangeAndID(id, atd.startdate, atd.Enddate);
                var list = _employeeServices.GetEmployeeDetails(id);
                ViewBag.Department = list.Department;
                ViewBag.Name = list.Name;
                ViewBag.office = list.OfficeName;
                ViewBag.Desg = list.Designation;
                ViewBag.EmployeeType = list.Group;
                ViewBag.Shift = list.Shift;
                ViewBag.level = list.Level;
                ViewBag.image = list.PhotoName;
                ViewBag.Empcode = list.Code;
                ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(ViewBag.Empcode);
                return View("../Employee/AttendanceDetails/Index", item);
            }
            catch (Exception Ex)
            {
                ViewBag.Error = "No Data found or No employee found with this employeeCode";
                return View("../Employee/AttendanceDetails/Index");
            }

        }

    }
}