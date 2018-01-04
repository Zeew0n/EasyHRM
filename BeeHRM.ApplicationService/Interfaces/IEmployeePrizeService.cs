using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IEmployeePrizeService
    {
        IEnumerable<EmployeePrizeDTO> GetAllPrizeOfEmployee(int id);
        EmployeePrizeDTO InsertPrizeOfEmployee(EmployeePrizeDTO data);
        EmployeePrizeDTO GetPrizeById(int id);
        int UpdateEmployeePrize(EmployeePrizeDTO data);
        void DeleteEmployeePrize(int prizeId);
    }
}
