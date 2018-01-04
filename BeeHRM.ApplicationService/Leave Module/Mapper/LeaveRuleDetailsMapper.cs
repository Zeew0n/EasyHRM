using BeeHRM.ApplicationService.Leave_Module.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Leave_Module.Mapper
{
   public class LeaveRuleDetailsMapper
    { 
        public static LeaverulesDetailsDtos LeaveRuleDetailToLeaveRulesDetailDTo(LeaveRuleDetail Record)
        {
            LeaverulesDetailsDtos Result = new LeaverulesDetailsDtos()
            {
                DetailId=Record.DetailId,
                LeaveDays= Record.LeaveDays,
                LeaveRuleId= Record.LeaveRuleId,
                LeaveTypeId=Record.LeaveTypeId,
                LeaveRule=new LeaveRule
                {
                    LeaveRuleId=Record.LeaveRule.LeaveRuleId
                },
                LeaveType=new LeaveType
                {
                    LeaveTypeId=Record.LeaveType.LeaveTypeId
                }
            };
            return Result;

        }
        public static LeaveRuleDetail LeaveRulesdetailDtoToLeaveRuleDetails(LeaverulesDetailsDtos Record)
        {
            LeaveRuleDetail Result = new LeaveRuleDetail()
            {
                DetailId = Record.DetailId,
                LeaveDays = Record.LeaveDays,
                LeaveRuleId = Record.LeaveRuleId,
                LeaveTypeId = Record.LeaveTypeId,
                
            };
            return Result;
        }
        public static IEnumerable<LeaverulesDetailsDtos> LeaveRuleDetailListToLeaveRuleDetailsDto(List<LeaveRuleDetail> Record)
        {
            List<LeaverulesDetailsDtos> Result = new List<LeaverulesDetailsDtos>();
            foreach(var Item in Record)
            {
                LeaverulesDetailsDtos single = new LeaverulesDetailsDtos()
                {
                    DetailId = Item.DetailId,
                    LeaveDays = Item.LeaveDays,
                    LeaveRuleId = Item.LeaveRuleId,
                    LeaveTypeId = Item.LeaveTypeId,
                    LeaveRule = new LeaveRule
                    {
                        LeaveRuleId = Item.LeaveRule.LeaveRuleId
                    },
                    LeaveType = new LeaveType
                    {
                        LeaveTypeId = Item.LeaveType.LeaveTypeId
                    }
                };
                Result.Add(single);
            }
            return Result;
        }
    }
}
