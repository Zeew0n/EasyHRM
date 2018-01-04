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
   public class EthnicityRequestFormatter
    {
        public static Ethnicity ConvertRespondentInfoFromDTO(EthnicityDTO ethnicityDTO)
        {

            Mapper.CreateMap<EthnicityDTO, Ethnicity>().ConvertUsing(
                      m =>
                      {
                          return new Ethnicity
                          {
                              EthnicityName = m.EthnicityName,
                              EthnicityId = m.EthnicityId

                          };

                      });
            return Mapper.Map<EthnicityDTO, Ethnicity>(ethnicityDTO);
        }

        public static EthnicityDTO ConvertRespondentInfoToDTO(Ethnicity ethnicity)
        {

            Mapper.CreateMap<Ethnicity, EthnicityDTO>().ConvertUsing(
                      m =>
                      {
                          return new EthnicityDTO
                          {
                              EthnicityName = m.EthnicityName,
                              EthnicityId = m.EthnicityId
                          };

                      });
            return Mapper.Map<Ethnicity, EthnicityDTO>(ethnicity);
        }
    }
}
