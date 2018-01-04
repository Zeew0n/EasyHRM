using BeeHRM.ApplicationService.Leave_Module.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Leave_Module.Serivices.Interface
{
  public  interface ILeaveAddAdminServices
    {
        LeaveBalance LeaveBalanceList(int YearId, int Empcode);

    }
}
