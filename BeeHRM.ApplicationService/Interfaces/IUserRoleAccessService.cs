using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
   public  interface IUserRoleAccessService
    {
       List<RoleAccessDTOs> GetRoleAccessData(int roleID);
        RoleAccessDTOs GetRoleAccessUserAccesData(int moduleId , int roleId);
    }
}
