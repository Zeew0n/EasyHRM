using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class LeaveMonthlyProcessDTO
    {
        public int ProcessId { get; set; }
        public int LeaveYearId { get; set; }
        public int MonthNumber { get; set; }
        public Nullable<int> ProcessByEmpCode { get; set; }
        public Nullable<System.DateTime> ProcessDate { get; set; }
        public Nullable<bool> ProcessStatus { get; set; }
    }
}
