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
    public class RoleAccessController : BaseController
    {
        // GET: RoleAccess
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
        public RoleAccessController()
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
            GetLoginInfo("Roles");
            GetUserAccess("Roles");
        }
        #region RoleAccess
        [HttpGet]
        [Route("RoleAccesses/{Id}")]
        public ActionResult RoleAccesses(int Id)
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }

            List<TreeModules> Record = _roleAccessService.GetMenuTree(Id);
            ModularLists ReturnRecord = new ModularLists();
            ReturnRecord.ListOfModules = Record;
            return View(ReturnRecord);
        }
        [HttpPost]
        [Route("RoleAccesses")]
        public ActionResult RoleAccesses(FormCollection data)
        {
            List<TreeModules> Insert = new List<TreeModules>();
            int i = 0;
            int Counter = Convert.ToInt32(data["Counter"]);
            int RoleId = 1;
            for (i = 0; i <= Counter; i++)
            {
                TreeModules Datas = new TreeModules();
                Datas.Id = Convert.ToInt32(data["ListOfModules[" + i + "].Id"]);
                Datas.Views = data["Views[" + i + "]"] == "on" ? true : false;
                Datas.Add = data["Add[" + i + "]"] == "on" ? true : false;
                Datas.Edit = data["Edit[" + i + "]"] == "on" ? true : false;
                Datas.Delete = data["Delete[" + i + "]"] == "on" ? true : false;
                Datas.RoleId = Convert.ToInt32(data["RoleId[" + i + "]"]);
                Insert.Add(Datas);
                RoleId = Convert.ToInt32(data["RoleId[" + i + "]"]);
            }
            _roleAccessService.InsertIntoRolesAccess(Insert, RoleId);
            TempData["Success"] = "Menu For This Role updated Successfully";
            return RedirectToAction("../RoleAccesses/" + RoleId);
        }



        [HttpGet]
        [Route("roleaccess/{roleId}")]
        public ActionResult RolesAccess(int roleId)
        {
            ViewBag.Roles = _rolesService.GetRoleById(roleId);
            IEnumerable<RolesAccessDTO> RolesList = _roleAccessService.GetRoleByRoleID(roleId);
            return View(RolesList);
        }
        [HttpGet]
        [Route("RolesAccess/Create/{roleId}")]
        public ActionResult RolesAccessCreate(int roleId)
        {
            ViewBag.RoleFor = _rolesService.GetRoleById(roleId).RoleName;
            //ViewBag.ParentModels = _roleAccessService.GetParentModulesList();
            //ViewBag.Modules = _roleAccessService.GetNotInModules(roleId);
            ViewBag.RoleId = roleId;
            return View();
        }

        //[HttpGet]
        //// [Route("GetModelByParentModel/{Id}")]
        //public ActionResult GetModelByParentModel(int Id, int roleId)
        //{
        //    List<SelectListItem> listForModules = new List<SelectListItem>();
        //    //var data = _roleAccessService.GetParentModules(Id, roleId);
        //    //var listData = new List<SelectListItem>();
        //    //foreach (var row in data)
        //    //{
        //    //    listData.Add(new SelectListItem
        //    //    {
        //    //        Text = row.MOduleName,
        //    //        Value = row.ModuleId.ToString()
        //    //    });
        //    //};
        //    return Json(listData, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        [Route("RolesAccess/Create/{roleId}")]
        public RedirectResult RoleAccessCreate(RolesAccessDTO data, int roleId)
        {
            data.AssignRoleId = roleId;
            try
            {
                RolesAccessDTO resRolesAccessDTO = new RolesAccessDTO();
                resRolesAccessDTO = _roleAccessService.InserRoleAccess(data);
                ViewBag.RoleFor = _rolesService.GetRoleById(roleId).RoleName;
                ViewBag.Modules = _moduleService.GetAllModules();
                ViewBag.RoleId = roleId;
                ViewBag.success = String.Format("New Role Added");
                return Redirect("/RoleAccess/" + roleId);
            }
            catch (Exception Ex)
            {
                ViewBag.error = Ex.Message;
                return Redirect("/RoleAccess/" + roleId);
            }
        }

        [HttpGet]
        [Route("roleaccess/Edit/")]
        public ActionResult RoleAccessEdit(string roleid, string moduleid, string assignid)
        {
            RolesAccessDTO roleByRoleIdModuleId = new RolesAccessDTO();
            roleByRoleIdModuleId = _roleAccessService.GetRoleByRoleIdModuleId(roleid, moduleid, assignid);
            return View("RolesAccess/Edit", roleByRoleIdModuleId);
        }

        [HttpPost]
        [Route("roleaccess/Edit/")]
        public RedirectResult RoleAccessEdit(RolesAccessDTO data)
        {
            int res = _roleAccessService.UpdateRolesAccess(data);
            return Redirect("Edit?roleid=" + data.AssignRoleId + "&moduleid=" + data.AssignModuleId + "&assignid=" + data.AssignId);
        }


        [Route("roleaccess/delete/")]
        public RedirectResult RoleDelete(string roleid, string assignid)
        {
            try
            {
                _roleAccessService.DeleteRoleAccess(Convert.ToInt16(assignid));
                ViewBag.Success = "The role has been deleted.";
                return Redirect(roleid);
            }
            catch (Exception Ex)
            {
                return Redirect(roleid);
            }

        }

        #endregion
    }
}