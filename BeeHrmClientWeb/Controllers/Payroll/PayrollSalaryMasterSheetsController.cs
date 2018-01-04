using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.Repository;
using BeeHrmClientWeb.CodeBase;
using System;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Payroll
{
    public class PayrollSalaryMasterSheetsController : BaseController
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
        private IPayrollRemoteAllowanceService _payrollRemoteAllowanceService;
        private IPayrollSalaryMasterSheetService _payrollSalaryMasterSheetService;
        #endregion
        public PayrollSalaryMasterSheetsController()
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
            _payrollRemoteAllowanceService = new PayrollRemoteAllowanceService();
            _payrollSalaryMasterSheetService = new PayrollSalaryMasterSheetService();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();

        }
        // GET: PayrollSalaryMasterSheets
        public ActionResult Index()
        {
            string id = Session["EmpCode"].ToString();
            PayrollSalaryMasterSheetListDTO Record = new PayrollSalaryMasterSheetListDTO();
            Record.PayrollSalaryMasterSheetDTO = _payrollSalaryMasterSheetService.GetAllPayrollSalaryMasterSheet(id);
            Record.FiscalYearList = _payrollSalaryMasterSheetService.GetFiscalYear();

            foreach (var item in Record.PayrollSalaryMasterSheetDTO)
            {
                Record.totalActual += item.SalaryAfterTaxDeduction;
                Record.totalAllowance += (item.TotalRankAllowances + item.NonTaxableAllowanceAmount + item.TaxableAllowanceAmount);
                Record.totalGrossIncome += item.GrossIncome;
                Record.totalGrade += item.GradeSalary;
                Record.totalRank += item.RankSalary;
                Record.totalTax += item.TaxAmount;
                Record.totalBasicSalaryForPf += item.TotalBasicSalaryForPf;
                Record.totalPfCompany += item.PfCompany;
                Record.totalPfExtra += item.PfExtra;
                Record.totalPfSelf += item.PfSelf;
                Record.totalCIT += item.CitAmount;
                item.PayrollEmployeeTaxDetail = _payrollSalaryMasterSheetService.GetPayrollEmployeeTaxDetails(id);
            }

            return View(Record);
        }
        [HttpPost]
        public ActionResult Index(string FiscalYear)
        {
            string id = Session["EmpCode"].ToString();
            PayrollSalaryMasterSheetListDTO Record = new PayrollSalaryMasterSheetListDTO();
            Record.PayrollSalaryMasterSheetDTO = _payrollSalaryMasterSheetService.GetAllPayrollSalaryMasterSheetFilter(id, FiscalYear);
            Record.FiscalYearList = _payrollSalaryMasterSheetService.GetFiscalYear();
            foreach (var item in Record.PayrollSalaryMasterSheetDTO)
            {
                Record.totalActual += item.SalaryAfterTaxDeduction;
                Record.totalAllowance += (item.TotalRankAllowances + item.NonTaxableAllowanceAmount + item.TaxableAllowanceAmount);
                Record.totalCIT += item.CitAmount;
                Record.totalGrade += item.GradeSalary;
                Record.totalRank += item.RankSalary;
                Record.totalTax += item.TaxAmount;
                Record.totalBasicSalaryForPf += item.TotalBasicSalaryForPf;
                Record.totalPfCompany += item.PfCompany;
                Record.totalPfExtra += item.PfExtra;
                Record.totalPfSelf += item.PfSelf;
                Record.totalCIT += item.CitAmount;
                item.PayrollEmployeeTaxDetail = _payrollSalaryMasterSheetService.GetPayrollEmployeeTaxDetails(id);
            }
            return View(Record);
        }
        // GET: PayrollSalaryMasterSheets/Details/5
        public ActionResult Details(int id)
        {
            PayrollSalaryMasterSheetDTO res = new PayrollSalaryMasterSheetDTO();
            res = _payrollSalaryMasterSheetService.GetPayrollSalaryMasterSheetById(id);
            res.totalPf = res.PfCompany + res.PfExtra + res.PfSelf;
            res.totalAllowance = res.TotalRankAllowances + res.RemoteAllowance + res.RankOtherAllowances;
            res.PayrollSalaryDetailSheets = _payrollSalaryMasterSheetService.GetAllPayrollSalaryDetail(res.Id.ToString());
            foreach (var all in res.PayrollSalaryDetailSheets)
            {
                res.totalAllowance += all.CalculatedValue;
            }
            return View(res);
        }

        // GET: PayrollSalaryMasterSheets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PayrollSalaryMasterSheets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PayrollSalaryMasterSheetDTO data)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                PayrollSalaryMasterSheetDTO res = new PayrollSalaryMasterSheetDTO();
                res = _payrollSalaryMasterSheetService.InsertPayrollSalaryMasterSheet(data);
                ViewBag.Success = "Grade " + data.EmployeeName + " salary payroll has been created";
                ModelState.Clear();
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: PayrollSalaryMasterSheets/Edit/5
        public ActionResult Edit(int id)
        {
            PayrollSalaryMasterSheetDTO res = new PayrollSalaryMasterSheetDTO();
            res = _payrollSalaryMasterSheetService.GetPayrollSalaryMasterSheetById(id);
            return View(res);
        }

        // POST: PayrollSalaryMasterSheets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PayrollSalaryMasterSheetDTO data)
        {
            int res = _payrollSalaryMasterSheetService.UpdatePayrollSalaryMasterSheet(data);
            return View();
        }

        // GET: PayrollSalaryMasterSheets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayrollSalaryMasterSheet payrollSalaryMasterSheet = db.PayrollSalaryMasterSheets.Find(id);
            if (payrollSalaryMasterSheet == null)
            {
                return HttpNotFound();
            }
            return View(payrollSalaryMasterSheet);
        }

        // POST: PayrollSalaryMasterSheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PayrollSalaryMasterSheet payrollSalaryMasterSheet = db.PayrollSalaryMasterSheets.Find(id);
            db.PayrollSalaryMasterSheets.Remove(payrollSalaryMasterSheet);
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
        public ActionResult RetirementFundIndex()
        {
            string id = Session["EmpCode"].ToString();
            PayrollSalaryMasterSheetListDTO Record = new PayrollSalaryMasterSheetListDTO();
            Record.PayrollSalaryMasterSheetDTO = _payrollSalaryMasterSheetService.GetAllPayrollSalaryMasterSheet(id);
            Record.FiscalYearList = _payrollSalaryMasterSheetService.GetFiscalYear();
            foreach (var item in Record.PayrollSalaryMasterSheetDTO)
            {
                Record.totalBasicSalaryForPf += item.TotalBasicSalaryForPf;
                Record.totalPfCompany += item.PfCompany;
                Record.totalPfExtra += item.PfExtra;
                Record.totalPfSelf += item.PfSelf;
                Record.totalCIT += item.CitAmount;
            }

            return View(Record);
        }
        [HttpPost]
        public ActionResult RetirementFundIndex(string FiscalYear)
        {
            string id = Session["EmpCode"].ToString();
            PayrollSalaryMasterSheetListDTO Record = new PayrollSalaryMasterSheetListDTO();
            Record.PayrollSalaryMasterSheetDTO = _payrollSalaryMasterSheetService.GetAllPayrollSalaryMasterSheetFilter(id, FiscalYear);
            Record.FiscalYearList = _payrollSalaryMasterSheetService.GetFiscalYear();
            foreach (var item in Record.PayrollSalaryMasterSheetDTO)
            {
                Record.totalBasicSalaryForPf += item.TotalBasicSalaryForPf;
                Record.totalPfCompany += item.PfCompany;
                Record.totalPfExtra += item.PfExtra;
                Record.totalPfSelf += item.PfSelf;
                Record.totalCIT += item.CitAmount;
            }

            return View(Record);
        }

    }
}
