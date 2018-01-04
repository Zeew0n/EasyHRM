using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using BeeHrmClientWeb.CodeBase;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Employee
{
    public class EmployeeDetailsController : BaseController
    {
        // GET: HR
        private IModulServices _moduleService;
        private IEmployeeService _employeeService;
        private IEmployeePrizeService _empPrizeService;
        private IJobHistoryService _jobhistory;

        public EmployeeDetailsController()
        {
            _moduleService = new ModuleService();
            _employeeService = new EmployeeService();
            _empPrizeService = new EmployeePrizeService();
            _jobhistory = new JobHistoryService();


        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();

        }
        [Route("admin/userDetail/{Empcode}")]
        public ActionResult Index(int Empcode)
        {
            int LoginEmpCode = Convert.ToInt32(Session["EmpCode"]);

            int isAccessAllow = _employeeService.GetIsProfileViewable(Empcode, LoginEmpCode);
            if (isAccessAllow == 0)
            {
                ViewBag.Error = "You do not have permission to view profile of employee code : " + Empcode;
                return PartialView("_partialviewNotFound");

            }
            try
            {
                ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(Empcode);
                EmployeeDetailsViewModel reEmp = _employeeService.GetEmployeeDetails(Empcode);
                EmployeeEditViewModel Details = _employeeService.GetEmployeeByID(Empcode);
                IEnumerable<EmployeeFamilyViewModel> Familydetails = _employeeService.GetEmployeeFamilyByID(Empcode);
                IEnumerable<EmployeePrizeDTO> res = _empPrizeService.GetAllPrizeOfEmployee(Empcode);
                IEnumerable<EmployeeSkillViewModel> skill = _employeeService.GetEmployeeSkillsByID(Empcode);
                IEnumerable<EmployeeDocumentViewModel> document = _employeeService.GetEmployeeDocumentsByID(Empcode);
                EmployeeDetailAdminViewModel EmpDetails = new EmployeeDetailAdminViewModel();


                EmpDetails.EmpDetails = reEmp;
                EmpDetails.OtherDetails = Details;
                EmpDetails.Familydetails = Familydetails;
                EmpDetails.Prize = res;
                EmpDetails.Skill = skill;
                EmpDetails.Documents = document;

                //get current history id
                EmployeeJobHistoryDTO jobhistoryCurrent = new EmployeeJobHistoryDTO();
                EmployeeJobHistoryDTO jobhistoryAppoint = new EmployeeJobHistoryDTO();
                int currentHistoryId = _jobhistory.GetJobHistoryOfEmployeeWIthCondition(Empcode, "current");
                if (currentHistoryId > 0)
                {
                    jobhistoryCurrent = _jobhistory.GetJobHistoryById(currentHistoryId);
                }
                else
                {
                    jobhistoryCurrent = null;
                }
                //get appointment history id
                int appointHistoryId = _jobhistory.GetJobHistoryOfEmployeeWIthCondition(Empcode, "appoint"); ;
                if (appointHistoryId > 0)
                {
                    jobhistoryAppoint = _jobhistory.GetJobHistoryById(appointHistoryId);
                }
                else
                {
                    jobhistoryAppoint = null;
                }

                EmpDetails.EmployeeCurrentJobHistory = jobhistoryCurrent;
                EmpDetails.EmployeeAppointJobHistory = jobhistoryAppoint;


                return View(EmpDetails);
            }
            catch (Exception Exception)
            {
                throw new Exception(Exception.ToString());
            }

        }

        [Route("admin/downloadDetail/{Empcode}")]
        public ActionResult DownloadPersonalDetails(int Empcode)
        {
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(Empcode);
            EmployeeDetailsViewModel reEmp = _employeeService.GetEmployeeDetails(Empcode);
            EmployeeEditViewModel Details = _employeeService.GetEmployeeByID(Empcode);
            IEnumerable<EmployeeFamilyViewModel> Familydetails = _employeeService.GetEmployeeFamilyByID(Empcode);
            IEnumerable<EmployeePrizeDTO> res = _empPrizeService.GetAllPrizeOfEmployee(Empcode);
            IEnumerable<EmployeeSkillViewModel> skill = _employeeService.GetEmployeeSkillsByID(Empcode);
            IEnumerable<EmployeeDocumentViewModel> document = _employeeService.GetEmployeeDocumentsByID(Empcode);
            EmployeeDetailAdminViewModel EmpDetails = new EmployeeDetailAdminViewModel();
            reEmp.EmployeeAppointmentDetail = _employeeService.GetEmployeeAppointmentDetails(Empcode);
            EmpDetails.EmpDetails = reEmp;
            EmpDetails.OtherDetails = Details;
            EmpDetails.Familydetails = Familydetails;
            EmpDetails.Prize = res;
            EmpDetails.Skill = skill;
            EmpDetails.Documents = document;
            return View(EmpDetails);
        }

        [Route("profile/downloadpdf/{Empcode}")]
        public ActionResult DownloadProfilePdf(int Empcode)

        {
            Dictionary<string, string> cookieCollection = new Dictionary<string, string>();

            foreach (var key in Request.Cookies.AllKeys)
            {
                cookieCollection.Add(key, Request.Cookies.Get(key).Value);
            }

            //return new ActionAsPdf("DownloadPersonalDetails", Empcode)
            //{
            //    FileName = "EmployeeDetails.pdf",
            //    Cookies = cookieCollection
            //};

            return new Rotativa.ActionAsPdf("DownloadPersonalDetails", Empcode)
            {
                FormsAuthenticationCookieName = System.Web.Security.FormsAuthentication.FormsCookieName,
                FileName = "EmployeeDetails.pdf"

            };

        }
    }
}