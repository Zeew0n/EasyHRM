using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ViewModel
{
    public sealed class LeaveStatViewModel
    {
        public int EmpCode { get; set; }
        public int EmpLeaveRuleId { get; set; }
        public int LeaveTypeId { get; set; }
        public decimal LeaveDays { get; set; }
        public string LeaveTypeName { get; set; }
        public int LeaveApplyBeforeDays { get; set; }
        public int MonthlyQuantity { get; set; }
        public decimal TotalLeaveTakenDays { get; set; }
        public decimal TotalRemainingDays { get; set; }
        public bool IsPayable { get; set; }
        public int LeaveYearId { get; set; }
        public decimal ProrataLeave { get; set; }
        public int LeaveAssiginId { get; set; }
    }
}
