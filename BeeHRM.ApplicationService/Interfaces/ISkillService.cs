using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface ISkillService
    {

        IEnumerable<SkillDTO> GetAllSkill();
        SkillDTO InsertSkill(SkillDTO data);
        SkillDTO GetSkillById(int id);
        int UpdateSkill(SkillDTO data);
        void DeleteSkill(int id);

    }
}
