using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Utilities;
using BeeHrmInterface.GlobalSelectLists;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers
{
    public class AttendanceLogController : BaseController
    {
        private IAttendanceLogService _attendanceLogServices;
        private IEmployeeService _empService;
        private IDynamicSelectList _dynamicSelectList;
        public AttendanceLogController()
        {
            _attendanceLogServices = new AttendanceLogService();
            _empService = new EmployeeService();
            _dynamicSelectList = new DynamicSelectList();
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();
        }
        // GET: AttendanceLog
        [Route("Attendancelog")]
        public ActionResult Index()
        {
            try
            {
                int EmpCode = Convert.ToInt32(Session["Empcode"]);
                ViewBag.ddlEmployeeList = _dynamicSelectList.GetEmployeeByEmpCode(EmpCode).ToList();
                return View();
            }
            catch (Exception Ex)
            {
                ViewBag.Error = Ex.Message;
                return View();
            }

        }
        [Route("Attendancelog")]
        [HttpPost]
        public ActionResult Index(AttendanceLogViewModel attlog, int? logEmpCode)
        {
            attlog.logDate = Convert.ToDateTime(NepEngDate.NepToEng(attlog.logDateNP));
            try
            {
                int EmpCode = Convert.ToInt32(Session["Empcode"]);
                ViewBag.ddlEmployeeList = _dynamicSelectList.GetEmployeeByEmpCode(EmpCode).ToList();

                attlog.logEmpCode = Convert.ToInt32(logEmpCode);
                ViewBag.logEmpCode = attlog.logEmpCode;
                ViewBag.logDateNP = attlog.logDateNP;

                ViewBag.attendanceloglist = _attendanceLogServices.GetAttendanceLog(attlog.logEmpCode, attlog.logDate).ToList();
                return View();
            }
            catch (Exception Ex)
            {
                ViewBag.Error = Ex.Message;
                return View();
            }



        }
    }
}