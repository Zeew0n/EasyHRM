using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeeHrmClientWeb.Models
{
    public class ApplyAttendance
    {
        public int EmpCode { get; set; }

        public string RequestType { get; set; }
        public string Recommnder { get; set; }
        public string Approver { get; set; }
        public bool Dualday { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime checkout { get; set; }
        public string Description { get; set; }
    }
}