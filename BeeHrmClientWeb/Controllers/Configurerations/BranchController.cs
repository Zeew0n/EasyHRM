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
    public class BranchController : BaseController
    {
        // GET: Branch

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

        public BranchController()
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
        #region Branch
        [Route("Branch")]
        public ActionResult index()

        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }


            IEnumerable<OfficeDTOs> officeList = _officeService.GetOfficeData();
            List<OfficeDTOs> officeLists = new List<OfficeDTOs>();
            foreach (var item in officeList)
            {
                item.parentOfficeName = _officeService.GetOfficeName(Convert.ToInt32(item.OfficeParentId));

                officeLists.Add(item);
            }

            return View(officeLists);

        }
        [Route("Branch/{id}")]
        public ActionResult BranchIndex(string id)
        {
            //if (!ViewBag.AllowView)
            //{
            //    ViewBag.Error = "You are not Authorize to use this Page";
            //    return PartialView("_partialviewNotFound");
            //}


            IEnumerable<OfficeDTOs> officeList = _officeService.getBranchOfficeData(id);
            List<OfficeDTOs> officeLists = new List<OfficeDTOs>();
            foreach (var item in officeList)
            {
                item.parentOfficeName = _officeService.GetOfficeName(Convert.ToInt32(item.OfficeParentId));
                //item.branchList = _officeService.getBranchOfficeData(item.OfficeId.ToString());
                officeLists.Add(item);


            }

            return View(officeLists);
        }
        [HttpGet]
        [Route("Branch/Create")]
        public ActionResult BranchCreate()
        {
            //if (!ViewBag.AllowCreate)
            //{
            //    ViewBag.Error = "You are not Authorize to use this Page";
            //    return PartialView("_partialviewNotFound");
            //}

            ViewBag.officeList = _officeService.GetOfficeData();
            ViewBag.officeTypeList = _officeTypeService.GetOfficeTypes();

            OfficeDTOs officeData = new OfficeDTOs();
            officeData.EmployeeList = _officeService.GetEmployeeList();
            officeData.RemoteList = _officeService.GetRemoteList();
            return View(officeData);
        }
        [HttpPost]
        [Route("Branch/Create")]
        public ActionResult BranchCreate(OfficeDTOs data)
        {
            //if (!ViewBag.AllowCreate)
            //{
            //    ViewBag.Error = "You are not Authorize to use this Page";
            //    return PartialView("_partialviewNotFound");
            //}
            OfficeDTOs officeData = new OfficeDTOs();
            try
            {
                ViewBag.officeList = _officeService.GetOfficeData();
                ViewBag.officeTypeList = _officeTypeService.GetOfficeTypes();

                officeData.EmployeeList = _officeService.GetEmployeeList();
                officeData.RemoteList = _officeService.GetRemoteList();

                OfficeDTOs resOfficeDTO = new OfficeDTOs();
                resOfficeDTO = _officeService.InsertOffice(data);

                ViewBag.success = String.Format("New Branch Added");
                ModelState.Clear();
                return View(officeData);
            }
            catch (Exception Ex)
            {
                ViewBag.error = Ex.Message;
                return View(officeData);
            }
        }

        [HttpGet]
        [Route("branch/edit/{officeId}")]
        public ActionResult BranchEdit(int officeId)
        {
            ViewBag.officeList = _officeService.GetOfficeData();
            ViewBag.officeTypeList = _officeTypeService.GetOfficeTypes();
            OfficeDTOs officebyID = _officeService.GetOfficeById(officeId);
            officebyID.EmployeeList = _officeService.GetEmployeeList();
            officebyID.RemoteList = _officeService.GetRemoteList();
            return View(officebyID);
        }

        [HttpPost]
        [Route("branch/edit/{officeId}")]
        public ActionResult BranchEdit(OfficeDTOs data)
        {
            OfficeDTOs officeData = new OfficeDTOs();

            ViewBag.officeList = _officeService.GetOfficeData();
            ViewBag.officeTypeList = _officeTypeService.GetOfficeTypes();

            officeData.EmployeeList = _officeService.GetEmployeeList();
            officeData.RemoteList = _officeService.GetRemoteList();
            int reOffice = _officeService.UpdateDarbandi(data);
            ViewBag.success = String.Format("Darbandi Updated");
            ModelState.Clear();
            return View(officeData);
        }
        #endregion
    }
}