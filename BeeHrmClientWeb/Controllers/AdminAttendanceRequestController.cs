using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Models;
using BeeHrmClientWeb.Utilities;
using EvaluationApi.CrossCutting.Infrastructure.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace BeeHrmClientWeb.Controllers
{
    public class AdminAttendanceRequestController : BaseController
    {
        private IAttendanceDailyService _attendanceDailyServices;

        private IDesignationService _designationServices;
        private IOfficeServices _officeServices;
        private INotification _notifications;
        private IEmployeeService _employeeServices;
        private IModulServices _moduleService;

        public AdminAttendanceRequestController()
        {
            _attendanceDailyServices = new AttendanceDailyService();
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
        // GET: AdminAttendanceRequest

        [HttpGet]
        public ActionResult Index()
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to view or update attendance request details";
                return PartialView("_partialviewNotFound");
            }


            int roleid = ViewBag.EmpRoleId;
            int EmpCode = Convert.ToInt32(Session["EmpCode"]);
            ViewBag.emplist = _employeeServices.GetEmployeeList(Convert.ToInt32(EmpCode));
            ViewBag.OfficeList = _officeServices.GetOfficeListByEmpRole(roleid);
            IEnumerable<AttendanceRequestsListViewModel> lst = _attendanceDailyServices.GetRequestAttendanceList(null, null, null, null, null).ToList();
            return View(lst);
        }


        [HttpPost]
        public ActionResult Index(AttendanceRequestsListViewModel att)
        {
            att.startdate = !String.IsNullOrEmpty(att.startdateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(att.startdateNP)) : att.startdate;
            att.enddate = !String.IsNullOrEmpty(att.enddateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(att.enddateNP)) : att.enddate;

            int roleid = ViewBag.EmpRoleId;
            int EmpCode = Convert.ToInt32(Session["EmpCode"]);
            ViewBag.officeid = att.OfficeId;
            ViewBag.empid = att.EmpCodes;
            ViewBag.startdate = att.startdateNP;
            ViewBag.enddate = att.enddateNP;
            ViewBag.approvestatus = att.ApproverStatus;
            ViewBag.recommendstatus = att.RecommendSatus;
            ViewBag.emplist = _employeeServices.GetEmployeeList(EmpCode);
            ViewBag.OfficeList = _officeServices.GetOfficeListByEmpRole(roleid);
            IEnumerable<AttendanceRequestsListViewModel> lst = _attendanceDailyServices.GetrequestAttendanceListByParms(att.OfficeId, att.EmpCodes, att.startdate, att.enddate, null, null, att.RecommendSatus, att.ApproverStatus).ToList();
            return View(lst);
        }
        [HttpGet]

        public ActionResult RequestDetail(int id)
        {

            try
            {
                int empcode = ViewBag.Empcode;
                IEnumerable<AttendanceRequestsListViewModel> lst = _attendanceDailyServices.GetRequestAttendanceList(null, null, null, id, null);
                return View(lst);
            }
            catch (Exception Ex)
            {
                ViewBag.Error = Ex.Message;
                return View();
            }

        }



        [HttpGet]
        public ActionResult ApplyAttendance()
        {
            try
            {
                int id = ViewBag.Empcode;
                int rid = ViewBag.EmpRoleId;
                ViewBag.EmployeeList = _employeeServices.GetEmployeeList(id);

                EmployeeDetailsViewModel emp = _employeeServices.GetEmployeeDetails(id);
                ViewBag.Approver = _employeeServices.GetAttendanceApprover(id);
                ViewBag.Recommande = _employeeServices.GetAttendanceRecommander(id);
                return View();
            }
            catch (Exception Ex)
            {
                ViewBag.Error = Ex.Message;
                return View();
            }



        }


        [HttpPost]
        public ActionResult ApplyAttendance(AttendanceRequestDTO att)
        {
            att.CheckInDate = !String.IsNullOrEmpty(att.CheckInDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(att.CheckInDateNP)) : att.CheckInDate;
            att.CheckOutDate = !String.IsNullOrEmpty(att.CheckOutDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(att.CheckOutDateNP)) : att.CheckOutDate;

            int id = ViewBag.Empcode;
            int roleid = ViewBag.EmpRoleId;
            ViewBag.EmployeeList = _employeeServices.GetEmployeeList(id);
            EmployeeDetailsViewModel emp = _employeeServices.GetEmployeeDetails(id);
            ViewBag.Approver = _employeeServices.GetAttendanceApprover(id);
            ViewBag.Recommande = _employeeServices.GetAttendanceRecommander(id);
            if (att.CheckInTime != null) {
                att.CheckInDate = att.CheckInDate + att.CheckInTime;
            }
            if (att.CheckOutTime != null) {
                att.CheckOutDate = att.CheckOutDate + att.CheckOutTime;
            }
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
            att.RequestCreatedDate = DateTime.Now;
            att.RecommendStatus = 1;
            att.ApproveStatus = 1;

            att.RecommendStatus = 1;
            att.ApproveStatus = 1;

            if (ModelState.IsValid)
            {
                try
                {
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

                    if (att.RequestEmpCode > 0)
                    {
                        AttendanceRequestDTO Atdo = new AttendanceRequestDTO();
                        Atdo = _attendanceDailyServices.InsertAttendanceRequest(att);
                        Session["attendance_request"] = "New attendance requested sucessfully !!!";

                        #region
                        // email sender test
                        string emailType = "Email";
                        EmailServices.Message msg = new EmailServices.Message();
                        htmlReader reader = new htmlReader();
                        List<string> recipient = new List<string>()
                            {


                               "youngmindsshree@gmail.com"

                            };
                        msg.Subject = "Attendance Requested for " + att.RequestType + "on  " + att.RequestedDate;
                        EmailTemplateModel emailTemplateData = new EmailTemplateModel()
                        {
                            UserName = att.ApproverEmpCode.ToString(),
                            Descriptions = "Password Changed",
                            FullName = "Young Mind Creation",
                            Title = "Password Changed",
                            Url = ""
                        };
                        msg.Body = reader.GetHtmlBodyTemplate(emailTemplateData, emailType);
                        EmailServices.Notify(recipient, msg);

                        #endregion
                        TempData["Success"] = "Attendance Request applied successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("EmployeeCode", "Please select Employee from dropdown list !!");
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Error While Requesting Attendance Online!" + ex.Message;
                    return View();
                }
            }

            else
            {
                ViewBag.Error = "Please fill required field below ";
                return View();
            }
        }

    }
}