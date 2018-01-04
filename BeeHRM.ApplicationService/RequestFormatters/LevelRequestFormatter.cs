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
    public class LevelRequestFormatter
    {
        public static Level ConvertRespondentInfoFromDTO(LevelDTO levelDTO)
        {

            Mapper.CreateMap<LevelDTO, Level>().ConvertUsing(
                      m =>
                      {
                          return new Level
                          {
                             LevelId = m.LevelId,
                             LevelName = m.LevelName,
                             LevelOrder = m.LevelOrder

                          };

                      });
            return Mapper.Map<LevelDTO, Level>(levelDTO);
        }

        public static LevelDTO ConvertRespondentInfoToDTO(Level level)
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

                      });
            return Mapper.Map<Level, LevelDTO>(level);
        }
    }
}
