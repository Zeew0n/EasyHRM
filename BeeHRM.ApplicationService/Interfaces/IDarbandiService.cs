using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IDarbandiService
    {
        IEnumerable<DarbandiDTO> GetDarbandilist();
        DarbandiDTO InsertDarabandi(DarbandiDTO data);
        DarbandiDTO GetDarbandiById(int darbandiId);
        int UpdateDarabandi(DarbandiDTO data);
    }

    //comment this is a comment
    //this is a change
}
