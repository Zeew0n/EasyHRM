using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.User
{
    public class UserEmployeeController : BaseController
    {
        private IEmployeeService _employeeService;
        private IJobTypeService _jobTypeservices;
        private IDepartmentService _departmentServices;
        private IDesignationService _designationServices;
        private ILeaveRuleService _leaveRuleServices;
        private ILevelService _levelServices;
        private ISectionService _sectionServices;
        private IGroupService _groupServices;
        private IRankService _rankServices;
        private IShiftService _shiftService;
        private IServiceEventService _serviceEventService;
        private IOfficeServices _officeTypeServices;
        private IModulServices _moduleService;
        private IBusinessGroupService _bgGroupService;
        private IEthnicityService _ethService;
        private IReligionService _religionService;
        private IEmployeeVisitService _employeeVisitService;
        private ICountryService _countryService;
        private IRolesService _rolesService;
        private IEmployeePrizeService _empPrizeService;
        private IJobHistoryService _jobHistoryService;
        private IOfficeServices _officeService;
        private IRemoteAreaService _remoteService;
        private IEmployeeTrainingService _employeeTrainingService;
        private IServiceEventSubGroupService _serviceEventSubGroupService;
        private IEmpEducationService _empEducationService;
        private IEmpFamilyService _empFamilyService;
        private IEducationLevelService _educationLevel;


        public UserEmployeeController()
        {
            _employeeService = new EmployeeService();
            _officeTypeServices = new OfficeServices();
            _departmentServices = new DepartmentService();
            _designationServices = new DesignationService();
            _leaveRuleServices = new LeaveRuleService();
            _levelServices = new LevelService();
            _sectionServices = new SectionService();
            _employeeService = new EmployeeService();
            _jobTypeservices = new JobTypeService();
            _rankServices = new RankService();
            _shiftService = new ShiftService();
            _serviceEventService = new ServiceEventGroupService();
            _groupServices = new GroupService();
            _moduleService = new ModuleService();
            _bgGroupService = new BusinessGroupService();
            _ethService = new EthnicityService();
            _religionService = new ReligionService();
            _employeeVisitService = new EmployeeVisitService();
            _countryService = new CountryService();
            _rolesService = new RolesService();
            _empPrizeService = new EmployeePrizeService();
            _jobHistoryService = new JobHistoryService();
            _officeService = new OfficeServices();
            _remoteService = new RemoteAreaService();
            _employeeTrainingService = new EmployeeTrainingService();
            _serviceEventSubGroupService = new ServiceEventSubGroupService();
            _empEducationService = new EmpEducationService();
            _empFamilyService = new EmpFamilyService();
            _educationLevel = new EducationLevelService();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();

        }
        public void ListOfDatas(int empCode)
        {
            ViewBag.EmployeeDetail = _employeeService.GetEmployeeDetails(empCode);
            List<SelectListItem> OfficeList = new List<SelectListItem>();
            foreach (var row in _officeService.GetOfficeAllData())
            {
                OfficeList.Add(new SelectListItem
                {
                    Text = row.OfficeName,
                    Value = row.OfficeId.ToString()
                });
            }
            List<SelectListItem> DepartmentList = new List<SelectListItem>();
            foreach (var row in _departmentServices.GetDepartmentlist())
            {
                DepartmentList.Add(new SelectListItem
                {
                    Text = row.DeptName,
                    Value = row.DeptId.ToString()
                });
            }
            List<SelectListItem> SectionList = new List<SelectListItem>();
            foreach (var row in _sectionServices.GetSectionList())
            {
                SectionList.Add(new SelectListItem
                {
                    Text = row.SectionName,
                    Value = row.SectionId.ToString()
                });
            }
            List<SelectListItem> DesignationList = new List<SelectListItem>();
            foreach (var row in _designationServices.GetDesignationList())
            {
                DesignationList.Add(new SelectListItem
                {
                    Text = row.DsgName,
                    Value = row.DsgId.ToString()
                });
            }
            List<SelectListItem> RankList = new List<SelectListItem>();
            foreach (var row in _rankServices.GetRankList())
            {
                RankList.Add(new SelectListItem
                {
                    Text = row.RankName.ToString(),
                    Value = row.RankId.ToString()
                });
            }
            List<SelectListItem> LevelList = new List<SelectListItem>();
            foreach (var row in _levelServices.GetLevellist())
            {
                LevelList.Add(new SelectListItem
                {
                    Text = row.LevelName,
                    Value = row.LevelId.ToString()
                });
            }
            List<SelectListItem> BusinessGroupList = new List<SelectListItem>();
            foreach (var row in _bgGroupService.GetBusinessGroupList())
            {
                BusinessGroupList.Add(new SelectListItem
                {
                    Text = row.BgName,
                    Value = row.BgId.ToString()
                });
            }
            List<SelectListItem> ShiftList = new List<SelectListItem>();
            foreach (var row in _shiftService.GetShiftsLIst())
            {
                ShiftList.Add(new SelectListItem
                {
                    Text = row.ShiftName,
                    Value = row.ShiftId.ToString()
                });
            }
            List<SelectListItem> RemoteList = new List<SelectListItem>();
            foreach (var row in _remoteService.GetRemoteList())
            {
                RemoteList.Add(new SelectListItem
                {
                    Text = row.RemoteAreaName,
                    Value = row.RemoteId.ToString()
                });
            }
            List<SelectListItem> JobTypeList = new List<SelectListItem>();
            foreach (var row in _jobTypeservices.GetJobTypeList())
            {
                JobTypeList.Add(new SelectListItem
                {
                    Text = row.JobTypeName,
                    Value = row.JobtypeId.ToString()
                });
            }
            List<SelectListItem> ServiceEventList = new List<SelectListItem>();
            foreach (var row in _serviceEventService.GetServiceEventList())
            {
                ServiceEventList.Add(new SelectListItem
                {
                    Text = row.ServiceEventGroupName,
                    Value = row.ServiceEventId.ToString()
                });
            }
            List<SelectListItem> EmployeesList = new List<SelectListItem>();
            foreach (var row in _employeeService.GetEmployeeList(2))
            {
                EmployeesList.Add(new SelectListItem
                {
                    Text = row.EmpName,
                    Value = row.EmpCode.ToString()
                });
            }
            ViewBag.officeList = OfficeList;
            ViewBag.departmentList = DepartmentList;
            ViewBag.sectionList = SectionList;
            ViewBag.designationList = DesignationList;
            ViewBag.rankList = RankList;
            ViewBag.levelList = LevelList;
            ViewBag.businessGroupList = BusinessGroupList;
            ViewBag.shiftList = ShiftList;
            ViewBag.remoteList = RemoteList;
            ViewBag.jobTypeList = JobTypeList;
            ViewBag.serviceEventList = ServiceEventList;
            ViewBag.employeeList = EmployeesList;
        }

        [Route("user/profile")]
        public ActionResult Index()
        {
            EmployeeDetailsViewModel empDetails = new EmployeeDetailsViewModel();
            int empCode = Convert.ToInt32(Session["EmpCode"]);
            ListOfDatas(empCode);
            empDetails = _employeeService.GetEmployeeDetails(empCode);
            empDetails.EmployeeAppointmentDetail = _employeeService.GetEmployeeAppointmentDetails(empCode);
            return View(empDetails);
        }
        //[Route("user/address/{empCode}")]
        public ActionResult Address()
        {

            EmployeeEditViewModel empDetails = new EmployeeEditViewModel();
            int empCode = Convert.ToInt32(Session["EmpCode"]);
            ListOfDatas(empCode);
            empDetails = _employeeService.GetEmployeeByID(empCode);

            return View(empDetails);
        }
        public ActionResult ContactDetail()
        {

            EmployeeEditViewModel empDetails = new EmployeeEditViewModel();
            int empCode = Convert.ToInt32(Session["EmpCode"]);
            ListOfDatas(empCode);
            empDetails = _employeeService.GetEmployeeByID(empCode);

            return View(empDetails);
        }
        public ActionResult FamilyDetail()
        {

            int empCode = Convert.ToInt32(Session["EmpCode"]);
            ListOfDatas(empCode);
            IEnumerable<EmployeeFamilyViewModel> empDetails = _employeeService.GetEmployeeFamilyByID(empCode);

            return View(empDetails);
        }
        public ActionResult Nominee()
        {
            EmployeeEditViewModel empDetails = new EmployeeEditViewModel();
            int empCode = Convert.ToInt32(Session["EmpCode"]);
            ListOfDatas(empCode);
            empDetails = _employeeService.GetEmployeeByID(empCode);

            return View(empDetails);
        }
        public ActionResult Award()
        {
            //if (!ViewBag.AllowView)
            //{
            //    ViewBag.Error = "You are not Authorize to use this Page";
            //    return PartialView("_partialviewNotFound");
            //}


            int empCode = Convert.ToInt32(Session["EmpCode"]);
            ListOfDatas(empCode);
            IEnumerable<EmployeePrizeDTO> res = _empPrizeService.GetAllPrizeOfEmployee(empCode);
            return View(res);
        }
        public ActionResult Education()
        {
            int empCode = Convert.ToInt32(Session["EmpCode"]);
            ListOfDatas(empCode);
            IEnumerable<EmpEducationDTO> res = _empEducationService.GetAllEducationById(empCode);
            return View(res);
        }
        public ActionResult EducationDetail(string id)
        {
            EmpEducationDTO empDetails = new EmpEducationDTO();
            int empCode = Convert.ToInt32(Session["EmpCode"]);
            ListOfDatas(empCode);
            int expId = Convert.ToInt32(id);
            empDetails = _empEducationService.GetEducationByEduId(expId);

            return View(empDetails);
        }
        public ActionResult Experince()
        {
            int empCode = Convert.ToInt32(Session["EmpCode"]);
            ListOfDatas(empCode);
            IEnumerable<EmployeeExperinceViewModel> empDetails = _employeeService.GetEmployeeExperiencesByID(empCode);

            return View(empDetails);
        }
        public ActionResult ExperinceDetail(string id)
        {
            int empCode = Convert.ToInt32(Session["EmpCode"]);
            ListOfDatas(empCode);
            int expId = Convert.ToInt32(id);
            IEnumerable<EmployeeExperinceViewModel> empDetails = _employeeService.GetEmployeeExperienceDetailsByID(expId);

            return View(empDetails);
        }
        public ActionResult History()
        {
            int empCode = Convert.ToInt32(Session["EmpCode"]);
            ListOfDatas(empCode);
            IEnumerable<EmployeeJobHistoryDTO> res = _jobHistoryService.GetAllHistoryOfEmployee(empCode);
            return View(res);
        }
        public ActionResult HistoryDetail(string id)
        {
            int empCode = Convert.ToInt32(Session["EmpCode"]);
            ListOfDatas(empCode);
            EmployeeJobHistoryDTO res = _jobHistoryService.GetJobHistoryById(Convert.ToInt32(id));
            return View(res);
        }
        public ActionResult Skill()
        {

            int empCode = Convert.ToInt32(Session["EmpCode"]);
            ListOfDatas(empCode);
            IEnumerable<EmployeeSkillViewModel> empDetails = _employeeService.GetEmployeeSkillsByID(empCode);

            return View(empDetails);
        }

        public ActionResult Training()
        {
            //if (!ViewBag.AllowView)
            //{
            //    ViewBag.Error = "You are not Authorize to use this Page";
            //    return PartialView("_partialviewNotFound");
            //}
            int empCode = Convert.ToInt32(Session["EmpCode"]);
            ListOfDatas(empCode);
            List<EmpTrainingDTO> res = _employeeTrainingService.GetAllTrainingOfEmployeeWithId(empCode);
            foreach (var data in res)
            {
                string cname = _countryService.GetCountryName(data.TrainingCountry);
                data.CountryName = cname;
            }
            return View(res);
        }

        public ActionResult Documents()
        {

            int empCode = Convert.ToInt32(Session["EmpCode"]);
            ListOfDatas(empCode);
            IEnumerable<EmployeeDocumentViewModel> empDetails = _employeeService.GetEmployeeDocumentsByID(empCode);

            return View(empDetails);
        }

        public ActionResult AddEducation()
        {
            EmpEducationDTO employeeEducation = new EmpEducationDTO();
            employeeEducation.CountryList = _empEducationService.GetCountryList();
            List<SelectListItem> educationLevel = new List<SelectListItem>();
            foreach (var row in _educationLevel.GetEducationLevel())
            {
                educationLevel.Add(new SelectListItem
                {
                    Text = row.LevelName,
                    Value = row.LevelId.ToString()
                });
            }
            IEnumerable<SelectListItem> CountryList = _countryService.GetCountryList();

            ViewBag.EducationLevel = educationLevel;
            ViewBag.Countries = CountryList;
            return View(employeeEducation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEducation(EmpEducationDTO data)
        {
            data.PassedDate = Convert.ToDateTime(NepEngDate.NepToEng(data.PassedDateNP));
            data.EmpCode = Convert.ToInt32(Session["EmpCode"]);

            List<SelectListItem> educationLevel = new List<SelectListItem>();
            foreach (var row in _educationLevel.GetEducationLevel())
            {
                educationLevel.Add(new SelectListItem
                {
                    Text = row.LevelName,
                    Value = row.LevelId.ToString()
                });
            }
            IEnumerable<SelectListItem> CountryList = _countryService.GetCountryList();
            ViewBag.Countries = CountryList;
            ViewBag.EducationLevel = educationLevel;
            //ViewBag.EmployeeDetail = _empDetails.GetEmployeeDetails(id);
            //ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
            try
            {
                if (!ModelState.IsValid)
                {

                    ModelState.AddModelError("Error", "Please Fill up all required field");
                    return View();
                }
                EmpEducationDTO res = new EmpEducationDTO();
                //data.EmpCode = id;
                res = _empEducationService.InsertEmpEducation(data);
                ViewBag.Success = "Education added";
                return View();
            }
            catch (Exception ex)
            {
                //ViewBag.EmployeeDetail = _empDetails.GetEmployeeDetails(id);
                //ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
                ViewBag.Error = ex.Message;
                ModelState.AddModelError("Error", ex.Message);
                return View(data);
            }
        }
        public ActionResult EditEducation(int id)
        {

            EmpEducationDTO res = new EmpEducationDTO();
            res = _empEducationService.GetEducationByEduId(id);
            res.PassedDateNP = NepEngDate.EngToNep(Convert.ToDateTime(res.PassedDate));
            List<SelectListItem> educationLevel = new List<SelectListItem>();
            foreach (var row in _educationLevel.GetEducationLevel())
            {
                educationLevel.Add(new SelectListItem
                {
                    Text = row.LevelName,
                    Value = row.LevelId.ToString()
                });
            }
            IEnumerable<SelectListItem> CountryList = _countryService.GetCountryList();

            ViewBag.EducationLevel = educationLevel;
            ViewBag.Countries = CountryList;
            return View(res);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEducation(EmpEducationDTO data)
        {
            data.PassedDate = Convert.ToDateTime(NepEngDate.NepToEng(data.PassedDateNP));

            data.EmpCode = Convert.ToInt32(Session["EmpCode"]);
            data.EducationStatus = 0;
            EmpEducationDTO jtd = new EmpEducationDTO();
            List<SelectListItem> educationLevel = new List<SelectListItem>();
            foreach (var row in _educationLevel.GetEducationLevel())
            {
                educationLevel.Add(new SelectListItem
                {
                    Text = row.LevelName,
                    Value = row.LevelId.ToString()
                });
            }
            IEnumerable<SelectListItem> CountryList = _countryService.GetCountryList();
            ViewBag.Countries = CountryList;
            ViewBag.EducationLevel = educationLevel;
            //if (!ModelState.IsValid)
            //{
            //    return View(jtd);
            //}
            if (data.EducationStatus == 0)
            {
                if (data.File != null)
                {
                    string extension = Path.GetExtension(data.File.FileName).ToLower();
                    if (extension == ".jpeg" || extension == ".jpg" || extension == ".png")
                    {
                        data.ScanDocument = (data.EduId + data.File.FileName).ToLower();
                        //housePermitModel = _housePermitService.insertHousePermits(housePermit);
                        if (data != null)
                        {
                            string path = Server.MapPath("~\\img\\");
                            data.File.SaveAs(path + data.ScanDocument);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("InvalidImage", "The file you are trying to upload is not image.");
                        return View();
                    }
                }
                int res = _empEducationService.UpdateEducation(data);
                ViewBag.Success = "Education of " + data.EmpCode + " has been updated";
                ModelState.Clear();
                return View(jtd);
            }
            else
            {
                ViewBag.Success = "Education already approved ";
                return View(jtd);
            }

        }
        public ActionResult AddFamily()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFamily(EmployeeFamilyViewModel data)
        {
            data.FDob = Convert.ToDateTime(NepEngDate.NepToEng(data.FDobNP));

            data.EmpCode = Convert.ToInt32(Session["EmpCode"]);
            EmployeeFamilyViewModel employeeEducation = new EmployeeFamilyViewModel();
            //employeeEducation.CountryList = _empEducationService.GetCountryList();
            //employeeEducation.EducationLevelList = _empEducationService.GetEducationLevelList();

            //if (!ModelState.IsValid)
            //{
            //    return View(employeeEducation);
            //}
            EmployeeFamilyViewModel res = new EmployeeFamilyViewModel();
            res = _empFamilyService.InsertEmpFamily(data);
            ViewBag.Success = "family of " + res.EmpCode + " has been created";
            ModelState.Clear();
            return View(employeeEducation);
        }
        public ActionResult EditFamily(int id)
        {
            EmployeeFamilyViewModel res = new EmployeeFamilyViewModel();
            res = _empFamilyService.GetEmpFamilyByID(id);
            res.FDobNP = NepEngDate.EngToNep(Convert.ToDateTime(res.FDob));
            return View(res);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFamily(EmployeeFamilyViewModel data)
        {
            data.FDob = Convert.ToDateTime(NepEngDate.NepToEng(data.FDobNP));
            data.EmpCode = Convert.ToInt32(Session["EmpCode"]);
            EmployeeFamilyViewModel jtd = new EmployeeFamilyViewModel();
            //if (!ModelState.IsValid)
            //{
            //    return View(jtd);
            //}

            int res = _empFamilyService.UpdateEmpFamily(data);
            ViewBag.Success = "Education of " + data.EmpCode + " has been updated";
            ModelState.Clear();
            return View(jtd);

        }
    }
}