using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Utilities;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Configurerations
{
    public class EthnicityController : BaseController
    {
        // GET: Ethnicity
        #region interfaces
        private IModulServices _moduleService;
        private IDepartmentService _departmentServices;
        private IDesignationService _designationServices;
        private ILeaveTypeService _leaveTypeService;
        private ILeaveRuleService _leaveRuleService;
        private ILeaveRuleDetailService _leaveRuleDetailService;
        private ILevelService _levelService;
        private IDarbandiService _darbandiService;
        private IOfficeServices _officeService;
        private IOfficeTypeService _officeTypeService;
        private ISectionService _sectionService;
        private ILeaveYearService _leaveYearService;
        private IBusinessGroupService _bgService;
        private IRankService _rankService;
        private IRolesService _rolesService;
        private IRolesAccessService _roleAccessService;
        private IShiftService _shifService;
        private IShiftDayService _shiftDayService;
        private IEthnicityService _ethnicityService;
        private IJobTypeService _jobTypeService;
        private IEducationLevelService _educationLevelService;
        private IRolesBusinessGroupAccessService _rolesBusinessGroupAccessService;
        private IRolesOfficeAccessService _rolesOfficeAccessService;
        private IFiscalService _fiscalService;

        #endregion
        public EthnicityController()
        {
            _departmentServices = new DepartmentService();
            _designationServices = new DesignationService();
            _moduleService = new ModuleService();
            _officeService = new OfficeServices();
            _bgService = new BusinessGroupService();
            _leaveTypeService = new LeaveTypeService();
            _leaveRuleService = new LeaveRuleService();
            _leaveRuleDetailService = new LeaveRuleDetailService();
            _levelService = new LevelService();
            _darbandiService = new DarbandiService();
            _officeTypeService = new OfficeTypeService();
            _rankService = new RankService();
            _rolesService = new RolesService();
            _roleAccessService = new RolesAccessService();
            _leaveYearService = new LeaveYearService();
            _sectionService = new SectionService();
            _shifService = new ShiftService();
            _shiftDayService = new ShiftDayService();
            _ethnicityService = new EthnicityService();
            _jobTypeService = new JobTypeService();
            _educationLevelService = new EducationLevelService();
            _rolesBusinessGroupAccessService = new RolesBusinessGroupAccessService();

            _fiscalService = new FiscalService();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();
        }

        #region Ethnicity
        [Route("Ethnicity")]
        public ActionResult Ethnicity()
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }


            IEnumerable<EthnicityDTO> ethnicityList = _ethnicityService.GetEthnicityList();
            return View(ethnicityList);
        }

        [Route("Ethnicity/Create")]
        public ActionResult EthnicityCreate()
        {
            return View();
        }


        [HttpPost]
        [Route("Ethnicity/Create")]
        [MultipleButton(Name = "action", Argument = "Create")]
        public ActionResult EthnicityCreate(EthnicityDTO data)
        {
            EthnicityDTO ed = new EthnicityDTO();
            if (!ModelState.IsValid)
            {
                return View(ed);
            }
            EthnicityDTO res = _ethnicityService.InsertEthnicity(data);
            ViewBag.Success = "Ethnicity " + res.EthnicityName + " has been created.";
            ModelState.Clear();
            return View();
        }

        [HttpPost]
        [Route("Ethnicity/Create")]
        [MultipleButton(Name = "action", Argument = "CreateClose")]
        public ActionResult EthnicityCreateClose(EthnicityDTO data)
        {
            EthnicityDTO ed = new EthnicityDTO();
            if (!ModelState.IsValid)
            {
                return View("Ethnicity/Create", ed);
            }
            EthnicityDTO res = _ethnicityService.InsertEthnicity(data);
            return RedirectToAction("Ethnicity", "Ethnicity");
        }
        [Route("Ethnicity/Edit/{id}")]
        public ActionResult EthnicityEdit(int id)
        {
            EthnicityDTO res = new EthnicityDTO();
            res = _ethnicityService.GetEthnicityById(id);
            return View(res);
        }

        [HttpPost]
        [Route("Ethnicity/Edit/{id}")]
        public ActionResult EthnicityEdit(EthnicityDTO data)
        {
            EthnicityDTO ed = new EthnicityDTO();
            if (!ModelState.IsValid)
            {
                return RedirectToAction("EthnicityEdit", ed);
            }
            int res = _ethnicityService.UpdateEthnicity(data);
            return RedirectToAction("Ethnicity", "Ethnicity");
        }
        [Route("Ethnicity/Delete/{id}")]
        public RedirectResult EthnicityDelete(int id)
        {
            _ethnicityService.DeleteEthnicityById(id);
            return Redirect("/Ethnicity");
        }
        #endregion
    }
}