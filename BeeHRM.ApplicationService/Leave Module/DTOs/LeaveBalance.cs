using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Leave_Module.DTOs
{
    public class LeaveBalance
    {
        public int EmpCode { get; set; }
        public int LeaveTypeId { get; set; }
        public int LeaveYearId { get; set; }
        public string LeaveTypeName { get; set; }
        public string LeaveTypeAssignment { get; set; }
        public Nullable<decimal> TotalLeaveAssigned { get; set; }
        public decimal TotalTaken { get; set; }
        public Nullable<decimal> Leave_Balance { get; set; }
        public decimal ThisYearEarned { get; set; }
        public decimal PrevYearBalance { get; set; }
        public IEnumerable<SelectListItem> EmpCodeList { get; set; }
        public List<SelectListItem> LeaveYearList { get; set; }

    }
}
