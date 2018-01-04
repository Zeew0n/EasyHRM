using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IRolesBusinessGroupAccessService
    {
        RolesBusinessGroupAccessDTO Insert(RolesBusinessGroupAccessDTO dataForBg);
        int[] GetBusinessGroupByRoleID(int roleId);
        void RemoveBg(string roleId, string bgID);
    }
}
