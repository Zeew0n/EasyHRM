using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.ApplicationService;
namespace BeeHRM.ApplicationService.ResponseFormatters
{
   public class EthnicityResponseFormatter
    {
        public static IEnumerable<EthnicityDTO> ModelData(IEnumerable<Ethnicity> modelData)
        {
            Mapper.CreateMap<Ethnicity, EthnicityDTO>().ConvertUsing(

                m =>
                {
                    return new EthnicityDTO
                    {
                       EthnicityId = m.EthnicityId,
                       EthnicityName = m.EthnicityName
                    };

                }
                );
            return Mapper.Map<IEnumerable<Ethnicity>, IEnumerable<EthnicityDTO>>(modelData);
        }
    }
}
