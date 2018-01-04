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
    public static class SectionRequestFormatter
    {
        public static Section ConvertRespondentInfoFromDTO(SectionDTO secDTO)
        {

            Mapper.CreateMap<SectionDTO, Section>().ConvertUsing(
                      m =>
                      {
                          return new Section
                          {
                              SectionId = m.SectionId,
                              SectionName = m.SectionName

                          };

                      });
            return Mapper.Map<SectionDTO, Section>(secDTO);
        }

        public static SectionDTO ConvertRespondentInfoToDTO(Section sec)
        {

            Mapper.CreateMap<Section, SectionDTO>().ConvertUsing(
                      m =>
                      {
                          return new SectionDTO
                          {
                              SectionId = m.SectionId,
                              SectionName = m.SectionName
                          };

                      });
            return Mapper.Map<Section, SectionDTO>(sec);
        }
    }
}
