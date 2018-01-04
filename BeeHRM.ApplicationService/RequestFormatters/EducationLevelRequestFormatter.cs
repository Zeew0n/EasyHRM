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
    public class EducationLevelRequestFormatter
    {
        public static EducationLevel ConvertRespondentInfoFromDTO(EducationLevelDTO educationLevelDTO)
        {

            Mapper.CreateMap<EducationLevelDTO, EducationLevel>().ConvertUsing(
                      m =>
                      {
                          return new EducationLevel
                          {
                              LevelId = m.LevelId,
                              LevelName = m.LevelName,
                              LevelOrder = m.LevelOrder
                          };

                      });
            return Mapper.Map<EducationLevelDTO, EducationLevel>(educationLevelDTO);
        }

        public static EducationLevelDTO ConvertRespondentInfoToDTO(EducationLevel educationLevel)
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

                      });
            return Mapper.Map<EducationLevel, EducationLevelDTO>(educationLevel);
        }
    }
}
