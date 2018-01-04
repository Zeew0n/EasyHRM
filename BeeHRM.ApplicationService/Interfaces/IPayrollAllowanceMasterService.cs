using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.ViewModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IPayrollAllowanceMasterService
    {
        IEnumerable<PayrollAllowanceMasterDTO> GetAllPayrollAllowanceMaster();
        void InsertIntoPayrollAllowance(PayrollAllowanceMasterDTO Record);
        IEnumerable<EmployeeCodeName> GetAllRelatedEmployees(int officeId);
        PayrollAllowanceMasterDTO GetPayrollDetailByMasterId(int Id, int officeId);
        PayrollAllowanceMasterDTO UpdatePayrollAllowance(PayrollAllowanceMasterDTO Record);
        void InsertIntoPayrollAllowanceDetail(string[] SelectedEmployees, int Id, string[] Value, string[] PercentageAmount);
        IEnumerable<PayrollAllowanceMasterDTO> GetAllRetirementFunds();
        PayrollAllowanceMasterDTO GetPayrollMasterByMasterId(int Id);

        PayrollAllowanceMasterDTO GetPayrollAllowanceCreateForm();
        List<SelectListItem> GetAllowanceMasterList();
    }
}
