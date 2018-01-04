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
   public class SkillReponseFormatter
    {
        public static IEnumerable<SkillDTO> ModelData(IEnumerable<Skill> modelData)
        {

            Mapper.CreateMap<Skill, SkillDTO>().ConvertUsing(

                m =>
                {
                    return new SkillDTO
                    {
                        SkillId = m.SkillId,
                        SkillName = m.SkillName
                      };

                }
                );
            return Mapper.Map<IEnumerable<Skill>, IEnumerable<SkillDTO>>(modelData);
        }
    }
}
