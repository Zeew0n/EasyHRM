using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IEmpEducationService
    {
        IEnumerable<EmpEducationDTO> GetAllEducationById(int id);
        EmpEducationDTO InsertEmpEducation(EmpEducationDTO data);
        EmpEducationDTO GetEducationByEduId(int eduId);
        int UpdateEducation(EmpEducationDTO data);
        void DeleteEducation(int eduId);
        IEnumerable<SelectListItem> GetCountryList();
        IEnumerable<SelectListItem> GetEducationLevelList();
    }
}
