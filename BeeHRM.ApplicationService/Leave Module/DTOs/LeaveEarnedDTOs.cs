using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Leave_Module.DTOs
{
    public class LeaveEarnedDTOs
    {
        public int LeaveEarnedId { get; set; }
        public Nullable<int> EmpCode { get; set; }
        public Nullable<System.DateTime> LeaveEarnRequestedDate { get; set; }
        public Nullable<int> RecommendedEmpCode { get; set; }
        public Nullable<byte> RecommendStatus { get; set; }
        public Nullable<System.DateTime> RecommendStatusDate { get; set; }
        public Nullable<int> ApproverEmpCode { get; set; }
        public byte ApproverStatus { get; set; }
        public Nullable<System.DateTime> ApproverStatusDate { get; set; }
        public Nullable<System.DateTime> WorkedStartDate { get; set; }
        public Nullable<System.DateTime> WorkedEndDate { get; set; }
        public int LeaveYearId { get; set; }
        public int LeaveTypeId { get; set; }
        public Nullable<decimal> LeaveTotalEanrnedDays { get; set; }
        public Nullable<System.DateTime> EarnLeaveExpiryDate { get; set; }
        public string Remarks { get; set; }
    }
}
