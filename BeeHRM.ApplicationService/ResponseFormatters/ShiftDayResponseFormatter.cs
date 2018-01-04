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
    public class ShiftDayResponseFormatter
    {
        public static IEnumerable<ShiftDayDTO> ModelData(IEnumerable<ShiftDay> modelData)
        {

            Mapper.CreateMap<ShiftDay, ShiftDayDTO>().ConvertUsing(

                m =>
                {
                    return new ShiftDayDTO
                    {
                        DayId = m.DayId,
                        DayShiftId = m.DayShiftId,
                        DayName = m.DayName,
                        DayNumber = m.DayNumber,
                        DayDual = m.DayDual,
                        DayWeekend = m.DayWeekend,
                        DayStartTime = m.DayStartTime,
                        DayEndTime = m.DayEndTime,
                        DayWorkingMinute = m.DayWorkingMinute
                    };

                }
                );
            return Mapper.Map<IEnumerable<ShiftDay>, IEnumerable<ShiftDayDTO>>(modelData);
        }
    }
}
