using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Leave_Module.DTOs
{
   public class LeaveMonthlyProcessDTOs
    {
        public int ProcessId { get; set; }
        public Nullable<int> LeaveYearId { get; set; }
        public Nullable<int> MonthNumber { get; set; }
        public Nullable<int> ProcessByEmpCode { get; set; }
        public Nullable<System.DateTime> ProcessDate { get; set; }
        public bool ProcessStatus { get; set; }

        public virtual LeaveYear LeaveYear { get; set; }
    }
}
