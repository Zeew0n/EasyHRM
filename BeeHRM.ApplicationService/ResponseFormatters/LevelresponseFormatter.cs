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
    public class LevelResponseFormatter
    {
        public static IEnumerable<LevelDTO> ModelData(IEnumerable<Level> modelData)
        {

            Mapper.CreateMap<Level, LevelDTO>().ConvertUsing(

                m =>
                {
                    return new LevelDTO
                    {
                        LevelId = m.LevelId,
                        LevelName = m.LevelName,
                        LevelOrder = m.LevelOrder
                    };

                }
                );
            return Mapper.Map<IEnumerable<Level>, IEnumerable<LevelDTO>>(modelData);
        }
    }
}
