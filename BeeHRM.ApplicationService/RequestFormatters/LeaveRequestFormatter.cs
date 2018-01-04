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
    public class LeaveRequestFormatter
    {
        public static LeaveAssigned ConvertRespondentInfoFromDTO(LeaveAssignedDTO lAssignDTO)
        {

            Mapper.CreateMap<LeaveAssignedDTO, LeaveAssigned>();
            return Mapper.Map<LeaveAssignedDTO, LeaveAssigned>(lAssignDTO);
        }

        public static LeaveAssignedDTO ConvertRespondentInfoToDTO(LeaveAssigned leaveAssign)
        {

            Mapper.CreateMap<LeaveAssigned, LeaveAssignedDTO>();
            return Mapper.Map<LeaveAssigned, LeaveAssignedDTO>(leaveAssign);
        }
    }
}
