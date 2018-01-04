using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IOfficeTypeService
    {
        IEnumerable<OfficeTypeDTO> GetOfficeTypes();
        OfficeTypeDTO InsertOfficeType(OfficeTypeDTO data);
        OfficeTypeDTO GetOfficeTypeById(int id);
        int UpdateOfficeType(OfficeTypeDTO data);
        void DeleteOfficeType(int id);
    }
}
