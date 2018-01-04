using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ViewModel
{
    public class PayrollSalaryViewModel
    {
        public int Id { get; set; }
        public int EmployeeCode { get; set; }
        public double Salary { get; set; }
        public double GradeSalary { get; set; }
        public double BankAllowance { get; set; }
        public double RankSpecialAllowance { get; set; }
        public double TankInchargeShipAllowance { get; set; }
        public double RankOtherAllowances { get; set; }
        public double RemoteTaxExcemption { get; set; }
        public double PFContribution { get; set; }
        public double CitAmount { get; set; }
        public bool SalaryConfirmed { get; set; }
        public double TaxableSalary { get; set; }
        public string EmployeeName { get; set; }
        public string OfficeName { get; set; }
        public string FiscalYear { get; set; }
        public string MonthNameNepali { get; set; }
        public string EmpPost { get; set; }
        public int GradeNo { get; set; }
        public string CitNo { get; set; }
        public string PANNo { get; set; }
        public double RegionalAllowance { get; set; }
        public double BankDay { get; set; }
        public double OneMonthSalPF { get; set; }
        public double Dashain { get; set; }
        public double OtherIncome { get; set; }
        public double MedicalDays { get; set; }
        public double Accomodation { get; set; }
        public double Dress { get; set; }
        public double Incentive { get; set; }
        public double HomeLeaveIncashment { get; set; }
        public double Bonus { get; set; }
        public decimal? LoanIntIncome { get; set; }
        public double DeductedCIT { get; set; }
        public double PMReliefFund { get; set; }
        public double PaidSocailSecTax { get; set; }
        public double PaidTax { get; set; }
        public double TotalPaidTax { get; set; }
        public double ProvidendFund { get; set; }
        public double Insurance { get; set; }
        public double EligibleRF { get; set; }
        public double RegionalDiscount { get; set; }
        public double TaxableIncome { get; set; }
        public double TaxExcemption { get; set; }
        public int InsuranceID { get; set; }
        public string InsuranceCompanyName { get; set; }
        public string PolicyNumber { get; set; }
        public double InsuredAmount { get; set; }
        public DateTime InsuranceEffectedDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public double AnnualPremium { get; set; }
        public virtual IEnumerable<PayrollInsurancePremiumDTO> InsurancePremium { get; set; }
        public virtual IEnumerable<AllowanceViewModel> AllowanceViewModel { get; set; }
        public virtual IEnumerable<TaxViewModel> TaxModel { get; set; }

        //public Employee Employee { get; set; }
        //public virtual PayrollMonthDescription PayrollMonthDescription { get; set; }
        //public virtual Fiscal Fiscal { get; set; }
        //public virtual Office Office { get; set; }

    }
    public partial class AllowanceViewModel {
        public int AllowanceId { get; set; }
        public string AllowanceName { get; set; }
        public decimal totalValue { get; set; }
    }

    public partial class TaxViewModel
    {
        public int OrderNumber { get; set; }
        public string TaxName { get; set; }
        public decimal totalValue { get; set; }
    }
}
