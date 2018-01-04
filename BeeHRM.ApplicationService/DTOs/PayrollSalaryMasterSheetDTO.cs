using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.DTOs
{
    public class PayrollSalaryMasterSheetDTO
    {
        public int Id { get; set; }
        [DisplayName("Employee code")]
        public int EmployeeCode { get; set; }
        [DisplayName("Employee name")]
        public string EmployeeName { get; set; }
        [DisplayName("Maritial status")]
        public string MaritialStatus { get; set; }
        [DisplayName("Designation Code")]
        public int DesignationCode { get; set; }
        [DisplayName("Department code")]
        public int DepartmentCode { get; set; }
        [DisplayName("Branch code")]
        public int BranchCode { get; set; }
        [DisplayName("Rank Salary")]
        public decimal RankSalary { get; set; }
        [DisplayName("Current grade")]
        public int CurrentGrade { get; set; }
        [DisplayName("Rank max grade")]
        public decimal RankMaxGrade { get; set; }
        [DisplayName("Rank per grade amount")]
        public decimal RankPerGradeAmount { get; set; }
        [DisplayName("Grade Salary")]
        public decimal GradeSalary { get; set; }
        [DisplayName("Basic salary")]
        public decimal RankAndGradeSalary { get; set; }
        [DisplayName("PF self")]
        public decimal PfSelf { get; set; }
        [DisplayName("PF company")]
        public decimal PfCompany { get; set; }
        [DisplayName("PF extra")]
        public decimal PfExtra { get; set; }
        [DisplayName("Bank allowance")]
        public decimal BankAllowance { get; set; }
        [DisplayName("Special allowance")]
        public decimal RankSpecialAllowance { get; set; }
        [DisplayName("Inchargeship allowance")]
        public decimal RankInchargeShipAllowance { get; set; }
        [DisplayName("Rank other allowance")]
        public decimal RankOtherAllowances { get; set; }
        [DisplayName("Total rank allowance")]
        public decimal TotalRankAllowances { get; set; }
        [DisplayName("Salary with rank allowance")]
        public decimal SalaryWithRankAllowance { get; set; }
        public decimal RemoteAllowanceId { get; set; }
        public string RemoteAllowanceType { get; set; }
        [DisplayName("Remote allowance")]
        public decimal RemoteAllowance { get; set; }
        [DisplayName("Remote tax excemption")]
        public decimal RemoteTaxExcemption { get; set; }
        [DisplayName("Taxable allowance amount")]
        public decimal TaxableAllowanceAmount { get; set; }
        [DisplayName("Non taxable allowance amount")]
        public decimal NonTaxableAllowanceAmount { get; set; }
        [DisplayName("Taxable PF & CIT amount")]
        public decimal TaxablePfCitAmount { get; set; }
        [DisplayName("CIT Amount")]
        public decimal CitAmount { get; set; }
        [DisplayName("Gross salary")]
        public decimal GrossSalary { get; set; }
        [DisplayName("Taxable salary")]
        public decimal TaxableSalary { get; set; }
        [DisplayName("Tax Amount")]
        public decimal TaxAmount { get; set; }
        [DisplayName("Actual Salary")]
        public decimal SalaryAfterTaxDeduction { get; set; }
        [DisplayName("Is attendance ignored")]
        public bool EmpAttendanceIgnore { get; set; }
        [DisplayName("Working days")]
        public decimal WorkingDays { get; set; }
        [DisplayName("Worked days")]
        public decimal WorkedDays { get; set; }
        [DisplayName("Per day salary")]
        public decimal PerDaysalary { get; set; }
        [DisplayName("Deductable days")]
        public decimal DeductableDays { get; set; }
        [DisplayName("Actual salary")]
        public decimal ActualSalary { get; set; }
        public int PayrollSalaryTableId { get; set; }
        [DisplayName("Rank & Grade salary")]
        public decimal TotalBasicSalaryForPf { get; set; }
        [DisplayName("Total basic salary")]
        public decimal TotalBasicSalary { get; set; }
        [DisplayName("Gross income")]
        public decimal GrossIncome { get; set; }
        public EmployeeDTO Employee { get; set; }
        public DepartmentDTO Department { get; set; }
        public DesignationDTO Designation { get; set; }
        public IEnumerable<PayrollEmployeeTaxDetailDTO> PayrollEmployeeTaxDetails { get; set; }
        public DataTable PayrollEmployeeTaxDetail { get; set; }
        public IEnumerable<PayrollSalaryDetailSheetDTO> PayrollSalaryDetailSheets { get; set; }
        public virtual PayrollSalaryTableDTO PayrollSalaryTable { get; set; }
        public virtual OfficeDTOs Office { get; set; }
        public decimal totalPf { get; set; }
        public decimal totalAllowance { get; set; }
    }

    public class PayrollSalaryMasterSheetListDTO
    {
        public IEnumerable<PayrollSalaryMasterSheetDTO> PayrollSalaryMasterSheetDTO { get; set; }
        public  IEnumerable<SelectListItem> FiscalYearList { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal totalRank { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal totalGrade { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal totalAllowance { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal totalCIT { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal totalTax { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal totalActual { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal totalPfSelf { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal totalPfCompany { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal totalPfExtra { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal totalTaxablePfCitAmount { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal totalBasicSalaryForPf { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal totalGrossIncome { get; set; }

    }
}
