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
    public class DesignationsController : BaseController
    {
        // GET: Designations
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
        public DesignationsController()
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

        #region Designations
        [Route("designations")]
        public ActionResult Designations()

        {

            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }


            IEnumerable<DesignationDTO> DesignationList = _designationServices.GetDesignationListWithLevelName();
            return View(DesignationList);
        }

        [HttpGet]
        [Route("Designations/Create")]
        public ActionResult DesignationCreate()
        {
            ViewBag.designationList = _designationServices.GetDesignationList();
            List<SelectListItem> GradeList = new List<SelectListItem>();
            ViewBag.gradeList = GradeList;
            ViewBag.levelList = _levelService.GetLevellist();
            return View();
        }

        [HttpPost]
        [Route("Designations/Create")]
        [MultipleButton(Name = "action", Argument = "DeptCreate")]
        public ActionResult DesignationCreate(DesignationDTO data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(data);
                }
                DesignationDTO resDesgDTO = new DesignationDTO();
                resDesgDTO = _designationServices.InsertDesignation(data);
                ViewBag.designationList = _designationServices.GetDesignationList();
                ViewBag.levelList = _levelService.GetLevellist();
                List<SelectListItem> GradeList = new List<SelectListItem>();

                ViewBag.gradeList = GradeList;
                ViewBag.success = String.Format("The designation with code {0} has been inserted.", resDesgDTO.DesgCode);
                ModelState.Clear();
                return View();
            }
            catch (Exception Ex)
            {
                ViewBag.error = Ex.Message;
                return View();
            }
        }
        [Route("Designations/Create")]
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "DeptCreateClose")]
        public ActionResult DesignationCreateClose(DesignationDTO data)
        {
            try
            {
                DesignationDTO resDeptDTO = new DesignationDTO();
                resDeptDTO = _designationServices.InsertDesignation(data);
                ViewBag.Success = "New Designation created successfully";
                ModelState.Clear();
                return View("Designations", _designationServices.GetDesignationList());
            }
            catch (Exception Ex)
            {
                ViewBag.error = Ex.Message;
                return View(data);
            }
        }

        [HttpGet]
        [Route("Designations/Edit/{DesgId}")]
        public ActionResult DesignationEdit(int DesgId)
        {
            ViewBag.designationList = _designationServices.GetDesignationList();
            ViewBag.levelList = _levelService.GetLevellist();
            DesignationDTO DesignationListById = _designationServices.GetDesignationById(DesgId);
            return View(DesignationListById);
        }

        [HttpPost]
        [Route("Designations/Edit/{DeptId}")]
        public ActionResult DesignationEdit(DesignationDTO data)
        {
            ViewBag.designationList = _designationServices.GetDesignationList();
            ViewBag.levelList = _levelService.GetLevellist();
            int reDesg = _designationServices.UpdateDesignation(data);
            ViewBag.Success = "Designation updated successfully";
            ModelState.Clear();
            return View(data);
        }
        #endregion
    }
}