using BeeHRM.ApplicationService.Leave_Module.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Leave_Module.Mapper
{
    public class LeaveRulesMapper
    {
        public static LeaveRulesDTOs LeaveRulesToLeaveRulesDto(LeaveRule Record)
        {
            LeaveRulesDTOs Result = new LeaveRulesDTOs()
            {
                LeaveRuleId = Record.LeaveRuleId,
                LeaveRuleName = Record.LeaveRuleName,
                LeaveRuleDetails = Record.LeaveRuleDetails     
                
            };
            return Result;
        }
        public static LeaveRule LeaveRuleDtoToLeaveRules(LeaveRulesDTOs Record)
        {
            LeaveRule Result = new LeaveRule()
            {
                LeaveRuleId = Record.LeaveRuleId,
                LeaveRuleName = Record.LeaveRuleName,
                LeaveRuleDetails = Record.LeaveRuleDetails

            };
            return Result;
        }
        public static List<LeaveRulesDTOs> LeaveRuleListToLeaveRulesDTOList(List<LeaveRule> Record)
        {
            List<LeaveRulesDTOs> Result = new List<LeaveRulesDTOs>();
            foreach (var item in Record)
            {
                LeaveRulesDTOs Single = new LeaveRulesDTOs()
                {
                    LeaveRuleId = item.LeaveRuleId,
                    LeaveRuleName = item.LeaveRuleName,
                    LeaveRuleDetails = item.LeaveRuleDetails,
                   LeaveRuleDetailsColection=item.LeaveRuleDetails1.ToList()
                };
                Result.Add(Single);

            }
            return Result;
        }
    }
}
