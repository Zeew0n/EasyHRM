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
    public class ReligionResponseFormatter
    {
        public static IEnumerable<ReligionDTO> ModelData(IEnumerable<Religion> modelData)
        {
            Mapper.CreateMap<Religion, ReligionDTO>().ConvertUsing(

                m =>
                {
                    return new ReligionDTO
                    {
                        ReligionId = m.ReligionId,
                        ReligionName = m.ReligionName
                    };

                }
                );
            return Mapper.Map<IEnumerable<Religion>, IEnumerable<ReligionDTO>>(modelData);
        }
    }
}
