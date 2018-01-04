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
    public class LeaveRuleDetailResponseFormatter
    {
        public static IEnumerable<LeaveRuleDetailDTO> ModelData(IEnumerable<LeaveRuleDetail> modelData)
        {
            Mapper.CreateMap<LeaveRuleDetail, LeaveRuleDetailDTO>().ConvertUsing(

                m =>
                {
                    return new LeaveRuleDetailDTO
                    {
                        DetailId = m.DetailId,
                        LeaveRuleId = m.LeaveRuleId,
                        LeaveDays = m.LeaveDays,
                        LeaveTypeId = m.LeaveTypeId
                    };

                }
                );
            return Mapper.Map<IEnumerable<LeaveRuleDetail>, IEnumerable<LeaveRuleDetailDTO>>(modelData);
        }
    }
}
