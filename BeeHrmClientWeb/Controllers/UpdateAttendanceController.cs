using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Models;
using BeeHrmClientWeb.Utilities;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers
{
    public class UpdateAttendanceController : BaseController
    {

        private IAttendanceDailyService _attendanceDailyServices;

        private IDepartmentService _departmentServices;
        private IDesignationService _designationServices;
        private IOfficeServices _officeServices;
        private INotification _notifications;
        private IEmployeeService _employeeServices;
        private IModulServices _moduleService;
        public UpdateAttendanceController()
        {
            _attendanceDailyServices = new AttendanceDailyService();
            _departmentServices = new DepartmentService();
            _designationServices = new DesignationService();
            _officeServices = new OfficeServices();
            _employeeServices = new EmployeeService();
            _notifications = new NotificationService();
            _moduleService = new ModuleService();

        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();

        }
        // GET: UpdateAttendance
        [HttpGet]
        public ActionResult Index(int id, string Sdate, string Enddate)
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to view or update attendance details";
                return PartialView("_partialviewNotFound");
            }

            try
            {
                if (String.IsNullOrEmpty(Sdate) && String.IsNullOrEmpty(Enddate))
                {
                    var list = _employeeServices.GetEmployeeDetails(id);
                    UpdateAttendanceSearchModel uds = new UpdateAttendanceSearchModel();
                    uds.AttendanceList = _attendanceDailyServices.GetAttendanceByRangeAndID(id, null, null);
                    uds.Name = list.Name;
                    uds.Empcode = list.Code;
                    uds.Branch = list.OfficeName;
                    uds.Department = list.Department;
                    uds.Designation = list.Designation;
                    uds.Shift = list.Shift;
                    uds.Group = list.Group;
                    uds.Level = list.Level;
                    ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
                    return View(uds);
                }
                else
                {
                    var list = _employeeServices.GetEmployeeDetails(id);
                    UpdateAttendanceSearchModel uds = new UpdateAttendanceSearchModel();
                    DateTime Stsrtdate, Edate;
                    Stsrtdate = Convert.ToDateTime(Sdate);
                    Edate = Convert.ToDateTime(Enddate);
                    uds.AttendanceList = _attendanceDailyServices.GetAttendanceByRangeAndID(id, Stsrtdate, Edate);
                    uds.Name = list.Name;
                    uds.SearchStartdate = Sdate;
                    uds.SeacrchEnddate = Enddate;
                    uds.Empcode = list.Code;
                    uds.Branch = list.OfficeName;
                    uds.Department = list.Department;
                    uds.Designation = list.Designation;
                    uds.Shift = list.Shift;
                    uds.Group = list.Group;
                    uds.Level = list.Level;
                    ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
                    return View(uds);
                }
            }
            catch (Exception Ex)
            {
                ViewBag.Error = "No Data found or No employee found with this employeeCode";

                return View();
            }



        }
        [HttpPost]

        public ActionResult Index(UpdateAttendanceSearchModel adt, int id)
        {
            adt.SearchStartdate = NepEngDate.NepToEng(adt.SearchStartdateNP);
            adt.SeacrchEnddate = NepEngDate.NepToEng(adt.SeacrchEnddateNP);
            ViewBag.sdate = adt.SearchStartdateNP;
            ViewBag.edate = adt.SeacrchEnddateNP;

            try
            {
                var list = _employeeServices.GetEmployeeDetails(id);
                ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
                UpdateAttendanceSearchModel uds = new UpdateAttendanceSearchModel();
                DateTime sadte, enddate;
                sadte = Convert.ToDateTime(adt.SearchStartdate);
                enddate = Convert.ToDateTime(adt.SeacrchEnddate);
                uds.AttendanceList = _attendanceDailyServices.GetAttendanceByRangeAndID(id, sadte, enddate);
                uds.Name = list.Name;
                uds.Empcode = list.Code;
                uds.Branch = list.OfficeName;
                uds.Department = list.Department;
                uds.Designation = list.Designation;
                uds.Shift = list.Shift;
                uds.Group = list.Group;
                uds.Level = list.Level;
                uds.SearchStartdate = adt.SearchStartdate;
                uds.SeacrchEnddate = adt.SeacrchEnddate;
                return View(uds);
            }
            catch (Exception Ex)
            {
                return View(Ex.Message);
            }


        }
        [HttpPost]
        public ActionResult UpdateAttendancedata(AttendanceDailyViewModel atd)
        {
            try
            {
                if (atd.IsAbsent == 1)
                {
                    atd.AttCheckIn = null;
                    atd.AttCheckOut = null;
                    _attendanceDailyServices.UpdateIndividualAttendanceDaily(atd);
                }

                else
                {

                    if (atd.AttCheckIn != null && atd.AttCheckOut != null)
                    {
                        DateTime checkin = Convert.ToDateTime(atd.AttDate);
                        DateTime checkout = Convert.ToDateTime(atd.AttDate);
                        TimeSpan chkints = TimeSpan.Parse(atd.AttCheckIn);
                        checkin = checkin.Add(chkints);
                        atd.AttCheckIn = checkin.ToString();
                        TimeSpan chkoutts = TimeSpan.Parse(atd.AttCheckOut);
                        checkout = checkout.Add(chkoutts);
                        atd.AttCheckOut = checkout.ToString();
                    }
                    else
                   if (atd.AttCheckIn != null)
                    {
                        DateTime checkin = Convert.ToDateTime(atd.AttDate);
                        TimeSpan chkints = TimeSpan.Parse(atd.AttCheckIn);
                        checkin = checkin.Add(chkints);
                        atd.AttCheckIn = checkin.ToString();
                    }
                    else
                         if (atd.AttCheckOut != null)
                    {
                        DateTime checkout = Convert.ToDateTime(atd.AttDate);
                        TimeSpan chkoutts = TimeSpan.Parse(atd.AttCheckOut);
                        checkout = checkout.Add(chkoutts);
                        atd.AttCheckOut = checkout.ToString();
                    }

                    _attendanceDailyServices.UpdateIndividualAttendanceDaily(atd);
                }


                int id = atd.EmpCode;
                string sdate = atd.SearchStartdate;
                string enddate = atd.SearchEndDate;

                return RedirectToAction("Index", new { id = id, Sdate = sdate, Enddate = enddate });

            }

            catch (Exception Ex)
            {
                return View(Ex.Message);
            }
        }
    }
}