using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class LeaveTypeDTO
    {
        public int LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        public string LeaveTypeDescription { get; set; }
        public Nullable<byte> NumberOfTime { get; set; }
        public Nullable<bool> IsPayable { get; set; }
        public Nullable<bool> IsCashable { get; set; }
        public Nullable<int> MaxCashable { get; set; }
        public Nullable<bool> IsTransferable { get; set; }
        public Nullable<int> MaxTransferable { get; set; }
        public Nullable<int> MonthlyQty { get; set; }
        public Nullable<int> LeaveApplyBefore { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public Nullable<decimal> ProRataLeaveRatio { get; set; }
        public string LeaveTypeAssignment { get; set; }
        public int LeaveRuleDetailId { get; set; }

        public string Days { get; set; }
        
        public string LeaveType1 { get; set; }
        public bool HalfdayAllow { get; set; }
    }
}
