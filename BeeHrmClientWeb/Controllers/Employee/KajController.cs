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

namespace BeeHrmClientWeb.Controllers.Employee
{
    public class KajController : BaseController
    {
        private IEmployeeService _employeeService;
        private IAttendanceDailyService _attendance;
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

        public KajController()
        {
            _employeeService = new EmployeeService();
            _attendance = new AttendanceDailyService();
            _officeTypeServices = new OfficeServices();
            _departmentServices = new DepartmentService();
            _designationServices = new DesignationService();
            _leaveRuleServices = new LeaveRuleService();
            _levelServices = new LevelService();
            _sectionServices = new SectionService();
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

        #region Kaj Ma Khataune

        [Route("kajmakhataune/{empCode}")]
        public ActionResult Index(int empCode)
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            ListOfDatas(empCode);
            List<EmployeeJobHistoryDTO> res = _jobHistoryService.GetAllHistoryOfEmployeeForKaaz(empCode);
            foreach (var item in res)
            {
                item.DesignationName = _designationServices.GetDesignationById((int)item.DesgId).DsgName;
                item.LetterIssueDateNP = (NepEngDate.EngToNep(Convert.ToDateTime(item.LetterIssueDate)));
                item.KajStartDateNP = (NepEngDate.EngToNep(Convert.ToDateTime(item.KajStartDate)));
                item.KajEndDateNP = (NepEngDate.EngToNep(Convert.ToDateTime(item.KajEndDate)));
            }

            ViewBag.EmpCode = empCode;
            return View("../Employee/kajmakhataune/Index", res);

        }
        [HttpGet]
        public ActionResult KajMaKhataune(int id)
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }

