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
    public class AttendanceDailyResponseFormatter
    {
        public static IEnumerable<AttendanceDailyDTO> ModelData(IEnumerable<AttendaceDaily> modelData)
        {

            Mapper.CreateMap<AttendaceDaily, AttendanceDailyDTO>().ConvertUsing(

                m =>
                {
                    return new AttendanceDailyDTO
                    {
                        AttCheckIn = m.AttCheckIn,
                        AttCheckOut = m.AttCheckOut,
                        AttDate = m.AttDate,
                        AttEmpCode = m.AttEmpCode,
                        AttId = m.AttId,
                        AttJobHistoryId = m.AttJobHistoryId,
                        AttOfficeId = m.AttOfficeId,
                        AttRemarks = m.AttRemarks,
                        AttShiftEnd = m.AttShiftEnd,
                        AttShiftId = m.AttShiftId,
                        AttShiftStart = m.AttShiftStart,
                        IsAbsent = m.IsAbsent,
                        IsHoliday = m.IsHoliday,
                        isLeave = m.IsLeave,
                        isOfficialVisit = m.IsLeave,
                        IsWeekend = m.IsWeekend,
                        PayableDay = m.PayableDay,
                        AttDateNp = m.AttDateNp
                    };
                }
                );
            return Mapper.Map<IEnumerable<AttendaceDaily>, IEnumerable<AttendanceDailyDTO>>(modelData);
        }
    }
}
