using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHrmClientWeb.CodeBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Configurerations
{
    public class RolesController : BaseController
    {
        // GET: Roles
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
        public RolesController()
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
        #region Roles
        [Route("Roles")]
        public ActionResult Roles()
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }

            IEnumerable<RolesDTO> rolesList = _rolesService.GetRoles();
            return View("../Roles/Index", rolesList);
        }

        [HttpGet]
        [Route("role/create")]
        public ActionResult RolesCreate()
        {
            List<SelectListItem> businessGroupList = new List<SelectListItem>();
            foreach (var row in _bgService.GetBusinessGroupList())
            {
                businessGroupList.Add(new SelectListItem
                {
                    Text = row.BgName,
                    Value = row.BgId.ToString()
                });
            }

            ViewBag.BusinessGroupList = businessGroupList;
            return View();
        }

        [HttpPost]
        [Route("role/create")]
        public ActionResult RolesCreate(RolesDTO data)
        {
            List<SelectListItem> businessGroupList = new List<SelectListItem>();
            foreach (var row in _bgService.GetBusinessGroupList())
            {
                businessGroupList.Add(new SelectListItem
                {
                    Text = row.BgName,
                    Value = row.BgId.ToString()
                });
            }


            ViewBag.BusinessGroupList = businessGroupList;
            try
            {
                if (data.RoleDataAccessAll == false)
                {
                    if (data.BusinessGroup == null)
                    {
                        ViewBag.Error = "You must choose at least one Business Group and/or Offices";
                        return View(data);
                    }

                }
                RolesDTO resRoles = new RolesDTO();
                RolesBusinessGroupAccessDTO dataForBg = new RolesBusinessGroupAccessDTO();
                RolesBusinessGroupAccessDTO resForBg = new RolesBusinessGroupAccessDTO();
                resRoles = _rolesService.InsertRoles(data);

                if (data.BusinessGroup != null)
                {
                    foreach (int item in data.BusinessGroup)
                    {
                        dataForBg.BusinssGroupId = item;
                        dataForBg.RoleId = resRoles.RoleId;
                        resForBg = _rolesBusinessGroupAccessService.Insert(dataForBg);
                    }
                }

                ViewBag.success = "New role added as " + data.RoleName;
                return RedirectToAction("Roles");

            }
            catch (Exception Ex)
            {
                ViewBag.error = Ex.Message;
                return View();
            }
        }
        [HttpGet]
        [Route("role/edit/{roleId}")]
        public ActionResult RoleEdit(int roleId)
        {
            List<SelectListItem> selectedBusinessGroupList = new List<SelectListItem>();
            int[] valuesOfBg = _rolesBusinessGroupAccessService.GetBusinessGroupByRoleID(roleId);
            #region List of BusinessGroup List
            List<SelectListItem> businessGroupList = new List<SelectListItem>();
            foreach (var row in _bgService.GetBusinessGroupList())
            {
                businessGroupList.Add(new SelectListItem
                {
                    Text = row.BgName,
                    Value = row.BgId.ToString()
                });
            }
            if (valuesOfBg != null || valuesOfBg.Length > 0)
            {
                foreach (var exist in valuesOfBg)
                {
                    selectedBusinessGroupList.Add(businessGroupList.Where(c => c.Value == exist.ToString()).Single());
                }
                foreach (var exist in valuesOfBg)
                {
                    if (businessGroupList.Where(c => c.Value == exist.ToString()).SingleOrDefault() != null)
                    {
                        businessGroupList.Remove(businessGroupList.Where(c => c.Value == exist.ToString()).SingleOrDefault());
                    }

                }
            }

            #endregion

            ViewBag.BusinessGroupList = businessGroupList;
            ViewBag.selectedBgList = selectedBusinessGroupList;
            RolesDTO rolebyId = _rolesService.GetRoleById(roleId);
            return View(rolebyId);
        }
        [Route("Role/RemoveBusinessGroup/")]
        public RedirectResult RemoveBusinessGroup(string roleId, string BgID)
        {
            _rolesBusinessGroupAccessService.RemoveBg(roleId, BgID);
            return Redirect("/role/edit/" + Convert.ToInt16(roleId));
        }

        [Route("Role/RemoveOffice/")]
        public RedirectResult RemoveOffice(string roleId, string officeId)
        {
            _rolesOfficeAccessService.RemoveOffice(roleId, officeId);
            return Redirect("/role/edit/" + Convert.ToInt16(roleId));
        }
        [HttpPost]
        [Route("role/edit/{roleId}")]
        public RedirectResult RoleEdit(RolesDTO data)
        {
            RolesBusinessGroupAccessDTO dataForBg = new RolesBusinessGroupAccessDTO();
            RolesBusinessGroupAccessDTO resForBg = new RolesBusinessGroupAccessDTO();

            int res = _rolesService.UpdateRole(data);
            if (data.BusinessGroup != null)
            {
                foreach (int item in data.BusinessGroup)
                {
                    dataForBg.BusinssGroupId = item;
                    dataForBg.RoleId = data.RoleId;
                    resForBg = _rolesBusinessGroupAccessService.Insert(dataForBg);
                }
            }
            Session["UpdateRole"] = "ok";
            return Redirect("/Roles");

        }
        #endregion
    }
}