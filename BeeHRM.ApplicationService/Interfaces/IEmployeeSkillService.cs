using System;
using BeeHRM.ApplicationService.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Interfaces
{
  public interface IEmployeeSkillService
    {

        void AddEmployeeSkills(EmployeeSkillsDTO Record);
        EmployeeSkillsDTO GetOneEmployeeSkills(int Id);
        int  GetEmpCode(int Id);
        void UpdateEmployeeSkill(EmployeeSkillsDTO Record);
        List<EmployeeSkillsDTO> GetEmployeeByEmpCode(int empcode);
        IEnumerable<SelectListItem> GetSkillSelectList();
        void Delete(int id);

    }
}
