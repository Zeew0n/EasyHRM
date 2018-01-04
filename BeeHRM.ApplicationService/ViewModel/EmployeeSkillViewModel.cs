using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ViewModel
{
    public class EmployeeSkillViewModel
    {
        public int Id { get; set; }
        public int EmpCode { get; set; }
        public int EmpSkillId { get; set; }
        public int EmpSkillPoint { get; set; }
        public bool EmpSkillStatus { get; set; }
        public string SkillName { get; set; }
        public virtual EmployeeDTO Employee { get; set; }
        public virtual EmployeeSkillViewModel Skill { get; set; }
    }
}
