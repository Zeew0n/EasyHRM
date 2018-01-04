using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ResponseFormatters
{
    public class LeaveRuleResponseFormatter
    {
        public static IEnumerable<LeaveRuleDTO> ModelData(IEnumerable<LeaveRule> modelData)
        {

            Mapper.CreateMap<LeaveRule, LeaveRuleDTO>().ConvertUsing(

                m =>
                {
                    return new LeaveRuleDTO
                    {
                        LeaveRuleId = m.LeaveRuleId,
                        LeaveRuleName = m.LeaveRuleName,
                        LeaveRuleDetails = m.LeaveRuleDetails
                    };

                }
                );
            return Mapper.Map<IEnumerable<LeaveRule>, IEnumerable<LeaveRuleDTO>>(modelData);
        }
    }
}
