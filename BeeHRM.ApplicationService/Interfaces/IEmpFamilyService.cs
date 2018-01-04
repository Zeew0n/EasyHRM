using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IEmpFamilyService
    {
        IEnumerable<EmployeeFamilyViewModel> GetEmpFamilyList();
        EmployeeFamilyViewModel InsertEmpFamily(EmployeeFamilyViewModel data);
        EmployeeFamilyViewModel GetEmpFamilyByID(int rankId);
        int UpdateEmpFamily(EmployeeFamilyViewModel data);
    }
}
