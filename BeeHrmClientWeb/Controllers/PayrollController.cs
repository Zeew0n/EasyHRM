using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using BeeHrmClientWeb.CodeBase;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers
{
    public class PayrollController : BaseController
    {
        private IModulServices _moduleService;
        private IDepartmentService _departmentServices;
        private IPayrollAllowanceMasterService _PayrollAllowanceMasterService;
        private IPayrollMonthDescriptionService _PayrollMonthDescriptionService;
        private IPayrollGenerationService _PayrollGenerationService;
        private ITaxSetupService _TaxSetupService;
        private ITaxDetailService _TaxDetailService;
        private IPayrollArrearService _PayrollArrearService;
        private IEmployeeService _EmployeeService;
        private IPayrollReportService _PayrollReportService;
        public PayrollController()
        {
            _moduleService = new ModuleService();
            _PayrollAllowanceMasterService = new PayrollAllowanceMasterService();
            _PayrollMonthDescriptionService = new PayrollMonthDescriptionService();
            _PayrollGenerationService = new PayrollGenerationService();
            _departmentServices = new DepartmentService();
            _TaxSetupService = new TaxSetupService();
            _TaxDetailService = new TaxDetailService();
            _PayrollArrearService = new PayrollArrearService();
            _EmployeeService = new EmployeeService();
            _PayrollReportService = new PayrollReportService();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
        }
        #region PayrollNotDefaultAllowances
        public ActionResult PayrollAllowances()
        {

            IEnumerable<PayrollAllowanceMasterDTO> PayrollAllowanceMasterSetup = _PayrollAllowanceMasterService.GetAllPayrollAllowanceMaster();
            return View(PayrollAllowanceMasterSetup);
        }
        public ActionResult PayrollAllowancesCreate()
        {
            PayrollAllowanceMasterDTO Record = _PayrollAllowanceMasterService.GetPayrollAllowanceCreateForm();
            return View(Record);
        }
        [HttpPost]
        public ActionResult PayrollAllowancesCreate(PayrollAllowanceMasterDTO Record)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                else
                {
                    _PayrollAllowanceMasterService.InsertIntoPayrollAllowance(Record);
                    return RedirectToAction("PayrollAllowances");
                }
            }
            catch (Exception Ex)
            {
                ViewBag.Error = Ex.Message;
                return View();
            }

        }
        public ActionResult PayrollAllowancesEdit(int Id)
        {
            PayrollAllowanceMasterDTO Record = _PayrollAllowanceMasterService.GetPayrollMasterByMasterId(Id);
            return View(Record);
        }
        [HttpPost]
        public ActionResult PayrollAllowancesEdit(PayrollAllowanceMasterDTO Record)
        {
            PayrollAllowanceMasterDTO data = _PayrollAllowanceMasterService.GetPayrollMasterByMasterId(Record.AllowanceMasterId);
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(data);
                }
                else
                {
                    PayrollAllowanceMasterDTO ReturnRecord = _PayrollAllowanceMasterService.UpdatePayrollAllowance(Record);
                    ViewBag.Success = "Data Updated Successfully";
                    return View(data);
                }
            }
            catch (Exception Ex)
            {
                ViewBag.Error = Ex.Message;
                return View(data);
            }

        }
        #endregion
        #region PayrollDefaultAllowances
        public ActionResult RetirementFunds()
        {
            try
            {
                IEnumerable<PayrollAllowanceMasterDTO> Record = _PayrollAllowanceMasterService.GetAllRetirementFunds();
                return View(Record);
            }
            catch (Exception Exception)
            {

            }
            return View();
        }
        public ActionResult RetirementFundsEdit(int Id)
        {
            PayrollAllowanceMasterDTO Record = _PayrollAllowanceMasterService.GetPayrollMasterByMasterId(Id);
            return View(Record);
        }
        [HttpPost]
        public ActionResult RetirementFundsEdit(PayrollAllowanceMasterDTO Record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _PayrollAllowanceMasterService.UpdatePayrollAllowance(Record);
                }
                else
                {
                    TempData["Error"] = "Form validation error.";
                    return View();
                }
            }
            catch (Exception Exception)
            {
                TempData["Error"] = Exception.Message;
                return View();
            }
            TempData["Success"] = "Fund Updated successfully";
            return RedirectToAction("RetirementFunds");
        }
        #endregion
        #region AssignEmployees
        public ActionResult AssignEmployees(int Id)
        {
            int officeID = Convert.ToInt32(Session["OfficeId"]);
            PayrollAllowanceMasterDTO Record = _PayrollAllowanceMasterService.GetPayrollDetailByMasterId(Id, officeID);
            return View(Record);
        }
        [HttpPost]
        public ActionResult AssignEmployees(string[] SelectedEmployees, int Id, string[] Value, string[] PercentageAmount)
        {

            _PayrollAllowanceMasterService.InsertIntoPayrollAllowanceDetail(SelectedEmployees, Id, Value, PercentageAmount);
            return RedirectToAction("AssignEmployees/" + Id);
        }
        #endregion
        //#region PayrollMonths
        //public ActionResult PayrollMonths()
        //{
        //    IEnumerable<PayrollMonthDescriptionDTO> Record = _PayrollMonthDescriptionService.GetAllPayrollMonths();
        //    return View(Record);
        //}

        //public ActionResult AddPayrollMonth()
        //{
        //    PayrollMonthDescriptionDTO Record = new PayrollMonthDescriptionDTO();
        //    Record.Fiscals = _PayrollMonthDescriptionService.GetFiscalDropDown();
        //    return View(Record);
        //}
        //[HttpPost]
        //public ActionResult AddPayrollMonth(PayrollMonthDescriptionDTO Record)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        _PayrollMonthDescriptionService.InsertIntoMonthDescription(Record);
        //        return RedirectToAction("PayrollMonths");
        //    }
        //}
        //public ActionResult EditPayrollMonth(int Id)
        //{
        //    PayrollMonthDescriptionDTO Record = _PayrollMonthDescriptionService.GetPayrollMonthById(Id);
        //    return View(Record);
        //}
        //[HttpPost]
        //public ActionResult EditPayrollMonth(PayrollMonthDescriptionDTO Data)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _PayrollMonthDescriptionService.UpdatePayrollMonth(Data);
        //        }
        //        else
        //        {
        //            ViewBag.Error = "Model Validation Error!";
        //            return RedirectToAction("EditPayrollMonth/" + Data.Id);

        //        }
        //        ViewBag.Success = "Month Updated Successfully";
        //    }
        //    catch (Exception Ex)
        //    {
        //        ViewBag.Error = Ex.Message;

        //    }
        //    return RedirectToAction("PayrollMonths");
        //}
        //#endregion
        //#region GeneratePayroll
        //public ActionResult GeneratePayroll()
        //{
        //    PayrollSalaryTableDTO Record = _PayrollGenerationService.GetPayollGenerationForm();
        //    return View(Record);
        //}
        //[HttpPost]
        //public ActionResult GeneratePayroll(PayrollSalaryTableDTO Record)
        //{
        //    PayrollSalaryTableDTO ModelRecord = _PayrollGenerationService.GetPayollGenerationForm();
        //    string Message = null;
        //    bool Success = false;
        //    bool UpdateExisting = false;
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            PayrollSalaryTableDTO ReturnRecord = _PayrollGenerationService.GeneratePayroll(Record, out Message, out Success, out UpdateExisting);
        //            if (Success == true)
        //            {
        //                TempData["Success"] = "Payroll Generated Successfully";

        //            }
        //            else
        //            {
        //                if (UpdateExisting)
        //                {
        //                    ViewBag.UpdateExisting = "true";
        //                }
        //                TempData["Error"] = Message;
        //                PayrollSalaryTableDTO FormRecord = _PayrollGenerationService.GetPayollGenerationForm();
        //                return View(FormRecord);
        //            }
        //            ModelRecord.SalaryConfirmed = ReturnRecord.SalaryConfirmed;
        //            TempData["Success"] = "Payroll Generated Successfully";
        //            return RedirectToAction("SalarySheetDetail", new { Id = ReturnRecord.Id });
        //        }
        //        else
        //        {
        //            ViewBag.Error = "Form Validation Failed";
        //            return View(ModelRecord);
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        ViewBag.Error = Ex.Message;
        //        return View(ModelRecord);
        //    }
        //}


        //#endregion
        //#region SalarySheet

        //public ActionResult SalarySheet()
        //{
        //    IEnumerable<PayrollSalaryTableDTO> ViewList = _PayrollGenerationService.GetPayrollSalaryTable();
        //    return View(ViewList);
        //}
        //public ActionResult ConfirmSalary(int Id)
        //{
        //    try
        //    {
        //        _PayrollGenerationService.ConfirmPayroll(Id);

        //    }
        //    catch (Exception Exception)
        //    {
        //        TempData["Error"] = Exception.Message;
        //    }
        //    TempData["Success"] = "Salary confirmed successfully";
        //    return RedirectToAction("SalarySheet");
        //}
        //public ActionResult SalarySheetDetail(int Id)
        //{
        //    IEnumerable<PayrollSalaryMasterSheetDTO> view = _PayrollGenerationService.GetPayrollSalaryMasterSheet(Id);
        //    return View(view);
        //}

        //public ActionResult IndividualSalarySheet(int Id)
        //{
        //    PayrollSalaryMasterSheetDTO db = _PayrollGenerationService.GetIndividualSalarySheetDescription(Id);
        //    return View(db);
        //}
        //#endregion
        /*
        #region TaxSetup
        public ActionResult TaxSetup()
        {
            IEnumerable<TaxSetupDTO> TaxSetupList = _TaxSetupService.GetAllTaxSetup();
            return View(TaxSetupList);
        }

        public ActionResult TaxSetupCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TaxSetupCreate(TaxSetupDTO Record)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Error = "Form validation errors.";
                    return View();
                }
                else
                {
                    ViewBag.Success = "Tax setup created successfully.";
                    _TaxSetupService.InsertIntoTaxSetup(Record);
                    return RedirectToAction("TaxSetup");
                }
            }
            catch (Exception Exception)
            {
                ViewBag.Error = Exception.Message;
                return View();
            }
        }
        public ActionResult TaxSetupEdit(int Id)
        {
            TaxSetupDTO TaxSetupDTO = _TaxSetupService.GetTaxSetupById(Id);
            return View(TaxSetupDTO);
        }
        [HttpPost]
        public ActionResult TaxSetupEdit(TaxSetupDTO Record)
        {
            int Id = _TaxSetupService.UpdateTaxSetup(Record);
            IEnumerable<TaxSetupDTO> TaxSetupList = _TaxSetupService.GetAllTaxSetup();
            return View("TaxSetup", TaxSetupList);
        }

        public ActionResult TaxDetails()
        {
            IEnumerable<TaxDetailDTO> TaxDetailList = _TaxDetailService.GetAllTaxDetails();
            return View(TaxDetailList);
        }
        public ActionResult TaxSetupDetails(int MasterId)
        {
            TaxSetupDTO TaxSetup = _TaxSetupService.GetTaxSetupById(MasterId);
            IEnumerable<TaxDetailDTO> TaxDetailList = _TaxDetailService.GetAllTaxDetailsByMasterId(MasterId);
            ViewBag.MasterName = TaxSetup.TaxName;
            return View(TaxDetailList);
        }
        [Route("Payroll/TaxDetailCreate/{MasterId}")]
        public ActionResult TaxDetailCreate(int MasterId)
        {
            return View();
        }

        [HttpPost]
        [Route("Payroll/TaxDetailCreate/{MasterId}")]
        public ActionResult TaxDetailCreate(TaxDetailDTO Record)
        {
            _TaxDetailService.InsertIntoTaxDetail(Record);
            TaxSetupDTO TaxSetup = _TaxSetupService.GetTaxSetupById(Record.MasterId);
            IEnumerable<TaxDetailDTO> TaxDetailList = _TaxDetailService.GetAllTaxDetailsByMasterId(Record.MasterId);
            ViewBag.MasterName = TaxSetup.TaxName;
            return View("TaxSetupDetails", TaxDetailList);
        }
        [Route("Payroll/TaxDetailEdit/{Id}")]
        public ActionResult TaxDetailEdit(int Id)
        {
            TaxDetailDTO TaxDetailList = _TaxDetailService.GetTaxDetailById(Id);
            TaxSetupDTO Setup = _TaxSetupService.GetTaxSetupById(TaxDetailList.MasterId);
            ViewBag.MasterName = Setup.TaxName;
            return View(TaxDetailList);
        }
        [HttpPost]
        [Route("Payroll/TaxDetailEdit/{Id}")]
        public ActionResult TaxDetailEdit(TaxDetailDTO Record)
        {
            _TaxDetailService.UpdateTaxDetail(Record);
            TaxSetupDTO TaxSetup = _TaxSetupService.GetTaxSetupById(Record.MasterId);
            IEnumerable<TaxDetailDTO> TaxDetailList = _TaxDetailService.GetAllTaxDetailsByMasterId(Record.MasterId);
            ViewBag.MasterName = TaxSetup.TaxName;
            return View("TaxSetupDetails", TaxDetailList);
        }
        #endregion
    */
        #region PayrollArrears
        public ActionResult PayrollArrears()
        {
            IEnumerable<PayrollArrearsDTO> items = _PayrollArrearService.GetPayrollArrears();
            return View(items);
        }
        public ActionResult PayrollArrearsCreate()
        {
            PayrollArrearsDTO item = _PayrollArrearService.GetCreateForm();
            return View(item);
        }
        [HttpPost]
        public ActionResult PayrollArrearsCreate(PayrollArrearsDTO item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _PayrollArrearService.InsertIntoArrears(item);
                }
                else
                {
                    ViewBag.Error = "Form validation error.";
                    return View();
                }
            }
            catch (Exception Exception)
            {
                ViewBag.Error = Exception.Message;
                return View();
            }
            ViewBag.Success = "Arrear created successfully.";
            IEnumerable<PayrollArrearsDTO> items = _PayrollArrearService.GetPayrollArrears();
            return View("PayrollArrears", items);
        }
        #endregion
        #region TaxAnnualReport
        public ActionResult TaxAnnualReport()
        {
            IEnumerable<EmployeeDTO> Record = _EmployeeService.GetEmployeeByOfficeidPayroll(1);

            PayrollActualAnnualTaxModel ReturnRecord = new PayrollActualAnnualTaxModel();
            ReturnRecord.OfficeList = _PayrollReportService.GetOfficeSelectList();
            ReturnRecord.EmployeeList = Record;
            ReturnRecord.OfficeId = 1;
            return View(ReturnRecord);
        }
        public ActionResult IndividualTaxAnnualReport(int Id)
        {
            IndividualYearlyTaxEstimationModel Record = _PayrollReportService.GetIndividualYearlyTax(Id);
            return View(Record);
        }
        public ActionResult TaxAnnualReportEstimation()
        {
            IEnumerable<EmployeeDTO> Record = _EmployeeService.GetEmployeeByOfficeidPayroll(1);
            PayrollActualAnnualTaxModel ReturnRecord = new PayrollActualAnnualTaxModel();
            ReturnRecord.EmployeeList = Record;
            return View(ReturnRecord);
        }
        public ActionResult IndividualTaxAnnualReportEstimation(int Id)
        {
            IndividualYearlyTaxEstimationModel Record = _PayrollReportService.GetIndividualYearlyTaxEstimation(Id);
            return View(Record);
        }

        #endregion
    }
}