using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IEmployeeVisitService
    {
        IEnumerable<EmployeeVisitDTO> GetAllVisitByEmpId(int id);
        EmployeeVisitDTO InsertVisit(EmployeeVisitDTO data);
        EmployeeVisitDTO GetVisitById(int id);
        int UpdateVisit(EmployeeVisitDTO data);
        void DeleteVisit(int id);
    }
}
