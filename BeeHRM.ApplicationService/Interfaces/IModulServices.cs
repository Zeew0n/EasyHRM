using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.ViewModel;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IModulServices
    {
        IEnumerable<ModuleDTOs> GetModuleParents(int? id);

        IEnumerable<ModuleDTOs> GetDefaultMenu(int id);
        List<ModuleDTOs> GetTopLevelModules(int roleId);
        IEnumerable<ModuleDTOs> GetAllModules();
        IEnumerable<ModuleDTOs> GetModuleIdByModuleName(string moduleName);
        //List<ModuleDTOsForparent> GetDefaultParentMenu();

        int GetParentId(string controllerName);
        IEnumerable<ModuleDTOs> GetDefaultMenu();
        Module GetModuleByController(int parentid);
        IEnumerable<ModuleDTOs> GetChildMenuList(int parentid, int roleid);
        IEnumerable<EmployeeDetailsMenuViewModel> AdminEmployeeDetailsMenu(int EmpCode);
    }
}
