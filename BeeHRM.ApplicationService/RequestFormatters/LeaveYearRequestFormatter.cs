using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;

namespace BeeHRM.ApplicationService.RequestFormatters
{
    public class LeaveYearRequestFormatter
    {
        public static LeaveYear ConvertRespondentInfoFromDTO(LeaveYearDTO leaveYearDTO)
        {

            Mapper.CreateMap<LeaveYearDTO, LeaveYear>().ConvertUsing(
                      m =>
                      {
                          return new LeaveYear
                          {
                              YearId = m.YearId,
                              YearCurrent = m.YearCurrent,
                              YearEndDate = Convert.ToDateTime(m.YearEndDate),
                              YearName = m.YearName,
                              YearStartDate = Convert.ToDateTime(m.YearStartDate),
                              YearStartDateNp = m.YearStartDateNp,
                              YearEndDateNp = m.YearEndDateNp
                          };

                      });
            return Mapper.Map<LeaveYearDTO, LeaveYear>(leaveYearDTO);
        }

        public static LeaveYearDTO ConvertRespondentInfoToDTO(LeaveYear leave)
        {

            Mapper.CreateMap<LeaveYear, LeaveYearDTO>().ConvertUsing(
                      m =>
                      {
                          return new LeaveYearDTO
                          {
                              YearId = m.YearId,
                              YearCurrent = m.YearCurrent,
                              YearEndDate = m.YearEndDate,
                              YearName = m.YearName,
                              YearStartDate = m.YearStartDate,
                              YearStartDateNp = m.YearStartDateNp,
                              YearEndDateNp = m.YearEndDateNp
                          };
                      });
            return Mapper.Map<LeaveYear, LeaveYearDTO>(leave);
        }
    }
}
