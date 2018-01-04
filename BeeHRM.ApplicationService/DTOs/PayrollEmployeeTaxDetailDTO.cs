namespace BeeHRM.ApplicationService.DTOs
{
    public class PayrollEmployeeTaxDetailDTO
    {
        public int Id { get; set; }
        public int EmployeeCode { get; set; }
        public decimal MaxAmount { get; set; }
        public decimal DeductPercentage { get; set; }
        public decimal DeductedAmount { get; set; }
        public int OrderNumber { get; set; }
        public int PayrollMasterSheetID { get; set; }
        public virtual PayrollSalaryMasterSheetDTO PayrollSalaryMasterSheet { get; set; }
    }
}
