using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.ViewModel;
using System.Collections.Generic;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IPayrollGenerationService
    {
        PayrollSalaryTableDTO GetPayollGenerationForm(int fyid);
        PayrollSalaryTableDTO GeneratePayroll(PayrollSalaryTableDTO Record, out string Message, out bool Success, out bool UpdateExisting);
        IEnumerable<PayrollSalaryTableDTO> GetMyPayrollSalaryTable(int fyid, int officeId);
        IEnumerable<PayrollSalaryTableDTO> GetPayrollSalaryTable(int fyid, int officeId);
        IEnumerable<PayrollSalaryViewModel> GetYearlyPayrollSalaryTable(int fyid, int officeId);
        IEnumerable<PayrollSalaryMasterSheetDTO> GetPayrollSalaryMasterSheet(int Id);
        PayrollSalaryMasterSheetDTO GetIndividualSalarySheetDescription(int Id);
        void ConfirmPayroll(int Id);
    }
}
