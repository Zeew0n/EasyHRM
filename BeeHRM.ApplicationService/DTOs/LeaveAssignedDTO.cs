using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class LeaveAssignedDTO
    {
        public int AssignedId { get; set; }
        public int AssignEmpCode { get; set; }
        public int AssignLeaveTypeId { get; set; }
        public Nullable<decimal> AssignedDays { get; set; }
        public Nullable<decimal> LeaveTakenDays { get; set; }
        public Nullable<int> AssignedLeaveYearId { get; set; }
        public string AssignedRemarks { get; set; }
    }
}
