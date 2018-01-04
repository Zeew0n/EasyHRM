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
    public class ShiftDayRequestFormatter
    {
        public static ShiftDay ConvertRespondentInfoFromDTO(ShiftDayDTO shiftDayDTO)
        {

            Mapper.CreateMap<ShiftDayDTO, ShiftDay>().ConvertUsing(
                      m =>
                      {
                          return new ShiftDay
                          {
                              DayId = m.DayId,
                              DayName = m.DayName,
                              DayNumber = m.DayNumber,
                              DayShiftId = m.DayShiftId,
                              DayStartTime = m.DayStartTime,
                              DayEndTime = m.DayEndTime,
                              DayWeekend = m.DayWeekend,
                              DayWorkingMinute = m.DayWorkingMinute,
                              DayDual = m.DayDual

                          };

                      });
            return Mapper.Map<ShiftDayDTO, ShiftDay>(shiftDayDTO);
        }

        public static ShiftDayDTO ConvertRespondentInfoToDTO(ShiftDay shift)
        {

            Mapper.CreateMap<ShiftDay, ShiftDayDTO>().ConvertUsing(
                      m =>
                      {
                          return new ShiftDayDTO
                          {
                              DayName = m.DayName,
                              DayNumber = m.DayNumber,
                              DayShiftId = m.DayShiftId,
                              DayStartTime = m.DayStartTime,
                              DayEndTime = m.DayEndTime,
                              DayWeekend = m.DayWeekend,
                              DayWorkingMinute = m.DayWorkingMinute,
                              DayDual = m.DayDual
                          };

                      });
            return Mapper.Map<ShiftDay, ShiftDayDTO>(shift);
        }
    }
}
