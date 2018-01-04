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
   public class CountryResponseFormatter
    {
        public static IEnumerable<CountryDTO> ModelData(IEnumerable<Country> modelData)
        {

            Mapper.CreateMap<Country, CountryDTO>();
            return Mapper.Map<IEnumerable<Country>, IEnumerable<CountryDTO>>(modelData);
        }
    }
}
