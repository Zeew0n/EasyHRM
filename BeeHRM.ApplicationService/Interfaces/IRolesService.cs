using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IRolesService
    {
        IEnumerable<RolesDTO> GetRoles();
        RolesDTO InsertRoles(RolesDTO data);
        RolesDTO GetRoleById(int roleId);
        int UpdateRole(RolesDTO data);
    }
}
