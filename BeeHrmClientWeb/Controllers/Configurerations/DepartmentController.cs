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
    public class DepartmentController : BaseController
    {
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
        public DepartmentController()
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

        #region Department
        [Route("Department")]
        public ActionResult Departments()
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }

            IEnumerable<DepartmentDTO> DepartmentList = _departmentServices.GetDepartmentlist();
            return View(DepartmentList);
        }

        [HttpGet]
        [Route("Departments/Create")]
        public ActionResult DepartmentCreate()
        {
            return View();
        }

        [HttpPost]
        [Route("Departments/Create")]
        [MultipleButton(Name = "action", Argument = "DeptCreate")]
        public ActionResult DepartmentCreate(DepartmentDTO data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(data);
                }
                DepartmentDTO resDeptDTO = new DepartmentDTO();
                resDeptDTO = _departmentServices.InsertDepartment(data);
                ViewBag.success = String.Format("The department with code {0} has been inserted.", resDeptDTO.DeptCode);
                ModelState.Clear();
                return View();
            }
            catch (Exception Ex)
            {
                ViewBag.error = Ex.Message;
                return View();
            }
        }

        [Route("Departments/Create")]
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "DeptCreateClose")]
        public ActionResult DepartmentCreateClose(DepartmentDTO data)
        {
            try
            {
                DepartmentDTO resDeptDTO = new DepartmentDTO();
                resDeptDTO = _departmentServices.InsertDepartment(data);
                ViewBag.Success = "New Deparment added";
                ModelState.Clear();
                return View("Departments", _departmentServices.GetDepartmentlist());
            }
            catch (Exception Ex)
            {
                ViewBag.error = Ex.Message;
                return View(data);
            }
        }

        [HttpGet]
        [Route("Departments/Edit/{DeptId}")]
        public ActionResult DepartmentEdit(int DeptId)
        {
            DepartmentDTO DepartmentListById = _departmentServices.GetDepartmentById(DeptId);

            return View(DepartmentListById);
        }

        [HttpPost]
        [Route("Departments/Edit/{DeptId}")]
        public ActionResult DepartmentEdit(DepartmentDTO data)
        {
            int reDept = _departmentServices.UpdateDepartment(data);
            ViewBag.Success = "Department updated successfully";
            ModelState.Clear();
            return View(data);
        }

        #endregion
    }
}