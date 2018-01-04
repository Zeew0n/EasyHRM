using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IEducationLevelService
    {
        IEnumerable<EducationLevelDTO> GetEducationLevel();
        EducationLevelDTO InsertEducationLevel(EducationLevelDTO data);
        EducationLevelDTO GetEducationLevelById(int id);
        int UpdateEducationLevel(EducationLevelDTO data);
        void DeleteEducationLevel(int id);
    }
}
