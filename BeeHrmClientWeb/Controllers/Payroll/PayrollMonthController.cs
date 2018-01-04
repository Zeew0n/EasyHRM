using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using BeeHrmClientWeb.CodeBase;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Payroll
{
    public class PayrollMonthController : BaseController
    {
        private IModulServices _moduleService;
        // private IDepartmentService _departmentServices;
        private IPayrollMonthDescriptionService _PayrollMonthDescriptionService;
        private IFiscalService _fiscalService;
        private IUnitOfWork _iunitofwork;

        public PayrollMonthController()
        {
            _moduleService = new ModuleService();
            _PayrollMonthDescriptionService = new PayrollMonthDescriptionService();
            _fiscalService = new FiscalService();
            _iunitofwork = new UnitOfWork();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
        }
        // GET: PayrollMonth

        #region PayrollMonths
        public ActionResult Index(int? fyid)
        {

            int currentFyId = _fiscalService.GetCurrentFyId();
            if (fyid != null)
            {
                currentFyId = Convert.ToInt32(fyid);
            }

            ViewBag.fsId = currentFyId;

            IEnumerable<PayrollMonthDescriptionDTO> Record = _PayrollMonthDescriptionService.GetAllPayrollMonths(currentFyId);

            ViewBag.FiscalsDropdown = _PayrollMonthDescriptionService.GetFiscalDropDown();

            return View(Record);
        }

        public ActionResult AddPayrollMonth()
        {
            PayrollMonthDescriptionDTO Record = new PayrollMonthDescriptionDTO();
            Record.Fiscals = _PayrollMonthDescriptionService.GetFiscalDropDown();
            return View(Record);
        }
        [HttpPost]
        public ActionResult AddPayrollMonth(PayrollMonthDescriptionDTO Record)
        {
            PayrollMonthDescriptionDTO EditRecord = new PayrollMonthDescriptionDTO();
            EditRecord.Fiscals = _PayrollMonthDescriptionService.GetFiscalDropDown();

            if (!ModelState.IsValid)
            {

                return View();
            }
            else
            {
                int chk = _PayrollMonthDescriptionService.PayrollMonthsCheck(Record.MonthIndex, Record.FyId);
                if (chk > 0)
                {
                    TempData["Danger"] = "Payroll Month is already exist for this fiscal year.";

                    return View(EditRecord);
                }
                else
                {
                    _PayrollMonthDescriptionService.InsertIntoMonthDescription(Record);
                    return RedirectToAction("Index");
                }

            }
        }
        public ActionResult EditPayrollMonth(int Id)
        {
            PayrollMonthDescriptionDTO Record = _PayrollMonthDescriptionService.GetPayrollMonthById(Id);
            return View(Record);
        }
        [HttpPost]
        public ActionResult EditPayrollMonth(PayrollMonthDescriptionDTO Data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _PayrollMonthDescriptionService.UpdatePayrollMonth(Data);
                }
                else
                {
                    ViewBag.Error = "Model Validation Error!";
                    return RedirectToAction("EditPayrollMonth/" + Data.Id);

                }
                ViewBag.Success = "Month Updated Successfully";
            }
            catch (Exception Ex)
            {
                ViewBag.Error = Ex.Message;

            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}