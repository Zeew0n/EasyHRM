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
    public class LeaveYearsController : BaseController
    {
        // GET: LeaveYears
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
        public LeaveYearsController()
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
        #region Leave Years
        [Route("LeaveYears")]
        public ActionResult LeaveYears()
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }


            var leaveYears = _leaveYearService.GetLeaveYears();
            return View(leaveYears);
        }

        [Route("leaveyear/create")]
        public ActionResult LeaveYearAdd()
        {
            return View();
        }

        [Route("leaveyear/create")]
        [MultipleButton(Name = "action", Argument = "Create")]
        [HttpPost]
        public ActionResult LeaveYearAdd(LeaveYearDTO newLeaveYear)
        {
            newLeaveYear.YearStartDate = !String.IsNullOrEmpty(newLeaveYear.YearStartDateNp) ? Convert.ToDateTime(NepEngDate.NepToEng(newLeaveYear.YearStartDateNp)) : newLeaveYear.YearStartDate;
            newLeaveYear.YearEndDate = !String.IsNullOrEmpty(newLeaveYear.YearEndDateNp) ? Convert.ToDateTime(NepEngDate.NepToEng(newLeaveYear.YearEndDateNp)) : newLeaveYear.YearEndDate;
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                //newLeaveYear.YearStartDateNp =
                //DateTimeConverter.DateInBS(newLeaveYear.YearStartDate != null ? Convert.ToDateTime(newLeaveYear.YearStartDate) : DateTime.Now);
                //newLeaveYear.YearEndDateNp =
                //DateTimeConverter.DateInBS(newLeaveYear.YearEndDate != null ? Convert.ToDateTime(newLeaveYear.YearEndDate) : DateTime.Now);
                LeaveYearDTO retLeaveYearDTO = _leaveYearService.InsertLeaveYear(newLeaveYear);
                ViewBag.Success = "The Year " + retLeaveYearDTO.YearName + " has been added.";
                ModelState.Clear();
                return View(retLeaveYearDTO);
            }
            catch (Exception Ex)
            {
                ViewBag.Error = "Error ! " + Ex.Message;
                ModelState.Clear();
                return View();
            }

        }

        [Route("leaveyear/create")]
        [MultipleButton(Name = "action", Argument = "CreateClose")]
        [HttpPost]
        public ActionResult LeaveYearCreateClose(LeaveYearDTO newLeaveYear)
        {
            newLeaveYear.YearStartDate = !String.IsNullOrEmpty(newLeaveYear.YearStartDateNp) ? Convert.ToDateTime(NepEngDate.NepToEng(newLeaveYear.YearStartDateNp)) : newLeaveYear.YearStartDate;
            newLeaveYear.YearEndDate = !String.IsNullOrEmpty(newLeaveYear.YearEndDateNp) ? Convert.ToDateTime(NepEngDate.NepToEng(newLeaveYear.YearEndDateNp)) : newLeaveYear.YearEndDate;

            if (!ModelState.IsValid)
            {
                return View();
            }
            string msg = "failed";
            try
            {
                //newLeaveYear.YearStartDateNp =
                //DateTimeConverter.DateInBS(newLeaveYear.YearStartDate != null ? Convert.ToDateTime(newLeaveYear.YearStartDate) : DateTime.Now);
                //newLeaveYear.YearEndDateNp =
                //    DateTimeConverter.DateInBS(newLeaveYear.YearEndDate != null ? Convert.ToDateTime(newLeaveYear.YearEndDate) : DateTime.Now);
                LeaveYearDTO retLeaveYearDTO = _leaveYearService.InsertLeaveYear(newLeaveYear);
                if (retLeaveYearDTO != null)
                {
                    msg = "done";
                }
                return RedirectToAction("Leaveyears", "Leaveyears", new { msg });
            }
            catch (Exception Ex)
            {
                return RedirectToAction("Leaveyears", "Leaveyears", new { msg });
            }
        }

        [Route("leaveyear/edit/{id}")]
        public ActionResult LeaveYearEdit(int id)
        {
            LeaveYearDTO leaveYearD = _leaveYearService.GetLeaveYearInfoById(id);
            leaveYearD.YearStartDateNp = !String.IsNullOrEmpty(Convert.ToString(leaveYearD.YearStartDate)) ? NepEngDate.EngToNep(Convert.ToDateTime(leaveYearD.YearStartDate)) : null;
            leaveYearD.YearEndDateNp = !String.IsNullOrEmpty(Convert.ToString(leaveYearD.YearEndDate)) ? NepEngDate.EngToNep(Convert.ToDateTime(leaveYearD.YearEndDate)) : null;

            return View(leaveYearD);
        }

        [Route("leaveyear/edit/{id}")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult LeaveYearEdit(LeaveYearDTO editLeaveYear)
        {
            editLeaveYear.YearStartDate = !String.IsNullOrEmpty(editLeaveYear.YearStartDateNp) ? Convert.ToDateTime(NepEngDate.NepToEng(editLeaveYear.YearStartDateNp)) : editLeaveYear.YearStartDate;
            editLeaveYear.YearEndDate = !String.IsNullOrEmpty(editLeaveYear.YearEndDateNp) ? Convert.ToDateTime(NepEngDate.NepToEng(editLeaveYear.YearEndDateNp)) : editLeaveYear.YearEndDate;
            string msg = "failed";
            //editLeaveYear.YearStartDateNp =
            //    DateTimeConverter.DateInBS(editLeaveYear.YearStartDate != null ? Convert.ToDateTime(editLeaveYear.YearStartDate) : DateTime.Now);
            //editLeaveYear.YearEndDateNp =
            //    DateTimeConverter.DateInBS(editLeaveYear.YearEndDate != null ? Convert.ToDateTime(editLeaveYear.YearEndDate) : DateTime.Now);
            int response = _leaveYearService.UpdateLeaveYear(editLeaveYear);
            if (response > 0)
            {
                return RedirectToAction("LeaveYearEdit", "LeaveYears", new { id = editLeaveYear.YearId, msg = "done" });
            }
            else
            {
                return RedirectToAction("LeaveYearEdit", "LeaveYears", new { id = editLeaveYear.YearId, msg });
            }
        }

        [Route("leaveyear/delete/{id}")]
        public RedirectResult LeaveYearDelete(int id)
        {
            _leaveYearService.DeleteLeaveYear(id);
            return Redirect("/leaveyears");
        }
        #endregion
    }
}