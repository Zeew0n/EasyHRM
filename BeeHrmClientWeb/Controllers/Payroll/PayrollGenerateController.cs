using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHrmClientWeb.CodeBase;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Payroll
{
    public class PayrollGenerateController : BaseController
    {
        private IModulServices _moduleService;
        private IDepartmentService _departmentServices;
        private IPayrollAllowanceMasterService _PayrollAllowanceMasterService;
        private IPayrollMonthDescriptionService _PayrollMonthDescriptionService;
        private IPayrollGenerationService _PayrollGenerationService;
        private IEmployeeService _EmployeeService;
        private IPayrollReportService _PayrollReportService;
        private IFiscalService _fiscalService;
        private IOfficeServices _officeService;



        public PayrollGenerateController()
        {
            _moduleService = new ModuleService();
            _PayrollAllowanceMasterService = new PayrollAllowanceMasterService();
            _PayrollMonthDescriptionService = new PayrollMonthDescriptionService();
            _PayrollGenerationService = new PayrollGenerationService();
            _departmentServices = new DepartmentService();
            _EmployeeService = new EmployeeService();
            _PayrollReportService = new PayrollReportService();
            _fiscalService = new FiscalService();
            _officeService = new OfficeServices();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
        }
        #region GeneratePayroll
        public ActionResult Index()
        {
            int EmpCode = Convert.ToInt32(Session["Empcode"]);

            int fyid = Convert.ToInt32(Request["fyId"]);
            int officeId = Convert.ToInt32(Request["officeId"]);

            if (fyid == 0)
            {
                fyid = _fiscalService.GetCurrentFyId();
            }
            if (officeId == 0)
            {
                officeId = Convert.ToInt32(Session["OfficeId"]);
            }

            ViewBag.fsId = fyid;
            ViewBag.officeId = officeId;

            IEnumerable<PayrollSalaryTableDTO> ViewList = _PayrollGenerationService.GetPayrollSalaryTable(fyid, officeId);

            ViewBag.FiscalsDropdown = _fiscalService.GetFiscalDropDown();
            ViewBag.OfficeList = _officeService.GetClildOfficeListByEmpCode(EmpCode);



            return View(ViewList);
        }
        public ActionResult GeneratePayroll()
        {
            int fyid = _fiscalService.GetCurrentFyId();
            var fdetails = _fiscalService.GetFiscalById(fyid);

            ViewBag.CurrentFiscalYear = fdetails.FyName;

            PayrollSalaryTableDTO Record = _PayrollGenerationService.GetPayollGenerationForm(fyid);
            return View(Record);
        }
        [HttpPost]
        public ActionResult GeneratePayroll(PayrollSalaryTableDTO Record)
        {
            int fyid = _fiscalService.GetCurrentFyId();
            var fdetails = _fiscalService.GetFiscalById(fyid);

            ViewBag.CurrentFiscalYear = fdetails.FyName;

            Record.FyId = fyid;

            PayrollSalaryTableDTO ModelRecord = _PayrollGenerationService.GetPayollGenerationForm(fyid);
            string Message = null;
            bool Success = false;
            bool UpdateExisting = false;
            try
            {
                if (ModelState.IsValid)
                {
                    PayrollSalaryTableDTO ReturnRecord = _PayrollGenerationService.GeneratePayroll(Record, out Message, out Success, out UpdateExisting);
                    if (Success == true)
                    {
                        TempData["Success"] = "Payroll Generated Successfully";

                    }
                    else
                    {
                        if (UpdateExisting)
                        {
                            ViewBag.UpdateExisting = "true";
                        }
                        TempData["Error"] = Message;
                        PayrollSalaryTableDTO FormRecord = _PayrollGenerationService.GetPayollGenerationForm(fyid);
                        return View(FormRecord);
                    }
                    ModelRecord.SalaryConfirmed = ReturnRecord.SalaryConfirmed;
                    TempData["Success"] = "Payroll Generated Successfully";
                    return RedirectToAction("Index", new { Id = ReturnRecord.Id });
                }
                else
                {
                    ViewBag.Error = "Form Validation Failed";
                    return View(ModelRecord);
                }
            }
            catch (Exception Ex)
            {
                ViewBag.Error = Ex.Message;
                return View(ModelRecord);
            }
        }

        #endregion
        public ActionResult ConfirmSalary(int Id)
        {

            try
            {
                _PayrollGenerationService.ConfirmPayroll(Id);

            }
            catch (Exception Exception)
            {
                TempData["Error"] = Exception.Message;
            }
            TempData["Success"] = "Salary confirmed successfully";
            return RedirectToAction("Index");
        }
    }
}