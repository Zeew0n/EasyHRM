using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IRolesAccessService
    {
        IEnumerable<RolesAccessDTO> GetAllRolesAccess();
        RolesAccessDTO InserRoleAccess(RolesAccessDTO data);

        //dynamic GetNotInModules(int roleId);
        RolesAccessDTO GetRoleByRoleIdModuleId(string roleid, string moduleid,string assignid);
        int UpdateRolesAccess(RolesAccessDTO data);
        void DeleteRoleAccess(short v);
        IEnumerable<RolesAccessDTO> GetRoleByRoleID(int roleId);
        //dynamic GetParentModulesList();
        //dynamic GetParentModules(int parentModuleId,int roleId);
        List<TreeModules> GetMenuTree(int Id);

        void InsertIntoRolesAccess(List<TreeModules> Insert, int RoleId);
    }
}
