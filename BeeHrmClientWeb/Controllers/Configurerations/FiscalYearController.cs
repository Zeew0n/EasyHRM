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
    public class FiscalYearController : BaseController
    {
        // GET: FiscalYear
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
        public FiscalYearController()
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
        #region Fiscal Year
        [Route("FiscalYear")]
        public ActionResult FiscalYear()
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }


            IEnumerable<FiscalDTO> list = _fiscalService.GetAllFiscal();
            return View(list);
        }

        [Route("Fiscal/Create")]
        public ActionResult FiscalCreate()
        {
            return View();
        }

        [HttpPost]
        [Route("Fiscal/Create")]
        [MultipleButton(Name = "action", Argument = "Create")]
        public ActionResult FiscalCreate(FiscalDTO data)
        {
            data.FyStartDate = !string.IsNullOrEmpty(data.FyStartDateNp) ? Convert.ToDateTime(NepEngDate.NepToEng(data.FyStartDateNp)) : data.FyStartDate;
            data.FyEndDate = !string.IsNullOrEmpty(data.FyEndDateNp) ? Convert.ToDateTime(NepEngDate.NepToEng(data.FyEndDateNp)) : data.FyEndDate;

            try
            {
                FiscalDTO res = new FiscalDTO();
                FiscalDTO fd = new FiscalDTO();
                if (!ModelState.IsValid)
                {
                    return View(fd);
                }
                res = _fiscalService.InsertFiscal(data);
                ViewBag.Success = "Fiscal year " + data.FyName + " has been created";
                ModelState.Clear();
                return RedirectToAction("FiscalYear");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [HttpPost]
        [Route("Fiscal/Create")]
        [MultipleButton(Name = "action", Argument = "CreateClose")]
        public ActionResult FiscalCreateClose(FiscalDTO data)
        {
            data.FyStartDate = !string.IsNullOrEmpty(data.FyStartDateNp) ? Convert.ToDateTime(NepEngDate.NepToEng(data.FyStartDateNp)) : data.FyStartDate;
            data.FyEndDate = !string.IsNullOrEmpty(data.FyEndDateNp) ? Convert.ToDateTime(NepEngDate.NepToEng(data.FyEndDateNp)) : data.FyEndDate;
            try
            {
                FiscalDTO res = new FiscalDTO();
                FiscalDTO fd = new FiscalDTO();
                if (!ModelState.IsValid)
                {
                    return View(fd);
                }
                res = _fiscalService.InsertFiscal(data);
                ViewBag.Success = "Fiscal year " + data.FyName + " has been created";
                return RedirectToAction("FiscalYear");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [Route("Fiscal/Edit/{id}")]
        public ActionResult FiscalEdit(int id)
        {
            FiscalDTO data = new FiscalDTO();
            data = _fiscalService.GetFiscalById(id);
            return View(data);
        }

        [HttpPost]
        [Route("Fiscal/Edit/{id}")]
        public ActionResult FiscalEdit(FiscalDTO data)
        {
            data.FyStartDate = !string.IsNullOrEmpty(data.FyStartDateNp) ? Convert.ToDateTime(NepEngDate.NepToEng(data.FyStartDateNp)) : data.FyStartDate;
            data.FyEndDate = !string.IsNullOrEmpty(data.FyEndDateNp) ? Convert.ToDateTime(NepEngDate.NepToEng(data.FyEndDateNp)) : data.FyEndDate;
            int res = _fiscalService.UpdateFiscal(data);
            return RedirectToAction("FiscalYear");
        }

        [Route("fiscal/delete/{id}")]
        public RedirectResult FiscalDelete(int id)
        {
            _fiscalService.DeleteFiscal(id);
            return Redirect("/fiscalyear");
        }
        #endregion
    }
}