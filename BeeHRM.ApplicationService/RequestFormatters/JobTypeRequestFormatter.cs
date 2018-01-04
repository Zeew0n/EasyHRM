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
   public class JobTypeRequestFormatter
    {
        public static JobType ConvertRespondentInfoFromDTO(JobTypeDTO jobTypeDTO)
        {

            Mapper.CreateMap<JobTypeDTO, JobType>().ConvertUsing(
                      m =>
                      {
                          return new JobType
                          {
                              JobtypeId = m.JobtypeId,
                              JobTypeName = m.JobTypeName,
                              JobPeriodMonth = m.JobPeriodMonth,
                              PayRollType=m.PayRollType,
                              JobAppointmentType = m.JobAppointmentType
                             
                          };

                      });
            return Mapper.Map<JobTypeDTO, JobType>(jobTypeDTO);
        }

        public static JobTypeDTO ConvertRespondentInfoToDTO(JobType job)
        {

            Mapper.CreateMap<JobType, JobTypeDTO>().ConvertUsing(
                      m =>
                      {
                          return new JobTypeDTO
                          {
                              JobtypeId = m.JobtypeId,
                              JobTypeName = m.JobTypeName,
                              JobPeriodMonth = m.JobPeriodMonth,
                              PayRollType = m.PayRollType,
                              JobAppointmentType = m.JobAppointmentType
                              
                          };

                      });
            return Mapper.Map<JobType, JobTypeDTO>(job);
        }
    }
}
