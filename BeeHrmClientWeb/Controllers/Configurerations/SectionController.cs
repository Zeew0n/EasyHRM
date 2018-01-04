using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHrmClientWeb.CodeBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Configurerations
{
    public class SectionController : BaseController
    {
        // GET: Section
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
        public SectionController()
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
        #region Section
        [Route("Section")]
        public ActionResult Section()
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }

            IEnumerable<SectionDTO> sectionList = _sectionService.GetSectionList();
            return View(sectionList);
        }

        [HttpGet]
        [Route("Section/Create")]
        public ActionResult SectionCreate()
        {
            return View();
        }

        [HttpPost]
        [Route("Section/Create")]
        public RedirectResult SectionCreate(SectionDTO data)
        {
            try
            {
                SectionDTO res = new SectionDTO();
                res = _sectionService.InsertSection(data);
                return Redirect("/section");
            }
            catch (Exception ex)
            {
                return Redirect("/section");
            }

        }
        [HttpGet]
        [Route("Section/Edit/{sectionId}")]
        public ActionResult SectionEdit(int sectionId)
        {
            SectionDTO res = _sectionService.GetSectionById(sectionId);
            return View(res);
        }

        [HttpPost]
        [Route("Section/Edit/{sectionId}")]
        public RedirectResult SectionEdit(SectionDTO data)
        {
            int res = _sectionService.UpdateSectionById(data);
            return Redirect("/section");
        }

        [HttpGet]
        [Route("Section/Delete/{sectionId}")]
        public RedirectResult SectionDelete(int sectionId)
        {
            _sectionService.DeleteSection(sectionId);
            return Redirect("/section");
        }
        #endregion

    }
}