using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.RequestFormatters
{
    public class LeaveRuleRequestFormatter
    {
        public static LeaveRule ConvertRespondentInfoFromDTO(LeaveRuleDTO leaveRuleDTO)
        {

            Mapper.CreateMap<LeaveRuleDTO, LeaveRule>().ConvertUsing(
                      m =>
                      {
                          return new LeaveRule
                          {
                            LeaveRuleId = m.LeaveRuleId,
                            LeaveRuleDetails = m.LeaveRuleDetails,
                            LeaveRuleName = m.LeaveRuleName
                          };

                      });
            return Mapper.Map<LeaveRuleDTO, LeaveRule>(leaveRuleDTO);
        }

        public static LeaveRuleDTO ConvertRespondentInfoToDTO(LeaveRule leave)
        {

            Mapper.CreateMap<LeaveRule, LeaveRuleDTO>().ConvertUsing(
                      m =>
                      {
                          return new LeaveRuleDTO
                          {
                            LeaveRuleDetails = m.LeaveRuleDetails,
                            LeaveRuleName = m.LeaveRuleName,
                            LeaveRuleId = m.LeaveRuleId
                          };
                      });
            return Mapper.Map<LeaveRule, LeaveRuleDTO>(leave);
        }
    }
}
