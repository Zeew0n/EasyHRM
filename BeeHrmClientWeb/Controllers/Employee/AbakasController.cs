﻿using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Employee
{
    public class AbakasController : BaseController
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

        public AbakasController()
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
        }

        #region Abakas
        [Route("abakas/{empCode}")]
        public ActionResult Abakash(int empCode)
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            ListOfDatas(empCode);
            EmployeeDetailsViewModel partialData = _employeeService.GetEmployeeDetails(empCode);
            EmployeeJobHistoryViewModel res = new EmployeeJobHistoryViewModel();
            res.Designation = partialData.Designation;
            res.JobType = partialData.JobType;
            res.Department = partialData.Department;
            res.OfficeName = partialData.OfficeName;
            res.Rank = partialData.Rank;
            res.Name = partialData.Name;
            res.Section = partialData.Section;
            res.Level = partialData.Level;
            res.BusinessGroup = partialData.BusinessGroup;
            res.Shift = partialData.Shift;
            res.JobType = partialData.JobType;
            res.RemoteArea = partialData.RemoteArea;
            List<SelectListItem> SubGroupList = new List<SelectListItem>();
            foreach (var row in _serviceEventSubGroupService.GetSubGroupById(6))
            {
                SubGroupList.Add(new SelectListItem
                {
                    Text = row.ServiceEventSubGroupName,
                    Value = row.ServiceEventSubGroupId.ToString()
                });
            }
            ViewBag.SubGroupList = SubGroupList;
            return View("../Employee/Abakas/Index", res);
        }

        [Route("abakas/{empCode}")]
        [HttpPost]
        public ActionResult Abakash(int empCode, EmployeeJobHistoryViewModel data)
        {
            data.DecisionDate = !string.IsNullOrEmpty(data.DecisionDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.DecisionDateNP)) : data.DecisionDate;
            data.LetterIssueDate = !string.IsNullOrEmpty(data.LetterIssueDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.LetterIssueDateNP)) : data.LetterIssueDate;
            data.EffectiveDate = !string.IsNullOrEmpty(data.EffectiveDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.EffectiveDateNP)) : data.EffectiveDate;
            data.ServiceCountingFromDate = !string.IsNullOrEmpty(data.ServiceCountingFromDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.ServiceCountingFromDateNP)) : data.ServiceCountingFromDate;
            data.OfficeJoinDate = !string.IsNullOrEmpty(data.OfficeJoinDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.OfficeJoinDateNP)) : data.OfficeJoinDate;
            data.EffectiveTillDate = !string.IsNullOrEmpty(data.EffectiveTillDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.EffectiveTillDateNP)) : data.EffectiveTillDate;
            data.SadarDate = !string.IsNullOrEmpty(data.SadarDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.SadarDateNP)) : data.SadarDate;

            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }

            ListOfDatas(empCode);
            List<SelectListItem> SubGroupList = new List<SelectListItem>();
            foreach (var row in _serviceEventSubGroupService.GetSubGroupById(6))
            {
                SubGroupList.Add(new SelectListItem
                {
                    Text = row.ServiceEventSubGroupName,
                    Value = row.ServiceEventSubGroupId.ToString()
                });
            }
            ViewBag.SubGroupList = SubGroupList;
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("../Employee/Abakas/Index", data);
                }
                EmployeeJobHistoryDTO dataToInsert = new EmployeeJobHistoryDTO();
                dataToInsert.EmpCode = data.EmpCode;
                dataToInsert.Instruction = data.Instruction;
                dataToInsert.JobTypeId = data.JobTypeId;
                dataToInsert.LetterRefNo = data.LetterChalaniNumber;
                dataToInsert.LetterIssueDate = data.LetterIssueDate;
                dataToInsert.LevelId = data.LevelId;
                dataToInsert.OfficeId = data.OfficeId;
                dataToInsert.OfficeJoinDate = data.OfficeJoinDate;
                dataToInsert.RankId = data.RankId;
                dataToInsert.RemoteCode = data.RemoteCode;
                dataToInsert.SadarDate = data.SadarDate;
                dataToInsert.SadarGarneEmployeeCode = data.SadarGarneEmployeeCode;
                dataToInsert.SectionId = data.SectionId;
                dataToInsert.ServiceHolidingDate = data.ServiceCountingFromDate;
                dataToInsert.ServiceEvent = data.ServiceEvent;
                dataToInsert.ServiceEventGroupId = 6;
                dataToInsert.ShiftId = data.ShiftId;
                dataToInsert.DeptId = data.DeptId;
                dataToInsert.DesgId = data.DesgId;
                dataToInsert.DesgKayamMukayamMuKaRaRa = data.DesgKayamMukayamMuKaRaRa;
                dataToInsert.EffectiveDate = data.EffectiveDate;
                dataToInsert.EffectiveTillDate = data.EffectiveTillDate;
                dataToInsert.BusinessGroupId = data.BusinessGroupId;
                dataToInsert.DecisionDate = data.DecisionDate;
                dataToInsert.ServiceEventSubGroupId = data.ServiceEventSubGroupId;
                int res = _jobHistoryService.InsertJobHistoryForAbakash(dataToInsert);
                //EmployeeJobHistoryDTO value = _jobHistoryService.GetJobHistoryById(res);  Need to find solution for this
                return Redirect("/History/" + data.EmpCode);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("../Employee/Abakas/Index", data);
            }
        }

        [Route("Abakas/AbakashEdit/{id}")]
        [HttpPost]
        public ActionResult AbakashEdit(JobHistoryViewModel data)
        {
            //if (data.UpdateAsCurrent == true)
            //{
            //    _jobHistoryService.UpdateAsCurrent(data.JobHistories);
            //}
            data.JobHistories.DecisionDate = !string.IsNullOrEmpty(data.JobHistories.DecisionDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.JobHistories.DecisionDateNP)) : data.JobHistories.DecisionDate;
            data.JobHistories.LetterIssueDate = !string.IsNullOrEmpty(data.JobHistories.LetterIssueDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.JobHistories.LetterIssueDateNP)) : data.JobHistories.LetterIssueDate;
            data.JobHistories.EffectiveDate = !string.IsNullOrEmpty(data.JobHistories.EffectiveDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.JobHistories.EffectiveDateNP)) : data.JobHistories.EffectiveDate;
            data.JobHistories.ServiceHolidingDate = !string.IsNullOrEmpty(data.JobHistories.ServiceHolidingDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.JobHistories.ServiceHolidingDateNP)) : data.JobHistories.ServiceHolidingDate;
            data.JobHistories.OfficeJoinDate = !string.IsNullOrEmpty(data.JobHistories.OfficeJoinDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.JobHistories.OfficeJoinDateNP)) : data.JobHistories.OfficeJoinDate;
            data.JobHistories.EffectiveTillDate = !string.IsNullOrEmpty(data.JobHistories.EffectiveTillDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.JobHistories.EffectiveTillDateNP)) : data.JobHistories.EffectiveTillDate;
            data.JobHistories.SadarDate = !string.IsNullOrEmpty(data.JobHistories.SadarDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.JobHistories.SadarDateNP)) : data.JobHistories.SadarDate;

            if (!ViewBag.AllowEdit)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            int res = _jobHistoryService.UpdateAbakas(data.JobHistories);
            return Redirect("/History/" + data.JobHistories.EmpCode);
        }
        #endregion
    }
}