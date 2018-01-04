using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.DTOs;


namespace BeeHRM.ApplicationService.Interfaces
{
    public interface ILoginService
    {
        EmployeeDTO GetLoginData(string EmpUserName, string EmpPassword);
    }
}