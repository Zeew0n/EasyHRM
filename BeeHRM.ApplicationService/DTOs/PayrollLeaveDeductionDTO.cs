using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BeeHRM.ApplicationService.DTOs
{
    public class PayrollLeaveDeductionDTO
    {
        public int DeductionId { get; set; }
        [Required(ErrorMessage = "Employee Name is required")]
        public int EmpCode { get; set; }
        public int EmpId { get; set; }
        [Required(ErrorMessage = "Leave Type is required")]
        public int LeaveTypeId { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [Range(15, 30)]
        public decimal LeaveDays { get; set; }
        public string DeductionType { get; set; }
        public Nullable<int> LeaveYearId { get; set; }
        public System.DateTime LeaveDate { get; set; }

        public string LeaveDateNepali { get; set; }
        public string Details { get; set; }
        public string LeaveName { get; set; }
        public IEnumerable<SelectListItem> EmployeeCodeSelectlist { get; set; }
        public IEnumerable<SelectListItem> YearSelectList { get; set; }
        public IEnumerable<SelectListItem> LeaveTypeSelectList { get; set; }
        public EmployeeDTO Employee { get; set; }
        public LeaveTypeDTO LeaveType { get; set; }
        public LeaveYearDTO LeaveYear { get; set; }
        public bool isEncashed { get; set; }
        public decimal leavebalance { get; set; }
    }
    public partial class PayrollLeaveDeductionInformation
    {
       public List<PayrollLeaveDeductionDTO> PayrollLeaveDeductionList { get; set; }
        public Nullable<int> LeaveYearId { get; set; }
        public int? EmpId { get; set; }
        public int MonthId { get; set; }
        public IEnumerable<SelectListItem> EmployeeCodeSelectlist { get; set; }
        public IEnumerable<SelectListItem> YearSelectList { get; set; }
        public IEnumerable<SelectListItem> LeaveTypeSelectList { get; set; }
   

    }
}
