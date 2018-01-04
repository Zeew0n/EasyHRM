using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Leave_Module.DTOs
{
   public class LeaverulesDetailsDtos
    {
        public int DetailId { get; set; }
        public int LeaveRuleId { get; set; }
        public int LeaveTypeId { get; set; }
        public Nullable<decimal> LeaveDays { get; set; }

        public virtual LeaveRule LeaveRule { get; set; }
        public virtual LeaveType LeaveType { get; set; }
    }
}
