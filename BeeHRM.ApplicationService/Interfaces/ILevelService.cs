using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface ILevelService
    {
        IEnumerable<LevelDTO> GetLevellist();
        LevelDTO InsertLevel(LevelDTO data);
        LevelDTO GetLevelById(int levelId);
        int UpdateLevelbyId(LevelDTO data);
        
    }
}
