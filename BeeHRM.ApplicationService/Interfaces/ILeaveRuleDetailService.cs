using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface ILeaveRuleDetailService
    {
        LeaveRuleDetailDTO InsertLeaveRuleDetail(LeaveRuleDetailDTO leaveRuleDetail);
        IEnumerable<LeaveRuleDetailDTO> GetLeaveRuleDetails(int leaveRuleId);
        int UpdateLeaveRuleDetails(LeaveRuleDetailDTO editLRD);

    }
}
