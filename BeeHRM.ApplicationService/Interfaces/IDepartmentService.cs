using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentDTO> GetDepartmentlist();
        DepartmentDTO InsertDepartment(DepartmentDTO data);
        DepartmentDTO GetDepartmentById(int deptId);
        int UpdateDepartment(DepartmentDTO data);
    }
}
