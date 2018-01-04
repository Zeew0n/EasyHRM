using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface ISectionService
    {
        IEnumerable<SectionDTO> GetSectionList();
        SectionDTO InsertSection(SectionDTO data);
        SectionDTO GetSectionById(int sectionId);
        int UpdateSectionById(SectionDTO data);
        void DeleteSection(int sectionId);
    }
}
