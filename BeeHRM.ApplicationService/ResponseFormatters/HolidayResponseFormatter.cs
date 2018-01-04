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
    public class HolidayResponseFormatter
    {
        public static IEnumerable<HolidayDTOs> ModelData(IEnumerable<Holiday> modelData)
        {
            Mapper.CreateMap<Holiday, HolidayDTOs>().ConvertUsing(
                m =>
                {
                    return new HolidayDTOs
                    {
                         HolidayId=m.HolidayId,
                         HolidayOfficeId=m.HolidayOfficeId,
                         HolidayDate=m.HolidayDate,
                         HolidayDescription=m.HolidayDescription,
                         HolidayStatus=m.HolidayStatus,
                         HolidayTitle=m.HolidayTitle,
                         HolidayGender=m.HolidayGender,
                         HolidayReligionId=m.HolidayReligionId,
                         HolidayEthnicityId=m.HolidayEthnicityId,
                         HolidayYearlyOccourence=m.HolidayYearlyOccourence

                    };
                });
            return Mapper.Map<IEnumerable<Holiday>, IEnumerable<HolidayDTOs>>(modelData);
        }
    }
}

