using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IDesignationService
    {
        IEnumerable<DesignationDTO> GetDesignationList();
        IEnumerable<DesignationDTO> GetDesignationListWithLevelName();
        DesignationDTO InsertDesignation(DesignationDTO data);
        DesignationDTO GetDesignationById(int desgId);
        int UpdateDesignation(DesignationDTO data);
    }
}
