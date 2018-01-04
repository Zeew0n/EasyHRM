using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Configurerations
{
    public class LeaveRulesController : BaseController
    {
        // GET: LeaveRules
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
        public LeaveRulesController()
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

        [Route("leaverules")]
        public ActionResult LeaveRules()
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }


            var leaveRules = _leaveRuleService.GetLeaveRulesList();
            return View(leaveRules);
        }

        [Route("leaverule/create")]
        public ActionResult LeaveRuleAdd()
        {
            var leaveTypes = _leaveTypeService.GetLeaveTypes().ToList();
            LeaveRuleAddViewModel lrVM = new LeaveRuleAddViewModel();
            lrVM.Params = leaveTypes;
            return View(lrVM);
        }

        [Route("leaverule/create")]
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Create")]
        public ActionResult LeaveRuleAdd(LeaveRuleAddViewModel vm)
        {
            var leaveTypes = _leaveTypeService.GetLeaveTypes().ToList();
            LeaveRuleAddViewModel lrVM = new LeaveRuleAddViewModel();
            lrVM.Params = leaveTypes;

            if (!ModelState.IsValid)
            {
                return View(lrVM);
            }
            LeaveRuleDTO lrd = new LeaveRuleDTO();
            lrd.LeaveRuleDetails = vm.LeaveRuleDescription;
            lrd.LeaveRuleName = vm.LeaveRuleName;

            //Inserting Leave Rules
            LeaveRuleDTO resLeaveRule = _leaveRuleService.InsertLeaveRule(lrd);

            //Inserting Leave Rule Details
            for (int i = 0; i < vm.Params.Count; i++)
            {
                LeaveRuleDetailDTO inLRD = new LeaveRuleDetailDTO();
                inLRD.LeaveRuleId = resLeaveRule.LeaveRuleId;
                inLRD.LeaveDays = vm.Params[i].Days != null ? Convert.ToDecimal(vm.Params[i].Days) : 0;
                inLRD.LeaveTypeId = Convert.ToInt32(vm.Params[i].LeaveTypeId);
                LeaveRuleDetailDTO reLRD = _leaveRuleDetailService.InsertLeaveRuleDetail(inLRD);
            }
            ViewBag.Success = "The leave rule \"" + resLeaveRule.LeaveRuleName + "\" has been added.";
            ModelState.Clear();
            return View(lrVM);
        }

        [Route("leaverule/create")]
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "CreateClose")]
        public ActionResult LeaveRuleAddClose(LeaveRuleAddViewModel vm)
        {
            var leaveTypes = _leaveTypeService.GetLeaveTypes().ToList();
            LeaveRuleAddViewModel lrVM = new LeaveRuleAddViewModel();
            lrVM.Params = leaveTypes;

            if (!ModelState.IsValid)
            {
                return View(lrVM);
            }
            LeaveRuleDTO lrd = new LeaveRuleDTO();
            lrd.LeaveRuleDetails = vm.LeaveRuleDescription;
            lrd.LeaveRuleName = vm.LeaveRuleName;

            //Inserting Leave Rules
            LeaveRuleDTO resLeaveRule = _leaveRuleService.InsertLeaveRule(lrd);

            //Inserting Leave Rule Details
            for (int i = 0; i < vm.Params.Count; i++)
            {
                LeaveRuleDetailDTO inLRD = new LeaveRuleDetailDTO();
                inLRD.LeaveRuleId = resLeaveRule.LeaveRuleId;
                inLRD.LeaveDays = vm.Params[i].Days != null ? Convert.ToDecimal(vm.Params[i].Days) : 0;
                inLRD.LeaveTypeId = Convert.ToInt32(vm.Params[i].LeaveTypeId);
                LeaveRuleDetailDTO reLRD = _leaveRuleDetailService.InsertLeaveRuleDetail(inLRD);
            }

            return RedirectToAction("LeaveRules", "LeaveRules");
        }

        [Route("leaverule/edit/{id}")]
        public ActionResult LeaveRuleEdit(int id)
        {
            LeaveRuleAddViewModel lrVM = new LeaveRuleAddViewModel();
            LeaveRuleDTO leaveRule = _leaveRuleService.GetLeaveRuleById(id);
            lrVM.LeaveRuleName = leaveRule.LeaveRuleName;
            lrVM.LeaveRuleDescription = leaveRule.LeaveRuleDetails;
            lrVM.LeaveRuleId = id;
            var leaveRuleDetailsList = _leaveRuleDetailService.GetLeaveRuleDetails(id);
            var leavetypeList = _leaveTypeService.GetLeaveTypes();
            List<LeaveTypeDTO> leaveTypes = (from lrdl in leaveRuleDetailsList
                                             join ltl in leavetypeList on lrdl.LeaveTypeId equals ltl.LeaveTypeId
                                             select new LeaveTypeDTO
                                             {
                                                 LeaveRuleDetailId = lrdl.DetailId,
                                                 LeaveTypeName = ltl.LeaveTypeName,
                                                 LeaveTypeId = lrdl.LeaveTypeId,
                                                 Days = lrdl.LeaveDays.ToString()
                                             }).ToList();
            lrVM.Params = leaveTypes;
            return View(lrVM);
        }

        [Route("leaverule/edit/{id}")]
        [HttpPost]
        public ActionResult LeaveRuleEdit(LeaveRuleAddViewModel vm)
        {
            string status = "failed";
            //var leaveTypes = _leaveTypeService.GetLeaveTypes().ToList();
            LeaveRuleAddViewModel lrVM = new LeaveRuleAddViewModel();
            //lrVM.Params = leaveTypes;

            LeaveRuleDTO lrd = new LeaveRuleDTO();
            lrd.LeaveRuleDetails = vm.LeaveRuleDescription;
            lrd.LeaveRuleName = vm.LeaveRuleName;
            lrd.LeaveRuleId = vm.LeaveRuleId;

            //Updating Leave Rules
            int abc = _leaveRuleService.UpdateLeaveRule(lrd);

            if (abc > 0)
            {
                status = "done";
            }
            //Editing Leave Rule Details
            for (int i = 0; i < vm.Params.Count; i++)
            {
                LeaveRuleDetailDTO inLRD = new LeaveRuleDetailDTO();
                inLRD.DetailId = vm.Params[i].LeaveRuleDetailId;
                inLRD.LeaveRuleId = vm.LeaveRuleId;
                inLRD.LeaveDays = vm.Params[i].Days != null ? Convert.ToDecimal(vm.Params[i].Days) : 0;
                inLRD.LeaveTypeId = Convert.ToInt32(vm.Params[i].LeaveTypeId);
                int def = _leaveRuleDetailService.UpdateLeaveRuleDetails(inLRD);

            }
            ViewBag.Success = "The leave rule has been updated.";
            return RedirectToAction("LeaveRuleEdit", "LeaveRules", new { id = vm.LeaveRuleId, status });
        }

        [Route("leaverule/delete/{id}")]
        public RedirectResult LeaveRuleDelete(int id)
        {
            _leaveRuleService.DeleteLeaveRule(id);
            return Redirect("/leaverules");
        }

    }
}