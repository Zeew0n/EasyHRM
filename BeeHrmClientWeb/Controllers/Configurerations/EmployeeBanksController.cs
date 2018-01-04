using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.Repository;
using BeeHrmClientWeb.CodeBase;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Configurerations
{
    public class EmployeeBanksController : BaseController
    {
        private dbBeeHRMEntities db = new dbBeeHRMEntities();
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
        private IEmployeeBankService _employeeBankService;

        #endregion
        public EmployeeBanksController()
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
            _employeeBankService = new EmployeeBankService();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();
        }
        // GET: EmployeeBanks
        public ActionResult Index()
        {
            IEnumerable<EmployeeBankDTO> bankList = _employeeBankService.GetEmployeeBankList();
            return View(bankList);
        }

        // GET: EmployeeBanks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeBank employeeBank = db.EmployeeBanks.Find(id);
            if (employeeBank == null)
            {
                return HttpNotFound();
            }
            return View(employeeBank);
        }

        // GET: EmployeeBanks/Create
        public ActionResult Create()
        {
            EmployeeBankDTO data = new EmployeeBankDTO();
            data.EmployeeList = _employeeBankService.GetEmployeeList();
            data.BankList = _employeeBankService.GetBankList();
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeBankDTO data)
        {
            EmployeeBankDTO bankData = new EmployeeBankDTO();
            bankData.EmployeeList = _employeeBankService.GetEmployeeList();
            bankData.BankList = _employeeBankService.GetBankList();
            try
            {
                EmployeeBankDTO resBankDTO = new EmployeeBankDTO();
                resBankDTO = _employeeBankService.InsertEmployeeBank(data);
                ViewBag.success = String.Format("New Employee Bank Added");
                ModelState.Clear();
                return View(bankData);
            }
            catch (Exception Ex)
            {
                ViewBag.error = Ex.Message;
                return View(bankData);
            }
        }

        // GET: EmployeeBanks/Edit/5
        public ActionResult Edit(int id)
        {
            EmployeeBankDTO bankByID = _employeeBankService.GetEmployeeBankId(id);
            bankByID.EmployeeList = _employeeBankService.GetEmployeeList();
            bankByID.BankList = _employeeBankService.GetBankList();
            return View(bankByID);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeBankDTO data)
        {
            EmployeeBankDTO bankData = new EmployeeBankDTO();
            bankData.EmployeeList = _employeeBankService.GetEmployeeList();
            bankData.BankList = _employeeBankService.GetBankList();
            int reOffice = _employeeBankService.UpdateEmployeeBank(data);
            ViewBag.success = String.Format("Employee Bank Updated");
            ModelState.Clear();
            return View(bankData);
        }

        // GET: EmployeeBanks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeBank employeeBank = db.EmployeeBanks.Find(id);
            if (employeeBank == null)
            {
                return HttpNotFound();
            }
            return View(employeeBank);
        }

        // POST: EmployeeBanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeBank employeeBank = db.EmployeeBanks.Find(id);
            db.EmployeeBanks.Remove(employeeBank);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
