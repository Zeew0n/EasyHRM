using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.DTOs
{
      public class EmployeeSkillsDTO
    {
        public int Id { get; set; }
        public int EmpCode { get; set; }
        public int EmpSkillId { get; set; }
        public Nullable<int> EmpSkillPoint { get; set; }
        public Nullable<bool> EmpSkillStatus { get; set; }
        public int EmpId { get; set; }
        public  EmployeeDTO Employee { get; set; }
        public  SkillDTO Skill { get; set; }
        public IEnumerable<SelectListItem> SkillSelectlist { get; set; }

    }

    public partial class EmployeeSkillInformation
    {
        public List<EmployeeSkillsDTO> EmployeeSkillList { get; set; }
        public int EmpId { get; set; }
        public IEnumerable<SelectListItem> SkillSelectlist { get; set; }
    }
}
