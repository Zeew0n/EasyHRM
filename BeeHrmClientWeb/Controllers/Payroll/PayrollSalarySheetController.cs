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
    public class PayrollSalarySheetController : BaseController
    {
        // GET: PayrollSalarySheet
        private IModulServices _moduleService;
        private IDepartmentService _departmentServices;
        private IPayrollAllowanceMasterService _PayrollAllowanceMasterService;
        private IPayrollGenerationService _PayrollGenerationService;
        private IEmployeeService _EmployeeService;
        private IPayrollReportService _PayrollReportService;
        private IFiscalService _fiscalService;
        private IOfficeServices _officeService;


        public PayrollSalarySheetController()
        {
            _moduleService = new ModuleService();
            _PayrollAllowanceMasterService = new PayrollAllowanceMasterService();

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
        #region SalarySheet

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

        public ActionResult SalarySheetDetail(int Id)
        {
            IEnumerable<PayrollSalaryMasterSheetDTO> view = _PayrollGenerationService.GetPayrollSalaryMasterSheet(Id);
            return View(view);
        }

        public ActionResult IndividualSalarySheet(int Id)
        {
            PayrollSalaryMasterSheetDTO db = _PayrollGenerationService.GetIndividualSalarySheetDescription(Id);
            return View(db);
        }
        public ActionResult SalaryReceipt(int Id)
        {
            PayrollSalaryMasterSheetDTO payrollData = _PayrollGenerationService.GetIndividualSalarySheetDescription(Id);

            return View("../PayrollSalarySheet/PayrollReceipt", payrollData);
        }
        #endregion
    }
}