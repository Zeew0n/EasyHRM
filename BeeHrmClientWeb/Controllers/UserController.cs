using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Models;
using BeeHrmClientWeb.Utilities;
using BeeHrmClientWeb.Utilities.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers
{
    public class UserController : BaseController
    {
        private IModulServices _moduleService;
        private ILeaveApplicationService _leaveApplicationService;
        private IEmployeeService _empService;
        private IAttendanceDailyService _attendanceDailyServices;
        private IDepartmentService _departmentServices;
        private IGroupService _groupServices;
        private IDesignationService _designationServices;
        private IOfficeServices _officeServices;
        private IEmployeeService _employeeServices;
        private INotification _notifications;
        private IReportsServices _reportServices;

        public UserController()
        {
            _moduleService = new ModuleService();
            _leaveApplicationService = new LeaveApplicationService();
            _empService = new EmployeeService();
            _attendanceDailyServices = new AttendanceDailyService();
            _departmentServices = new DepartmentService();
            _designationServices = new DesignationService();
            _groupServices = new GroupService();
            _officeServices = new OfficeServices();
            _moduleService = new ModuleService();
            _employeeServices = new EmployeeService();
            _notifications = new NotificationService();
            _reportServices = new ReportsServices();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
        }
        [Route("user/index")]
        public ActionResult Index()
        {
            //  return View();

            int roleid = Convert.ToInt32(Session["RoleId"].ToString());

            //return View();

            string controllerName = "mymenu";// this.ControllerContext.RouteData.Values["controller"].ToString();


            int ParentId = _moduleService.GetParentId(controllerName);

            IEnumerable<ModuleDTOs> ChildList = _moduleService.GetChildMenuList(ParentId, roleid);


            return View("../Configurations/index", ChildList);
        }

        #region UserAttendance
        [Route("user/attendance")]

        public ActionResult Attendance()
        {


            try
            {
                int id = ViewBag.Empcode;
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


                return View(item);

            }
            catch (Exception Ex)
            {
                ViewBag.Error = "No Data found or No employee found with this employeeCode";

                return View();
            }

        }


        [Route("user/attendance")]
        [HttpPost]
        public ActionResult Attendance(DailyAttendanceFilterViewModel atd)
        {
            int id = ViewBag.Empcode;
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

                return View(item);
            }
            catch (Exception Ex)
            {
                ViewBag.Error = "No Data found or No employee found with this employeeCode";
                return View();
            }

        }

        [Route("user/applyattendance")]
        [HttpGet]
        public ActionResult RequestAttendance()
        {
            try
            {
                int id = ViewBag.Empcode;
                int roleid = ViewBag.EmpRoleId;
                EmployeeDetailsViewModel emp = _employeeServices.GetEmployeeDetails(id);
                ViewBag.Approver = _employeeServices.GetAttendanceApprover(id);
                ViewBag.Recommande = _employeeServices.GetAttendanceRecommander(id);
                ViewBag.id = id;
                return View();
            }
            catch (Exception Ex)
            {
                ViewBag.error = Ex.Message;
                return View();
            }

        }
        [Route("user/applyattendance")]
        [HttpPost]
        public ActionResult RequestAttendance(AttendanceRequestDTO att)
        {
            if (att.CheckInDateNP != null) {
                att.CheckInDate = Convert.ToDateTime(NepEngDate.NepToEng(att.CheckInDateNP));
            }
            if (att.CheckOutDateNP != null) {
                att.CheckOutDate = Convert.ToDateTime(NepEngDate.NepToEng(att.CheckOutDateNP));
            }

            int id = ViewBag.Empcode;
            int roleid = ViewBag.EmpRoleId;
            att.RequestEmpCode = id;
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            att.IpAddress = localIP;
            EmployeeDetailsViewModel emp = _employeeServices.GetEmployeeDetails(id);
            ViewBag.Approver = _employeeServices.GetAttendanceApprover(id);
            ViewBag.Recommande = _employeeServices.GetAttendanceRecommander(id);

            if (att.CheckInTime != null)
            {
                att.CheckInDate = att.CheckInDate + att.CheckInTime;
            }
            if (att.CheckOutTime != null) {
                att.CheckOutDate = att.CheckOutDate + att.CheckOutTime;
            }

            if (att.RequestType == "CheckIn")
            {
                att.RequestedDate = att.CheckInDate;
                att.CheckOutDate = null;
            }
            if (att.RequestType == "CheckOut")
            {
                att.RequestedDate = att.CheckOutDate;
                att.CheckInDate = null;
            }
            else
            {
                att.RequestedDate = att.CheckInDate;
            }
            att.RequestCreatedDate = DateTime.Now;
            att.RecommendStatus = 1;
            att.ApproveStatus = 1;

            if (ModelState.IsValid)
            {
                try
                {


                    AttendanceRequestDTO Atdo = new AttendanceRequestDTO();
                    Atdo = _attendanceDailyServices.InsertAttendanceRequest(att);
                    NotificationRequestAttendance(att);

                    Session["attendance_request"] = "New attendance requested sucessfully !!!";

                    return RedirectToAction("IndividualAttendanceList", "User");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Error While Requesting Attendance Online!" + ex.Message;
                    return View();
                }
            }

            else
            {
                ViewBag.Error = "Please Select required field below ";
                return View();
            }
        }

        [Route("user/attendancerequest/recommend/{id}")]
        [HttpGet]
        public ActionResult RecommendAttendance(int id)
        {
            IEnumerable<AttendanceRequestsListViewModel> lst = _attendanceDailyServices.GetRequestAttendanceList(null, null, null, id, null).ToList();
            int status = Convert.ToInt32(lst.FirstOrDefault().RecommendStatus);
            ViewBag.id = Convert.ToInt32(lst.FirstOrDefault().RecommenderEmpCode);
            return View(lst);




        }

        [Route("user/attendancerequest/recommend/{id}")]
        [HttpPost]
        public ActionResult RecommendAttendance(AttendanceRequestDTO att)

        {

            try
            {
                int id = Convert.ToInt32(att.RequestId);
                IEnumerable<AttendanceRequestsListViewModel> lst = _attendanceDailyServices.GetRequestAttendanceList(null, null, null, id, null);

                att.RequestDescription = lst.FirstOrDefault().Description;
                att.RecommendStatusDate = DateTime.Now;
                att.RequestType = lst.FirstOrDefault().RequestType;
                att.RecommendarEmpCode = Convert.ToInt32(lst.FirstOrDefault().RecommenderEmpCode);
                att.ApproverEmpCode = Convert.ToInt32(lst.FirstOrDefault().ApproverEmpCode);
                att.RequestEmpCode = Convert.ToInt32(lst.FirstOrDefault().EmpCode);
                att.IpAddress = lst.FirstOrDefault().IpAddress;
                ViewBag.id = att.RecommendarEmpCode;
                att.ApproveStatus = 1;
                var chkindate = lst.FirstOrDefault().CheckIn_Date;
                var chaoutdate = lst.FirstOrDefault().CheckOut_Date;
                if (chkindate == null || chkindate == "")
                {
                    att.CheckInDate = null;
                }
                else
                {
                    att.CheckInDate = Convert.ToDateTime(lst.FirstOrDefault().CheckIn_Date);
                }
                if (chaoutdate == null || chaoutdate == "")
                {
                    att.CheckOutDate = null;
                }
                else
                {
                    att.CheckOutDate = Convert.ToDateTime(lst.FirstOrDefault().CheckOut_Date);
                }

                att.RequestedDate = Convert.ToDateTime(lst.FirstOrDefault().RequestDate);
                _attendanceDailyServices.UpdateAttendanceRequest(att);
                NotificationRecommendAttendance(att);

                Session["recommend-status_message"] = " Recommend status has changed";
                return RedirectToAction("RecommenderList", "User");
            }

            catch (Exception Ex)
            {
                ViewBag.error = "Error while confirming the request !!" + Ex.Message;
                return View();
            }

        }

        [Route("user/attendancerequest/approve/{id}")]
        [HttpGet]
        public ActionResult ApproveAttendance(int id)
        {
            try
            {
                IEnumerable<AttendanceRequestsListViewModel> lst = _attendanceDailyServices.GetRequestAttendanceList(null, null, null, id, null).ToList();
                ViewBag.id = Convert.ToInt32(lst.FirstOrDefault().ApproverEmpCode);
                return View(lst);

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();

            }

        }


        [Route("user/attendancerequest/approve/{id}")]
        [HttpPost]
        public ActionResult ApproveAttendance(AttendanceRequestDTO att)
        {
            try
            {
                int id = Convert.ToInt32(att.RequestId);
                IEnumerable<AttendanceRequestsListViewModel> lst = _attendanceDailyServices.GetRequestAttendanceList(null, null, null, id, null);
                att.ApproveStatusDate = DateTime.Now;
                att.RequestDescription = lst.FirstOrDefault().Description;
                att.RequestType = lst.FirstOrDefault().RequestType;
                att.RecommendarEmpCode = Convert.ToInt32(lst.FirstOrDefault().RecommenderEmpCode);
                att.ApproverEmpCode = Convert.ToInt32(lst.FirstOrDefault().ApproverEmpCode);
                att.RequestEmpCode = Convert.ToInt32(lst.FirstOrDefault().EmpCode);
                att.RecommendStatus = Convert.ToByte(lst.FirstOrDefault().RecommendStatus);
                att.RequestCreatedDate = DateTime.Now;
                att.RecommedarMessage = lst.FirstOrDefault().RecommendMessage;
                att.IpAddress = lst.FirstOrDefault().IpAddress;
                att.RecommendStatusDate = Convert.ToDateTime(lst.FirstOrDefault().RecommendeDate);
                att.RequestedDate = Convert.ToDateTime(lst.FirstOrDefault().RequestDate);
                var chkindate = lst.FirstOrDefault().CheckIn_Date;
                var chaoutdate = lst.FirstOrDefault().CheckOut_Date;
                int? status = lst.FirstOrDefault().ApproverStatus;

                if (status != 2)
                {

                    if (String.IsNullOrEmpty(chkindate))
                    {
                        att.CheckInDate = null;
                    }
                    else
                    {
                        att.CheckInDate = Convert.ToDateTime(lst.FirstOrDefault().CheckIn_Date);
                    }
                    if (String.IsNullOrEmpty(chaoutdate))
                    {
                        att.CheckOutDate = null;
                    }
                    else
                    {
                        att.CheckOutDate = Convert.ToDateTime(lst.FirstOrDefault().CheckOut_Date);
                    }

                    att.RequestedDate = Convert.ToDateTime(lst.FirstOrDefault().RequestDate);

                    _attendanceDailyServices.UpdateAttendanceRequest(att);
                    _attendanceDailyServices.GetenareDailyAttendance(att.RequestId);
                    NotificationApproveAttendance(att);


                    Session["approver-status_message"] = " Approver status has changed";
                    return RedirectToAction("ApproverList", "User");
                }
                else
                {
                    if (att.ApproveStatus == 3)
                    {

                        _attendanceDailyServices.RejectApprovedAttendance(att);
                        NotificationApproveAttendance(att);

                        Session["approver-status_message"] = "Attendance Request Rejected Sucessfully !!!";
                        return RedirectToAction("ApproverList", "User");
                    }
                    else
                    {


                        Session["approver-status_message"] = " Attendance Request already been Approved !!";
                        return RedirectToAction("ApproverList", "User");
                    }


                }
            }



            catch (Exception Ex)
            {
                ViewBag.error = "Error while confirming the request !!" + Ex.Message;
                return View();
            }

        }

        [Route("employeelistbyofficeid")]
        public JsonResult GetemployeeListByOfficeId(int id)
        {

            IEnumerable<DDLEmployeemodel> modelList = new List<DDLEmployeemodel>();
            var mtrlstk = _employeeServices.GetEmployeeByOfficeid(id);
            modelList = mtrlstk.Select(x =>
                        new DDLEmployeemodel()
                        {
                            Empcode = x.EmpCode,
                            EmpName = x.EmpName

                        });

            return Json(new { data = modelList }, JsonRequestBehavior.AllowGet);

        }


        [Route("user/attendancerequest/approvelist")]
        public ActionResult ApproverList()
        {
            try
            {
                int id = ViewBag.Empcode;
                int role = ViewBag.EmpRoleId;
                ViewBag.emplist = _employeeServices.GetEmployeeList(id);
                ViewBag.OfficeList = _officeServices.GetOfficeListByEmpRole(role);
                IEnumerable<AttendanceRequestsListViewModel> lst = _attendanceDailyServices.GetRequestAttendanceList(null, id, null, null, 2).ToList();
                return View(lst);
            }
            catch (Exception Ex)
            {
                ViewBag.error = Ex.Message;
                return View();
            }

        }

        [Route("user/attendancerequest/approvelist")]
        [HttpPost]
        public ActionResult ApproverList(AttendanceRequestsListViewModel att)
        {
            try
            {
                att.startdate = Convert.ToDateTime(NepEngDate.NepToEng(att.startdateNP));
                att.enddate = Convert.ToDateTime(NepEngDate.NepToEng(att.enddateNP));

                int id = ViewBag.Empcode;
                int role = ViewBag.EmpRoleId;
                ViewBag.emplist = _employeeServices.GetEmployeeList(id);
                ViewBag.OfficeList = _officeServices.GetOfficeListByEmpRole(role);
                ViewBag.startdate = att.startdateNP;
                ViewBag.officeid = att.OfficeId;
                ViewBag.code = att.EmpCodes;
                ViewBag.enddate = att.enddateNP;
                ViewBag.approvestatus = att.ApproverStatus;
                ViewBag.Id = id;
                IEnumerable<AttendanceRequestsListViewModel> lst = _attendanceDailyServices.GetrequestAttendanceListByParms(att.OfficeId, att.EmpCodes, att.startdate, att.enddate, null, id, 2, att.ApproverStatus).ToList();
                return View(lst);
            }
            catch (Exception Ex)
            {
                ViewBag.Error = Ex.Message;
                return View();
            }

        }

        [Route("user/attendancerequest/recommendlist")]
        [HttpGet]
        public ActionResult RecommenderList()
        {

            try
            {
                int id = ViewBag.Empcode;
                int role = ViewBag.EmpRoleID;
                ViewBag.emplist = _employeeServices.GetEmployeeList(id);
                ViewBag.officeList = _officeServices.GetOfficeListByEmpRole(role);
                IEnumerable<AttendanceRequestsListViewModel> lst = _attendanceDailyServices.GetRequestAttendanceList(null, null, id, null, null).ToList();
                return View(lst);
            }
            catch (Exception Ex)
            {
                ViewBag.Error = Ex.Message;
                return View();
            }

        }
        [Route("user/attendancerequest/recommendlist")]
        [HttpPost]
        public ActionResult RecommenderList(AttendanceRequestsListViewModel att)
        {
            att.startdate = Convert.ToDateTime(NepEngDate.NepToEng(att.startdateNP));
            att.enddate = Convert.ToDateTime(NepEngDate.NepToEng(att.enddateNP));

            ViewBag.startdate = att.startdateNP;
            ViewBag.enddate = att.enddateNP;

            int id = ViewBag.Empcode;
            ViewBag.emplist = _employeeServices.GetEmployeeList(id);
            ViewBag.OfficeList = _officeServices.GetOfficeAllData();
            ViewBag.officeid = att.OfficeId;
            ViewBag.code = att.EmpCodes;
            ViewBag.approvestatus = att.ApproverStatus;
            ViewBag.recommendstatus = att.RecommendSatus;
            ViewBag.id = id;
            IEnumerable<AttendanceRequestsListViewModel> lst = _attendanceDailyServices.GetrequestAttendanceListByParms(att.OfficeId, att.EmpCodes, att.startdate, att.enddate, id, null, att.RecommendSatus, att.ApproverStatus).ToList();
            return View(lst);
        }

        [Route("user/attendancerequest/selfattlist")]
        [HttpGet]
        public ActionResult IndividualAttendanceList()
        {
            int id = ViewBag.Empcode;
            IEnumerable<AttendanceRequestsListViewModel> lst = _attendanceDailyServices.GetRequestAttendanceList(id, null, null, null, null).ToList();
            return View(lst);
        }

        [Route("user/attendancerequest/selfattlist")]
        [HttpPost]
        public ActionResult IndividualAttendanceList(AttendanceRequestsListViewModel att)
        {
            att.startdate = Convert.ToDateTime(NepEngDate.NepToEng(att.startdateNP));
            att.enddate = Convert.ToDateTime(NepEngDate.NepToEng(att.enddateNP));

            int id = ViewBag.Empcode;
            ViewBag.startdate = att.startdateNP;
            ViewBag.enddate = att.enddateNP;
            ViewBag.approvestatus = att.ApproverStatus;
            ViewBag.recommendstatus = att.RecommendSatus;
            IEnumerable<AttendanceRequestsListViewModel> lst = _attendanceDailyServices.GetrequestAttendanceListByParms(att.OfficeId, id, att.startdate, att.enddate, null, null, att.RecommendSatus, att.ApproverStatus).ToList();
            return View(lst);
        }

        [Route("user/attendancerequest/AttendanceDetail/{id}")]
        public ActionResult AttendanceDetail(int id)
        {
            int empcode = ViewBag.Empcode;
            IEnumerable<AttendanceRequestsListViewModel> lst = _attendanceDailyServices.GetRequestAttendanceList(empcode, null, null, id, null);
            return View(lst);
        }

        [Route("user/changepassword")]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Route("user/changepassword")]
        [HttpPost]
        public ActionResult ChangePassword(ChangePassword ch)
        {
            int id = ViewBag.EmpCode;
            try
            {
                if (ModelState.IsValid)
                {

                    string oldpassword = Session["Userdata"].ToString();
                    MD5 old = MD5.Create();
                    string oldpass = GetMd5Hash(old, ch.OldPassword);
                    if (oldpass != oldpassword)
                    {
                        ModelState.AddModelError("OldPassword", "Old Password does not match !!");
                        return View();
                    }
                    else
                    if (ch.NewPassword != ch.ConfirmPassword)
                    {

                        ModelState.AddModelError("ConfirmPassword", "New Password and Confirm Password should be same !!");
                        return View();

                    }
                    else

                    {
                        if (ch.ConfirmPassword == ch.OldPassword)
                        {
                            ModelState.AddModelError("ConfirmPassword", "Old Password and New Password cannot be same  !!");
                            return View();
                        }

                        MD5 md5Hash = MD5.Create();
                        string hash = GetMd5Hash(md5Hash, ch.ConfirmPassword);

                        _employeeServices.UpdatePassword(id, hash);
                        ViewBag.sucess = " Thank you  !! Your password has been changed sucessfully.";
                        return View();
                    }
                }
                else
                {
                    return View();
                }

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        [Route("user/notifications")]
        public ActionResult Notifications()
        {
            int id = ViewBag.EmpCode;
            IEnumerable<NotificationsViewModel> lst = _notifications.Notificationlist(id);

            return View();
        }

        public ActionResult SendSms()
        {
            SMS sms = new SMS();
            sms.To = "9844381332";
            sms.From = "DEMO";
            sms.MgsBody = "Dear";
            sms.Token = "AwwBOmsFhIjUlt5DFrbY";
            PostSendSMS(sms);
            return View();
        }
        #endregion
        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString());
            }
            return sBuilder.ToString();
        }
    }
}