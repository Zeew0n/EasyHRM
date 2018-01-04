using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.Repository;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Configurerations
{
    public class BanksController : BaseController
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
        private IBankService _bankService;
        #endregion
        public BanksController()
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
            _bankService = new BankService();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();
        }
        // GET: Banks
        public ActionResult Index()
        {
            //if (!ViewBag.AllowView)
            //{
            //    ViewBag.Error = "You are not Authorize to use this Page";
            //    return PartialView("_partialviewNotFound");
            //}
            IEnumerable<BankDTO> bankList = _bankService.GetBankList();
            return View(bankList);
        }

        // GET: Banks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bank bank = db.Banks.Find(id);
            if (bank == null)
            {
                return HttpNotFound();
            }
            return View(bank);
        }

        // GET: Banks/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BankDTO data)
        {
            data.BankAddedDate = !String.IsNullOrEmpty(data.BankAddedDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.BankAddedDateNP)) : data.BankAddedDate;

            BankDTO bankData = new BankDTO();
            try
            {
                BankDTO resBankDTO = new BankDTO();
                resBankDTO = _bankService.InsertBank(data);
                ViewBag.success = String.Format("New Bank Added");
                ModelState.Clear();
                return View(bankData);
            }
            catch (Exception Ex)
            {
                ViewBag.error = Ex.Message;
                return View(bankData);
            }
        }

        // GET: Banks/Edit/5
        public ActionResult Edit(int id)
        {
            BankDTO bankByID = _bankService.GetBankId(id);
            bankByID.BankAddedDateNP = !String.IsNullOrEmpty(Convert.ToString(bankByID.BankAddedDate)) ? NepEngDate.EngToNep(Convert.ToDateTime(bankByID.BankAddedDate)) : null;

            return View(bankByID);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BankDTO data)
        {
            data.BankAddedDate = !String.IsNullOrEmpty(data.BankAddedDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.BankAddedDateNP)) : data.BankAddedDate;
            BankDTO bankData = new BankDTO();
            int reOffice = _bankService.UpdateBank(data);
            ViewBag.success = String.Format("Bank Updated");
            ModelState.Clear();
            return View(bankData);
        }

        // GET: Banks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bank bank = db.Banks.Find(id);
            if (bank == null)
            {
                return HttpNotFound();
            }
            return View(bank);
        }

        // POST: Banks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bank bank = db.Banks.Find(id);
            db.Banks.Remove(bank);
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
