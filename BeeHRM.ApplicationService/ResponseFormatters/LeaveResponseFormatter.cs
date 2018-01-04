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
   public class LeaveResponseFormatter
    {
        public static LeaveApplication ConvertRespondentInfoFromDTO(LeaveApplicationDTO modelData)
        {
            Mapper.CreateMap<LeaveApplicationDTO, LeaveApplication>();
            return Mapper.Map<LeaveApplicationDTO, LeaveApplication>(modelData);
        }

        public static LeaveApplicationDTO ConvertRespondentInfoToDTO(LeaveApplication employeeEducation)
        {

            Mapper.CreateMap<LeaveApplication, LeaveApplicationDTO>();
            return Mapper.Map<LeaveApplication, LeaveApplicationDTO>(employeeEducation);
        }
    }
}
