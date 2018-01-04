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
    public class BusinessGroupRequestFormatter
    {
        public static BusinessGroup ConvertRespondentInfoFromDTO(BusinessGroupDTO bgDTO)
        {

            Mapper.CreateMap<BusinessGroupDTO, BusinessGroup>().ConvertUsing(
                      m =>
                      {
                          return new BusinessGroup
                          {
                             BgId = m.BgId,
                             BgName = m.BgName

                          };

                      });
            return Mapper.Map<BusinessGroupDTO, BusinessGroup>(bgDTO);
        }

        public static BusinessGroupDTO ConvertRespondentInfoToDTO(BusinessGroup bg)
        {

            Mapper.CreateMap<BusinessGroup, BusinessGroupDTO>().ConvertUsing(
                      m =>
                      {
                          return new BusinessGroupDTO
                          {
                              BgId = m.BgId,
                              BgName = m.BgName
                          };

                      });
            return Mapper.Map<BusinessGroup, BusinessGroupDTO>(bg);
        }
    }
}
