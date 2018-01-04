using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface ILeaveRuleService
    {
        IEnumerable<LeaveRuleDTO> GetLeaveRulesList();
        LeaveRuleDTO InsertLeaveRule(LeaveRuleDTO leaveRule);
        LeaveRuleDTO GetLeaveRuleById(int id);
        int UpdateLeaveRule(LeaveRuleDTO editLeaveRule);
        int DeleteLeaveRule(int id);
        IEnumerable<LeaveAssignedViewModel> LeaveAssignDetail(int empcode);
    }
}
