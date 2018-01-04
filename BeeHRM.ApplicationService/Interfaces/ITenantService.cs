using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
   public interface ITenantService
    {
        TenantDTO GetTenant(string domain);
        int GetApplicationId(string userName);
    }
}