            EmployeeDetailsViewModel partialData = _employeeService.GetEmployeeDetails(id);
            EmployeeKaazViwModel res = new EmployeeKaazViwModel();
            res.Designation = partialData.Designation;
            res.JobType = partialData.JobType;
            res.Department = partialData.Department;
            res.OfficeName = partialData.OfficeName;
            res.Rank = partialData.Rank;
            res.Name = partialData.Name;
            res.Section = partialData.Section;
            res.Level = partialData.Level;
            res.Name = partialData.Name;
            res.EmpCode = id;
            ListOfDatas(id);
            return View("../Employee/kajmakhataune/Create", res);
        }


        [HttpPost]
        public ActionResult KajMaKhataune(EmployeeKaazViwModel data)
        {
            int empCode = data.EmpCode;
            data.LetterIssueDate = Convert.ToDateTime(NepEngDate.NepToEng(data.LetterIssueDateNP));
            data.KajStartDate = Convert.ToDateTime(NepEngDate.NepToEng(data.KajStartDateNP));
            data.KajEndDate = Convert.ToDateTime(NepEngDate.NepToEng(data.KajEndDateNP));
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            ListOfDatas(empCode);
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("../Employee/Kajmakhataune/Create", data);
                }
                EmployeeJobHistoryDTO dataToInsert = new EmployeeJobHistoryDTO();
                EmpAllCodesVIewmodels emp = _employeeService.EmployeesIds(empCode);
                //mandatory field 
                dataToInsert.EmpCode = empCode;
                dataToInsert.OfficeId = emp.EmpOfficeId;
                dataToInsert.DeptId = emp.EmpDeptId;
                dataToInsert.SectionId = emp.EmpSectionId;
                dataToInsert.DesgId = emp.EmpDesgId;
                dataToInsert.RankId = emp.EmpRankId;
                dataToInsert.LevelId = emp.EmpLevelId;
                dataToInsert.BusinessGroupId = emp.EmpBgId;
                dataToInsert.ShiftId = emp.EmpShiftId;
                dataToInsert.JobTypeId = emp.EmpJobTypeId;
                // Kazz releated field
                dataToInsert.LetterIssueDate = data.LetterIssueDate;
                dataToInsert.LetterRefNo = data.LetterRefNo;
                dataToInsert.ChalaniNumber = data.LetterChalaniNumber;
                dataToInsert.KajStartDate = data.KajStartDate;
                dataToInsert.KajEndDate = data.KajEndDate;
                dataToInsert.ServiceEventGroupId = 3;
                int ServiceEventSubGroupId = 0;
                if (data.KaajType == "U")
                {
                    ServiceEventSubGroupId = 10;
                }
                else
                {
                    ServiceEventSubGroupId = 11;
                }
                dataToInsert.ServiceEventSubGroupId = ServiceEventSubGroupId;
                dataToInsert.SadarGarneEmployeeCode = data.SadarGarneEmployeeCode;
                dataToInsert.Remarks = data.Remarks;
                dataToInsert.Instruction = data.Instruction;
                dataToInsert.SadarDate = data.LetterIssueDate;
                dataToInsert.KaajType = data.KaajType;
                int res = _jobHistoryService.InsertJobHistoryForKaj(dataToInsert);
                string sdate = data.KajStartDate.ToString();
                string edate = data.KajEndDate.ToString();
                string type = "insert";
                int att = _attendance.InsertKaajAttendance(empCode, sdate, edate, type);
                Session["sucessMgsKaaz"] = "Kaaj Record of employee " + empCode + " inserted Sucessfully.";

                return Redirect("/kajmakhataune/" + data.EmpCode);

            }
            catch (Exception ex)
            {

                Session["ErrorMgsKaaz"] = ex.Message;
                return View("../Employee/Kajmakhataune/Create", data);
            }
        }

        public ActionResult KajMaKhateuneEdit(int id)
        {
            EmployeeJobHistoryDTO Record = _jobHistoryService.GetJobHistoryById(id);
            EmployeeDetailsViewModel partialData = _employeeService.GetEmployeeDetails(Record.EmpCode);
            EmployeeKaazViwModel res = new EmployeeKaazViwModel();
            res.LetterIssueDateNP = (NepEngDate.EngToNep(Convert.ToDateTime(Record.LetterIssueDate)));
            res.KajStartDateNP = (NepEngDate.EngToNep(Convert.ToDateTime(Record.KajStartDate)));
            res.KajEndDateNP = (NepEngDate.EngToNep(Convert.ToDateTime(Record.KajEndDate)));
            res.Id = Record.HistoryId;
            res.LetterIssueDate = Record.LetterIssueDate;
            res.LetterRefNo = Record.LetterRefNo;
            res.LetterChalaniNumber = Record.ChalaniNumber;
            res.KajStartDate = Record.KajStartDate;
            res.KajEndDate = Record.KajEndDate;
            res.SadarGarneEmployeeCode = Record.SadarGarneEmployeeCode;
            res.KaajType = Record.KaajType;
            res.Instruction = Record.Instruction;
            res.Remarks = Record.Remarks;
            res.Designation = partialData.Designation;
            res.JobType = partialData.JobType;
            res.Department = partialData.Department;
            res.OfficeName = partialData.OfficeName;
            res.Rank = partialData.Rank;
            res.Name = partialData.Name;
            res.Section = partialData.Section;
            res.Level = partialData.Level;
            res.Name = partialData.Name;
            res.EmpCode = Record.EmpCode;
            ListOfDatas(Record.EmpCode);
            return View("../Employee/Kajmakhataune/Edit", res);
        }
        [HttpPost]
        public ActionResult KajMaKhateuneEdit(EmployeeKaazViwModel data)
        {
            int empCode = data.EmpCode;
            data.LetterIssueDate = Convert.ToDateTime(NepEngDate.NepToEng(data.LetterIssueDateNP));
            data.KajStartDate = Convert.ToDateTime(NepEngDate.NepToEng(data.KajStartDateNP));
            data.KajEndDate = Convert.ToDateTime(NepEngDate.NepToEng(data.KajEndDateNP));
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            ListOfDatas(empCode);
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("../Employee/Kajmakhataune/Edit", data);
                }
                EmployeeJobHistoryDTO dataToInsert = new EmployeeJobHistoryDTO();
                EmpAllCodesVIewmodels emp = _employeeService.EmployeesIds(empCode);
                //mandatory field 
                dataToInsert.HistoryId = data.Id;
                dataToInsert.EmpCode = empCode;
                dataToInsert.OfficeId = emp.EmpOfficeId;
                dataToInsert.DeptId = emp.EmpDeptId;
                dataToInsert.SectionId = emp.EmpSectionId;
                dataToInsert.DesgId = emp.EmpDesgId;
                dataToInsert.RankId = emp.EmpRankId;
                dataToInsert.LevelId = emp.EmpLevelId;
                dataToInsert.BusinessGroupId = emp.EmpBgId;
                dataToInsert.ShiftId = emp.EmpShiftId;
                dataToInsert.JobTypeId = emp.EmpJobTypeId;
                // Kazz releated field
                dataToInsert.LetterIssueDate = data.LetterIssueDate;
                dataToInsert.LetterRefNo = data.LetterRefNo;
                dataToInsert.ChalaniNumber = data.LetterChalaniNumber;
                dataToInsert.KajStartDate = data.KajStartDate;
                dataToInsert.KajEndDate = data.KajEndDate;
                dataToInsert.ServiceEventGroupId = 3;
                int ServiceEventSubGroupId = 0;
                if (data.KaajType == "U")
                {
                    ServiceEventSubGroupId = 10;
                }
                else
                {
                    ServiceEventSubGroupId = 11;
                }
                dataToInsert.ServiceEventSubGroupId = ServiceEventSubGroupId;
                dataToInsert.SadarGarneEmployeeCode = data.SadarGarneEmployeeCode;
                dataToInsert.Remarks = data.Remarks;
                dataToInsert.Instruction = data.Instruction;
                dataToInsert.SadarDate = data.LetterIssueDate;
                dataToInsert.KaajType = data.KaajType;

                //before updating get start and end date from database

                EmployeeJobHistoryDTO RecordUpdate = _jobHistoryService.GetJobHistoryById(data.Id);
                string updaeSdate = RecordUpdate.KajStartDate.ToString();
                string updaEdate = RecordUpdate.KajEndDate.ToString();
                string type = "onlyupdate";
                int att = _attendance.InsertKaajAttendance(empCode, updaeSdate, updaEdate, type);



                int res = _jobHistoryService.UpdateJobHistoryForKaj(dataToInsert);
                string sdate = data.KajStartDate.ToString();
                string edate = data.KajEndDate.ToString();
                type = "Insert";
                att = _attendance.InsertKaajAttendance(empCode, sdate, edate, type);
                Session["sucessMgsKaaz"] = "Kaaj Record of employee " + empCode + " Updated Sucessfully.";
                return Redirect("/kajmakhataune/" + data.EmpCode);

            }
            catch (Exception ex)
            {

                Session["ErrorMgsKaaz"] = ex.Message;
                return View("../Employee/Kajmakhataune/Edit", data);
            }
        }


        public ActionResult Details(int id)
        {
            EmployeeJobHistoryDTO Record = _jobHistoryService.GetJobHistoryById(id);
            EmployeeDetailsViewModel partialData = _employeeService.GetEmployeeDetails(Record.EmpCode);
            EmployeeKaazViwModel res = new EmployeeKaazViwModel();
            res.LetterIssueDateNP = (NepEngDate.EngToNep(Convert.ToDateTime(Record.LetterIssueDate)));
            res.KajStartDateNP = (NepEngDate.EngToNep(Convert.ToDateTime(Record.KajStartDate)));
            res.KajEndDateNP = (NepEngDate.EngToNep(Convert.ToDateTime(Record.KajEndDate)));
            res.Id = Record.HistoryId;
            res.LetterIssueDate = Record.LetterIssueDate;
            res.LetterRefNo = Record.LetterRefNo;
            res.LetterChalaniNumber = Record.ChalaniNumber;
            res.KajStartDate = Record.KajStartDate;
            res.KajEndDate = Record.KajEndDate;
            res.SadarGarneEmployeeCode = Record.SadarGarneEmployeeCode;
            res.KaajType = Record.KaajType;
            res.Instruction = Record.Instruction;
            res.Remarks = Record.Remarks;
            res.Designation = partialData.Designation;
            res.JobType = partialData.JobType;
            res.Department = partialData.Department;
            res.OfficeName = partialData.OfficeName;
            res.Rank = partialData.Rank;
            res.Name = partialData.Name;
            res.Section = partialData.Section;
            res.Level = partialData.Level;
            res.Name = partialData.Name;
            res.EmpCode = Record.EmpCode;
            ListOfDatas(Record.EmpCode);
            return View("../Employee/Kajmakhataune/Details", res);
        }
        #endregion
    }
}