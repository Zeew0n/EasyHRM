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
    public class LeaveRuleDetailRequestFormatter
    {
        public static LeaveRuleDetail ConvertRespondentInfoFromDTO(LeaveRuleDetailDTO leaveRuleDetailDTO)
        {

            Mapper.CreateMap<LeaveRuleDetailDTO, LeaveRuleDetail>().ConvertUsing(
                      m =>
                      {
                          return new LeaveRuleDetail
                          {
                              LeaveDays = m.LeaveDays,
                              LeaveRuleId = m.LeaveRuleId,
                              LeaveTypeId = m.LeaveTypeId
                          };

                      });
            return Mapper.Map<LeaveRuleDetailDTO, LeaveRuleDetail>(leaveRuleDetailDTO);
        }

        public static LeaveRuleDetailDTO ConvertRespondentInfoToDTO(LeaveRuleDetail emp)
        {

            Mapper.CreateMap<LeaveRuleDetail, LeaveRuleDetailDTO>().ConvertUsing(
                      m =>
                      {
                          return new LeaveRuleDetailDTO
                          {
                              LeaveDays = m.LeaveDays,
                              LeaveRuleId = m.LeaveRuleId,
                              LeaveTypeId = m.LeaveTypeId
                          };
                      });
            return Mapper.Map<LeaveRuleDetail, LeaveRuleDetailDTO>(emp);
        }
    }
}
