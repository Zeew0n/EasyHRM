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

namespace BeeHrmClientWeb.Controllers.Payroll
{
    public class PayrollRemoteAllowancesController : BaseController
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
        #endregion
        public PayrollRemoteAllowancesController()
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
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();
        }
        // GET: PayrollRemoteAllowances
        public ActionResult Index()
        {
            //if (!ViewBag.AllowView)
            //{
            //    ViewBag.Error = "You are not Authorize to use this Page";
            //    return PartialView("_partialviewNotFound");
            //}


            IEnumerable<PayrollRemoteAllowancesDTO> payrollRemoteAllowanceList = _payrollRemoteAllowanceService.GetPayrollRemoteAllowanceList();
            return View(payrollRemoteAllowanceList);
        }

        // GET: PayrollRemoteAllowances/Create
        public ActionResult Create()
        {
            PayrollRemoteAllowancesDTO data = new PayrollRemoteAllowancesDTO();
            data.RankList = _payrollRemoteAllowanceService.GetRankList();
            data.RemoteList = _payrollRemoteAllowanceService.GetRemoteList();
            return View(data);
        }

        // POST: PayrollRemoteAllowances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PayrollRemoteAllowancesDTO payrollRemoteAllowance)
        {
            PayrollRemoteAllowancesDTO data = new PayrollRemoteAllowancesDTO();
            try
            {
                data.RankList = _payrollRemoteAllowanceService.GetRankList();
                data.RemoteList = _payrollRemoteAllowanceService.GetRemoteList();
                _payrollRemoteAllowanceService.InsertPayrollRemoteAllowance(payrollRemoteAllowance);
                ViewBag.success = String.Format("New Payroll Remote Service Added");
                ModelState.Clear();
                return View(data);
            }
            catch (Exception Ex)
            {

                ViewBag.error = Ex.Message;
                return View(data);
            }
        }

        // GET: PayrollRemoteAllowances/Edit/5
        public ActionResult Edit(int id)
        {
            PayrollRemoteAllowancesDTO rAId = _payrollRemoteAllowanceService.GetPayrollRemoteAllowanceByID(id);
            rAId.RankList = _payrollRemoteAllowanceService.GetRankList();
            rAId.RemoteList = _payrollRemoteAllowanceService.GetRemoteList();
            return View(rAId);
        }

        // POST: PayrollRemoteAllowances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PayrollRemoteAllowancesDTO payrollRemoteAllowance)
        {
            int rA = _payrollRemoteAllowanceService.UpdatePayrollRemoteAllowance(payrollRemoteAllowance);
            payrollRemoteAllowance.RankList = _payrollRemoteAllowanceService.GetRankList();
            payrollRemoteAllowance.RemoteList = _payrollRemoteAllowanceService.GetRemoteList();
            ViewBag.success = String.Format("New Payroll Remote Service Updated");
            ModelState.Clear();
            return View(payrollRemoteAllowance);
        }

        // GET: PayrollRemoteAllowances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayrollRemoteAllowance payrollRemoteAllowance = db.PayrollRemoteAllowances.Find(id);
            if (payrollRemoteAllowance == null)
            {
                return HttpNotFound();
            }
            return View(payrollRemoteAllowance);
        }

        // POST: PayrollRemoteAllowances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PayrollRemoteAllowance payrollRemoteAllowance = db.PayrollRemoteAllowances.Find(id);
            db.PayrollRemoteAllowances.Remove(payrollRemoteAllowance);
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
