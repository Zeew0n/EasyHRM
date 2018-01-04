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
    public class PayrollEmployeeAllowanceController : BaseController
    {
        private IModulServices _moduleService;
        private IDepartmentService _departmentServices;
        private IPayrollAllowanceMasterService _PayrollAllowanceMasterService;
        private IPayrollGenerationService _PayrollGenerationService;
        private IEmployeeService _EmployeeService;
        private IPayrollReportService _PayrollReportService;
        private IOfficeServices _officeService;
        public PayrollEmployeeAllowanceController()
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
        // GET: PayrollEmployeeAllowance
        public ActionResult Index()
        {
            IEnumerable<PayrollAllowanceMasterDTO> PayrollAllowanceMasterSetup = _PayrollAllowanceMasterService.GetAllPayrollAllowanceMaster();

            return View(PayrollAllowanceMasterSetup);

        }
        
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
            return RedirectToAction("AssignEmployees/" + Id,ViewBag.officeId);
        }
        #endregion
    }
}