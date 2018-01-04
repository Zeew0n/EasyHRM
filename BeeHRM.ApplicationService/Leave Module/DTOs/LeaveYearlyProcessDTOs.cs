using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Leave_Module.DTOs
{
   public class LeaveYearlyProcessDTOs
    {
        public int ProcessId { get; set; }
        public int PrevLeavYearId { get; set; }
        public int LeaveYearId { get; set; }
        public int ProcessByEmpCode { get; set; }
        public Nullable<System.DateTime> ProcessDate { get; set; }
    }
}
