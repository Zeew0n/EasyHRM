using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IEmployeeBankService
    {
        IEnumerable<EmployeeBankDTO> GetEmployeeBankList();
        EmployeeBankDTO InsertEmployeeBank(EmployeeBankDTO data);
        EmployeeBankDTO GetEmployeeBankId(int id);
        int UpdateEmployeeBank(EmployeeBankDTO data);
        void DeleteEmployeeBankId(int id);
        IEnumerable<SelectListItem> GetEmployeeList();
        IEnumerable<SelectListItem> GetBankList();
    }
}
