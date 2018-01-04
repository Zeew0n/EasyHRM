using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IInterestGainService
    {
        IEnumerable<PayrollIntrestGainDTO> GetInterestGainList(int? EmpId, int? OfficeId);
        PayrollIntrestGainDTO GetInterestGainByEmpCode(int EmpCode);

        //PayrollRemoteAllowancesDTO InsertPayrollRemoteAllowance(PayrollRemoteAllowancesDTO data);
        //PayrollRemoteAllowancesDTO GetPayrollRemoteAllowanceByID(int rankId);
        //int UpdatePayrollRemoteAllowance(PayrollRemoteAllowancesDTO data);
        //IEnumerable<SelectListItem> GetRankList();
        //IEnumerable<SelectListItem> GetRemoteList();
    }
}
