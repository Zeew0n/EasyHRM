using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Configurerations
{
    public class EducationlevelController : BaseController
    {
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
        public EducationlevelController()
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

        #region Education Level
        [Route("EducationLevel")]
        public ActionResult EducationLevel()
        {

            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }

            IEnumerable<EducationLevelDTO> list = _educationLevelService.GetEducationLevel();
            return View(list);
        }
        [Route("EducationLevel/Create")]
        public ActionResult EducationLevelCreate()
        {
            return View();
        }
        [HttpPost]
        [Route("EducationLevel/Create")]
        [MultipleButton(Name = "action", Argument = "Create")]
        public ActionResult EducationLevelCreate(EducationLevelDTO data)
        {
            EducationLevelDTO el = new EducationLevelDTO();
            if (!ModelState.IsValid)
            {
                return View(el);
            }
            try
            {
                EducationLevelDTO res = new EducationLevelDTO();
                res = _educationLevelService.InsertEducationLevel(data);
                ViewBag.Success = "Education level " + data.LevelName + " has been created successfully";
                ModelState.Clear();
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [HttpPost]
        [Route("EducationLevel/Create")]
        [MultipleButton(Name = "action", Argument = "CreateClose")]
        public ActionResult EducationLevelCreateClose(EducationLevelDTO data)
        {
            EducationLevelDTO el = new EducationLevelDTO();
            if (!ModelState.IsValid)
            {
                return View(el);
            }
            try
            {
                EducationLevelDTO res = new EducationLevelDTO();
                res = _educationLevelService.InsertEducationLevel(data);
                ViewBag.Success = "Education level " + data.LevelName + " has been created successfully";
                return RedirectToAction("EducationLevel");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("EducationLevel/Create");
            }
        }
        [Route("educationlevel/edit/{id}")]
        public ActionResult EducationLevelEdit(int id)
        {
            EducationLevelDTO data = new EducationLevelDTO();
            data = _educationLevelService.GetEducationLevelById(id);
            return View(data);
        }
        [Route("educationlevel/edit/{id}")]
        [HttpPost]
        public RedirectResult EducationLevelEdit(EducationLevelDTO data)
        {
            int res = _educationLevelService.UpdateEducationLevel(data);
            return Redirect("/educationlevel");
        }
        [Route("educationlevel/delete/{id}")]
        public RedirectResult EducationLevelDelete(int id)
        {
            _educationLevelService.DeleteEducationLevel(id);
            return Redirect("/educationlevel");
        }
        #endregion
    }
}