using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace BeeHrmClientWeb.Controllers
{
    public class AttendanceController : BaseController
    {
        private IAttendanceDailyService _attendanceDailyServices;
        //private IOfficeTypeService _officeTypeServices;
        private IDepartmentService _departmentServices;
        private IGroupService _groupServices;
        private IDesignationService _designationServices;
        private IModulServices _moduleService;
        private IOfficeServices _officeServices;
        private INotification _notifications;
        private IEmployeeService _employeeServices;
        public AttendanceController()
        {
            _attendanceDailyServices = new AttendanceDailyService();
            _departmentServices = new DepartmentService();
            _designationServices = new DesignationService();
            _groupServices = new GroupService();
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


        // GET: Attendance
        [Route("Attendances")]
        public ActionResult Index()
        {
            try
            {
                ViewBag.ddlOfficeTypeList = _officeServices.GetOfficeData().ToList();
                ViewBag.ddlDesignationList = _designationServices.GetDesignationList().ToList();
                ViewBag.ddlBusinessGroupList = _groupServices.GetGroupList().ToList();
                ViewBag.ddlDepartmentList = _departmentServices.GetDepartmentlist().ToList();

                return View();
            }
            catch (Exception Ex)
            {
                ViewBag.Error = Ex.Message;
                return View();
            }

        }
        [Route("Attendances")]
        [HttpPost]
        public ActionResult Index(DailyAttendanceFilterViewModel att)
        {
			ViewBag.ddlDesignationList = _designationServices.GetDesignationList().ToList();
            ViewBag.ddlBusinessGroupList = _groupServices.GetGroupList().ToList();
            ViewBag.ddlDepartmentList = _departmentServices.GetDepartmentlist().ToList();
            ViewBag.ddlOfficeTypeList = _officeServices.GetOfficeData().ToList();

            try
            {

                //convert nepalidate to eng


                string englishDate = NepEngDate.NepToEng(att.startdateNP.ToString());
                ViewBag.nepaliDate = att.startdateNP;

                att.startdate = Convert.ToDateTime(englishDate);
                ViewBag.date = att.startdate;
                ViewBag.edate = att.Enddate;
                ViewBag.attempcode = att.EmpCode;
                ViewBag.Desgid = att.DsgId;
                ViewBag.deprtId = att.dprtid;
                ViewBag.officeid = att.officeid;
                ViewBag.emptype = att.emptypeid;
                

                ViewBag.attendancelist = _attendanceDailyServices.GetAttendanceDaily(att.officeid, att.DsgId, att.emptypeid, att.dprtid, att.EmpCode, att.startdate).ToList();
                return View();
            }
            catch (Exception Ex)
            {
                ViewBag.Error = Ex.Message;
                return View();
            }



        }


        //[Route("admin/attendance/{id}")]

        //public ActionResult Attendance(int id)
        //{


        //    try
        //    {
        //        ViewBag.usercode = id;
        //        var item = _attendanceDailyServices.GetAttendanceByRangeAndID(id, null, null);
        //        var list = _employeeServices.GetEmployeeDetails(id);
        //        ViewBag.Department = list.Department;
        //        ViewBag.Name = list.Name;
        //        ViewBag.office = list.OfficeName;
        //        ViewBag.Desg = list.Designation;
        //        ViewBag.EmployeeType = list.Group;
        //        ViewBag.Shift = list.Shift;
        //        ViewBag.level = list.Level;
        //        ViewBag.image = list.PhotoName;
        //        ViewBag.Empcode = list.Code;
        //        ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(ViewBag.Empcode);

        //        return View(item);

        //    }
        //    catch (Exception Ex)
        //    {
        //        ViewBag.Error = "No Data found or No employee found with this employeeCode";

        //        return View();
        //    }

        //}

        //[Route("admin/attendance/{id}")]
        //[HttpPost]
        //public ActionResult Attendance(DailyAttendanceFilterViewModel atd, int id)
        //{

        //    try
        //    {
        //        IEnumerable<DailyAttendanceFilterViewModel> item = _attendanceDailyServices.GetAttendanceByRangeAndID(id, atd.startdate, atd.Enddate);
        //        var list = _employeeServices.GetEmployeeDetails(id);
        //        ViewBag.Department = list.Department;
        //        ViewBag.Name = list.Name;
        //        ViewBag.office = list.OfficeName;
        //        ViewBag.Desg = list.Designation;
        //        ViewBag.EmployeeType = list.Group;
        //        ViewBag.Shift = list.Shift;
        //        ViewBag.level = list.Level;
        //        ViewBag.image = list.PhotoName;
        //        ViewBag.Empcode = list.Code;
        //        ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(ViewBag.Empcode);
        //        return View(item);
        //    }
        //    catch (Exception Ex)
        //    {
        //        ViewBag.Error = "No Data found or No employee found with this employeeCode";
        //        return View();
        //    }

        //}


        //[Route("empattendancestatus")]
        //[HttpGet]
        //public ActionResult EmployeeAttendanceStatus()
        //{


        //    ViewBag.ddlDesignationList = _designationServices.GetDesignationList();
        //    ViewBag.ddlOfficeTypeList = _officeServices.GetOfficeData();
        //    DateTime date1 = DateTime.Now;


        //    int AdminEmpCode = ViewBag.EmpCode;

        //    IEnumerable<DailyAttendanceFilterViewModel> list = _attendanceDailyServices.GetAttendanceDailyStatus(AdminEmpCode, date1, null, null, null);
        //    return View(list);

        //}

        //[Route("empattendancestatus")]
        //[HttpPost]
        //public ActionResult EmployeeAttendanceStatus(DailyAttendanceFilterViewModel att)
        //{
        //    try
        //    {
        //        ViewBag.date = att.searchdate;
        //        ViewBag.empdoce = att.code;
        //        ViewBag.Desgid = att.DsgId;
        //        ViewBag.officeid = att.officeid;
        //        ViewBag.ddlDesignationList = _designationServices.GetDesignationList();
        //        int empcode = Convert.ToInt32(Session["EmpCode"]);
        //        //IEnumerable<OfficeDTOs> officelist = _officeServices.GetClildOfficeListByEmpCode(empcode);

        //        int AdminEmpCode = ViewBag.EmpCode;

        //        ViewBag.ddlOfficeTypeList = _officeServices.GetClildOfficeListByEmpCode(empcode);
        //        IEnumerable<DailyAttendanceFilterViewModel> list = _attendanceDailyServices.GetAttendanceDailyStatus(AdminEmpCode, att.searchdate, att.EmpCode, att.DsgId, att.officeid);
        //        return View(list);
        //    }
        //    catch (Exception Ex)
        //    {
        //        ViewBag.Error = Ex.Message;
        //        return View();
        //    }

        //}
        //[Route("attendancerequest/list")]
        //[HttpGet]
        //public ActionResult AttendanceRequestList()
        //{
        //    int roleid = ViewBag.EmpRoleId;
        //    int EmpCode = Convert.ToInt32(Session["EmpCode"]);
        //    ViewBag.emplist = _employeeServices.GetEmployeeList(Convert.ToInt32(EmpCode));
        //    ViewBag.OfficeList = _officeServices.GetOfficeListByEmpRole(roleid);
        //    IEnumerable<AttendanceRequestsListViewModel> lst = _attendanceDailyServices.GetRequestAttendanceList(null, null, null, null, null).ToList();
        //    return View(lst);
        //}

        //[Route("attendancerequest/list")]
        //[HttpPost]
        //public ActionResult AttendanceRequestList(AttendanceRequestsListViewModel att)
        //{
        //    int roleid = ViewBag.EmpRoleId;
        //    int EmpCode = Convert.ToInt32(Session["EmpCode"]);
        //    ViewBag.officeid = att.OfficeId;
        //    ViewBag.empid = att.EmpCodes;
        //    ViewBag.startdate = att.startdate;
        //    ViewBag.enddate = att.enddate;
        //    ViewBag.approvestatus = att.ApproverStatus;
        //    ViewBag.recommendstatus = att.RecommendSatus;
        //    ViewBag.emplist = _employeeServices.GetEmployeeList(EmpCode);
        //    ViewBag.OfficeList = _officeServices.GetOfficeListByEmpRole(roleid);
        //    IEnumerable<AttendanceRequestsListViewModel> lst = _attendanceDailyServices.GetrequestAttendanceListByParms(att.OfficeId, att.EmpCodes, att.startdate, att.enddate, null, null, att.RecommendSatus, att.ApproverStatus).ToList();
        //    return View(lst);
        //}

        [Route("deleteattendence/{id}")]
        public JsonResult DeleteAttendance(int id)
        {



            try
            {
                IEnumerable<AttendanceRequestsListViewModel> lst = _attendanceDailyServices.GetRequestAttendanceList(null, null, null, id, null).ToList();

                int recommender_status = Convert.ToInt32(lst.FirstOrDefault().RecommendStatus);
                if (recommender_status == 1)
                {
                    _attendanceDailyServices.DeleteAttendancerequest(id);
                    return Json(new { Success = true }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);

            }
        }

        //[Route("admin/attendancerequestdetail/{id}")]
        //public ActionResult AdminAttendanceRequestDetail(int id)
        //{

        //    try
        //    {
        //        int empcode = ViewBag.Empcode;
        //        IEnumerable<AttendanceRequestsListViewModel> lst = _attendanceDailyServices.GetRequestAttendanceList(null, null, null, id, null);
        //        return View(lst);
        //    }
        //    catch (Exception Ex)
        //    {
        //        ViewBag.Error = Ex.Message;
        //        return View();
        //    }

        //}

        //[Route("attendance/admin/applyattendance")]
        //[HttpGet]
        //public ActionResult ApplyAttendance()
        //{
        //    try
        //    {
        //        int id = ViewBag.Empcode;
        //        int rid = ViewBag.EmpRoleId;
        //        ViewBag.EmployeeList = _employeeServices.GetEmployeeList(id);

        //        EmployeeDetailsViewModel emp = _employeeServices.GetEmployeeDetails(id);
        //        ViewBag.Approver = _employeeServices.GetAttendanceApprover(id);
        //        ViewBag.Recommande = _employeeServices.GetAttendanceRecommander(id);
        //        return View();
        //    }
        //    catch (Exception Ex)
        //    {
        //        ViewBag.Error = Ex.Message;
        //        return View();
        //    }



        //}

        //[Route("attendance/admin/applyattendance")]
        //[HttpPost]
        //public ActionResult ApplyAttendance(AttendanceRequestDTO att)
        //{
        //    int id = ViewBag.Empcode;
        //    int roleid = ViewBag.EmpRoleId;
        //    ViewBag.EmployeeList = _employeeServices.GetEmployeeList(id);
        //    EmployeeDetailsViewModel emp = _employeeServices.GetEmployeeDetails(id);
        //    ViewBag.Approver = _employeeServices.GetAttendanceApprover(id);
        //    ViewBag.Recommande = _employeeServices.GetAttendanceRecommander(id);
        //    att.CheckInDate = att.CheckInDate + att.CheckInTime;
        //    att.CheckOutDate = att.CheckOutDate + att.CheckOutTime;
        //    IPHostEntry host;
        //    string localIP = "?";
        //    host = Dns.GetHostEntry(Dns.GetHostName());
        //    foreach (IPAddress ip in host.AddressList)
        //    {
        //        if (ip.AddressFamily.ToString() == "InterNetwork")
        //        {
        //            localIP = ip.ToString();
        //        }
        //    }
        //    att.IpAddress = localIP;
        //    att.RequestCreatedDate = DateTime.Now;
        //    att.RecommendStatus = 1;
        //    att.ApproveStatus = 1;

        //    att.RecommendStatus = 1;
        //    att.ApproveStatus = 1;

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            if (att.RequestType == "CheckIn")
        //            {
        //                att.RequestedDate = att.CheckInDate;
        //                att.CheckOutDate = null;
        //            }
        //            if (att.RequestType == "CheckOut")
        //            {
        //                att.RequestedDate = att.CheckOutDate;
        //                att.CheckInDate = null;
        //            }
        //            else
        //            {
        //                att.RequestedDate = att.CheckInDate;
        //            }

        //            if (att.RequestEmpCode > 0)
        //            {
        //                AttendanceRequestDTO Atdo = new AttendanceRequestDTO();
        //                Atdo = _attendanceDailyServices.InsertAttendanceRequest(att);
        //                Session["attendance_request"] = "New attendance requested sucessfully !!!";

        //                #region
        //                // email sender test
        //                string emailType = "Email";
        //                EmailServices.Message msg = new EmailServices.Message();
        //                htmlReader reader = new htmlReader();
        //                List<string> recipient = new List<string>()
        //                    {


        //                       "bikrampaudel89@gmail.com"

        //                    };
        //                msg.Subject = "Attendance Requested for " + att.RequestType + "on  " + att.RequestedDate;
        //                EmailTemplateModel emailTemplateData = new EmailTemplateModel()
        //                {
        //                    UserName = att.ApproverEmpCode.ToString(),
        //                    Descriptions = "Password Changed",
        //                    FullName = "Young Mind Creation",
        //                    Title = "Password Changed",
        //                    Url = ""
        //                };
        //                msg.Body = reader.GetHtmlBodyTemplate(emailTemplateData, emailType);
        //                EmailServices.Notify(recipient, msg);

        //                #endregion
        //                return RedirectToAction("AttendanceRequestList", "Attendance");
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("EmployeeCode", "Please select Employee from dropdown list !!");
        //                return View();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ViewBag.Error = "Error While Requesting Attendance Online!" + ex.Message;
        //            return View();
        //        }
        //    }

        //    else
        //    {
        //        ViewBag.Error = "Please fill required field below ";
        //        return View();
        //    }
        //}

        //[HttpGet]
        //[Route("admin/updateattendances/{id}")]
        //public ActionResult UpdateIndividualAttendance(int id, string Sdate, string Enddate)
        //{
        //    try
        //    {
        //        if (String.IsNullOrEmpty(Sdate) && String.IsNullOrEmpty(Enddate))
        //        {
        //            var list = _employeeServices.GetEmployeeDetails(id);
        //            UpdateAttendanceSearchModel uds = new UpdateAttendanceSearchModel();
        //            uds.AttendanceList = _attendanceDailyServices.GetAttendanceByRangeAndID(id, null, null);
        //            uds.Name = list.Name;
        //            uds.Empcode = list.Code;
        //            uds.Branch = list.OfficeName;
        //            uds.Department = list.Department;
        //            uds.Designation = list.Designation;
        //            uds.Shift = list.Shift;
        //            uds.Group = list.Group;
        //            uds.Level = list.Level;
        //            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
        //            return View(uds);
        //        }
        //        else
        //        {
        //            var list = _employeeServices.GetEmployeeDetails(id);
        //            UpdateAttendanceSearchModel uds = new UpdateAttendanceSearchModel();
        //            DateTime Stsrtdate, Edate;
        //            Stsrtdate = Convert.ToDateTime(Sdate);
        //            Edate = Convert.ToDateTime(Enddate);
        //            uds.AttendanceList = _attendanceDailyServices.GetAttendanceByRangeAndID(id, Stsrtdate, Edate);
        //            uds.Name = list.Name;
        //            uds.SearchStartdate = Sdate;
        //            uds.SeacrchEnddate = Enddate;
        //            uds.Empcode = list.Code;
        //            uds.Branch = list.OfficeName;
        //            uds.Department = list.Department;
        //            uds.Designation = list.Designation;
        //            uds.Shift = list.Shift;
        //            uds.Group = list.Group;
        //            uds.Level = list.Level;
        //            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
        //            return View(uds);
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        ViewBag.Error = "No Data found or No employee found with this employeeCode";

        //        return View();
        //    }
        //}

        //[HttpPost]
        //[Route("admin/updateattendances/{id}")]
        //public ActionResult UpdateIndividualAttendance(UpdateAttendanceSearchModel adt, int id)
        //{
        //    try
        //    {
        //        var list = _employeeServices.GetEmployeeDetails(id);
        //        ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
        //        UpdateAttendanceSearchModel uds = new UpdateAttendanceSearchModel();
        //        DateTime sadte, enddate;
        //        sadte = Convert.ToDateTime(adt.SearchStartdate);
        //        enddate = Convert.ToDateTime(adt.SeacrchEnddate);
        //        uds.AttendanceList = _attendanceDailyServices.GetAttendanceByRangeAndID(id, sadte, enddate);
        //        uds.Name = list.Name;
        //        uds.Empcode = list.Code;
        //        uds.Branch = list.OfficeName;
        //        uds.Department = list.Department;
        //        uds.Designation = list.Designation;
        //        uds.Shift = list.Shift;
        //        uds.Group = list.Group;
        //        uds.Level = list.Level;
        //        uds.SearchStartdate = adt.SearchStartdate;
        //        uds.SeacrchEnddate = adt.SeacrchEnddate;
        //        return View(uds);
        //    }
        //    catch (Exception Ex)
        //    {
        //        return View(Ex.Message);
        //    }


        //}
        //[Route("admin/update/attendances/")]
        //public ActionResult UpdateAttendance(AttendanceDailyViewModel atd)
        //{
        //    try
        //    {
        //        if (atd.IsAbsent == 1)
        //        {
        //            atd.AttCheckIn = null;
        //            atd.AttCheckOut = null;
        //            _attendanceDailyServices.UpdateIndividualAttendanceDaily(atd);
        //        }

        //        else
        //        {

        //            if (atd.AttCheckIn != null && atd.AttCheckOut != null)
        //            {
        //                DateTime checkin = Convert.ToDateTime(atd.AttDate);
        //                DateTime checkout = Convert.ToDateTime(atd.AttDate);
        //                TimeSpan chkints = TimeSpan.Parse(atd.AttCheckIn);
        //                checkin = checkin.Add(chkints);
        //                atd.AttCheckIn = checkin.ToString();
        //                TimeSpan chkoutts = TimeSpan.Parse(atd.AttCheckOut);
        //                checkout = checkout.Add(chkoutts);
        //                atd.AttCheckOut = checkout.ToString();
        //            }
        //            else
        //           if (atd.AttCheckIn != null)
        //            {
        //                DateTime checkin = Convert.ToDateTime(atd.AttDate);
        //                TimeSpan chkints = TimeSpan.Parse(atd.AttCheckIn);
        //                checkin = checkin.Add(chkints);
        //                atd.AttCheckIn = checkin.ToString();
        //            }
        //            else
        //                 if (atd.AttCheckOut != null)
        //            {
        //                DateTime checkout = Convert.ToDateTime(atd.AttDate);
        //                TimeSpan chkoutts = TimeSpan.Parse(atd.AttCheckOut);
        //                checkout = checkout.Add(chkoutts);
        //                atd.AttCheckOut = checkout.ToString();
        //            }

        //            _attendanceDailyServices.UpdateIndividualAttendanceDaily(atd);
        //        }


        //        int id = atd.EmpCode;
        //        string sdate = atd.SearchStartdate;
        //        string enddate = atd.SearchEndDate;

        //        return RedirectToAction("UpdateIndividualAttendance", new { id = id, Sdate = sdate, Enddate = enddate });

        //    }

        //    catch (Exception Ex)
        //    {
        //        return View(Ex.Message);
        //    }
        //}

        //[Route("attendance/admin/attendancebydate")]
        //public ActionResult AttendanceBetweenByEmpCode()
        //{

        //    int EmpCode = Convert.ToInt32(Session["EmpCode"]);
        //    ViewBag.emplist = _employeeServices.GetEmployeeList(EmpCode);
        //    return View();



        //}


        //[Route("attendance/admin/attendancebydate")]
        //[HttpPost]
        //public ActionResult AttendanceBetweenByEmpCode(DailyAttendanceFilterViewModel atd)
        //{
        //    try
        //    {
        //        int EmpCode = Convert.ToInt32(Session["EmpCode"]);
        //        ViewBag.emplist = _employeeServices.GetEmployeeList(EmpCode);
        //        IEnumerable<DailyAttendanceFilterViewModel> item = _attendanceDailyServices.GetAttendanceByRangeAndID(atd.EmpSearchCode, atd.startdate, atd.Enddate);
        //        var list = _employeeServices.GetEmployeeDetails(atd.EmpSearchCode);
        //        ViewBag.Department = list.Department;
        //        ViewBag.code = atd.EmpSearchCode;
        //        ViewBag.sdate = atd.startdate;
        //        ViewBag.edate = atd.Enddate;
        //        ViewBag.Name = list.Name;
        //        ViewBag.office = list.OfficeName;
        //        ViewBag.Desg = list.Designation;
        //        ViewBag.EmployeeType = list.Group;
        //        ViewBag.Shift = list.Shift;
        //        ViewBag.level = list.Level;
        //        ViewBag.image = list.PhotoName;

        //        return View(item);
        //    }
        //    catch (Exception Ex)
        //    {
        //        ViewBag.Error = "No Data Found !!" + Ex.Message;
        //        return View();
        //    }
        //}

        [Route("notification/readnotification/{id}")]
        public ActionResult NotificationRead(int id)
        {


            try
            {
                IEnumerable<NotificationsViewModel> ntd = _notifications.SingleNotification(id);
                NotificationsDTOs notif = new NotificationsDTOs();
                notif.NotificationId = Convert.ToInt32(ntd.FirstOrDefault().NotificationID);
                notif.NotificationMessage = ntd.FirstOrDefault().Message;
                notif.NotificationReceiverId = Convert.ToInt32(ntd.FirstOrDefault().ReceiverID);
                notif.NotificationReceiverType = ntd.FirstOrDefault().ReceiverType;
                notif.NotificationDate = Convert.ToDateTime(ntd.FirstOrDefault().Date);
                notif.NotificationSubject = ntd.FirstOrDefault().Subject;
                notif.NotificationReadDate = DateTime.Now;
                notif.NotificationDetailURL = ntd.FirstOrDefault().DetailUrl;

                _notifications.UpdateNotification(notif);
                if (String.IsNullOrEmpty(notif.NotificationDetailURL))
                {
                    return RedirectToAction("Notifications", "User");
                }
                else
                {
                    return Redirect(notif.NotificationDetailURL);
                }



            }
            catch (Exception ex)
            {
                return RedirectToAction("Notifications", "User");
            }



        }
    }

}