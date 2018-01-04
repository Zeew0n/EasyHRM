using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using BeeHrmClientWeb.CodeBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Payroll
{
    public class OverallPayrollSheetController : BaseController
    {

        private IModulServices _moduleService;
        private IDepartmentService _departmentServices;
        private IPayrollAllowanceMasterService _PayrollAllowanceMasterService;
        private IPayrollGenerationService _PayrollGenerationService;
        private IEmployeeService _EmployeeService;
        private IPayrollReportService _PayrollReportService;
        private IFiscalService _fiscalService;
        private IOfficeServices _officeService;
        private IUnitOfWork _unitofwork;


        public OverallPayrollSheetController()
        {
            _moduleService = new ModuleService();
            _PayrollAllowanceMasterService = new PayrollAllowanceMasterService();

            _PayrollGenerationService = new PayrollGenerationService();
            _departmentServices = new DepartmentService();
            _EmployeeService = new EmployeeService();
            _PayrollReportService = new PayrollReportService();
            _fiscalService = new FiscalService();
            _officeService = new OfficeServices();
            _unitofwork = new UnitOfWork();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
        }
        // GET: OverallPayrollSheet
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

            IEnumerable<PayrollSalaryViewModel> ViewList = _PayrollGenerationService.GetYearlyPayrollSalaryTable(fyid, officeId);

            ViewBag.FiscalsDropdown = _fiscalService.GetFiscalDropDown();
            ViewBag.OfficeList = _officeService.GetClildOfficeListByEmpCode(EmpCode);

            return View(ViewList);
        }

        public ActionResult Details(int empcode)
        {
            int EmpCode = Convert.ToInt32(Session["Empcode"]);
            int fyid = _fiscalService.GetCurrentFyId();
            int officeId = _EmployeeService.GetEmployeeDetails(empcode).OfficeId;

            ViewBag.fsId = fyid;
            ViewBag.officeId = officeId;
            IEnumerable<PayrollSalaryViewModel> ViewList = _PayrollGenerationService.GetYearlyPayrollSalaryTable(fyid, officeId);
            PayrollSalaryViewModel singleData = ViewList.Where(a => a.EmployeeCode == empcode).SingleOrDefault();
            foreach (var item in singleData.AllowanceViewModel)
            {
                item.AllowanceName = _unitofwork.PayrollAllowanceMasterRepository.GetById(item.AllowanceId).AllowanceName;
            }
            double TotalTax = 0;
            foreach (var item in singleData.TaxModel)
            {
                if (item.OrderNumber == 1)
                {
                    item.TaxName = "Paid Social Sec Tax 1 % :";
                }
                else if (item.OrderNumber == 2)
                {
                    item.TaxName = "Paid Tax 15% :";
                }
                else
                {
                    item.TaxName = "Paid Tax 25% :";
                }
                TotalTax += (double)item.totalValue;
            }

            singleData.TotalPaidTax = TotalTax;
            decimal TotalInsured = 0;
            decimal TotalPremium = 0;
            foreach (var item in singleData.InsurancePremium)
            {
                TotalInsured += item.InsuredAmount;
                TotalPremium += (decimal)item.PremiumAmount;
            }
            ViewBag.TotalInsured = TotalInsured;
            ViewBag.TotalPremium = TotalPremium;
            //foreach (var item1 in singleData.AllowanceViewModel)
            //{
            //    singleData.BankDay = _unitofwork.PayrollAllowanceMasterRepository.GetById(item1.AllowanceId).;
            //}
            return View(singleData);
        }
    }
}