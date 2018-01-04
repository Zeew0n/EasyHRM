using BeeHRM.ApplicationService.Leave_Module.DTOs;
using System.Collections.Generic;

namespace BeeHrmClientWeb.Models
{
    public class LeaveBalanceModel
    {
        public LeaveBalance LeaveBalance { get; set; }
        public IEnumerable<LeaveBalance> LeaveBalanceDetails { get; set; }

    }
}