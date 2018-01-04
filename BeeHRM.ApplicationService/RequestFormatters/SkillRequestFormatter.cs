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
   public class SkillRequestFormatter
    {
        public static Skill ConvertRespondentInfoFromDTO(SkillDTO skillDTO)
        {

            Mapper.CreateMap<SkillDTO, Skill>().ConvertUsing(
                      m =>
                      {
                          return new Skill
                          {
                              SkillId = m.SkillId,
                              SkillName = m.SkillName
                          };

                      });
            return Mapper.Map<SkillDTO, Skill>(skillDTO);
        }

        public static SkillDTO ConvertRespondentInfoToDTO(Skill skill)
        {

            Mapper.CreateMap<Skill, SkillDTO>().ConvertUsing(
                      m =>
                      {
                          return new SkillDTO
                          {
                              SkillId = m.SkillId,
                              SkillName = m.SkillName
                          };

                      });
            return Mapper.Map<Skill, SkillDTO>(skill);
        }
    
}
}
