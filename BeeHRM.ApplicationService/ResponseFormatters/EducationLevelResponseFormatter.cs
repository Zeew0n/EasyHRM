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
    public class EducationLevelResponseFormatter
    {
        public static IEnumerable<EducationLevelDTO> ModelData(IEnumerable<EducationLevel> modelData)
        {

            Mapper.CreateMap<EducationLevel, EducationLevelDTO>().ConvertUsing(

                m =>
                {
                    return new EducationLevelDTO
                    {
                       LevelId = m.LevelId,
                       LevelName = m.LevelName,
                       LevelOrder = m.LevelOrder
                    };

                }
                );
            return Mapper.Map<IEnumerable<EducationLevel>, IEnumerable<EducationLevelDTO>>(modelData);
        }
    }
}
