using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IReligionService
    {
        IEnumerable<ReligionDTO> GetReligionList();

        ReligionDTO GetReligionById(int id);
    }
}
