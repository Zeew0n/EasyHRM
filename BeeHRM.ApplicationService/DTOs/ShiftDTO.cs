using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class ShiftDTO
    {
        public int ShiftId { get; set; }
        public string ShiftName { get; set; }
        public Nullable<bool> ShiftStatus { get; set; }
        public Nullable<System.TimeSpan> ShiftDelayAllow { get; set; }
    }
}
