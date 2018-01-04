using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.DTOs
{
    public class LeaveEarnedDTO
    {
        public int LeaveEarnedId { get; set; }
        [Required(ErrorMessage = "Employee Name is required")]
        public int EmpCode { get; set; }
        public int EmpId { get; set; }
        public Nullable<System.DateTime> LeaveEarnRequestedDate { get; set; }
        public Nullable<int> RecommendedEmpCode { get; set; }
        public string RecommendStatus { get; set; }
        public Nullable<System.DateTime> RecommendStatusDate { get; set; }
        public int ApproverEmpCode { get; set; }
        public Nullable<int> ApproverStatus { get; set; }
        public Nullable<System.DateTime> ApproverStatusDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime WorkedStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public string WorkedStartDateNP { get; set; }
        public System.DateTime WorkedEndDate { get; set; }
        public string WorkedEndDateNP { get; set; }
        public Nullable<decimal> LeaveTotalEanrnedDays { get; set; }
        public Nullable<int> LeaveTypeId { get; set; }
        public Nullable<System.DateTime> EarnLeaveExpiryDate { get; set; }
        public string Remarks { get; set; }
        [Required(ErrorMessage = "Year is required")]
        public int LeaveYearId { get; set; }

        public EmployeeDTO Employee { get; set; }
        public EmployeeDTO RecommendateEmployee { get; set; }
        public EmployeeDTO ApprovalEmployee { get; set; }
        public LeaveTypeDTO LeaveType { get; set; }
        public LeaveYearDTO LeaveYear { get; set; }
        public IEnumerable<SelectListItem> EmployeeCodeSelectlist { get; set; }
        public IEnumerable<SelectListItem> ApproveEmployeeCodeSelectlist { get; set; }
        public IEnumerable<SelectListItem> GetRecommederSelectList { get; set; }
        public IEnumerable<SelectListItem> YearSelectList { get; set; }
        public IEnumerable<SelectListItem> LeaveTypeSelectList { get; set; }
        public IEnumerable<SelectListItem> BranchEmployeeCodeSelectlist { get; set; }




    }
    public partial class LeaveEarnedInfomationDTO
    {
        public List<LeaveEarnedDTO> LeaveEarnedList { get; set; }
        public IEnumerable<SelectListItem> BranchEmployeeCodeSelectlist { get; set; }
        public IEnumerable<SelectListItem> YearSelectList { get; set; }
        [Required(ErrorMessage = "Year is required")]
        public int LeaveYearId { get; set; }
        public int? EmpCode { get; set; }
        public int? EmpId { get; set; }
        public int? MonthId { get; set; }
    }
}
