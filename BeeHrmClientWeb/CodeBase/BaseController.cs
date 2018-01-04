using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using BeeHRM.Repository;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using BeeHrmClientWeb.Models;
using EvaluationApi.CrossCutting.Infrastructure.Email;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.CodeBase
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.Required)]

    public abstract class BaseController : Controller
    {
        //private ModelFactory _modelFactory;
        private ApplicationUserManager _AppUserManager = null;
        private ApplicationRoleManager _AppRoleManager = null;
        private IEmployeeService _employeeService;
        private ILoginService _loginService;
        public IModulServices _moduleService;
        private IUserRoleAccessService _userRoleAccessService;
        private IEnumerable<ModuleDTOs> _topmenu;
        private EmployeeDTO loginData;
        private INotification _notifications;
        private IRolesService _role;
        private readonly IUnitOfWork _unitOfWork;


        protected ApplicationUserManager AppUserManager
        {
            get
            {
                return _AppUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        string baseUrl = ConfigurationManager.AppSettings["BaseUrl"];

        protected ApplicationRoleManager AppRoleManager
        {
            get
            {
                return _AppRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }
        ITenantService _tenantService;
        public BaseController(IUnitOfWork unitOfWork = null)
        {
            _employeeService = new EmployeeService();
            _loginService = new LoginService();
            _moduleService = new ModuleService();
            _userRoleAccessService = new UserRoleAccessService();
            _notifications = new NotificationService();
            _role = new RolesService();
            _unitOfWork = unitOfWork ?? new UnitOfWork();
            var uow = new UnitOfWork();

        }

        protected override void Initialize(RequestContext requestContext)
        {
            try
            {
                base.Initialize(requestContext);

                ViewBag.Empcode = this.Session["EmpCode"];
                ViewBag.EmpRoleId = this.Session["RoleId"];
            }
            catch
            {
                throw new Exception("SOme Thing Went Wrong...");
            }

        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["EmpCode"] == null && filterContext.HttpContext.Session["UserName"] == null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.StatusCode = 403;
                    filterContext.Result = new JsonResult { Data = "LogOut", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else
                    filterContext.Result = new RedirectResult("~/Account/Login");
            }

        }


        //public string GetLoginInfo(string ForecController=null)
        //    {
        //        string controllerName = null;
        //        if (ForecController == null)

        //            controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
        //        else
        //            controllerName = ForecController;
        //        int parentId = _moduleService.GetParentId(controllerName);
        //        var empcode = ViewBag.Empcode;
        //        var roleId = ViewBag.EmpRoleId;
        //        int role = roleId != null ? Convert.ToInt32(roleId) : 0;
        //        int emcode = empcode !=null ? Convert.ToInt32(empcode):0;

        //        var roleInformation = _userRoleAccessService.GetRoleAccessData(role).ToList();
        //        try
        //        {
        //           List<ModuleDTOs> mdl = new List<ModuleDTOs>();
        //            ModuleModules md = new ModuleModules();
        //            List<RoleAccessDTOs> acc = new List<RoleAccessDTOs>();
        //            RoleAccessModel accModel = new RoleAccessModel();
        //            List<ParentModuleModels> parent = new List<ParentModuleModels>();
        //            List<ParentModule> parentModelDatas = new List<ParentModule>();
        //            List<ModuleDTOsForparent> pmt = new List<ModuleDTOsForparent>();
        //            if (roleInformation.Count >= 1)
        //            {
        //                foreach (var item in roleInformation)
        //                {
        //                    var moduleaccess1 = _moduleService.GetModuleParents(item.ModuleData.ModuleId).ToList();
        //                    var moduleaccess = moduleaccess1.Where(x => x.ModuleParentId == parentId).ToList();
        //                    if (moduleaccess != null && moduleaccess.Count >= 1)
        //                    {
        //                        foreach (var item1 in moduleaccess)
        //                        {
        //                            md.MduleLink = item1.MduleLink;
        //                            md.ModuleCssClass = item1.ModuleCssClass;
        //                            md.ModuleId = item1.ModuleId;
        //                            md.MOduleName = item1.MOduleName;
        //                            md.ModuleParentId = item1.ModuleParentId;
        //                        }
        //                        var da = formatter.ConvertModuleDataFromDTO(md);
        //                        mdl.Add(da);
        //                    }
        //                }
        //                ViewBag.TopMenuList = _moduleService.GetTopLevelModules(role);
        //                ViewBag.SideBar = mdl;
        //                int id = ViewBag.EmpCode != null ? ViewBag.EmpCode : 0;
        //                ViewBag.res = _notifications.Notificationlist(id);
        //            }
        //            else
        //            {
        //                var getRoleId = _unitOfWork.RoleRepository.Get(x => x.RoleId == role).SingleOrDefault();
        //                if (getRoleId != null)
        //                {
        //                    pmt = _moduleService.GetDefaultParentMenu();
        //                    foreach (var item in pmt)
        //                    {
        //                        var moduleaccess = _moduleService.GetDefaultMenu(item.ModuleParentId).ToList();
        //                        foreach (var item1 in moduleaccess)
        //                        {
        //                            md.MduleLink = item1.MduleLink;
        //                            md.ModuleCssClass = item1.ModuleCssClass;
        //                            md.ModuleId = item1.ModuleId;
        //                            md.MOduleName = item1.MOduleName;
        //                            md.ModuleParentId = item1.ModuleParentId;
        //                            var da = formatter.ConvertModuleDataFromDTO(md);
        //                            mdl.Add(da);
        //                        }
        //                    }
        //                }
        //                ViewBag.TopMenuList = _moduleService.GetTopLevelModules(role);
        //                ViewBag.SideBar = mdl;
        //                int id = ViewBag.EmpCode != null ? ViewBag.EmpCode : 0;
        //                ViewBag.res = _notifications.Notificationlist(id);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex ;
        //        }
        //        return null ;
        //    }
        public string GetLoginInfo(string ForecController = null)
        {
            string controllerName = null;
            if (ForecController == null)
                controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            else
                controllerName = ForecController;
            int parentId = _moduleService.GetParentId(controllerName);
            var empcode = ViewBag.Empcode;
            var roleId = ViewBag.EmpRoleId;
            int role = roleId != null ? Convert.ToInt32(roleId) : 0;
            int emcode = empcode != null ? Convert.ToInt32(empcode) : 0;
            var roleInformation = _userRoleAccessService.GetRoleAccessData(role).ToList();
            try
            {
                List<ModuleDTOs> mdl = new List<ModuleDTOs>();
                Module mod = _moduleService.GetModuleByController(parentId);

                ModuleModules md = new ModuleModules();
                List<RoleAccessDTOs> acc = new List<RoleAccessDTOs>();
                RoleAccessModel accModel = new RoleAccessModel();
                //List<ParentModuleModels> parent = new List<ParentModuleModels>();
                //List<ParentModule> parentModelDatas = new List<ParentModule>();
                List<ModuleDTOsForparent> pmt = new List<ModuleDTOsForparent>();
                if (roleInformation.Count >= 1)
                {
                    foreach (var item in roleInformation)
                    {
                        var moduleaccess1 = _moduleService.GetModuleParents(item.ModuleData.ModuleId).ToList();
                        var moduleaccess = moduleaccess1.Where(x => x.ModuleParentId == parentId).OrderBy(x => x.Order).ToList();
                        if (moduleaccess != null && moduleaccess.Count >= 1)
                        {
                            foreach (var item1 in moduleaccess)
                            {
                                md.MduleLink = item1.MduleLink;
                                md.ModuleCssClass = item1.ModuleCssClass;
                                md.ModuleId = item1.ModuleId;
                                md.MOduleName = item1.MOduleName;
                                md.ModuleParentId = item1.ModuleParentId;
                            }
                            var da = formatter.ConvertModuleDataFromDTO(md);
                            mdl.Add(da);
                        }
                    }
                    if (mod.IsDefault)
                    {
                        var moduleaccess2 = _moduleService.GetDefaultMenu();

                        if (moduleaccess2 != null)
                        {
                            foreach (var item1 in moduleaccess2)
                            {
                                md.MduleLink = item1.MduleLink;
                                md.ModuleCssClass = item1.ModuleCssClass;
                                md.ModuleId = item1.ModuleId;
                                md.MOduleName = item1.MOduleName;
                                md.ModuleParentId = item1.ModuleParentId;
                                var da = formatter.ConvertModuleDataFromDTO(md);
                                mdl.Add(da);
                            }

                        }
                    }
                    else
                    {

                    }



                    ViewBag.TopMenuList = _moduleService.GetTopLevelModules(role);
                    ViewBag.SideBar = mdl;
                    int id = ViewBag.EmpCode != null ? ViewBag.EmpCode : 0;
                    ViewBag.res = _notifications.Notificationlist(id);
                }
                else
                {
                    var getRoleId = _unitOfWork.RoleRepository.Get(x => x.RoleId == role).SingleOrDefault();
                    if (getRoleId != null)
                    {
                        var moduleaccess2 = _moduleService.GetDefaultMenu();

                        if (moduleaccess2 != null)
                        {
                            foreach (var item1 in moduleaccess2)
                            {
                                md.MduleLink = item1.MduleLink;
                                md.ModuleCssClass = item1.ModuleCssClass;
                                md.ModuleId = item1.ModuleId;
                                md.MOduleName = item1.MOduleName;
                                md.ModuleParentId = item1.ModuleParentId;
                                var da = formatter.ConvertModuleDataFromDTO(md);
                                mdl.Add(da);
                            }

                        }
                    }
                    ViewBag.TopMenuList = _moduleService.GetTopLevelModules(role);
                    ViewBag.SideBar = mdl;
                    int id = ViewBag.EmpCode != null ? ViewBag.EmpCode : 0;
                    ViewBag.res = _notifications.Notificationlist(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        /// <summary>
        /// Get user access user edit create view and delete access
        /// </summary>?
        /// <returns>viewbagdata</returns>
        public void GetUserAccess(string CustomCtrolller = null)
        {
            string controllerName = null;
            if (CustomCtrolller == null)
                controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            else
                controllerName = CustomCtrolller;

            bool? AllowView = false;
            bool? AllowCreate = false;
            bool? AllowEdit = false;
            bool? AllowDelete = false;
            ViewBag.AllowView = false;
            ViewBag.AllowCreate = false;
            ViewBag.AllowEdit = false;
            ViewBag.AllowDelete = false;
            var roleId = ViewBag.EmpRoleId;
            RoleAccessDTOs roleAccessData = new RoleAccessDTOs();
            int role = roleId != null ? Convert.ToInt32(roleId) : 0;
            IEnumerable<ModuleDTOs> moduleData = _moduleService.GetModuleIdByModuleName(controllerName);
            int moduleDataCount = moduleData.Count();
            if (moduleDataCount >= 1)
            {

                var mod = moduleData.Where(x => x.Controller == controllerName.ToLower()).FirstOrDefault();
                int moduleId = mod.ModuleId;
                int parentId = mod.ModuleParentId;
                roleAccessData = _userRoleAccessService.GetRoleAccessUserAccesData(moduleId, role);
                if (roleAccessData != null)
                {
                    AllowView = roleAccessData.AllowView;
                    AllowCreate = roleAccessData.AllowAdd;
                    AllowEdit = roleAccessData.AllowEdit;
                    AllowDelete = roleAccessData.AllowDelete;
                }
                ViewBag.AllowView = AllowView;
                ViewBag.AllowCreate = AllowCreate;
                ViewBag.AllowEdit = AllowEdit;
                ViewBag.AllowDelete = AllowDelete;

            }

        }


        public ActionResult logincheck()
        {
            if (this.Session["UserName"] == null)
            {
                return RedirectToAction("index", "Login");
            }
            return View();
        }
        //protected ModelFactory TheModelFactory
        //{
        //    get
        //    {
        //        if (_modelFactory == null)
        //        {
        //            _modelFactory = new ModelFactory(this.Request, this.AppUserManager);
        //        }
        //        return _modelFactory;
        //    }
        //}

        public string HostIPAddress
        {
            get
            {
                return System.Web.HttpContext.Current.Request.UserHostAddress;
            }
        }
        public string TenantDomain
        {
            get
            {
                return System.Web.HttpContext.Current.Request.Url.Host;

            }
        }
        private TenantDTO _tenant;
        public TenantDTO Tenant
        {
            get
            {


                return _tenant ?? (_tenant = _tenantService.GetTenant(TenantDomain));
            }
        }
        public int TenantID
        {
            get
            {
                return Tenant.TenantID;
            }
        }
        public string TenantName
        {
            get
            {
                return Tenant.Name;
            }
        }
        public string TenantLogo
        {
            get
            {
                return Tenant.Logo;
            }
        }
        public string TenantCulture
        {
            get
            {
                return Tenant.Culture;
            }
        }
        public string TenantCultureCode
        {
            get
            {
                return Tenant.CultureCode;
            }
        }
        public int ApplicationID
        {
            get
            {
                if (User.Identity == null)
                {
                    throw new ArgumentNullException("Identity Is Null");
                }
                return _tenantService.GetApplicationId(User.Identity.Name);
            }
        }



        public int NotificationRequestAttendance(AttendanceRequestDTO atd)
        {
            EmployeeDetailsViewModel user = _employeeService.GetEmployeeDetails(atd.RequestEmpCode);
            EmployeeDetailsViewModel recomend = _employeeService.GetEmployeeDetails(Convert.ToInt32(atd.RecommendarEmpCode));


            #region Notification
            NotificationsDTOs ntd = new NotificationsDTOs();
            ntd.NotificationDate = DateTime.Now;
            ntd.NotificationReceiverId = Convert.ToInt32(atd.RecommendarEmpCode);
            ntd.NotificationSubject = "Recommender for Attendance request";
            ntd.NotificationReceiverType = "E";
            ntd.NotificationMessage = user.Name + " Selected you as a Recommender. Please confirm  the request";
            ntd.NotificationDetailURL = "/user/attendancerequest/recommendlist";
            ntd.NotificationReadDate = null;
            _notifications.InsertNotification(ntd);
            #endregion
            #region Email

            string emailType = "Email";
            EmailServices.Message msg = new EmailServices.Message();
            htmlReader reader = new htmlReader();
            List<string> recipient = new List<string>()
                            {
                               recomend.Email

                            };
            msg.Subject = "Attendnace request Recommend";
            EmailTemplateModel emailTemplateData = new EmailTemplateModel()
            {
                UserName = recomend.Name,
                Descriptions = user.Name + " has requested attendance for " + atd.RequestType + " on  " + atd.RequestedDate + ". Please visit the link below and confirm the request ",
                FullName = user.Name,
                Title = "Attendance Request for recommendation",
                Url = baseUrl + "/user/attendancerequest/recommendlist"
            };
            msg.Body = reader.GetHtmlBodyTemplate(emailTemplateData, emailType);
            EmailServices.Notify(recipient, msg);

            #endregion
            return 0;
        }

        public int NotificationRecommendAttendance(AttendanceRequestDTO atd)
        {
            EmployeeDetailsViewModel user = _employeeService.GetEmployeeDetails(atd.RequestEmpCode);
            EmployeeDetailsViewModel recomend = _employeeService.GetEmployeeDetails(Convert.ToInt32(atd.RecommendarEmpCode));
            EmployeeDetailsViewModel approver = _employeeService.GetEmployeeDetails(Convert.ToInt32(atd.ApproverEmpCode));

            if (atd.RecommendStatus == 2)
            {

                #region NotificationUser
                NotificationsDTOs ntd = new NotificationsDTOs();
                ntd.NotificationDate = DateTime.Now;
                ntd.NotificationReceiverId = Convert.ToInt32(atd.RequestEmpCode);
                ntd.NotificationSubject = "Recommender approve your Attendnace";
                ntd.NotificationMessage = "Your attendnace request on " + atd.RequestedDate + " is Recommended by " + recomend.Name;
                ntd.NotificationReceiverType = "E";
                ntd.NotificationDetailURL = "/user/attendancerequest/AttendanceDetail/" + atd.RequestId;
                ntd.NotificationReadDate = null;
                _notifications.InsertNotification(ntd);
                #endregion

                #region Notificationapprover
                NotificationsDTOs ntf = new NotificationsDTOs();
                ntf.NotificationDate = DateTime.Now;
                ntf.NotificationReceiverId = approver.Code;
                ntf.NotificationSubject = "Attendnace Requested to approve";
                ntf.NotificationMessage = user.Name + " Slected you as Approver as this request is already been recommened by " + recomend.Name + ".Please confirm the request ";
                ntf.NotificationReceiverType = "E";
                ntf.NotificationDetailURL = "/user/attendancerequest/recommend/" + atd.RequestId;
                ntf.NotificationReadDate = null;
                _notifications.InsertNotification(ntf);
                #endregion

                #region Emailapprover

                string emailTypeapprover = "Email";
                EmailServices.Message mgs_r = new EmailServices.Message();
                htmlReader reader_r = new htmlReader();
                List<string> receiver = new List<string>()
                            {
                               approver.Email

                            };

                mgs_r.Subject = "Attendnace request for Approve ";
                EmailTemplateModel emailTemplateData = new EmailTemplateModel()
                {
                    UserName = approver.Name,
                    Descriptions = user.Name + "  has request attendance for" + atd.RequestType + " on " + atd.RequestedDate + ". Please confirm the request as this request is already been  recommended by " + recomend.Name + " on " + DateTime.Now + " Please follow the link below for more detail.",
                    FullName = recomend.Name,
                    Title = "Attendance Request for approve",
                    Url = baseUrl + "/user/attendancerequest/approved/" + atd.RequestId.ToString()
                };
                mgs_r.Body = reader_r.GetHtmlBodyTemplate(emailTemplateData, emailTypeapprover);
                EmailServices.Notify(receiver, mgs_r);

                #endregion
            }
            else
            if (atd.RecommendStatus == 3)
            {

                #region NotificationUser
                NotificationsDTOs ntd = new NotificationsDTOs();
                ntd.NotificationDate = DateTime.Now;
                ntd.NotificationReceiverId = Convert.ToInt32(user.Code);
                ntd.NotificationSubject = "Attendance request rejected by " + recomend.Name;
                ntd.NotificationMessage = "Sorry!! Your attendnace request on " + atd.RequestedDate + " is rejected by " + recomend.Name;
                ntd.NotificationReceiverType = "E";
                ntd.NotificationDetailURL = "/user/attendancerequest/AttendanceDetail/" + atd.RequestId;
                ntd.NotificationReadDate = null;
                _notifications.InsertNotification(ntd);
                #endregion

            }

            #region Emailuser

            string emailType = "Email";
            EmailServices.Message msg = new EmailServices.Message();
            htmlReader reader = new htmlReader();
            List<string> recipient = new List<string>()
                            {
                               user.Email

                            };
            if (atd.RecommendStatus == 2)
            {
                msg.Subject = "Recommender for Attendnace request";
                EmailTemplateModel emailTemplateData = new EmailTemplateModel()
                {
                    UserName = user.Name,
                    Descriptions = "Attendance you requested on " + atd.RequestedDate + " for " + atd.RequestType + " has been accepted by " + recomend.Name + " on " + DateTime.Now + ". Please visit the link below for details",
                    FullName = recomend.Name,
                    Title = "Recommend Attendance Request",
                    Url = baseUrl + "user/attendancerequest/AttendanceDetail/" + atd.RequestId.ToString()
                };
                msg.Body = reader.GetHtmlBodyTemplate(emailTemplateData, emailType);
                EmailServices.Notify(recipient, msg);
            }
            else if (atd.RecommendStatus == 3)
            {
                msg.Subject = "Attendance request rejected by Recommender";
                EmailTemplateModel emailTemplateData = new EmailTemplateModel()
                {
                    UserName = user.Name,
                    Descriptions = "Attendance you requested on " + atd.RequestedDate + " for " + atd.RequestType + " has been rejected by " + recomend.Name + " on " + DateTime.Now + ". Please visit the link below for details",
                    FullName = recomend.Name,
                    Title = "Recommend Attendance Request",
                    Url = baseUrl + "user/attendancerequest/AttendanceDetail/" + atd.RequestId.ToString()
                };
                msg.Body = reader.GetHtmlBodyTemplate(emailTemplateData, emailType);
                EmailServices.Notify(recipient, msg);
            }
            #endregion








            return 0;
        }

        public int NotificationApproveAttendance(AttendanceRequestDTO atd)
        {
            EmployeeDetailsViewModel user = _employeeService.GetEmployeeDetails(atd.RequestEmpCode);
            EmployeeDetailsViewModel recomend = _employeeService.GetEmployeeDetails(Convert.ToInt32(atd.RecommendarEmpCode));
            EmployeeDetailsViewModel approver = _employeeService.GetEmployeeDetails(Convert.ToInt32(atd.ApproverEmpCode));


            if (atd.ApproveStatus == 2)
            {

                #region NotificationUser
                NotificationsDTOs ntd = new NotificationsDTOs();
                ntd.NotificationDate = DateTime.Now;
                ntd.NotificationReceiverId = Convert.ToInt32(user.Code);
                ntd.NotificationSubject = "Approver approve your Attendnace";
                ntd.NotificationMessage = "Attendance you requested on " + atd.RequestedDate + " is Approved by " + approver.Name;
                ntd.NotificationReceiverType = "E";
                ntd.NotificationDetailURL = "/user/attendancerequest/AttendanceDetail/" + atd.RequestId;
                ntd.NotificationReadDate = null;
                _notifications.InsertNotification(ntd);
                #endregion
                #region Notificationrecommender
                NotificationsDTOs ntf = new NotificationsDTOs();
                ntf.NotificationDate = DateTime.Now;
                ntf.NotificationReceiverId = recomend.Code;
                ntf.NotificationSubject = "Approved attendance request";
                ntf.NotificationMessage = "Attendance requested by" + user.Name + " on " + atd.RequestedDate + "is aproved by" + approver.Name;
                ntf.NotificationReceiverType = "E";
                ntf.NotificationDetailURL = "/user/attendancerequest/recommend/" + atd.RequestId;
                ntf.NotificationReadDate = null;
                _notifications.InsertNotification(ntf);
                #endregion
            }
            else
                if (atd.ApproveStatus == 3)
            {

                #region NotificationUser
                NotificationsDTOs ntd = new NotificationsDTOs();
                ntd.NotificationDate = DateTime.Now;
                ntd.NotificationReceiverId = Convert.ToInt32(user.Code);
                ntd.NotificationSubject = "Approver reject your Attendnace request";
                ntd.NotificationMessage = "Attendance requested by" + user.Name + " on " + atd.RequestedDate + "is rejected by" + approver.Name;
                ntd.NotificationReceiverType = "E";
                ntd.NotificationDetailURL = "/user/attendancerequest/AttendanceDetail/" + atd.RequestId;
                ntd.NotificationReadDate = null;
                _notifications.InsertNotification(ntd);
                #endregion
                #region Notificationrecommender
                NotificationsDTOs ntf = new NotificationsDTOs();
                ntf.NotificationDate = DateTime.Now;
                ntf.NotificationReceiverId = recomend.Code;
                ntf.NotificationSubject = "Approver Rejected attendance request of " + user.Name;
                ntf.NotificationMessage = "Attendance requested for " + atd.RequestType + "requested on " + atd.RequestedDate + "  is rejected by" + approver.Name;
                ntf.NotificationReceiverType = "E";
                ntf.NotificationDetailURL = "/user/attendancerequest/recommend/" + atd.RequestId;
                ntf.NotificationReadDate = null;
                _notifications.InsertNotification(ntf);
                #endregion
            }

            #region Emailuser

            string emailType = "Email";
            EmailServices.Message msg = new EmailServices.Message();
            htmlReader reader = new htmlReader();
            List<string> recipient = new List<string>()
                            {user.Email

                            };
            if (atd.ApproveStatus == 2)
            {
                msg.Subject = "Approver confirm  Attendnace request";
                EmailTemplateModel emailTemplateData = new EmailTemplateModel()
                {
                    UserName = user.Name,
                    Descriptions = "Attendance requested on " + atd.RequestedDate + " for " + atd.RequestType + " is approved  by " + approver.Name + " for more detail follow the link below",
                    FullName = recomend.Name,
                    Title = "Approver confirm  Attendnace request for " + atd.RequestType,
                    Url = baseUrl + "user/attendancerequest/AttendanceDetail/" + atd.RequestId.ToString()
                };
                msg.Body = reader.GetHtmlBodyTemplate(emailTemplateData, emailType);
                EmailServices.Notify(recipient, msg);
            }
            else if (atd.ApproveStatus == 3)
            {
                msg.Subject = "Approver Rejected   Attendnace request";
                EmailTemplateModel emailTemplateData = new EmailTemplateModel()
                {
                    UserName = user.Name,
                    Descriptions = "Attendance requested on " + atd.RequestedDate + " for " + atd.RequestType + " is rejected  by " + approver.Name + " for more detail follow the link below",
                    FullName = recomend.Name,
                    Title = "Rejected Attendnace request for " + atd.RequestType,
                    Url = baseUrl + "/user/attendancerequest/AttendanceDetail/" + atd.RequestId.ToString()
                };
                msg.Body = reader.GetHtmlBodyTemplate(emailTemplateData, emailType);
                EmailServices.Notify(recipient, msg);
            }
            #endregion
            #region Emailapprover

            string emailTypeapprover = "Email";
            EmailServices.Message msgapprover = new EmailServices.Message();
            htmlReader readerapprover = new htmlReader();
            List<string> listapprover = new List<string>()
                            {
                               recomend.Email

                            };
            if (atd.RecommendStatus == 2)
            {
                msg.Subject = "Attendnace request approved of " + user.Name;
                EmailTemplateModel emailTemplateData = new EmailTemplateModel()
                {
                    UserName = user.Name,
                    Descriptions = "Attendnace you recommended on " + atd.RecommendStatusDate + " of " + user.Name + " is aproved  by " + approver.Name + " on " + DateTime.Now + ". Please visit link below for details",
                    FullName = recomend.Name,
                    Title = "Attendnace request of " + user.Name + " for " + atd.RequestType + " is approved ",
                    Url = baseUrl + "/user/attendancerequest/recommend/" + atd.RequestId.ToString()
                };
                msg.Body = reader.GetHtmlBodyTemplate(emailTemplateData, emailTypeapprover);
                EmailServices.Notify(listapprover, msgapprover);
            }
            else if (atd.RecommendStatus == 3)
            {
                msg.Subject = " Attendnace request rejected of " + user.Name;
                EmailTemplateModel emailTemplateData = new EmailTemplateModel()
                {
                    UserName = recomend.Name,
                    Descriptions = "Attendnace you recommended on " + atd.RecommendStatusDate + " of " + user.Name + " is rejected  by " + approver.Name + " on " + DateTime.Now + ". Please visit link below for details",
                    FullName = recomend.Name,
                    Title = "Attendnace request of " + user.Name + " for " + atd.RequestType + " is approved ",
                    Url = baseUrl + "/user/attendancerequest/recommend/" + atd.RequestId.ToString()
                };
                msg.Body = reader.GetHtmlBodyTemplate(emailTemplateData, emailTypeapprover);
                EmailServices.Notify(listapprover, msgapprover);
            }



            #endregion






            return 0;
        }

        public int NotificationLeaveRequestRecommend(LeaveApplicationViewModel Lat)
        {
            EmployeeDetailsViewModel user = _employeeService.GetEmployeeDetails(Lat.LeaveEmpCode);
            EmployeeDetailsViewModel recomend = _employeeService.GetEmployeeDetails(Convert.ToInt32(Lat.LeaveRecommenderEmpcode));
            EmployeeDetailsViewModel approver = _employeeService.GetEmployeeDetails(Convert.ToInt32(Lat.LeaveApproverEmpcode));

            if (Lat.RecommendStatus == 2)
            {

                #region NotificationUser
                NotificationsDTOs ntd = new NotificationsDTOs();
                ntd.NotificationDate = DateTime.Now;
                ntd.NotificationReceiverId = Convert.ToInt32(Lat.LeaveEmpCode);
                ntd.NotificationSubject = "Recommender approve your Leave Request";
                ntd.NotificationMessage = "Your " + Lat.LeaveTypeName + " requested on " + Lat.LeaveAppliedDate.ToString("yyyy,MMM dd") + " is Recommended by " + recomend.Name;
                ntd.NotificationReceiverType = "E";
                ntd.NotificationDetailURL = "/user/leave/detail/" + Lat.LeaveId;
                ntd.NotificationReadDate = null;
                _notifications.InsertNotification(ntd);
                #endregion

                #region Notificationapprover
                NotificationsDTOs ntf = new NotificationsDTOs();
                ntf.NotificationDate = DateTime.Now;
                ntf.NotificationReceiverId = approver.Code;
                ntf.NotificationSubject = "Leave  Requested to approve";
                ntf.NotificationMessage = user.Name + " Slected you as Approver as this leave request is already been recommened by " + recomend.Name + ".Please confirm the request ";
                ntf.NotificationReceiverType = "E";
                ntf.NotificationDetailURL = "/user/leaveapplication/approve/" + Lat.LeaveId;
                ntf.NotificationReadDate = null;
                _notifications.InsertNotification(ntf);
                #endregion


            }
            else
            if (Lat.RecommendStatus == 3)
            {

                #region NotificationUser
                NotificationsDTOs ntd = new NotificationsDTOs();
                ntd.NotificationDate = DateTime.Now;
                ntd.NotificationReceiverId = Convert.ToInt32(user.Code);
                ntd.NotificationSubject = Lat.LeaveTypeName + " request rejected by " + recomend.Name;
                ntd.NotificationMessage = "Sorry!! Your " + Lat.LeaveTypeName + " applied on " + Lat.LeaveAppliedDate.ToString("yyyy,MMM dd") + " is rejected by " + recomend.Name;
                ntd.NotificationReceiverType = "E";
                ntd.NotificationDetailURL = "/user/leave/detail/" + Lat.LeaveId;
                ntd.NotificationReadDate = null;
                _notifications.InsertNotification(ntd);
                #endregion

            }

            return 0;
        }

        public int NoftificationLeaveRequestApprove(LeaveApplicationViewModel lat)
        {
            EmployeeDetailsViewModel user = _employeeService.GetEmployeeDetails(lat.LeaveEmpCode);
            EmployeeDetailsViewModel recomend = _employeeService.GetEmployeeDetails(Convert.ToInt32(lat.LeaveRecommenderEmpcode));
            EmployeeDetailsViewModel approver = _employeeService.GetEmployeeDetails(Convert.ToInt32(lat.LeaveApproverEmpcode));


            if (lat.LeaveStatus == 2)
            {

                #region NotificationUser
                NotificationsDTOs ntd = new NotificationsDTOs();
                ntd.NotificationDate = DateTime.Now;
                ntd.NotificationReceiverId = Convert.ToInt32(user.Code);
                ntd.NotificationSubject = lat.LeaveTypeName + " Request has been approved";
                ntd.NotificationMessage = lat.LeaveTypeName + " requested on " + lat.LeaveAppliedDate.ToString("yyyy,MM dd") + " is Approved by " + approver.Name;
                ntd.NotificationReceiverType = "E";
                ntd.NotificationDetailURL = "/user/leave/detail/" + lat.LeaveId;
                ntd.NotificationReadDate = null;
                _notifications.InsertNotification(ntd);
                #endregion
                #region Notificationrecommender
                NotificationsDTOs ntf = new NotificationsDTOs();
                ntf.NotificationDate = DateTime.Now;
                ntf.NotificationReceiverId = recomend.Code;
                ntf.NotificationSubject = "Recommended Leave request has been approved ";
                ntf.NotificationMessage = lat.LeaveTypeName + " requested by" + user.Name + " on " + lat.LeaveAppliedDate.ToString("yyyy-MMM-dd") + " is aproved by" + approver.Name;
                ntf.NotificationReceiverType = "E";
                ntf.NotificationDetailURL = "/user/leaveapplication/recommend/" + lat.LeaveId;
                ntf.NotificationReadDate = null;
                _notifications.InsertNotification(ntf);
                #endregion
            }
            else
                if (lat.LeaveStatus == 3)
            {

                #region NotificationUser
                NotificationsDTOs ntd = new NotificationsDTOs();
                ntd.NotificationDate = DateTime.Now;
                ntd.NotificationReceiverId = Convert.ToInt32(user.Code);
                ntd.NotificationSubject = "Approver reject your " + lat.LeaveTypeName + "  request";
                ntd.NotificationMessage = lat.LeaveTypeName + " requested on " + lat.LeaveAppliedDate.ToString("yyyy,MM dd") + " is Rejected by " + approver.Name;
                ntd.NotificationReceiverType = "E";
                ntd.NotificationDetailURL = "/user/leave/detail/" + lat.LeaveId;
                ntd.NotificationReadDate = null;
                _notifications.InsertNotification(ntd);
                #endregion
                #region Notificationrecommender
                NotificationsDTOs ntf = new NotificationsDTOs();
                ntf.NotificationDate = DateTime.Now;
                ntf.NotificationReceiverId = recomend.Code;
                ntf.NotificationSubject = "Recommended Leave request has been Rejected ";
                ntf.NotificationMessage = lat.LeaveTypeName + "  Request of " + user.Name + " requested on " + lat.LeaveAppliedDate.ToString("yyyy,MM dd") + "  is rejected by" + approver.Name;
                ntf.NotificationReceiverType = "E";
                ntf.NotificationDetailURL = "/user/leave/detail/" + lat.LeaveId;
                ntf.NotificationReadDate = null;
                _notifications.InsertNotification(ntf);
                #endregion
            }


            return 0;

        }

        public string PostSendSMS(SMS sms)
        {
            using (var client = new WebClient())
            {
                string parameters = "?";
                parameters += "from=" + sms.From;
                parameters += "&to=" + sms.To;
                parameters += "&text=" + sms.MgsBody;
                parameters += "&token=" + sms.Token;
                string url = "http://api.sparrowsms.com/v2/sms/" + parameters;
                var responseString = client.DownloadString(url);
                return responseString;
            }

        }

    }
}