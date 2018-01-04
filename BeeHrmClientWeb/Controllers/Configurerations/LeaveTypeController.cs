using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Configurerations
{
    public class LeaveTypeController : BaseController
    {
        // GET: LeaveType
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
        public LeaveTypeController()
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
        #region Leave Type
        //LeaveTypes Region
        [Route("LeaveType")]
        public ActionResult LeaveType()
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }


            var leaveTypeList = _leaveTypeService.GetLeaveTypes();
            return View(leaveTypeList);
        }

        [Route("leavetype/create")]
        public ActionResult LeaveTypeAdd()
        {
            return View();
        }

        [Route("leavetype/create")]
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "LeaveTypeCreate")]
        public ActionResult LeaveTypeAdd(LeaveTypeDTO newLeave)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                var returnLeaveType = _leaveTypeService.InsertLeaveType(newLeave);
                ViewBag.Success = "The leave type " + returnLeaveType.LeaveTypeName + " has been added.";
                ModelState.Clear();
                return View();
            }
            catch (Exception Ex)
            {
                ViewBag.Error = "Oops! There was some problem while inserting leave type.";
                return View();
            }

        }

        [Route("leavetype/create")]
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "LeaveTypeCreateClose")]
        public ActionResult LeaveTypeAddClose(LeaveTypeDTO newLeave)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                LeaveTypeDTO resLeaveDTO = new LeaveTypeDTO();
                resLeaveDTO = _leaveTypeService.InsertLeaveType(newLeave);
                return Redirect("/leavetypes");
            }
            catch (Exception Ex)
            {
                ViewBag.error = Ex.Message;
                return Redirect("/leavetypes");
            }
        }

        [Route("leavetype/edit/{id}")]
        public ActionResult LeaveTypeEdit(int id)
        {
            var editLeaveType = _leaveTypeService.GetLeaveTypeById(id);
            return View(editLeaveType);
        }

        [Route("leavetype/edit")]
        [HttpPost]
        public RedirectResult LeaveTypeEdit(LeaveTypeDTO editLeaveType)
        {
            try
            {
                int done = _leaveTypeService.UpdateLeaveType(editLeaveType);
                return Redirect("/leavetype/edit/" + editLeaveType.LeaveTypeId);
            }
            catch (Exception Ex)
            {
                return Redirect("/leavetype/edit/" + editLeaveType.LeaveTypeId);
            }

        }

        [Route("leavetype/delete/{id}")]
        public ActionResult DeleteLeaveType(int id)
        {
            try
            {
                _leaveTypeService.DeleteLeaveType(id);
                ViewBag.Success = "The leave type has been deleted.";
                return Redirect("/leavetype");
            }
            catch (Exception Ex)
            {
                var leaveTypeList = _leaveTypeService.GetLeaveTypes().OrderByDescending(x => x.LeaveTypeId);
                ModelState.AddModelError("ForeignKeyError", "This leave cannot be deleted as it has already been associated.");
                return View("LeaveType", leaveTypeList);
            }
        }
        #endregion
    }
}