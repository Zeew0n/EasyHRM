using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Reports
{
    public class EmployeeAdvanceSearchController : BaseController
    {
        private IEmployeeService _employeeService;
        private IJobTypeService _jobTypeservices;
        private IDepartmentService _departmentServices;
        private IDesignationService _designationServices;
        private ILeaveRuleService _leaveRuleServices;
        private ILevelService _levelServices;
        private ISectionService _sectionServices;
        private IGroupService _groupServices;
        private IRankService _rankServices;
        private IShiftService _shiftService;
        private IServiceEventService _serviceEventService;
        private IOfficeServices _officeTypeServices;
        private IModulServices _moduleService;
        private IBusinessGroupService _bgGroupService;
        private IEthnicityService _ethService;
        private IReligionService _religionService;
        private IEmployeeVisitService _employeeVisitService;
        private ICountryService _countryService;
        private IRolesService _rolesService;
        private IEmployeePrizeService _empPrizeService;
        private IJobHistoryService _jobHistoryService;
        private IOfficeServices _officeService;
        private IRemoteAreaService _remoteService;
        private IEmployeeTrainingService _employeeTrainingService;
        private IServiceEventSubGroupService _serviceEventSubGroupService;
        private IEmpEducationService _empEducationService;
        private ITaxSetupService _taxsetupservice;
        public EmployeeAdvanceSearchController()
        {
            _employeeService = new EmployeeService();
            _officeTypeServices = new OfficeServices();
            _departmentServices = new DepartmentService();
            _designationServices = new DesignationService();
            _leaveRuleServices = new LeaveRuleService();
            _levelServices = new LevelService();
            _sectionServices = new SectionService();
            _employeeService = new EmployeeService();
            _jobTypeservices = new JobTypeService();
            _rankServices = new RankService();
            _shiftService = new ShiftService();
            _serviceEventService = new ServiceEventGroupService();
            _groupServices = new GroupService();
            _moduleService = new ModuleService();
            _bgGroupService = new BusinessGroupService();
            _ethService = new EthnicityService();
            _religionService = new ReligionService();
            _employeeVisitService = new EmployeeVisitService();
            _countryService = new CountryService();
            _rolesService = new RolesService();
            _empPrizeService = new EmployeePrizeService();
            _jobHistoryService = new JobHistoryService();
            _officeService = new OfficeServices();
            _remoteService = new RemoteAreaService();
            _employeeTrainingService = new EmployeeTrainingService();
            _serviceEventSubGroupService = new ServiceEventSubGroupService();
            _empEducationService = new EmpEducationService();
            _taxsetupservice = new TaxSetupService();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();

        }

        public ActionResult Index(int? page, string EmpName, int? SearchEmpCode, int? OfficeId, int? DeptId, int? DesgId, int? GroupId, int? BgId, int? LevelId, int? RankId, int? ShiftId, int? SectionId, int? JobTypeId)
        {

            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            if (ViewBag.AllowView == true)
            {
                ViewBag.dllOfficeTypeList = _officeTypeServices.GetClildOfficeListByEmpCode(Convert.ToInt32(ViewBag.EmpCode));
                ViewBag.dllDepartmentList = _departmentServices.GetDepartmentlist();
                ViewBag.dllDesginationList = _designationServices.GetDesignationList().ToList();
                ViewBag.ddlGroupList = _groupServices.GetGroupList();
                ViewBag.ddlBgGroupList = _bgGroupService.GetBusinessGroupByEmpRoleId(Convert.ToInt32(ViewBag.EmpRoleId));

                ViewBag.dllLevelList = _levelServices.GetLevellist();
                ViewBag.dllRankList = _rankServices.GetRankList();
                ViewBag.dllShiftList = _shiftService.GetShiftsLIst();
                ViewBag.dllSectionList = _sectionServices.GetSectionList();
                ViewBag.dllJobTypeList = _jobTypeservices.GetJobTypeList();


                int roleId = Convert.ToInt32(ViewBag.EmpRoleId);
                // // int curentemp = Convert.ToInt32(Session["Empcode"]); //Logged in user employee code
                //   var emplist = _employeeService.GetEmployeeList(curentemp);
                EmployeeSearchViewModel searchModel = new EmployeeSearchViewModel();

                ViewBag.SearchEmpCode = SearchEmpCode;
                ViewBag.EmpName = EmpName;
                ViewBag.BgId = BgId;
                ViewBag.OfficeId = OfficeId;
                ViewBag.DesgId = DesgId;
                ViewBag.GroupId = GroupId;
                ViewBag.LevelId = LevelId;
                ViewBag.RankId = RankId;
                ViewBag.ShiftId = ShiftId;
                ViewBag.SectionId = SectionId;
                ViewBag.JobTypeId = JobTypeId;

                var emplist = _employeeService.
                    SearchEmployees(SearchEmpCode, EmpName, OfficeId,
                        DeptId, DesgId, GroupId, BgId, roleId);
                if (LevelId > 0)
                {
                    emplist = emplist.Where(a => a.EmpLevelId == searchModel.LevelId).ToList();
                }
                if (RankId > 0)
                {
                    emplist = emplist.Where(a => a.EmpRankId == searchModel.RankId).ToList();
                }
                if (ShiftId > 0)
                {
                    emplist = emplist.Where(a => a.EmpShiftId == searchModel.ShiftId).ToList();
                }
                if (SectionId > 0)
                {
                    emplist = emplist.Where(a => a.EmpSectionId == searchModel.SectionId).ToList();
                }
                if (JobTypeId > 0)
                {
                    emplist = emplist.Where(a => a.EmpJobTypeId == searchModel.JobTypeId).ToList();
                }
                int pageSizes = 20;

                int pageNumber = (page ?? 1);
                return View(emplist.ToPagedList(pageNumber, pageSizes));
            }
            else
            {
                ViewBag.errorMsg = "Not Authorize";
                return View();
            }
        }

        [HttpPost]

        public ActionResult Index(EmployeeSearchViewModel searchModel, string sortOrder, string currentFilter, string searchString, int? page, string pageSize)
        {


            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            if (ViewBag.AllowView == true)
            {
                ViewBag.Test = searchModel.OfficeId;
                ViewBag.dllOfficeTypeList = _officeTypeServices.GetOfficeListByEmpRole(Convert.ToInt32(ViewBag.EmpRoleId));
                ViewBag.dllDepartmentList = _departmentServices.GetDepartmentlist();
                ViewBag.dllDesginationList = _designationServices.GetDesignationList().ToList();
                ViewBag.ddlGroupList = _groupServices.GetGroupList();
                ViewBag.ddlBgGroupList = _bgGroupService.GetBusinessGroupByEmpRoleId(Convert.ToInt32(ViewBag.EmpRoleId));

                ViewBag.dllLevelList = _levelServices.GetLevellist();
                ViewBag.dllRankList = _rankServices.GetRankList();
                ViewBag.dllShiftList = _shiftService.GetShiftsLIst();
                ViewBag.dllSectionList = _sectionServices.GetSectionList();
                ViewBag.dllJobTypeList = _jobTypeservices.GetJobTypeList();

                ViewBag.SearchEmpCode = searchModel.EmpCode;
                ViewBag.EmpName = searchModel.EmpName;
                ViewBag.BgId = searchModel.BgId;
                ViewBag.OfficeId = searchModel.OfficeId;
                ViewBag.DesgId = searchModel.DesgId;
                ViewBag.DeptId = searchModel.DeptId;
                ViewBag.GroupId = searchModel.GroupId;

                ViewBag.LevelId = searchModel.LevelId;
                ViewBag.RankId = searchModel.RankId;
                ViewBag.ShiftId = searchModel.ShiftId;
                ViewBag.SectionId = searchModel.SectionId;
                ViewBag.JobTypeId = searchModel.JobTypeId;


                int roleId = Convert.ToInt32(ViewBag.EmpRoleId);
                var emplist = _employeeService.
                    SearchEmployees(searchModel.EmpCode, searchModel.EmpName, searchModel.OfficeId,
                        searchModel.DeptId, searchModel.DesgId, searchModel.GroupId, searchModel.BgId, roleId);

                if (searchModel.LevelId > 0)
                {
                    emplist = emplist.Where(a => a.EmpLevelId == searchModel.LevelId).ToList();
                }
                if (searchModel.RankId > 0)
                {
                    emplist = emplist.Where(a => a.EmpRankId == searchModel.RankId).ToList();
                }
                if (searchModel.ShiftId > 0)
                {
                    emplist = emplist.Where(a => a.EmpShiftId == searchModel.ShiftId).ToList();
                }
                if (searchModel.SectionId > 0)
                {
                    emplist = emplist.Where(a => a.EmpSectionId == searchModel.SectionId).ToList();
                }
                if (searchModel.JobTypeId > 0)
                {
                    emplist = emplist.Where(a => a.EmpJobTypeId == searchModel.JobTypeId).ToList();
                }
                int pageSizes = 20;

                int pageNumber = 1;

                return View(emplist.ToPagedList(pageNumber, pageSizes));
            }
            else
            {
                return View();
            }
        }

    }
}