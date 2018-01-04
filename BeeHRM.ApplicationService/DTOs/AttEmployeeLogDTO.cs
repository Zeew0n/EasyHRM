using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public  class AttEmployeeLogDTO
    {
        public long LogId { get; set; }
        public Nullable<int> logEmpCode { get; set; }
        public Nullable<System.DateTime> logDate { get; set; }
        public Nullable<System.TimeSpan> logTime { get; set; }
        public string logIpAddress { get; set; }
        public Nullable<int> logTypeID { get; set; }
        public Nullable<int> logMethodId { get; set; }
    }
}
