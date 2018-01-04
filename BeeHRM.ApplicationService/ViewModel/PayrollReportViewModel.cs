using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.ViewModel
{
    public class PayrollActualAnnualTaxModel
    {
        public IEnumerable<EmployeeDTO> EmployeeList { get; set; }
        public IEnumerable<SelectListItem> OfficeList { get; set; }
        public int OfficeId { get; set; }
    }
    public class IndividualYearlyTaxEstimationModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string DesignationName { get; set; }
        public int Grade { get; set; }
        public decimal RankSalary { get; set; }
        public PayrollSalaryMasterSheetDTO SalaryMasterSheet { get; set; }
        public decimal AnnualSalary { get; set; }
        public decimal Gradesalary { get; set; }
        public decimal BankAllowance { get; set; }
        public decimal RegionalAllowance { get; set; }
        public decimal OtherAllowances { get; set; }
        public IEnumerable<PayrollEmployeeTaxDetailDTO> EmployeeTaxDetail { get; set; }
        public decimal FirstSlabTax { get; set; }
        public decimal SecondSlabTax { get; set; }
        public string SecondSlabName { get; set; }

        public decimal PfSelf { get; set; }

        public decimal PfCompany { get; set; }

        public decimal Cit { get; set; }

        public decimal PfExtra { get; set;}

        public decimal RankAllowancesTotal { get; set; }
        public IEnumerable<PayrollAllowanceMasterDTO> PayrollAllowanceMaster { get; set; }

        public decimal TaxableYearlyIncome { get; set; }

        public decimal YearlySocialSecurityTax { get; set; }

        public decimal AccumulatedTaxAmount { get; set; }

        public decimal SpecialAllowance { get; set; }

        public decimal InchargeshipAllowance { get; set; }
       
    }
}
