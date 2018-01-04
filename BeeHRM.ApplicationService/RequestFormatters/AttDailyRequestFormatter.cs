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
    public static class AttDailyRequestFormatter
    {
        public static AttDaily ConvertRespondentInfoFromDTO(this AttDailyDTOs attDailyInfoDTO)
        {
            Mapper.CreateMap<AttDailyDTOs, AttDaily>();
            //Mapper.CreateMap<AttDailyDTOs, AttDaily>().ConvertUsing(
            //          m =>
            //          {
            //              return new AttDaily
            //              {

            //              };

            //          });
            return Mapper.Map<AttDailyDTOs, AttDaily>(attDailyInfoDTO);
        }
    }
}
