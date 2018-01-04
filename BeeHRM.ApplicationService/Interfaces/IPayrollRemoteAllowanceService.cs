using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IPayrollRemoteAllowanceService
    {
        IEnumerable<PayrollRemoteAllowancesDTO> GetPayrollRemoteAllowanceList();
        PayrollRemoteAllowancesDTO InsertPayrollRemoteAllowance(PayrollRemoteAllowancesDTO data);
        PayrollRemoteAllowancesDTO GetPayrollRemoteAllowanceByID(int rankId);
        int UpdatePayrollRemoteAllowance(PayrollRemoteAllowancesDTO data);
        IEnumerable<SelectListItem> GetRankList();
        IEnumerable<SelectListItem> GetRemoteList();
    }
}
