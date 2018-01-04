using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ViewModel
{
    public class LeaveRuleAddViewModel
    {
        [Required(ErrorMessage ="The rule name is required.")]
        public string LeaveRuleName { get; set; }
        public string LeaveRuleDescription { get; set; }
        public IEnumerable<LeaveTypeDTO> LeaveTypes { get; set; }
        public List<LeaveTypeDTO> Params { get; set; }
        public List<LeaveAssignedViewModel> LeaveAssign { get; set; }
        public EmployeeDetailsViewModel Empdetail { get; set; }
        public int LeaveRuleId { get; set; }
        public string LeaveTypeName { get; set; }
        public List<LeaveAssignDetailViewModel> UpdateResult { get; set; }


    }
}
