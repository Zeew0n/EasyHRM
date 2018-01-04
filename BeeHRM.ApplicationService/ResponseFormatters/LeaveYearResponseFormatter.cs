using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System.Collections.Generic;

namespace BeeHRM.ApplicationService.ResponseFormatters
{
    public class LeaveYearResponseFormatter
    {
        public static IEnumerable<LeaveYearDTO> ModelData(IEnumerable<LeaveYear> modelData)
        {

            Mapper.CreateMap<LeaveYear, LeaveYearDTO>().ConvertUsing(

                m =>
                {
                    return new LeaveYearDTO
                    {
                        YearId = m.YearId,
                        YearCurrent = m.YearCurrent,
                        YearEndDate = m.YearEndDate,
                        YearEndDateNp = m.YearEndDateNp,
                        YearName = m.YearName,
                        YearStartDate = m.YearStartDate,
                        YearStartDateNp = m.YearStartDateNp
                    };

                }
                );
            return Mapper.Map<IEnumerable<LeaveYear>, IEnumerable<LeaveYearDTO>>(modelData);
        }
    }

}
