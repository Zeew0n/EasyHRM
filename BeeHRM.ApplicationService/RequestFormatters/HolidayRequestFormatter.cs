using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository;
using AutoMapper;


namespace BeeHRM.ApplicationService.RequestFormatters
{
    public static class HolidayRequestFormatter
    {
        public static Holiday ConvertRespondentInfoFromDTO(this HolidayDTOs hld)
        {
            try
            {
                Mapper.CreateMap<HolidayDTOs, Holiday>().ConvertUsing(
             m =>
             {
                 return new Holiday
                 {
                     HolidayDate = m.HolidayDate,
                     HolidayDescription = m.HolidayDescription,
                     HolidayEthnicityId = m.HolidayEthnicityId,
                     HolidayGender = m.HolidayGender,
                     HolidayId = m.HolidayId,
                     HolidayOfficeId = m.HolidayOfficeId,
                     HolidayReligionId = m.HolidayReligionId,
                     HolidayStatus = m.HolidayStatus,
                     HolidayTitle = m.HolidayTitle,
                     HolidayYearlyOccourence = m.HolidayYearlyOccourence
                 };
             });
                return Mapper.Map<HolidayDTOs, Holiday>(hld);
            }
            catch (Exception ex)
            {
                return null;
            }
          
        }

        public static HolidayDTOs ConvertRespondentInfoToDTO(this Holiday holid)
        {

            try {
                Mapper.CreateMap<Holiday, HolidayDTOs>().ConvertUsing(
             m =>
             {

                 return new HolidayDTOs
                 {

                     HolidayDate = m.HolidayDate,
                     HolidayDescription = m.HolidayDescription,
                     HolidayEthnicityId = m.HolidayEthnicityId,
                     HolidayGender = m.HolidayGender,
                     HolidayId = m.HolidayId,
                     HolidayOfficeId = m.HolidayOfficeId,
                     HolidayReligionId = m.HolidayReligionId,
                     HolidayStatus = m.HolidayStatus,
                     HolidayTitle = m.HolidayTitle,
                     HolidayYearlyOccourence = m.HolidayYearlyOccourence
                 };
             });
                return Mapper.Map<Holiday, HolidayDTOs>(holid);
            }
            catch(Exception Ex)
            {
                return null;
            }
          
        }
    }
}
