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
   public class LeaveAssignRequestFormatters
    {

        public static LeaveAssigned ConvertRespondentInfoFromDTO(LeaveAssignedDTO leaveassign)
        {

            Mapper.CreateMap<LeaveAssignedDTO, LeaveAssigned>().ConvertUsing(
                      m =>
                      {
                          return new LeaveAssigned
                          {
                             AssignedId=m.AssignedId,
                             AssignedDays=m.AssignedDays,
                             AssignedLeaveYearId=m.AssignedLeaveYearId,
                             AssignEmpCode=m.AssignEmpCode,
                            AssignLeaveTypeId=m.AssignLeaveTypeId

                          };

                      });
            return Mapper.Map<LeaveAssignedDTO, LeaveAssigned>(leaveassign);

        }

        public static LeaveRuleDetailDTO ConvertRespondentInfoToDTO(LeaveAssigned leaveassign)
        {

            Mapper.CreateMap<LeaveAssigned, LeaveRuleDetailDTO>();
            return Mapper.Map<LeaveAssigned, LeaveRuleDetailDTO>(leaveassign);
        }
    }
}
