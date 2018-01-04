using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Leave_Module.DTOs
{
   public class LeaveRulesDTOs
    {
        public int LeaveRuleId { get; set; }
        [Required]
        [Display (Name =" Rule Name")]
        public string LeaveRuleName { get; set; }
        [Required]
        [Display(Name = "Rule Details")]
        [DataType (DataType.MultilineText)]
        public string LeaveRuleDetails { get; set; }

        public virtual List<Employee> Employees { get; set; }
        public virtual List<LeaveRuleDetail> LeaveRuleDetailsColection { get; set; }
        public IEnumerable<SelectListItem> LeaveTypeList { get; set; }
    }
}
