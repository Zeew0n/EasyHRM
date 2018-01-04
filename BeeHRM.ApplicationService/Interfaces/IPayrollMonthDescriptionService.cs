using BeeHRM.ApplicationService.DTOs;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IPayrollMonthDescriptionService
    {
        IEnumerable<PayrollMonthDescriptionDTO> GetAllPayrollMonths(int fyId);
        void InsertIntoMonthDescription(PayrollMonthDescriptionDTO Record);
        IEnumerable<SelectListItem> GetFiscalDropDown();
        PayrollMonthDescriptionDTO GetPayrollMonthById(int Id);
        int PayrollMonthsCheck(int monthIndex, int fyid);
        void UpdatePayrollMonth(PayrollMonthDescriptionDTO Data);
    }
}
