using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHrmClientWeb.CodeBase;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Configurerations
{
    public class RanksController : BaseController
    {
        // GET: Ranks
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
        public RanksController()
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
        #region Ranks
        [Route("Ranks")]
        public ActionResult Ranks()
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }


            IEnumerable<RankDTO> RankList = _rankService.GetRankList();
            return View(RankList);

        }

        [HttpGet]
        [Route("Rank/Create")]
        public ActionResult RankCreate()
        {
            return View();
        }

        [HttpPost]
        [Route("Rank/Create")]
        public ActionResult RankCreate(RankDTO data)
        {
            try
            {
                _rankService.InsertRank(data);
                ViewBag.success = String.Format("New Rank Added");
                ModelState.Clear();
                return View();
            }
            catch (Exception Ex)
            {

                ViewBag.error = Ex.Message;
                TempData["Danger"] = Ex.Message;
                return View();
            }
        }
        [HttpGet]
        [Route("rank/edit/{rankId}")]
        public ActionResult RankEdit(int rankId)
        {
            RankDTO rankbyId = _rankService.GetRankByID(rankId);
            return View(rankbyId);
        }
        [HttpPost]
        [Route("rank/edit/{rankId}")]
        public RedirectResult RankEdit(RankDTO data)
        {
            int rnk = _rankService.UpdateRank(data);
            return Redirect("/rank/edit/" + data.RankId);
        }

        #endregion
    }
}