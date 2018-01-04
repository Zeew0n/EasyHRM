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
    public class SectionResponseFormatter
    {
        public static IEnumerable<SectionDTO> ModelData(IEnumerable<Section> modelData)
        {

            Mapper.CreateMap<Section, SectionDTO>().ConvertUsing(

                m =>
                {
                    return new SectionDTO
                    {
                        SectionId = m.SectionId,
                        SectionName = m.SectionName
                    };

                }
                );
            return Mapper.Map<IEnumerable<Section>, IEnumerable<SectionDTO>>(modelData);
        }
    }
}
