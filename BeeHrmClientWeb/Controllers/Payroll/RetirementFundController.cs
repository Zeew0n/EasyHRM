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
    public class RetirementFundController : BaseController
    {

        private IModulServices _moduleService;
        private IDepartmentService _departmentServices;
        private IPayrollAllowanceMasterService _PayrollAllowanceMasterService;
        private IPayrollGenerationService _PayrollGenerationService;
        private IEmployeeService _EmployeeService;
        private IPayrollReportService _PayrollReportService;
        private IOfficeServices _officeService;

        public RetirementFundController()
        {
            _moduleService = new ModuleService();
            _PayrollAllowanceMasterService = new PayrollAllowanceMasterService();

            _PayrollGenerationService = new PayrollGenerationService();
            _departmentServices = new DepartmentService();
            _EmployeeService = new EmployeeService();
            _PayrollReportService = new PayrollReportService();
            _officeService = new OfficeServices();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
        }
        // GET: RetirementFund

        #region PayrollDefaultAllowances
        public ActionResult Index()
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
            return RedirectToAction("Index");
        }
        #endregion
        #region AssignEmployees
        [HttpGet]
        public ActionResult AssignEmployees(int Id)
        {
            int officeId = Convert.ToInt32(Request["officeId"]);
            if (officeId == 0)
            {
                officeId = Convert.ToInt32(Session["OfficeId"]);
            }
            ViewBag.officeId = officeId;
            ViewBag.OfficeList = _officeService.GetOfficeData();

            PayrollAllowanceMasterDTO Record = _PayrollAllowanceMasterService.GetPayrollDetailByMasterId(Id, officeId);
            return View(Record);
        }
        [HttpPost]
        public ActionResult AssignEmployees(int Id, int officeId)
        {
            //int officeId = Convert.ToInt32(Request["officeId"]);
            //if (officeId == 0)
            //{
            //    officeId = Convert.ToInt32(Session["OfficeId"]);
            //}
            ViewBag.officeId = officeId;
            ViewBag.OfficeList = _officeService.GetOfficeData();

            PayrollAllowanceMasterDTO Record = _PayrollAllowanceMasterService.GetPayrollDetailByMasterId(Id, officeId);
            return View(Record);
        }
        [HttpPost]
        public ActionResult AssignSelectedEmployees(string[] SelectedEmployees, int Id, string[] Value, string[] PercentageAmount)
        {
            int officeId = Convert.ToInt32(Request["OfficeId"]);
            ViewBag.officeId = officeId;
            _PayrollAllowanceMasterService.InsertIntoPayrollAllowanceDetail(SelectedEmployees, Id, Value, PercentageAmount);
            return RedirectToAction("AssignEmployees/" + Id, ViewBag.officeId);
        }
        #endregion
    }
}