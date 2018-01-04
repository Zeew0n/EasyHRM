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
    public class DarbandiController : BaseController
    {
        // GET: Darbandi
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

        public DarbandiController()
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





        [Route("Darbandi")]
        public ActionResult Darbandi()
        {

            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }

            try
            {

                IEnumerable<DarbandiDTO> DarbandiList = _darbandiService.GetDarbandilist();
                return View(DarbandiList);

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                throw ex;
            }
        }

        [HttpGet]
        [Route("Darbandi/Create")]
        public ActionResult DarbandiCreate()
        {
            try
            {
                if (ViewBag.AllowCreate)
                {
                    ViewBag.officeList = _officeService.GetOfficeData();
                    ViewBag.desgList = _designationServices.GetDesignationList();
                    return View();
                }
                else
                {
                    ViewBag.Error = "You are not Authorize to use this Page";
                    return PartialView("_partialviewNotFound");
                }

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                throw ex;
            }

        }

        [HttpPost]
        [Route("Darbandi/Create")]
        public ActionResult DarbandiCreate(DarbandiDTO data)
        {
            data.DarbandiDate = !string.IsNullOrEmpty(data.DarbandiDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.DarbandiDateNP)) : data.DarbandiDate;

            try
            {
                if (ViewBag.AllowCreate)
                {
                    DarbandiDTO darb = new DarbandiDTO();
                    if (!ModelState.IsValid)
                    {
                        ViewBag.officeList = _officeService.GetOfficeData();
                        ViewBag.desgList = _designationServices.GetDesignationList();
                        return View(darb);
                    }
                    DarbandiDTO resDarbandiDTO = new DarbandiDTO();
                    resDarbandiDTO = _darbandiService.InsertDarabandi(data);
                    ViewBag.officeList = _officeService.GetOfficeData();
                    ViewBag.desgList = _designationServices.GetDesignationList();
                    ViewBag.success = String.Format("New Darbandi Added");
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ViewBag.Error = "You are not Authorize to use this Page";
                    return PartialView("_partialviewNotFound");
                }

            }
            catch (Exception Ex)
            {
                ViewBag.error = Ex.Message;
                return View();
            }
        }

        [HttpGet]
        [Route("Darbandi/Edit/{DarbandiId}")]
        public ActionResult DarbandiEdit(int DarbandiId)
        {
            try
            {
                if (ViewBag.AllowEdit)
                {
                    ViewBag.officeList = _officeService.GetOfficeData();
                    ViewBag.desgList = _designationServices.GetDesignationList();
                    DarbandiDTO DarbandiById = _darbandiService.GetDarbandiById(DarbandiId);
                    DarbandiById.DarbandiDateNP = !String.IsNullOrEmpty(Convert.ToString(DarbandiById.DarbandiDate)) ? NepEngDate.EngToNep(Convert.ToDateTime(DarbandiById.DarbandiDate)) : null;

                    ModelState.Clear();
                    return View(DarbandiById);
                }
                else
                {
                    ViewBag.Error = "You are not Authorize to use this Page";
                    return PartialView("_partialviewNotFound");
                }
            }
            catch (Exception Ex)
            {
                ViewBag.error = Ex.Message;
                throw Ex;
            }

        }

        //[HttpPost]
        //[Route("Darbandi/Edit/{DarbandiId}")]
        //public RedirectResult DarbandiEdit(DarbandiDTO data)
        //{
        //    //int reDesg = _designationServices.UpdateDesignation(data);
        //    int reDarbandi = _darbandiService.UpdateDarabandi(data);
        //    ViewBag.success = "Darbandi updated successfully";
        //    return Redirect("/Darbandi/Edit/" + data.DarbandiId);
        //}

        [HttpPost]
        [Route("Darbandi/Edit/{DarbandiId}")]
        public ActionResult DarbandiEdit(DarbandiDTO data)
        {
            data.DarbandiDate = !string.IsNullOrEmpty(data.DarbandiDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.DarbandiDateNP)) : data.DarbandiDate;

            try
            {
                if (ViewBag.AllowEdit)
                {
                    if (!ModelState.IsValid)
                    {
                        ViewBag.officeList = _officeService.GetOfficeData();
                        ViewBag.desgList = _designationServices.GetDesignationList();
                        return View(data);
                    }
                    int reDarbandi = _darbandiService.UpdateDarabandi(data);
                    ViewBag.officeList = _officeService.GetOfficeData();
                    ViewBag.desgList = _designationServices.GetDesignationList();
                    ViewBag.success = "Darbandi updated successfully";
                    ModelState.Clear();
                    return View("DarbandiEdit", data);
                }
                else
                {
                    ViewBag.Error = "You are not Authorize to use this Page";
                    return PartialView("_partialviewNotFound");
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                throw ex;
            }
            //int reDesg = _designationServices.UpdateDesignation(data);

        }

    }
}