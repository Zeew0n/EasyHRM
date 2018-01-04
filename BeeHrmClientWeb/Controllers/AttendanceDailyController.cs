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
    public class AttendanceDailyController : BaseController
    {
        private IAttendanceDailyService _attendanceDailyServices;

        private IDepartmentService _departmentServices;
        private IDesignationService _designationServices;
        private IModulServices _moduleService;
        private IOfficeServices _officeServices;
        private INotification _notifications;
        private IEmployeeService _employeeServices;

        public AttendanceDailyController()
        {
            _attendanceDailyServices = new AttendanceDailyService();
            _departmentServices = new DepartmentService();
            _designationServices = new DesignationService();

            _officeServices = new OfficeServices();
            _moduleService = new ModuleService();
            _employeeServices = new EmployeeService();
            _notifications = new NotificationService();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();

        }


        // GET: AttendanceByDate



        [HttpGet]
        public ActionResult Index()
        {


            ViewBag.ddlDesignationList = _designationServices.GetDesignationList();
            ViewBag.ddlOfficeTypeList = _officeServices.GetOfficeData();
            DateTime date1 = DateTime.Now;


            int AdminEmpCode = ViewBag.EmpCode;

            IEnumerable<DailyAttendanceFilterViewModel> list = _attendanceDailyServices.GetAttendanceDailyStatus(AdminEmpCode, date1, null, null, null);
            return View(list);

        }


        [HttpPost]
        public ActionResult Index(DailyAttendanceFilterViewModel att)
        {
            att.searchdate = Convert.ToDateTime(NepEngDate.NepToEng(att.searchdateNP));
            
            try
            {
                ViewBag.date = att.searchdateNP;
                ViewBag.empdoce = att.EmpCode;
                ViewBag.Desgid = att.DsgId;
                ViewBag.officeid = att.officeid;
                ViewBag.ddlDesignationList = _designationServices.GetDesignationList();
                int empcode = Convert.ToInt32(Session["EmpCode"]);
                //IEnumerable<OfficeDTOs> officelist = _officeServices.GetClildOfficeListByEmpCode(empcode);

                int AdminEmpCode = ViewBag.EmpCode;

                ViewBag.ddlOfficeTypeList = _officeServices.GetClildOfficeListByEmpCode(empcode);
                IEnumerable<DailyAttendanceFilterViewModel> list = _attendanceDailyServices.GetAttendanceDailyStatus(AdminEmpCode, att.searchdate, att.EmpCode, att.DsgId, att.officeid);
                return View(list);
            }
            catch (Exception Ex)
            {
                ViewBag.Error = Ex.Message;
                return View();
            }

        }
    }
}