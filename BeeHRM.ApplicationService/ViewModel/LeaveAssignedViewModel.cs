using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ViewModel
{
    public class LeaveAssignedViewModel
    {
        public int LeaveTypeId { get; set; }
        public int AssignedId { get; set; }
        public string LeaveTypename { get; set; }
        public decimal TotalAssignDay { get; set; }
        public decimal TotalRemainingDay { get; set; }
    }
}
