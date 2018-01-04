using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Interfaces
{
   public interface IPayrollLeaveDeductionService
    {
        List<PayrollLeaveDeductionDTO> GetAllPayrollLeaveDeductionList();
        void AddPayrollLeaveDeduction(PayrollLeaveDeductionDTO Record);
        void UpdatePayrollLeaveDeduction(PayrollLeaveDeductionDTO Record);
        void Delete(int id);
        IEnumerable<SelectListItem> GetPayrollLeaveDeductionLeaveTypeSelectList();
        PayrollLeaveDeductionDTO GetOnePayrollLeaveDeduction(int id);

        List<PayrollLeaveDeductionDTO>GetAllPayrollLeaveDeductionListByYearIdAndEmpCode(PayrollLeaveDeductionInformation Record);
        decimal GetPayrollLeaveBalance(int leaveTypeId, int leaveYearId, int empCode);

        int GetCurrentYear();
    }
}
