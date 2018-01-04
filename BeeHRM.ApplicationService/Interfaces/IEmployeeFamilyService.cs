using BeeHRM.ApplicationService.DTOs;
using System.Collections.Generic;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IEmployeeFamilyService
    {

        void AddEmplyeeFamily(EmployeeFamilyDTO Record);
        EmployeeFamilyDTO GetOneEmployeeFamily(int Id);
        void UpdateEmployeeFamily(EmployeeFamilyDTO Record);

        List<EmployeeFamilyDTO> GetEmployeeFamilyInformationByEmpCode(int? empcode);
        List<EmployeeFamilyDTO> GetEmployeeByEmpCode(int empcode);

    }
}
