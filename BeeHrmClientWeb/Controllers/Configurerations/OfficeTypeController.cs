using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Utilities;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Configurerations
{
    public class OfficeTypeController : BaseController
    {
        // GET: OfficeType
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
        public OfficeTypeController()
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
        #region Office Type 
        [Route("OfficeType")]
        public ActionResult OfficeTypes()
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }


            return View(_officeTypeService.GetOfficeTypes());
        }

        [Route("OfficeType/Create")]
        public ActionResult OfficeTypeCreate()
        {
            return View();
        }

        [HttpPost]
        [Route("OfficeType/Create")]
        [MultipleButton(Name = "action", Argument = "Create")]
        public ActionResult OfficeCreate(OfficeTypeDTO data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("OfficeTypes");
                }
                OfficeTypeDTO res = new OfficeTypeDTO();
                res = _officeTypeService.InsertOfficeType(data);
                ViewBag.Success = "New Office Type created successfully";
                ModelState.Clear();
                return RedirectToAction("OfficeTypes");

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [HttpPost]
        [Route("OfficeType/Create")]
        [MultipleButton(Name = "action", Argument = "CreateClose")]
        public ActionResult OfficeCreateClose(OfficeTypeDTO data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("OfficeTypes");
                }
                OfficeTypeDTO res = new OfficeTypeDTO();
                res = _officeTypeService.InsertOfficeType(data);
                ViewBag.Success = "New Office Type created successfully";
                return RedirectToAction("OfficeTypes");

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("OfficeTypes");
            }
        }

        [Route("OfficeType/Edit/{id}")]
        public ActionResult OfficeTypeEdit(int id)
        {
            OfficeTypeDTO res = _officeTypeService.GetOfficeTypeById(id);
            return View(res);
        }

        [HttpPost]
        [Route("OfficeType/Edit/{id}")]
        public ActionResult OfficeTypeEdit(OfficeTypeDTO data)
        {

            int res = _officeTypeService.UpdateOfficeType(data);
            return RedirectToAction("OfficeTypeEdit", data.OfficeTypeId);
        }
        [Route("OfficeType/Delete/{id}")]
        public ActionResult OfficeTypeDelete(int id)
        {
            _officeTypeService.DeleteOfficeType(id);
            return RedirectToAction("OfficeTypes");
        }

        #endregion
    }
}