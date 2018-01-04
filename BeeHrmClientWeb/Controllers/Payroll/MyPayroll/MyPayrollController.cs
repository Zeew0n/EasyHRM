using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHrmClientWeb.CodeBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Payroll.MyPayroll
{
    public class MyPayrollController : BaseController
    {
        // GET: PayrollSalarySheet


        private IPayrollGenerationService _PayrollGenerationService;

        private IFiscalService _fiscalService;



        public MyPayrollController()
        {
            _PayrollGenerationService = new PayrollGenerationService();
            _fiscalService = new FiscalService();

        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
        }
        public ActionResult Index()
        {
            decimal totalRankandGradesalary=0;
            decimal totalRankAllowances=0;
            decimal totalAllowances=0;
            decimal totalGrossSalary=0;
            decimal totalpfcompany=0;
            decimal totalpfself=0;
            decimal totalpfExtra=0;
            decimal totalpf=0;
            decimal totalcit=0;
            decimal totaltax=0;
            decimal totalcashinhand=0;
            ViewBag.FiscalsDropdown = _fiscalService.GetFiscalDropDown();
            int EmpCode = Convert.ToInt32(Session["Empcode"]);

            int fyid = Convert.ToInt32(Request["fyId"]);
            int officeId = Convert.ToInt32(Session["OfficeId"]);

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

            List<MyPayrollSalaryTableDTO> MyPayroll = new List<MyPayrollSalaryTableDTO>();
            IEnumerable<PayrollSalaryTableDTO> ViewList = _PayrollGenerationService.GetMyPayrollSalaryTable(fyid, officeId).Where(a=>a.SalaryConfirmed==true).ToList();
            foreach (var data in ViewList)
            {

                IEnumerable<PayrollSalaryMasterSheetDTO> view = _PayrollGenerationService.GetPayrollSalaryMasterSheet(data.Id);
                PayrollSalaryMasterSheetDTO singledata = view.Where(a => a.EmployeeCode == EmpCode).FirstOrDefault();
                if (singledata != null)
                {
                    MyPayrollSalaryTableDTO singles = new MyPayrollSalaryTableDTO()
                    {
                        Id = singledata.Id,
                        PayrollMonth = data.PayrollMonthDescription.MonthNameNepali,
                        FyId = data.Fiscal.FyName,
                        PfSelf=singledata.PfSelf,
                        PfCompany=singledata.PfCompany,
                        PfExtra=singledata.PfExtra,
                        PF = singledata.PfSelf + singledata.PfCompany + singledata.PfExtra,
                        CIT = singledata.CitAmount,
                        Tax = singledata.TaxAmount,
                        GrossSalary = singledata.GrossSalary,
                        CashInHand = singledata.SalaryAfterTaxDeduction,
                        RankAndGradeSalary=singledata.RankAndGradeSalary,
                        RankAllowances=singledata.RankOtherAllowances,
                        TotalAllowances=singledata.TotalRankAllowances,
                        
                    };
                    totalRankandGradesalary += singledata.RankAndGradeSalary;
                    totalRankAllowances += singledata.RankOtherAllowances;
                    totalAllowances += singledata.TotalRankAllowances;
                    totalpfself += singledata.PfSelf;
                    totalpfcompany += singledata.PfCompany;
                    totalpfExtra += singledata.PfExtra;
                    totalpf += singles.PF;
                    totalcit = singledata.CitAmount;
                    totaltax += singledata.TaxAmount;
                    totalGrossSalary += singledata.GrossSalary;
                    totalcashinhand += singledata.SalaryAfterTaxDeduction;
                    MyPayroll.Add(singles);
                }

              

            }
            ViewBag.TotalRankAndGradeSalary = totalRankandGradesalary.ToString("0.00");
            ViewBag.TotalRankAllowances = totalRankAllowances.ToString("0.00");
            ViewBag.TotalAllowances = totalAllowances.ToString("0.00");
            ViewBag.TotalPFSelf = totalpfself.ToString("0.00");
            ViewBag.TotalPFCompany = totalpfcompany.ToString("0.00");
            ViewBag.TotalPfExtra = totalpfExtra.ToString("0.00");
            ViewBag.TotalPf = totalpf.ToString("0.00");
            ViewBag.TotalCit = totalcit.ToString("0.00");
            ViewBag.TotalTax = totaltax.ToString("0.00");
            ViewBag.TotalGrossSalary = totalGrossSalary.ToString("0.00");
            ViewBag.TotalCashInHand = totalcashinhand.ToString("0.00");


            return View("../Payroll/MyPayroll/Index", MyPayroll);
        }

        public ActionResult SalarySheetDetail(int Id)
        {
            PayrollSalaryMasterSheetDTO db = _PayrollGenerationService.GetIndividualSalarySheetDescription(Id);
            return View("../Payroll/MyPayroll/SalarySheetDetail", db);
        }
    }
}