using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Configurerations
{
    public class ShiftController : BaseController
    {
        // GET: Shift
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
        public ShiftController()
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
        #region Shift
        [Route("Shift")]
        public ActionResult Shift()
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }

            IEnumerable<ShiftDTO> shiftList = _shifService.GetShiftsLIst();
            return View(shiftList);
        }

        [HttpGet]
        [Route("Shift/Create")]
        public ActionResult ShiftCreate()
        {
            return View();
        }

        [HttpPost]
        [Route("Shift/Create")]
        [MultipleButton(Name = "action", Argument = "Create")]
        public ActionResult ShiftCreate(ShiftDetailViewModel data)
        {
            ShiftDetailViewModel sdvm = new ShiftDetailViewModel();
            if (!ModelState.IsValid)
            {
                return View(sdvm);
            }
            ShiftDTO res = new ShiftDTO();
            res.ShiftName = data.ShiftName;
            res.ShiftDelayAllow = data.ShiftDelayAllow;
            res.ShiftStatus = data.ShiftStatus;
            ShiftDTO result = _shifService.InsertShift(res);
            ShiftDayDTO detail = new ShiftDayDTO();
            foreach (var item in data.ShiftDay)
            {
                detail.DayNumber = item.DayNumber + 1;
                detail.DayName = item.DayName;
                detail.DayShiftId = result.ShiftId;
                detail.DayStartTime = item.DayStartTime;
                detail.DayEndTime = item.DayEndTime;
                detail.DayWeekend = item.DayWeekend;
                TimeSpan a = item.DayEndTime.Value.Subtract(item.DayStartTime.Value);
                double b = a.TotalMinutes;
                detail.DayWorkingMinute = Convert.ToInt32(b);
                ShiftDayDTO resultDetail = _shiftDayService.InsertShiftDayDetail(detail);
                ViewBag.Success = "New Shift created successfully";

            }
            ModelState.Clear();
            return View();
        }

        [HttpPost]
        [Route("Shift/Create")]
        [MultipleButton(Name = "action", Argument = "CreateClose")]
        public ActionResult ShiftCreateClose(ShiftDetailViewModel data)
        {
            ShiftDetailViewModel sdvm = new ShiftDetailViewModel();
            if (!ModelState.IsValid)
            {
                return View("Shift/Create", sdvm);
            }
            ShiftDTO res = new ShiftDTO();
            res.ShiftName = data.ShiftName;
            res.ShiftDelayAllow = data.ShiftDelayAllow;
            res.ShiftStatus = data.ShiftStatus;
            ShiftDTO result = _shifService.InsertShift(res);
            ShiftDayDTO detail = new ShiftDayDTO();
            foreach (var item in data.ShiftDay)
            {
                detail.DayNumber = item.DayNumber + 1;
                detail.DayName = item.DayName;
                detail.DayShiftId = result.ShiftId;
                detail.DayStartTime = item.DayStartTime;
                detail.DayEndTime = item.DayEndTime;
                detail.DayWeekend = item.DayWeekend;
                TimeSpan a = item.DayEndTime.Value.Subtract(item.DayStartTime.Value);
                double b = a.TotalMinutes;
                detail.DayWorkingMinute = Convert.ToInt32(b);
                ShiftDayDTO resultDetail = _shiftDayService.InsertShiftDayDetail(detail);
                ViewBag.Success = "New Office Type created successfully";

            }
            ViewBag.Success = "Shift " + result.ShiftName + " has been created";
            return RedirectToAction("Shift");
        }

        [HttpGet]
        [Route("shift/edit/{id}")]
        public ActionResult ShiftEdit(int id)
        {
            ShiftDetailViewModel resById = new ShiftDetailViewModel();
            ShiftDTO getParentDetails = _shifService.GetShiftById(id);
            resById.ShiftId = getParentDetails.ShiftId;
            resById.ShiftName = getParentDetails.ShiftName;
            resById.ShiftDelayAllow = getParentDetails.ShiftDelayAllow;
            resById.ShiftStatus = getParentDetails.ShiftStatus;
            List<ShiftDayDTO> getDetails = _shiftDayService.GetShiftByParentId(getParentDetails.ShiftId);
            resById.ShiftDay = getDetails;
            return View(resById);
        }
        [HttpPost]
        [Route("shift/edit/{id}")]
        public ActionResult ShiftEdit(ShiftDetailViewModel data)
        {
            ShiftDetailViewModel sdvm = new ShiftDetailViewModel();
            //if (!ModelState.IsValid)
            //{
            //    return View("Shift/Edit", sdvm);
            //}
            ShiftDTO res = new ShiftDTO();
            res.ShiftId = data.ShiftId;
            res.ShiftName = data.ShiftName;
            res.ShiftDelayAllow = data.ShiftDelayAllow;
            res.ShiftStatus = data.ShiftStatus;
            int result = _shifService.UpdateShift(res);
            ShiftDayDTO detail = new ShiftDayDTO();
            if (result > 0)
            {
                foreach (var item in data.ShiftDay)
                {
                    detail.DayId = item.DayId;
                    detail.DayNumber = item.DayNumber + 1;
                    detail.DayName = item.DayName;
                    detail.DayShiftId = item.DayShiftId;
                    detail.DayStartTime = item.DayStartTime;
                    detail.DayEndTime = item.DayEndTime;
                    detail.DayWeekend = item.DayWeekend;
                    TimeSpan a = item.DayEndTime.Value.Subtract(item.DayStartTime.Value);
                    double b = a.TotalMinutes;
                    detail.DayWorkingMinute = Convert.ToInt32(b);
                    int resultDetail = _shiftDayService.UpdateShiftDetail(detail);

                }
            }

            return RedirectToAction("Shift", "Shift");
        }

        #endregion
    }
}