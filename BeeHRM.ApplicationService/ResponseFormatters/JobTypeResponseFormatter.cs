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
    public class JobTypeResponseFormatter
    {
        public static IEnumerable<JobTypeDTO> ModelData(IEnumerable<JobType> modelData)
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

                }
                );
            return Mapper.Map<IEnumerable<JobType>, IEnumerable<JobTypeDTO>>(modelData);
        }
    }
}
