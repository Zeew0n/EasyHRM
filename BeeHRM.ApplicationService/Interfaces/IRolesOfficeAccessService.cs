using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IRolesOfficeAccessService
    {
        RolesOfficeAccessDTO Insert(RolesOfficeAccessDTO dataForOffice);
        int[] GetOfficeByRoleId(int roleId);
        void RemoveOffice(string roleId, string officeId);
    }
}
