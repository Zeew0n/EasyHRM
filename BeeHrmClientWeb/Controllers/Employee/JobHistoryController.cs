using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using BeeHrmClientWeb.CodeBase;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using System;
using BeeHrmClientWeb.Utilities;
using BeeHrmInterface.GlobalSelectLists;

namespace BeeHrmClientWeb.Controllers.Employee
{
    public class JobHistoryController : BaseController
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

        public JobHistoryController()
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
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();

        }
        public void ListOfDatas(int empCode)
        {
            ViewBag.EmployeeDetail = _employeeService.GetEmployeeDetails(empCode);
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(empCode);
            List<SelectListItem> OfficeList = new List<SelectListItem>();
            foreach (var row in _officeService.GetOfficeAllData())
            {
                OfficeList.Add(new SelectListItem
                {
                    Text = row.OfficeName,
                    Value = row.OfficeId.ToString()
                });
            }
            List<SelectListItem> DepartmentList = new List<SelectListItem>();
            foreach (var row in _departmentServices.GetDepartmentlist())
            {
                DepartmentList.Add(new SelectListItem
                {
                    Text = row.DeptName,
                    Value = row.DeptId.ToString()
                });
            }
            List<SelectListItem> SectionList = new List<SelectListItem>();
            foreach (var row in _sectionServices.GetSectionList())
            {
                SectionList.Add(new SelectListItem
                {
                    Text = row.SectionName,
                    Value = row.SectionId.ToString()
                });
            }
            List<SelectListItem> DesignationList = new List<SelectListItem>();
            foreach (var row in _designationServices.GetDesignationList())
            {
                DesignationList.Add(new SelectListItem
                {
                    Text = row.DsgName,
                    Value = row.DsgId.ToString()
                });
            }
            List<SelectListItem> RankList = new List<SelectListItem>();
            foreach (var row in _rankServices.GetRankList())
            {
                RankList.Add(new SelectListItem
                {
                    Text = row.RankName.ToString(),
                    Value = row.RankId.ToString()
                });
            }
            List<SelectListItem> LevelList = new List<SelectListItem>();
            foreach (var row in _levelServices.GetLevellist())
            {
                LevelList.Add(new SelectListItem
                {
                    Text = row.LevelName,
                    Value = row.LevelId.ToString()
                });
            }
            List<SelectListItem> BusinessGroupList = new List<SelectListItem>();
            foreach (var row in _bgGroupService.GetBusinessGroupList())
            {
                BusinessGroupList.Add(new SelectListItem
                {
                    Text = row.BgName,
                    Value = row.BgId.ToString()
                });
            }
            List<SelectListItem> ShiftList = new List<SelectListItem>();
            foreach (var row in _shiftService.GetShiftsLIst())
            {
                ShiftList.Add(new SelectListItem
                {
                    Text = row.ShiftName,
                    Value = row.ShiftId.ToString()
                });
            }
            List<SelectListItem> RemoteList = new List<SelectListItem>();
            foreach (var row in _remoteService.GetRemoteList())
            {
                RemoteList.Add(new SelectListItem
                {
                    Text = row.RemoteAreaName,
                    Value = row.RemoteId.ToString()
                });
            }
            List<SelectListItem> JobTypeList = new List<SelectListItem>();
            foreach (var row in _jobTypeservices.GetJobTypeList())
            {
                JobTypeList.Add(new SelectListItem
                {
                    Text = row.JobTypeName,
                    Value = row.JobtypeId.ToString()
                });
            }
            List<SelectListItem> ServiceEventList = new List<SelectListItem>();
            foreach (var row in _serviceEventService.GetServiceEventList())
            {
                ServiceEventList.Add(new SelectListItem
                {
                    Text = row.ServiceEventGroupName,
                    Value = row.ServiceEventId.ToString()
                });
            }
            List<SelectListItem> ServiceEventSubGroupList = new List<SelectListItem>();
            foreach (var row in _serviceEventSubGroupService.GetSubGroupById(1))
            {
                ServiceEventSubGroupList.Add(new SelectListItem
                {
                    Text = row.ServiceEventSubGroupName,
                    Value = row.ServiceEventSubGroupId.ToString()
                });
            }
            var EmployeesList = _employeeService.GetEmployeeSelectList();
            ViewBag.officeList = OfficeList;
            ViewBag.departmentList = DepartmentList;
            ViewBag.sectionList = SectionList;
            ViewBag.designationList = DesignationList;
            ViewBag.rankList = RankList;
            ViewBag.levelList = LevelList;
            ViewBag.businessGroupList = BusinessGroupList;
            ViewBag.shiftList = ShiftList;
            ViewBag.remoteList = RemoteList;
            ViewBag.jobTypeList = JobTypeList;
            ViewBag.serviceEventList = ServiceEventList;
            ViewBag.employeeList = EmployeesList;
            ViewBag.ServiceEventSubGroupList = ServiceEventSubGroupList;


        }

        #region History of Employee
        [Route("history/{empCode}")]
        public ActionResult History(int empCode)
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            ListOfDatas(empCode);
            List<EmployeeJobHistoryDTO> res = _jobHistoryService.GetAllHistoryOfEmployee(empCode);
           
         
            return View("History/Index", res);
        }

        [Route("jobhistory/{empCode}/HistoryDetails/{id}")]
        public ActionResult HistoryDetail(int empCode, int id)
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            ListOfDatas(empCode);
            EmployeeJobHistoryDTO res = _jobHistoryService.GetJobHistoryById(id);
            return View("History/Details", res);
        }
        [Route("jobhistory/{empCode}/HistoryEdit/{id}")]
        public ActionResult HistoryEdit(int empCode, int id)
        {
            if (!ViewBag.AllowEdit)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            ListOfDatas(empCode);
            EmployeeJobHistoryDTO res = _jobHistoryService.GetJobHistoryById(id);
            JobHistoryViewModel data = new JobHistoryViewModel();
            data.EmployeeDetails = _employeeService.GetEmployeeDetails(empCode);
            data.JobHistories = _jobHistoryService.GetJobHistoryById(id);
            data.JobHistories.DecisionDateNP = !String.IsNullOrEmpty(Convert.ToString(data.JobHistories.DecisionDate)) ? NepEngDate.EngToNep(Convert.ToDateTime(data.JobHistories.DecisionDate)) : null;
            data.JobHistories.LetterIssueDateNP = !String.IsNullOrEmpty(Convert.ToString(data.JobHistories.LetterIssueDate)) ? NepEngDate.EngToNep(Convert.ToDateTime(data.JobHistories.LetterIssueDate)) : null;
            data.JobHistories.EffectiveDateNP = !String.IsNullOrEmpty(Convert.ToString(data.JobHistories.EffectiveDate)) ? NepEngDate.EngToNep(Convert.ToDateTime(data.JobHistories.EffectiveDate)) : null;
            data.JobHistories.EffectiveTillDateNP = !String.IsNullOrEmpty(Convert.ToString(data.JobHistories.EffectiveTillDate)) ? NepEngDate.EngToNep(Convert.ToDateTime(data.JobHistories.EffectiveTillDate)) : null;
            data.JobHistories.ServiceHolidingDateNP = !String.IsNullOrEmpty(Convert.ToString(data.JobHistories.ServiceHolidingDate)) ? NepEngDate.EngToNep(Convert.ToDateTime(data.JobHistories.ServiceHolidingDate)) : null;
            data.JobHistories.OfficeJoinDateNP = !String.IsNullOrEmpty(Convert.ToString(data.JobHistories.OfficeJoinDate)) ? NepEngDate.EngToNep(Convert.ToDateTime(data.JobHistories.OfficeJoinDate)) : null;
            data.JobHistories.SadarDateNP = !String.IsNullOrEmpty(Convert.ToString(data.JobHistories.SadarDate)) ? NepEngDate.EngToNep(Convert.ToDateTime(data.JobHistories.SadarDate)) : null;

            List<SelectListItem> SubGroupList = new List<SelectListItem>();
            foreach (var row in _serviceEventSubGroupService.GetSubGroupById((int)res.ServiceEventGroupId))
            {
                SubGroupList.Add(new SelectListItem
                {
                    Text = row.ServiceEventSubGroupName,
                    Value = row.ServiceEventSubGroupId.ToString()
                });
            }
            ViewBag.SubGroupList = SubGroupList;
            ViewBag.remoteList = StaticSelectList.RemoteAreaTypeList();

            switch (res.ServiceEventGroupId)
            {
                case 2: return View("../Employee/Transfer/Edit", data);
                case 3: return View("../Employee/Kajmakhataune/Edit", data);
                case 7:return View("../Employee/Badhuwa/Edit", data);
                case 5:return View("../Employee/Punishment/Edit", data);
                case 6:

                    return View("../Employee/Abakas/Edit", data);
            }
            return RedirectToAction("EmployeeDetailsAdmin", new { id = empCode });
        }


        public ActionResult EmployeeNiyukti(int id=0)
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            EmployeeDetailsViewModel partialData = _employeeService.GetEmployeeDetails(id);
            EmployeeJobHistoryViewModel res = new EmployeeJobHistoryViewModel();
            res.Designation = partialData.Designation;
            res.JobType = partialData.JobType;
            res.Department = partialData.Department;
            res.OfficeName = partialData.OfficeName;
            res.Rank = partialData.Rank;
            res.Name = partialData.Name;
            res.Section = partialData.Section;
            res.Level = partialData.Level;
            ListOfDatas(id);
           

            var empDefault = _employeeService.EmployeesIds(id);
            res.OfficeId = empDefault.EmpOfficeId;
            res.DeptId = empDefault.EmpDeptId;
            res.DesgId = empDefault.EmpDesgId;
            res.SectionId = empDefault.EmpSectionId;
            res.RankId = empDefault.EmpRankId;
            res.LevelId = empDefault.EmpLevelId;
            res.BusinessGroupId = empDefault.EmpBgId;
            res.ShiftId = empDefault.EmpShiftId;
            res.JobTypeId = empDefault.EmpJobTypeId;


            return View("../JobHistory/History/Niyukti", res);
        }
        [HttpPost]
        public ActionResult EmployeeNiyukti(int id, EmployeeJobHistoryViewModel data)
        {
            data.DecisionDate = !string.IsNullOrEmpty(data.DecisionDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.DecisionDateNP)) : data.DecisionDate;
            data.LetterIssueDate = !string.IsNullOrEmpty(data.LetterIssueDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.LetterIssueDateNP)) : data.LetterIssueDate;
            data.EffectiveDate = !string.IsNullOrEmpty(data.EffectiveDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.EffectiveDateNP)) : data.EffectiveDate;
            data.ServiceCountingFromDate = !string.IsNullOrEmpty(data.ServiceCountingFromDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.ServiceCountingFromDateNP)) : data.ServiceCountingFromDate;
            data.OfficeJoinDate = !string.IsNullOrEmpty(data.OfficeJoinDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.OfficeJoinDateNP)) : data.OfficeJoinDate;
            data.SadarDate = !string.IsNullOrEmpty(data.SadarDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.SadarDateNP)) : data.SadarDate;

            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            ListOfDatas(id);
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("../JobHistory/History/Niyukti", data);
                }
                EmployeeJobHistoryDTO dataToInsert = new EmployeeJobHistoryDTO();
                dataToInsert.EmpCode = id;
                dataToInsert.Instruction = data.Instruction;
                dataToInsert.JobTypeId = data.JobTypeId;
                dataToInsert.LetterRefNo = data.LetterChalaniNumber;
                dataToInsert.LetterIssueDate = data.LetterIssueDate;
                dataToInsert.LevelId = data.LevelId;
                dataToInsert.OfficeId = data.OfficeId;
                dataToInsert.OfficeJoinDate = data.OfficeJoinDate;
                dataToInsert.RankId = data.RankId;
                dataToInsert.RemoteId = data.RemoteId;
                dataToInsert.SadarDate = data.SadarDate;
                dataToInsert.SadarGarneEmployeeCode = data.SadarGarneEmployeeCode;
                dataToInsert.SectionId = data.SectionId;
                dataToInsert.ServiceHolidingDate = data.ServiceCountingFromDate;
                dataToInsert.ServiceEvent = data.ServiceEvent;
                dataToInsert.Instruction = data.Instruction;
                dataToInsert.ServiceEventGroupId = 1;
                dataToInsert.ServiceEventSubGroupId = data.ServiceEventSubGroupId;
                dataToInsert.ShiftId = data.ShiftId;
                dataToInsert.DeptId = data.DeptId;
                dataToInsert.DesgId = data.DesgId;
                dataToInsert.DesgKayamMukayamMuKaRaRa = data.DesgKayamMukayamMuKaRaRa;
                dataToInsert.EffectiveDate = data.EffectiveDate;
                dataToInsert.EffectiveTillDate = data.EffectiveTillDate;
                dataToInsert.BusinessGroupId = data.BusinessGroupId;
                dataToInsert.DecisionDate = data.DecisionDate;
                int res = _jobHistoryService.InsertJobHistoryForNiyukti(dataToInsert);
                return Redirect("/Admin/UserDetail/" + id);

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("../Employee/Transfer/Index", data);
            }
        }
        #endregion

    }
}