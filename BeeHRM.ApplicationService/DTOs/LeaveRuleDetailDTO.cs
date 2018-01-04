using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class LeaveRuleDetailDTO
    {
        public int DetailId { get; set; }
        public int LeaveRuleId { get; set; }
        public int LeaveTypeId { get; set; }
        public Nullable<decimal> LeaveDays { get; set; }
    }
}
