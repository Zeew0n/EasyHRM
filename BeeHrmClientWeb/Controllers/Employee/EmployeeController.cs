using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Models;
using BeeHrmClientWeb.Utilities;
using BeeHrmClientWeb.Utilities.Date;
using EvaluationApi.CrossCutting.Infrastructure.Email;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BeeHrmClientWeb.Controllers.Employee
{
    public class EmployeeController : BaseController
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
        public EmployeeController()
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

        private DropDownListViewmodelcs DropDownlist()
        {

            IEnumerable<OfficeDTOs> office = _officeTypeServices.GetOfficeData();
            IEnumerable<DepartmentDTO> department = _departmentServices.GetDepartmentlist();
            IEnumerable<LeaveRuleDTO> leaveRule = _leaveRuleServices.GetLeaveRulesList();
            IEnumerable<LevelDTO> level = _levelServices.GetLevellist();
            IEnumerable<SectionDTO> section = _sectionServices.GetSectionList();
            IEnumerable<GroupDTO> employeeGrp = _groupServices.GetGroupList().ToList();
            IEnumerable<JobTypeDTO> jobType = _jobTypeservices.GetJobTypeList();
            IEnumerable<RankDTO> rank = _rankServices.GetRankList();
            IEnumerable<ShiftDTO> shift = _shiftService.GetShiftsLIst();
            IEnumerable<ServiceEventGroupDTO> serviceEvent = _serviceEventService.GetServiceEventList();
            IEnumerable<DesignationDTO> desgn = _designationServices.GetDesignationList();
            IEnumerable<BusinessGroupDTO> businessgroup = _bgGroupService.GetBusinessGroupList();
            IEnumerable<TaxSetupDTO> taxrule = _taxsetupservice.GetAllTaxSetup();


            List<SelectListItem> officeList = new List<SelectListItem>();
            List<SelectListItem> departmentList = new List<SelectListItem>();
            List<SelectListItem> LeaveRuleList = new List<SelectListItem>();
            List<SelectListItem> LevelList = new List<SelectListItem>();
            List<SelectListItem> SectionList = new List<SelectListItem>();
            List<SelectListItem> EmployeeGroupList = new List<SelectListItem>();
            List<SelectListItem> JobtypeList = new List<SelectListItem>();
            List<SelectListItem> RankList = new List<SelectListItem>();
            List<SelectListItem> ShiftList = new List<SelectListItem>();
            List<SelectListItem> EventGroupList = new List<SelectListItem>();
            List<SelectListItem> DesginationList = new List<SelectListItem>();
            List<SelectListItem> BusinessGroupList = new List<SelectListItem>();
            List<SelectListItem> RoleList = new List<SelectListItem>();
            List<SelectListItem> SubEventGroupList = new List<SelectListItem>();
            List<SelectListItem> TaxRuleList = new List<SelectListItem>();

            officeList.Add(new SelectListItem
            {
                Text = "Select Office ",
                Value = ""
            });
            foreach (OfficeDTOs str in office)
            {
                officeList.Add(new SelectListItem
                {
                    Text = str.OfficeName,
                    Value = str.OfficeId.ToString()
                });
            }

            departmentList.Add(new SelectListItem
            {
                Text = "Select Department",
                Value = ""
            });
            foreach (DepartmentDTO str in department)
            {
                departmentList.Add(new SelectListItem
                {
                    Text = str.DeptName,
                    Value = str.DeptId.ToString()
                });
            }
            LeaveRuleList.Add(new SelectListItem
            {
                Text = "Select Leave Rule",
                Value = ""
            });
            foreach (LeaveRuleDTO str in leaveRule)
            {
                LeaveRuleList.Add(new SelectListItem
                {
                    Text = str.LeaveRuleName,
                    Value = str.LeaveRuleId.ToString()
                });
            }
            LevelList.Add(new SelectListItem
            {
                Text = "Select Level",
                Value = ""
            });
            foreach (LevelDTO str in level)
            {
                LevelList.Add(new SelectListItem
                {
                    Text = str.LevelName,
                    Value = str.LevelId.ToString()
                });
            }
            SectionList.Add(new SelectListItem
            {
                Text = "Select Section ",
                Value = ""
            });
            foreach (SectionDTO str in section)
            {
                SectionList.Add(new SelectListItem
                {
                    Text = str.SectionName,
                    Value = str.SectionId.ToString()
                });
            }
            EmployeeGroupList.Add(new SelectListItem
            {
                Text = "Select Group",
                Value = ""
            });
            foreach (GroupDTO str in employeeGrp)
            {
                EmployeeGroupList.Add(new SelectListItem
                {
                    Text = str.GroupName,
                    Value = str.GroupId.ToString()
                });
            }
            JobtypeList.Add(new SelectListItem
            {
                Text = "Select JobType",
                Value = ""
            });
            foreach (JobTypeDTO str in jobType)
            {
                JobtypeList.Add(new SelectListItem
                {
                    Text = str.JobTypeName,
                    Value = str.JobtypeId.ToString()
                });
            }

            RankList.Add(new SelectListItem
            {
                Text = "Select Rank ",
                Value = ""
            });
            foreach (RankDTO str in rank)
            {
                RankList.Add(new SelectListItem
                {
                    Text = str.RankName.ToString(),
                    Value = str.RankId.ToString()
                });
            }

            ShiftList.Add(new SelectListItem
            {
                Text = "Select Shift ",
                Value = ""
            });
            foreach (ShiftDTO str in shift)
            {
                ShiftList.Add(new SelectListItem
                {
                    Text = str.ShiftName,
                    Value = str.ShiftId.ToString()
                });
            }

            EventGroupList.Add(new SelectListItem
            {
                Text = "Select Service Event Group",
                Value = ""
            });
            foreach (ServiceEventGroupDTO str in serviceEvent)
            {
                EventGroupList.Add(new SelectListItem
                {
                    Text = str.ServiceEventGroupName,
                    Value = str.ServiceEventId.ToString()
                });
            }
            DesginationList.Add(new SelectListItem
            {
                Text = "Select Designation",
                Value = ""
            });
            foreach (DesignationDTO str in desgn)
            {
                DesginationList.Add(new SelectListItem
                {
                    Text = str.DsgName,
                    Value = str.DsgId.ToString()
                });
            }
            BusinessGroupList.Add(new SelectListItem
            {
                Text = "Select Business Group",
                Value = ""
            });
            foreach (BusinessGroupDTO str in businessgroup)
            {
                BusinessGroupList.Add(new SelectListItem
                {
                    Text = str.BgName,
                    Value = str.BgId.ToString()
                });
            }


            foreach (var row in _serviceEventSubGroupService.GetSubGroupById(1))
            {
                SubEventGroupList.Add(new SelectListItem
                {
                    Text = row.ServiceEventSubGroupName,
                    Value = row.ServiceEventSubGroupId.ToString()
                });
            }


            TaxRuleList.Add(new SelectListItem
            {
                Text = "Select Tax Rule",
                Value = ""
            });

            foreach (TaxSetupDTO str in taxrule)
            {
                TaxRuleList.Add(new SelectListItem
                {
                    Text = str.TaxName,
                    Value = str.MasterId.ToString()
                });
            }




            DropDownListViewmodelcs ddlvm = new DropDownListViewmodelcs();
            ddlvm.OfficeList = officeList;
            ddlvm.DepartmentList = departmentList;
            ddlvm.LeaveRuleList = LeaveRuleList;
            ddlvm.LevelList = LevelList;
            ddlvm.SectionList = SectionList;
            ddlvm.EmployeeGroup = EmployeeGroupList;
            ddlvm.JobTypeList = JobtypeList;
            ddlvm.RankList = RankList;
            ddlvm.dllShiftList = ShiftList;
            ddlvm.dllEventGroup = EventGroupList;
            ddlvm.dllDesginationList = DesginationList;
            ddlvm.ddlBusinessGroupList = BusinessGroupList;
            ddlvm.dllSubEventGroupList = SubEventGroupList;
            ddlvm.dllTaxRuleList = TaxRuleList;
            return ddlvm;

        }
        #region OldCode
        //[Route("employees")]
        //public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, string pageSize)
        //{
        //    ViewBag.CurrentSort = sortOrder;
        //    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //    ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

        //    if (searchString != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        searchString = currentFilter;
        //    }

        //    ViewBag.CurrentFilter = searchString;
        //    if (!ViewBag.AllowView)
        //    {
        //        ViewBag.Error = "You are not Authorize to use this Page";
        //        return PartialView("_partialviewNotFound");
        //    }
        //    if (ViewBag.AllowView == true)
        //    {
        //        ViewBag.dllOfficeTypeList = _officeTypeServices.GetClildOfficeListByEmpCode(Convert.ToInt32(ViewBag.EmpCode));
        //        ViewBag.dllDepartmentList = _departmentServices.GetDepartmentlist();
        //        ViewBag.dllDesginationList = _designationServices.GetDesignationList().ToList();
        //        ViewBag.ddlGroupList = _groupServices.GetGroupList();
        //        ViewBag.ddlBgGroupList = _bgGroupService.GetBusinessGroupByEmpRoleId(Convert.ToInt32(ViewBag.EmpRoleId));
        //        int roleId = Convert.ToInt32(ViewBag.EmpRoleId);
        //        var emplist = _employeeService.GetEmployeeList(roleId);
        //        int pageSizes = 0;
        //        if (pageSize == null)
        //        {
        //            pageSizes = 10;
        //        }
        //        else
        //        {
        //            pageSizes = Convert.ToInt32(pageSize);
        //        }
        //        int pageNumber = (page ?? 1);
        //        return View(emplist.ToPagedList(pageNumber, pageSizes));
        //    }
        //    else
        //    {
        //        ViewBag.errorMsg = "Not Authorize";
        //        return View();
        //    }

        //}

        //[HttpPost]
        //[Route("employees")]
        //public ActionResult Index(EmployeeSearchViewModel searchModel, string sortOrder, string currentFilter, string searchString, int? page, string pageSize)
        //{
        //    ViewBag.CurrentSort = sortOrder;
        //    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //    ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

        //    if (searchString != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        searchString = currentFilter;
        //    }

        //    ViewBag.CurrentFilter = searchString;
        //    if (!ViewBag.AllowView)
        //    {
        //        ViewBag.Error = "You are not Authorize to use this Page";
        //        return PartialView("_partialviewNotFound");
        //    }
        //    if (ViewBag.AllowView == true)
        //    {
        //        ViewBag.Test = searchModel.OfficeId;
        //        ViewBag.dllOfficeTypeList = _officeTypeServices.GetOfficeListByEmpRole(Convert.ToInt32(ViewBag.EmpRoleId));
        //        ViewBag.dllDepartmentList = _departmentServices.GetDepartmentlist();
        //        ViewBag.dllDesginationList = _designationServices.GetDesignationList().ToList();
        //        ViewBag.ddlGroupList = _groupServices.GetGroupList();
        //        ViewBag.ddlBgGroupList = _bgGroupService.GetBusinessGroupByEmpRoleId(Convert.ToInt32(ViewBag.EmpRoleId));
        //        int roleId = Convert.ToInt32(ViewBag.EmpRoleId);
        //        var emplist = _employeeService.
        //            SearchEmployees(searchModel.EmpCode, searchModel.EmpName, searchModel.OfficeId,
        //            searchModel.DeptId, searchModel.DesgId, searchModel.GroupId, searchModel.BgId, roleId);
        //        int pageSizes = 0;
        //        if (pageSize == null)
        //        {
        //            pageSizes = 10;
        //        }
        //        else
        //        {
        //            pageSizes = Convert.ToInt32(pageSize);
        //        }
        //        int pageNumber = (page ?? 1);
        //        return View(emplist.ToPagedList(pageNumber, pageSizes));
        //        //return View(emplist);
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        #endregion

        [Route("employees")]
        public ActionResult Index(int? page, string EmpName, int? SearchEmpCode, int? OfficeId, int? DeptId, int? DesgId, int? GroupId, int? BgId)
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


                var emplist = _employeeService.
                    SearchEmployees(SearchEmpCode, EmpName, OfficeId,
                        DeptId, DesgId, GroupId, BgId, roleId);

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
        [Route("employees")]
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

                ViewBag.SearchEmpCode = searchModel.EmpCode;
                ViewBag.EmpName = searchModel.EmpName;
                ViewBag.BgId = searchModel.BgId;
                ViewBag.OfficeId = searchModel.OfficeId;
                ViewBag.DesgId = searchModel.DesgId;
                ViewBag.DeptId = searchModel.DeptId;
                ViewBag.GroupId = searchModel.GroupId;


                int roleId = Convert.ToInt32(ViewBag.EmpRoleId);
                var emplist = _employeeService.
                    SearchEmployees(searchModel.EmpCode, searchModel.EmpName, searchModel.OfficeId,
                        searchModel.DeptId, searchModel.DesgId, searchModel.GroupId, searchModel.BgId, roleId);

                int pageSizes = 20;

                int pageNumber = 1;

                return View(emplist.ToPagedList(pageNumber, pageSizes));
            }
            else
            {
                return View();
            }
        }

        [Route("employee/create")]
        [HttpGet]
        public ActionResult Create()
        {

            if (!ViewBag.AllowCreate)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            if (ViewBag.AllowCreate == true)
            {
                EmployeeDTO Empdt = new EmployeeDTO();
                DropDownListViewmodelcs ddlvm = DropDownlist();
                Empdt.OfficeList = ddlvm.OfficeList;
                Empdt.DepartmentList = ddlvm.DepartmentList;
                Empdt.LeaveRuleList = ddlvm.LeaveRuleList;
                Empdt.LevelList = ddlvm.LevelList;
                Empdt.SectionList = ddlvm.SectionList;
                Empdt.EmployeeGroup = ddlvm.EmployeeGroup;
                Empdt.JobTypeList = ddlvm.JobTypeList;
                Empdt.RankList = ddlvm.RankList;
                Empdt.dllShiftList = ddlvm.dllShiftList;
                Empdt.dllEventGroup = ddlvm.dllEventGroup;
                Empdt.dllSubEventGroup = ddlvm.dllSubEventGroupList;
                Empdt.dllDesginationList = ddlvm.dllDesginationList;
                Empdt.ddlBusinessGroupList = ddlvm.ddlBusinessGroupList;
                Empdt.dllTaxRuleList = ddlvm.dllTaxRuleList;
                return View(Empdt);
            }
            else
            {
                return View();
            }

        }

        [Route("employee/create")]
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Create")]
        public ActionResult Create(EmployeeDTO emp)
        {
            //var npDate = emp.EmpAppointmentDate;
            //emp.EmpAppointmentDate = DateTimeConverter.BStoAD(Convert.ToDateTime(npDate));
            if (!ViewBag.AllowCreate)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            else
            {


                EmployeeDTO Empdt = new EmployeeDTO();
                DropDownListViewmodelcs ddlvm = DropDownlist();
                Empdt.OfficeList = ddlvm.OfficeList;
                Empdt.DepartmentList = ddlvm.DepartmentList;
                Empdt.LeaveRuleList = ddlvm.LeaveRuleList;
                Empdt.LevelList = ddlvm.LevelList;
                Empdt.SectionList = ddlvm.SectionList;
                Empdt.EmployeeGroup = ddlvm.EmployeeGroup;
                Empdt.JobTypeList = ddlvm.JobTypeList;
                Empdt.RankList = ddlvm.RankList;
                Empdt.dllShiftList = ddlvm.dllShiftList;
                Empdt.dllEventGroup = ddlvm.dllEventGroup;
                Empdt.dllSubEventGroup = ddlvm.dllSubEventGroupList;
                Empdt.dllDesginationList = ddlvm.dllDesginationList;
                Empdt.ddlBusinessGroupList = ddlvm.ddlBusinessGroupList;
                Empdt.dllSubEventGroup = ddlvm.dllSubEventGroupList;
                emp.EmpRemoteType = "L";

                Empdt.dllTaxRuleList = ddlvm.dllTaxRuleList;

                if (!ModelState.IsValid)
                {
                    return View(Empdt);
                }
                MD5 md5Hash = MD5.Create();
                string hash = GetMd5Hash(md5Hash, emp.EmpPassword);
                emp.EmpPassword = hash;
                if (emp.EmpAppointmentDateNP != null) {
                    emp.EmpAppointmentDate = Convert.ToDateTime(NepEngDate.NepToEng(emp.EmpAppointmentDateNP));
                }

                if (_employeeService.EmployeeExists(emp))
                {
                    ModelState.AddModelError("EmployeeExistsError", "The employee with code " + emp.EmpCode + " already exists!");
                    return View(Empdt);
                }
                try
                {
                    EmployeeDTO resEmpDTO = new EmployeeDTO();

                    emp.EmpCreatedDate = DateTime.Now;
                    emp.EmpDisplayCode = emp.EmpCode.ToString();
                    emp.EmpRoleId = 2;

                    // ViewBag.success = String.Format("The employee profile of code {0} has been created.", resEmpDTO.EmpCode);
                    ModelState.Clear();
                    TempData["Success"] = "New Employee Created Successfully";
                    if (emp.EmpCode.ToString() != "")
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Uploads/" + emp.EmpCode.ToString()));
                    }

                    // Session["EmployeeInsert"] = "Success";

                    if (emp.EmpImage != null)
                    {
                        string extension = Path.GetExtension(emp.EmpImage.FileName).ToLower();
                        if (extension == ".png" || extension == ".jpg" || extension == ".gif" || extension == ".bmp")
                        {



                            if (emp.EmpCode.ToString() != "")
                            {
                                emp.EmpPhoto = (emp.EmpCode + "_profile_" + extension);

                                string path = Server.MapPath("~\\Uploads\\" + emp.EmpCode.ToString() + "\\");
                                emp.EmpImage.SaveAs(path + emp.EmpPhoto);

                            }

                        }
                        else
                        {
                            TempData["Success"] = "New Employee Created Successfully but could not upload the employee Photo";
                        }


                    }
                    resEmpDTO = _employeeService.InsertEmployee(emp);
                    // return View(Empdt);
                    return Redirect("/admin/userDetail/" + emp.EmpCode);

                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError("NoImage", Ex.Message);
                    return View(Empdt);
                }
            }

        }

        //[Route("employee/create")]
        //[HttpPost]
        //[MultipleButton(Name = "action", Argument = "CreateClose")]
        //public ActionResult CreateClose(EmployeeDTO emp)
        //{
        //    if (!ViewBag.AllowCreate)
        //    {
        //        ViewBag.Error = "You are not Authorize to use this Page";
        //        return PartialView("_partialviewNotFound");
        //    }
        //    EmployeeDTO Empdt = new EmployeeDTO();
        //    DropDownListViewmodelcs ddlvm = DropDownlist();
        //    Empdt.OfficeList = ddlvm.OfficeList;
        //    Empdt.DepartmentList = ddlvm.DepartmentList;
        //    Empdt.LeaveRuleList = ddlvm.LeaveRuleList;
        //    Empdt.LevelList = ddlvm.LevelList;
        //    Empdt.SectionList = ddlvm.SectionList;
        //    Empdt.EmployeeGroup = ddlvm.EmployeeGroup;
        //    Empdt.JobTypeList = ddlvm.JobTypeList;
        //    Empdt.RankList = ddlvm.RankList;
        //    Empdt.dllShiftList = ddlvm.dllShiftList;
        //    Empdt.dllEventGroup = ddlvm.dllEventGroup;
        //    Empdt.dllDesginationList = ddlvm.dllDesginationList;
        //    Empdt.ddlBusinessGroupList = ddlvm.ddlBusinessGroupList;
        //    emp.EmpRemoteType = "L";
        //    MD5 md5Hash = MD5.Create();
        //    string hash = GetMd5Hash(md5Hash, emp.EmpPassword);
        //    emp.EmpPassword = hash;
        //    ViewBag.Office = emp.ToString();
        //    if (!ModelState.IsValid)
        //    {
        //        return View("Create");
        //    }
        //    if (_employeeService.EmployeeExists(emp))
        //    {
        //        ModelState.AddModelError("EmployeeExistsError", "The employee with code " + emp.EmpCode + " already exists!");
        //        return View("Create", Empdt);
        //    }
        //    try
        //    {
        //        EmployeeDTO resEmpDTO = new EmployeeDTO();
        //        if (emp.EmpImage != null)
        //        {
        //            string extension = Path.GetExtension(emp.EmpImage.FileName).ToLower();
        //            if (extension == ".png" || extension == ".jpg" || extension == ".gif" || extension == ".bmp")
        //            {
        //                emp.EmpCreatedDate = DateTime.Now;
        //                emp.EmpDisplayCode = emp.EmpCode.ToString();
        //                emp.EmpPhoto = (emp.EmpCode + emp.EmpImage.FileName).ToLower();
        //                emp.EmpRoleId = 1;
        //                resEmpDTO = _employeeService.InsertEmployee(emp);
        //                if (resEmpDTO != null)
        //                {
        //                    string path = Server.MapPath("~\\img\\emp_photos\\");
        //                    emp.EmpImage.SaveAs(path + emp.EmpPhoto);
        //                }
        //                else
        //                {
        //                    ModelState.AddModelError("InsertError", "Sorry! The Employee was not inserted. Please try again.");
        //                    return View("Create", Empdt);
        //                }
        //                ViewBag.success = String.Format("The employee with code {0} has been inserted.", resEmpDTO.EmpCode);
        //                ModelState.Clear();
        //                Session["EmployeeInsert"] = "Success";
        //                return RedirectToAction("Index", "Employee");
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("InvalidImage", "The file you are trying to upload is not image.");
        //                return View("Create", Empdt);
        //            }
        //        }
        //        else
        //        {
        //            emp.EmpCreatedDate = DateTime.Now;
        //            emp.EmpDisplayCode = emp.EmpCode.ToString();
        //            emp.EmpRoleId = 1;
        //            resEmpDTO = _employeeService.InsertEmployee(emp);
        //            ViewBag.success = String.Format("The employee with code {0} has been inserted.", resEmpDTO.EmpCode);
        //            ModelState.Clear();
        //            Session["EmployeeInsert"] = "Success";
        //            return RedirectToAction("Index", "Employee");
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        ModelState.AddModelError("OtherError", "Oops! There was some problem and the employee cannot be added.\nPlease try again later.");
        //        return View("Create", Empdt);
        //    }
        //}

        [HttpGet]
        [Route("employee/edit/{empCode}")]
        public ActionResult Edit(int empCode)
        {
            if (!ViewBag.AllowEdit)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }

            EmployeeDTO Empdt = new EmployeeDTO();
            DropDownListViewmodelcs ddlvm = DropDownlist();

            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(empCode);
            EmployeeEditViewModel reEmp = _employeeService.GetEmployeeByID(empCode);

            reEmp.Districts = _employeeService.GetDistrictList();
            reEmp.Ethnicities = _ethService.GetEthnicityList();
            reEmp.Religions = _religionService.GetReligionList();
            reEmp.AppointedDateNP = !String.IsNullOrEmpty(reEmp.AppointedDate) ? NepEngDate.EngToNep(Convert.ToDateTime(reEmp.AppointedDate)) : "";
            reEmp.CitIssDateNP = !String.IsNullOrEmpty(reEmp.CitIssDate) ? NepEngDate.EngToNep(Convert.ToDateTime(reEmp.CitIssDate)) : "";
            reEmp.DateOfBirthNP = !String.IsNullOrEmpty(reEmp.DateOfBirth) ? NepEngDate.EngToNep(Convert.ToDateTime(reEmp.DateOfBirth)) : "";
            reEmp.MarriageAnniversaryNP = !String.IsNullOrEmpty(reEmp.MarriageAnniversary) ? NepEngDate.EngToNep(Convert.ToDateTime(reEmp.MarriageAnniversary)) : "";
            reEmp.AgeRetireDateNP = !String.IsNullOrEmpty(reEmp.AgeRetireDate) ? NepEngDate.EngToNep(Convert.ToDateTime(reEmp.AgeRetireDate)) : "";
            reEmp.NomineeDobNP = !String.IsNullOrEmpty(reEmp.NomineeDob) ? NepEngDate.EngToNep(Convert.ToDateTime(reEmp.NomineeDob)) : "";
            reEmp.NomCitIssueDateNP = !String.IsNullOrEmpty(reEmp.NomCitIssueDate) ? NepEngDate.EngToNep(Convert.ToDateTime(reEmp.NomCitIssueDate)) : "";
            reEmp.GratuityEffectiveDateNP = !String.IsNullOrEmpty(reEmp.GratuityEffectiveDate) ? NepEngDate.EngToNep(Convert.ToDateTime(reEmp.GratuityEffectiveDate)) : "";
            reEmp.PfEffectiveDateNP = !String.IsNullOrEmpty(reEmp.PfEffectiveDate) ? NepEngDate.EngToNep(Convert.ToDateTime(reEmp.PfEffectiveDate)) : "";

            //for dropdown
            reEmp.LeaveRuleList = ddlvm.LeaveRuleList;
            reEmp.EmployeeGroup = ddlvm.EmployeeGroup;
            reEmp.dllShiftList = ddlvm.dllShiftList;
            reEmp.ddlBusinessGroupList = ddlvm.ddlBusinessGroupList;
            reEmp.dllTaxRuleList = ddlvm.dllTaxRuleList;
            return View(reEmp);
        }

        [HttpPost]
        [Route("employee/edit/{empCode}")]
        public ActionResult Edit(EmployeeEditViewModel editEmp)
        {
            EmployeeEditViewModel reEmp = _employeeService.GetEmployeeByID(Convert.ToInt32(editEmp.Code));
            editEmp.Districts = _employeeService.GetDistrictList();
            editEmp.Ethnicities = _ethService.GetEthnicityList();
            editEmp.Religions = _religionService.GetReligionList();

            DropDownListViewmodelcs ddlvm = DropDownlist();
            editEmp.EmployeeGroup = ddlvm.EmployeeGroup;
            editEmp.dllShiftList = ddlvm.dllShiftList;
            editEmp.ddlBusinessGroupList = ddlvm.ddlBusinessGroupList;
            editEmp.dllTaxRuleList = ddlvm.dllTaxRuleList;

            if (!ViewBag.AllowEdit)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }



            string status = "failed";
            try
            {
                if (ModelState.IsValid)
                {

                    if (editEmp.Password != null)
                    {
                        MD5 md5Hash = MD5.Create();
                        string hash = GetMd5Hash(md5Hash, editEmp.Password);
                        editEmp.Password = hash;
                    }
                    else
                    {
                        editEmp.Password = reEmp.Password;
                    }

                    editEmp.AppointedDate = !String.IsNullOrEmpty(editEmp.AppointedDateNP) ? NepEngDate.NepToEng(editEmp.AppointedDateNP) : null;
                    editEmp.CitIssDate = !String.IsNullOrEmpty(editEmp.CitIssDateNP) ? NepEngDate.NepToEng(editEmp.CitIssDateNP).ToString() : null;
                    editEmp.DateOfBirth = !String.IsNullOrEmpty(editEmp.DateOfBirthNP) ? NepEngDate.NepToEng(editEmp.DateOfBirthNP).ToString() : null;
                    editEmp.MarriageAnniversary = !String.IsNullOrEmpty(editEmp.MarriageAnniversaryNP) ? NepEngDate.NepToEng(editEmp.MarriageAnniversaryNP).ToString() : null;
                    editEmp.AgeRetireDate = !String.IsNullOrEmpty(editEmp.AgeRetireDateNP) ? NepEngDate.NepToEng(editEmp.AgeRetireDateNP).ToString() : null;
                    editEmp.NomineeDob = !String.IsNullOrEmpty(editEmp.NomineeDobNP) ? NepEngDate.NepToEng(editEmp.NomineeDobNP).ToString() : null;
                    editEmp.NomCitIssueDate = !String.IsNullOrEmpty(editEmp.NomCitIssueDateNP) ? NepEngDate.NepToEng(editEmp.NomCitIssueDateNP).ToString() : null;
                    editEmp.PfEffectiveDate = !String.IsNullOrEmpty(editEmp.PfEffectiveDateNP) ? NepEngDate.NepToEng(editEmp.PfEffectiveDateNP).ToString() : null;
                    editEmp.GratuityEffectiveDate = !String.IsNullOrEmpty(editEmp.GratuityEffectiveDateNP) ? NepEngDate.NepToEng(editEmp.GratuityEffectiveDateNP).ToString() : null;

                    EmployeeEditViewModel retEmp = _employeeService.GetEmployeeByID(Convert.ToInt32(editEmp.Code));
                    editEmp.PhotoName = retEmp.PhotoName;
                    
                    editEmp.NomineePhoto = retEmp.NomineePhoto;
                    editEmp.PhotoName = retEmp.PhotoName;

                    if (!System.IO.Directory.Exists("~\\Uploads\\" + reEmp.Code.ToString() + "\\"))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Uploads/" + reEmp.Code.ToString()));
                    }
                    else
                    {

                    }

                    //nominee image
                    if (editEmp.NomImage != null)
                    {
                        string extension = Path.GetExtension(editEmp.NomImage.FileName).ToLower();

                        if (extension == ".png" || extension == ".jpg" || extension == ".gif" || extension == ".bmp")
                        {
                            editEmp.NomineePhoto = (editEmp.Code + "_nom_" + editEmp.NomImage.FileName).ToLower();

                            string path = Server.MapPath("~\\Uploads\\" + reEmp.Code.ToString() + "\\");

                            if (System.IO.File.Exists(path + reEmp.NomineePhoto))
                            {
                                System.IO.File.Delete(path + reEmp.NomineePhoto);
                            }
                            editEmp.NomImage.SaveAs(path + editEmp.NomineePhoto);

                        }
                    }
                    /////////////

                    //profile image


                    if (editEmp.EmpImage != null)
                    {
                        string extension = Path.GetExtension(editEmp.EmpImage.FileName).ToLower();
                        if (extension == ".png" || extension == ".jpg" || extension == ".gif" || extension == ".bmp")
                        {



                            if (editEmp.Code.ToString() != "")
                            {
                                editEmp.PhotoName = (editEmp.Code + "_profile_" + extension);

                                string path = Server.MapPath("~\\Uploads\\" + editEmp.Code.ToString() + "\\");
                                editEmp.EmpImage.SaveAs(path + editEmp.PhotoName);

                            }

                        }

                    }
                    //if (editEmp.EmpImage != null)
                    //{
                    //    string extension = Path.GetExtension(editEmp.EmpImage.FileName).ToLower();

                    //    if (extension == ".png" || extension == ".jpg" || extension == ".gif" || extension == ".bmp")
                    //    {

                    //        editEmp.PhotoName = (editEmp.Code + "_nom_" + editEmp.NomImage.FileName).ToLower();

                    //        string path = Server.MapPath("~\\Uploads\\" + reEmp.Code.ToString() + "\\");
                    //        editEmp.EmpImage.SaveAs(path + editEmp.EmpImage);

                    //    }
                    //}

                    ///

                    _employeeService.UpdateEmployee(editEmp);
                    status = "done";
                    return RedirectToAction("Edit", "Employee", new { empCode = editEmp.Code, status });

                    /*
                    if (editEmp.EmpImage == null)
                    {
                        EmployeeEditViewModel retEmp = _employeeService.GetEmployeeByID(Convert.ToInt32(editEmp.Code));
                        editEmp.PhotoName = retEmp.PhotoName;
                        editEmp.CitIssDate = !String.IsNullOrEmpty(editEmp.CitIssDate) ? DateTimeConverter.BStoAD(Convert.ToDateTime(editEmp.CitIssDate)).ToString() : null;
                        editEmp.DateOfBirth = !String.IsNullOrEmpty(editEmp.DateOfBirth) ? DateTimeConverter.BStoAD(Convert.ToDateTime(editEmp.DateOfBirth)).ToString() : null;
                        editEmp.MarriageAnniversary = !String.IsNullOrEmpty(editEmp.MarriageAnniversary) ? DateTimeConverter.BStoAD(Convert.ToDateTime(editEmp.MarriageAnniversary)).ToString() : null;
                        editEmp.AgeRetireDate = !String.IsNullOrEmpty(editEmp.AgeRetireDate) ? DateTimeConverter.BStoAD(Convert.ToDateTime(editEmp.AgeRetireDate)).ToString() : null;
                        editEmp.NomineeDob = !String.IsNullOrEmpty(editEmp.NomineeDob) ? DateTimeConverter.BStoAD(Convert.ToDateTime(editEmp.NomineeDob)).ToString() : null;
                        editEmp.NomCitIssueDate = !String.IsNullOrEmpty(editEmp.NomCitIssueDate) ? DateTimeConverter.BStoAD(Convert.ToDateTime(editEmp.NomCitIssueDate)).ToString() : null;
                        editEmp.PfEffectiveDate = !String.IsNullOrEmpty(editEmp.PfEffectiveDate) ? DateTimeConverter.BStoAD(Convert.ToDateTime(editEmp.PfEffectiveDate)).ToString() : null;
                        editEmp.GratuityEffectiveDate = !String.IsNullOrEmpty(editEmp.GratuityEffectiveDate) ? DateTimeConverter.BStoAD(Convert.ToDateTime(editEmp.GratuityEffectiveDate)).ToString() : null;
                        if (editEmp.NomImage == null)
                        {
                            editEmp.NomineePhoto = retEmp.NomineePhoto;
                        }
                        else
                        {
                            string extension = Path.GetExtension(editEmp.NomImage.FileName).ToLower();
                            if (extension == ".png" || extension == ".jpg" || extension == ".gif" || extension == ".bmp")
                            {
                                editEmp.NomineePhoto = (editEmp.Code + "_nom_" + editEmp.NomImage.FileName).ToLower();
                                string path = Server.MapPath("~\\img\\nom_photos\\");
                                if (System.IO.File.Exists(path + reEmp.NomineePhoto))
                                {
                                    System.IO.File.Delete(path + reEmp.NomineePhoto);
                                }
                                editEmp.NomImage.SaveAs(path + editEmp.NomineePhoto);
                            }
                        }

                        _employeeService.UpdateEmployee(editEmp);
                        status = "done";
                        return RedirectToAction("Edit", "Employee", new { empCode = editEmp.Code, status });
                    }
                    else
                    {
                        string extension = Path.GetExtension(editEmp.EmpImage.FileName).ToLower();
                        if (extension == ".png" || extension == ".jpg" || extension == ".gif" || extension == ".bmp")
                        {
                            editEmp.PhotoName = (editEmp.Code + editEmp.EmpImage.FileName).ToLower();
                            editEmp.CitIssDate = !String.IsNullOrEmpty(editEmp.CitIssDate) ? DateTimeConverter.BStoAD(Convert.ToDateTime(editEmp.CitIssDate)).ToString() : null;
                            editEmp.DateOfBirth = !String.IsNullOrEmpty(editEmp.DateOfBirth) ? DateTimeConverter.BStoAD(Convert.ToDateTime(editEmp.DateOfBirth)).ToString() : null;
                            editEmp.MarriageAnniversary = !String.IsNullOrEmpty(editEmp.MarriageAnniversary) ? DateTimeConverter.BStoAD(Convert.ToDateTime(editEmp.MarriageAnniversary)).ToString() : null;
                            editEmp.AgeRetireDate = !String.IsNullOrEmpty(editEmp.AgeRetireDate) ? DateTimeConverter.BStoAD(Convert.ToDateTime(editEmp.AgeRetireDate)).ToString() : null;
                            editEmp.NomineeDob = !String.IsNullOrEmpty(editEmp.NomineeDob) ? DateTimeConverter.BStoAD(Convert.ToDateTime(editEmp.NomineeDob)).ToString() : null;
                            editEmp.NomCitIssueDate = !String.IsNullOrEmpty(editEmp.NomCitIssueDate) ? DateTimeConverter.BStoAD(Convert.ToDateTime(editEmp.NomCitIssueDate)).ToString() : null;
                            if (editEmp.NomImage == null)
                            {
                                editEmp.NomineePhoto = reEmp.NomineePhoto;
                            }
                            else
                            {
                                string ext = Path.GetExtension(editEmp.NomImage.FileName).ToLower();
                                if (ext == ".png" || ext == ".jpg" || ext == ".gif" || ext == ".bmp")
                                {
                                    editEmp.NomineePhoto = (editEmp.Code + "_nom_" + editEmp.NomImage.FileName).ToLower();
                                    string path = Server.MapPath("~\\img\\nom_photos\\");
                                    if (System.IO.File.Exists(path + reEmp.NomineePhoto))
                                    {
                                        System.IO.File.Delete(path + reEmp.NomineePhoto);
                                    }
                                    editEmp.NomImage.SaveAs(path + editEmp.NomineePhoto);
                                }
                            }
                            int abc = _employeeService.UpdateEmployee(editEmp);
                            if (abc == 0)
                            {
                                string path = Server.MapPath("~\\img\\emp_photos\\");
                                if (System.IO.File.Exists(path + reEmp.PhotoName))
                                {
                                    System.IO.File.Delete(path + reEmp.PhotoName);
                                }
                                editEmp.EmpImage.SaveAs(path + editEmp.PhotoName);
                            }
                            else
                            {
                                return RedirectToAction("Edit", "Employee", new { empCode = editEmp.Code, status });
                            }
                            status = "done";
                            return RedirectToAction("Edit", "Employee", new { empCode = editEmp.Code, status });
                        }
                        else
                        {
                            return RedirectToAction("Edit", "Employee", new { empCode = editEmp.Code, status });
                        }
                    }*/

                }
                else
                {
                    Session["Error"] = "Form Error";


                    return View(editEmp);
                    //return RedirectToAction("Edit", "Employee", new { empCode = editEmp.Code });
                }
            }
            catch (Exception Ex)
            {
                Session["Error"] = Ex.Message;
                return RedirectToAction("Edit", "Employee", new { empCode = editEmp.Code, status });
            }
        }

        public void ListOfDatas(int empCode)
        {
            ViewBag.EmployeeDetail = _employeeService.GetEmployeeDetails(empCode);

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
            List<SelectListItem> EmployeesList = new List<SelectListItem>();
            foreach (var row in _employeeService.GetEmployeeList(2))
            {
                EmployeesList.Add(new SelectListItem
                {
                    Text = row.EmpName,
                    Value = row.EmpCode.ToString()
                });
            }
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

        //[Route("admin/{id}")]
        //public ActionResult EmployeeDetailsAdmin(int id)
        //{

        //    try
        //    {
        //        if (!ViewBag.AllowView)
        //        {
        //            ViewBag.Error = "You are not Authorize to use this Page";
        //            return PartialView("_partialviewNotFound");
        //        }

        //        EmployeeDetailsViewModel empDetails = new EmployeeDetailsViewModel();
        //        int AdminEmpCode = Convert.ToInt32(Session["EmpCode"]);
        //        int isProfileViewable = _employeeService.GetIsProfileViewable(id, AdminEmpCode);
        //        if (isProfileViewable == 1)
        //        {
        //            empDetails = _employeeService.GetEmployeeDetails(id);
        //        }
        //        else
        //        {
        //            ViewBag.Error = "You do not have permission to access this page!!!!";
        //            return View("_partialviewNotFound");
        //        }
        //        ViewBag.EmployeeDetail = empDetails;
        //        return View(empDetails);
        //    }
        //    catch (Exception Ex)
        //    {
        //        Session["error"] = Ex.Message;

        //        return RedirectToAction("Hr");

        //    }

        //}

        [Route("admin/assignroles/{Empcode}")]
        public ActionResult AssignRole(int Empcode)
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            else
            {
                EmployeeAssignRole emp = new EmployeeAssignRole();
                int AdminEmpCode = Convert.ToInt32(Session["EmpCode"]);
                var data = _employeeService.EmployeesIds(Empcode);
              
                emp.RoleId = data.EmpRoleId;
                ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(Empcode);
                int isProfileViewable = _employeeService.GetIsProfileViewable(Empcode, AdminEmpCode);
                if (isProfileViewable == 1)
                {
                    emp.EmpDetail = _employeeService.GetEmployeeDetails(Empcode);
                    ViewBag.EmployeeDetail = emp.EmpDetail;
                    emp.Rolelist = _rolesService.GetRoles();

                }
                else
                {
                    ViewBag.Error = "You do not have permission to access this page!!!!";
                    return View("_partialviewNotFound");

                }
                ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(Empcode);
                return View(emp);
            }




        }

        [Route("admin/setroles")]
        public JsonResult SetEmployeeRoles(int emp, int roleid)
        {

            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
            int admin = Convert.ToInt32(Session["EmpCode"]);
            EmployeeDetailsViewModel empDetails = new EmployeeDetailsViewModel();
            int isProfileViewable = _employeeService.GetIsProfileViewable(emp, admin);
            if (isProfileViewable == 1)
            {
                try
                {
                    int id = _employeeService.UpdateEmployeeRoles(emp, roleid);
                    return Json(new { Success = true }, JsonRequestBehavior.AllowGet);


                }
                catch (Exception)
                {
                    return Json(new { Success = false }, JsonRequestBehavior.AllowGet);

                }
            }
            else
            {
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [Route("admin/resetpassword/{Empcode}")]
        public ActionResult ResetPassword(int Empcode)
        {
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(Empcode);

            return View(Empcode);
        }
        [Route("admin/passwordreset/{Empcode}")]
        public JsonResult GenereatePassword(int Empcode)
        {

            try
            {

                string password = _employeeService.CreateRandomPassword(6);
                MD5 md5Hash = MD5.Create();
                string hash = GetMd5Hash(md5Hash, password);

                _employeeService.UpdatePassword(Empcode, hash);
                #region

                string emailType = "Email";
                EmailServices.Message msg = new EmailServices.Message();
                htmlReader reader = new htmlReader();
                EmployeeEditViewModel emp = _employeeService.GetEmployeeByID(Empcode);
                List<string> recipient = new List<string>()
                {
                    emp.Email!=""?emp.Email:"amin.duwal@adbl.gov.np"
                };
                msg.Subject = "Password Reset Sscessfully ";
                EmailTemplateModel emailTemplateData = new EmailTemplateModel()
                {
                    UserName = emp.Name,
                    Descriptions = "Your Password has been reset sucessfully  by " + Session["username"] + " on " + DateTime.Now.ToString("yyyy-MMM-dd") + ". Now your new password is " + password + ", Please login with your new passwords and change passwords as soon as possible for security purpose ",
                    Title = "",
                    Url = ""
                };
                msg.Body = reader.GetHtmlBodyTemplate(emailTemplateData, emailType);
                EmailServices.Notify(recipient, msg);

                #endregion
                return Json(new { Success = true, password }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception)
            {
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpGet]
        [Route("admin/employeebyroles")]
        public ActionResult EmployeeByRoles()
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            EmployeeSearchByRoles emproles = new EmployeeSearchByRoles();
            emproles.Roles = _rolesService.GetRoles();
            emproles.Office = _officeService.GetClildOfficeListByEmpCode(Convert.ToInt32(ViewBag.EmpCode));
            List<SelectListItem> roles = new List<SelectListItem>();
            List<SelectListItem> offices = new List<SelectListItem>();
            EmployeeDetailsViewModel detail = _employeeService.GetEmployeeDetails(Convert.ToInt32(Session["EmpCode"]));
            emproles.Employee = _employeeService.SearchEmployeesByRoleId(detail.OfficeId, Convert.ToInt32(Session["RoleId"]));

            foreach (OfficeDTOs str in emproles.Office)
            {
                offices.Add(new SelectListItem
                {
                    Text = str.OfficeName,
                    Value = str.OfficeId.ToString()
                });
            }
            foreach (RolesDTO rols in emproles.Roles)
            {
                roles.Add(new SelectListItem
                {
                    Text = rols.RoleName,
                    Value = rols.RoleId.ToString()
                });
            }
            emproles.BranchSelectList = offices;
            emproles.RolesSelectList = roles;
            return View(emproles);
        }

        [HttpPost]
        [Route("admin/employeebyroles")]
        public ActionResult EmployeeByRoles(EmployeeSearchByRoles Emp)
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            EmployeeSearchByRoles emproles = new EmployeeSearchByRoles();
            emproles.Roles = _rolesService.GetRoles();
            emproles.Office = _officeService.GetClildOfficeListByEmpCode(Convert.ToInt32(ViewBag.EmpCode));
            List<SelectListItem> roles = new List<SelectListItem>();
            List<SelectListItem> offices = new List<SelectListItem>();
            emproles.Employee = _employeeService.SearchEmployeesByRoleId(Emp.OfficeId, Emp.RoleId);


            foreach (OfficeDTOs str in emproles.Office)
            {
                offices.Add(new SelectListItem
                {
                    Text = str.OfficeName,
                    Value = str.OfficeId.ToString()
                });
            }
            foreach (RolesDTO rols in emproles.Roles)
            {
                roles.Add(new SelectListItem
                {
                    Text = rols.RoleName,
                    Value = rols.RoleId.ToString()
                });
            }
            emproles.BranchSelectList = offices;
            emproles.RolesSelectList = roles;
            return View(emproles);
        }

        [HttpGet]
        [Route("admin/updateepmloyeesroles")]
        public ActionResult UpdateEmployeeRoles()
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            EmployeeSearchByRoles emproles = new EmployeeSearchByRoles();
            emproles.Roles = _rolesService.GetRoles();
            emproles.Employee = _employeeService.EmployeeListByRoleId(Convert.ToInt32(Session["RoleId"]));
            List<SelectListItem> roles = new List<SelectListItem>();
            List<SelectListItem> employee = new List<SelectListItem>();
            foreach (RolesDTO rols in emproles.Roles)
            {
                roles.Add(new SelectListItem
                {
                    Text = rols.RoleName,
                    Value = rols.RoleId.ToString()
                });
            }
            foreach (EmployeeDTO rols in emproles.Employee)
            {
                employee.Add(new SelectListItem
                {
                    Text = rols.EmpName + " - [" + rols.EmpCode + "]",
                    Value = rols.EmpCode.ToString()
                });
            }
            emproles.EmployeeSelectList = employee;
            emproles.RolesSelectList = roles;
            return View(emproles);
        }

        [HttpPost]
        [Route("admin/updateepmloyeesroles")]
        public ActionResult UpdateEmployeeRoles(EmployeeSearchByRoles Emp, int[] employees)
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            EmployeeSearchByRoles emproles = new EmployeeSearchByRoles();
            emproles.Roles = _rolesService.GetRoles();
            emproles.Employee = _employeeService.EmployeeListByRoleId(Convert.ToInt32(Session["RoleId"]));
            List<SelectListItem> roles = new List<SelectListItem>();
            List<SelectListItem> employee = new List<SelectListItem>();
            foreach (RolesDTO rols in emproles.Roles)
            {
                roles.Add(new SelectListItem
                {
                    Text = rols.RoleName,
                    Value = rols.RoleId.ToString()
                });
            }
            foreach (EmployeeDTO rols in emproles.Employee)
            {
                employee.Add(new SelectListItem
                {
                    Text = rols.EmpName + " - [" + rols.EmpCode + "]",
                    Value = rols.EmpCode.ToString()
                });
            }
            emproles.EmployeeSelectList = employee;
            emproles.RolesSelectList = roles;
            try
            {
                int roleid = Convert.ToInt32(Emp.RoleId);
                for (int i = 0; i < employees.Length; i++)
                {
                    int EmpCode = employees[i];
                    _employeeService.UpdateEmployeeRoles(EmpCode, roleid);
                }
                return View(emproles);
            }
            catch (Exception Ex)
            {
                return View(Ex.Message);
            }
        }

        public ActionResult EmployeeExcel()
        {
            int roleId = Convert.ToInt32(ViewBag.EmpRoleId);
            List<EmployeeExcelVoewModel> filterData = new List<EmployeeExcelVoewModel>();
            // Step 1 - get the data from database
            var datas = _employeeService.
                SearchEmployees(ViewBag.SearchEmpCode, ViewBag.EmpName, ViewBag.OfficeId,
                    ViewBag.DeptId, ViewBag.DesgId, ViewBag.GroupId, ViewBag.BgId, roleId); ;
            foreach (var data in datas)
            {
                //EmployeeEditViewModel Details = _employeeService.GetEmployeeByID(data.EmpCode);
                EmployeeAppointmentDetailsViewModel employeeAppoint = _employeeService.GetEmployeeAppointmentDetail(data.EmpCode);
                //EmpEducationDTO education = _empEducationService.GetEducationByEduId(data.EmpCode);
                //OfficeDTOs office = _officeService.GetOfficeById(data.EmpOfficeId);
                //int eth = Convert.ToInt32(Details.EthnicityId);
                EmployeeExcelVoewModel excelData = new EmployeeExcelVoewModel();
                excelData.Code = data.EmpCode.ToString();
                excelData.Name = data.EmpName;
                excelData.Gender = data.EmpGender != "" ? data.EmpGender : "N/A";
                excelData.Ethincity = data.EthnicityName != "" ? data.EthnicityName : "N/A";
                excelData.District = data.DistrictName != "" ? data.DistrictName : "N/A";
                excelData.Adress = data.Address != "" ? data.Address : "N/A";
                excelData.BirthDate = data.EmpDob != "" ? data.EmpDob : "N/A";
                excelData.AppointDate = employeeAppoint.AppointDate != "" ? employeeAppoint.AppointDate : "N/A";
                excelData.AppointRank = employeeAppoint.AppointRank != "" ? employeeAppoint.AppointRank : "N/A";
                excelData.AppointPost = employeeAppoint.AppointDesignation != "" ? employeeAppoint.AppointDesignation : "N/A";
                excelData.AppOffice = employeeAppoint.AppointBranch != "" ? employeeAppoint.AppointBranch : "N/A";
                excelData.CurPost = data.DsgName != "" ? data.DsgName : "N/A";
                excelData.CurRank = data.RankName != "" ? data.RankName : "N/A";
                excelData.CurRankAppDate = data.EmpCurrentRankAppDate != "" ? data.EmpCurrentRankAppDate : "N/A";
                excelData.CurServiceDate = employeeAppoint.AppointDate.ToString() != "" ? employeeAppoint.AppointDate.ToString() : "N/A";
                excelData.CurServiceStatus = data.JobTypeName != "" ? data.JobTypeName : "N/A";
                excelData.CurOffice = data.OfficeName != "" ? data.OfficeName : "N/A";
                excelData.UpperOffice = _employeeService.GetUpperOfficeName(Convert.ToInt32(data.OfficeParentId));
                excelData.Section = data.SectionName != "" ? data.SectionName : "N/A";
                excelData.Qualification = data.DegreeName != "" ? data.DegreeName : "N/A";
                excelData.Division = data.Division != "" ? data.Division : "N/A";
                excelData.Faculty = data.FacultyName != "" ? data.FacultyName : "N/A";
                filterData.Add(excelData);

            }

            GridView gridview = new GridView();
            gridview.DataSource = filterData;
            gridview.DataBind();

            // Clear all the content from the current response
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename = EmployeeDetails" + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            // create HtmlTextWriter object with StringWriter
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            return RedirectToAction("Index", "Employee");



        }

        public ActionResult ApproverList(int? OfficeId)
        {
            if (OfficeId != null)
            {
                int empcode = Convert.ToInt32(Session["EmpCode"]);
                ApproverList Record = new ApproverList();
                Record.Office = _officeService.GetClildOfficeListByEmpCode(Convert.ToInt32(ViewBag.EmpCode));
                List<SelectListItem> OfficeList = new List<SelectListItem>();
                Record.Employee = _employeeService.GetApproverByOffice(Convert.ToInt32(OfficeId));

                foreach (OfficeDTOs str in Record.Office)
                {
                    OfficeList.Add(new SelectListItem
                    {
                        Text = str.OfficeName,
                        Value = str.OfficeId.ToString()
                    });
                }
                Record.BranchSelectList = OfficeList;
                return View(Record);
            }
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            ApproverList appList = new ApproverList();
            appList.Office = _officeService.GetClildOfficeListByEmpCode(Convert.ToInt32(ViewBag.EmpCode));
            List<SelectListItem> offices = new List<SelectListItem>();

            offices.Add(new SelectListItem
            {
                Text = "Select Office ",
                Value = null
            });
            foreach (OfficeDTOs str in appList.Office)
            {
                offices.Add(new SelectListItem
                {
                    Text = str.OfficeName,
                    Value = str.OfficeId.ToString()
                });
            }

            appList.BranchSelectList = offices;

            return View(appList);
        }

        [HttpPost]
        public ActionResult ApproverList(ApproverList approver)
        {

            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            int empcode = Convert.ToInt32(Session["EmpCode"]);
            ApproverList appList = new ApproverList();
            appList.Office = _officeService.GetClildOfficeListByEmpCode(Convert.ToInt32(ViewBag.EmpCode));
            List<SelectListItem> offices = new List<SelectListItem>();
            appList.Employee = _employeeService.GetApproverByOffice(approver.OfficeId);

            foreach (OfficeDTOs str in appList.Office)
            {
                offices.Add(new SelectListItem
                {
                    Text = str.OfficeName,
                    Value = str.OfficeId.ToString()
                });
            }
            appList.BranchSelectList = offices;
            return View(appList);

        }

        public ActionResult OfficeList()
        {
            //List<OfficeDTOs> Record = new List<OfficeDTOs>();
            IEnumerable<OfficeDTOs> Record = _officeService.GetActiveOfficeList();
            return View(Record);
        }
        public ActionResult AssignApprovers()
        {
            List<EmployeeDTO> Record = new List<EmployeeDTO>();
            return View(Record);
        }
        public ActionResult AssiginApprover()
        {
            int empcode = Convert.ToInt32(Session["EmpCode"]);
            ApproverList appList = new ApproverList();
            appList.Office = _officeService.GetClildOfficeListByEmpCode(Convert.ToInt32(ViewBag.EmpCode));
            List<SelectListItem> offices = new List<SelectListItem>();
            foreach (OfficeDTOs str in appList.Office)
            {
                offices.Add(new SelectListItem
                {
                    Text = str.OfficeName,
                    Value = str.OfficeId.ToString()
                });
            }
            appList.BranchSelectList = offices;
            return View(appList);
        }

        [HttpPost]
        public ActionResult AssiginApprover(ApproverList apv, int[] offices, int[] employee)
        {
            int empcode = Convert.ToInt32(Session["EmpCode"]);
            ApproverList appList = new ApproverList();
            appList.Office = _officeService.GetClildOfficeListByEmpCode(Convert.ToInt32(ViewBag.EmpCode));
            List<SelectListItem> office = new List<SelectListItem>();

            foreach (OfficeDTOs str in appList.Office)
            {
                office.Add(new SelectListItem
                {
                    Text = str.OfficeName,
                    Value = str.OfficeId.ToString()
                });
            }
            appList.BranchSelectList = office;

            for (int i = 0; i < offices.Length; i++)
            {
                int officeId = offices[i];
                for (int j = 0; j < employee.Length; j++)
                {
                    int emp = employee[j];
                    _employeeService.InsertApprover(officeId, emp);
                }
            }

            return View(appList);
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {


            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString());
            }
            return sBuilder.ToString();
        }

        public ActionResult UpdatePassword()
        {
            _employeeService.UpdateEncryptPassword();
            return View();
        }


        public JsonResult ApproverEmployeeList(int id)
        {
            int roleid = Convert.ToInt32(Session["RoleId"]);
            IEnumerable<EmployeeDTO> emp = _employeeService.EmployeeListByRoleId(roleid);
            IEnumerable<EmployeeDTO> final = emp.Where(x => x.EmpOfficeId == id);
            IEnumerable<EmployeeApprover> modelList = final.Select(x =>
                new EmployeeApprover()
                {
                    Empcode = x.EmpCode,
                    Empname = x.EmpName + " -[" + x.EmpCode + "]",
                    OfficeId = x.EmpOfficeId

                });


            return Json(new { data = modelList }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DeleteEmployeeApprover(int ApproverId, int EmpOfficeId)
        {
            try
            {
                _employeeService.DeleleEmployeeApprover(ApproverId);
                TempData["Success"] = "Successfully Record Deleted ";
                return RedirectToAction("ApproverList", new { OfficeId = EmpOfficeId });

            }
            catch (Exception Exception)
            {
                TempData["Danger"] = Exception.Message;
            }
            return View();
        }
    }

}


