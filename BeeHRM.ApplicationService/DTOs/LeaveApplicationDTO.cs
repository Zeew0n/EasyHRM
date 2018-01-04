using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BeeHRM.ApplicationService.DTOs
{
    public class LeaveApplicationDTO
    {
        public int LeaveId { get; set; }

      
        public int LeaveEmpCode { get; set; }
        [Required(ErrorMessage = "Year is required")]
        public int LeaveYearId { get; set; }
        [Required(ErrorMessage = "Leave Type required")]
        public int LeaveTypeId { get; set; }
       
        public int RecommededEmpCode { get; set; }
        public int RecommendStatus { get; set; }
        public string RecommenderMessage { get; set; }
        public Nullable<System.DateTime> RecommendStatusDate { get; set; }
       
        public int LeaveApproverEmpCode { get; set; }
        public int LeaveStatus { get; set; }
        public string ApproverMessage { get; set; }
        public Nullable<System.DateTime> LeaveStatusDate { get; set; }
        public System.DateTime LeaveStartDate { get; set; }
        public string LeaveStartDateNP { get; set; }
        public System.DateTime LeaveEndDate { get; set; }
        public string LeaveEndDateNP { get; set; }
        public decimal LeaveDays { get; set; }
        public bool PaidLeave { get; set; }
        [Required]
        public string LeaveSubject { get; set; }
        public string LeaveDetails { get; set; }
        public System.DateTime LeaveAppliedDate { get; set; }
        public string LeaveDaysType { get; set; }
        public string LeaveDaysPart { get; set; }
        public string LeaveGUICode { get; set; }
        public string EmpName { get; set; }
        public bool LeaveBG { get; set; }

        public int MonthId { get; set; }
        public bool IsHalfDayLeave { get; set; }
        public IEnumerable<SelectListItem> EmployeeCodeSelectlist { get; set; }
        public IEnumerable<SelectListItem> YearSelectList { get; set; }
        public IEnumerable<SelectListItem> LeaveTypeSelectList { get; set; }

        public LeaveTypeDTO LeaveType { get; set; }
        public LeaveYearDTO LeaveYear { get; set; }
        public EmployeeDTO Employee { get; set; }
        public EmployeeDTO RecommendateEmployee { get; set; }
        public EmployeeDTO ApprovalEmployee { get; set; }

        public IEnumerable<SelectListItem> ApproverSelectList { get; set; }

    }


    public partial class LeaveApplicationDTOInformation
    {
        public List<LeaveApplicationDTO> LeaveApplicationDTOList { get; set; }
        public IEnumerable<SelectListItem> EmployeeCodeSelectlist { get; set; }
        public IEnumerable<SelectListItem> YearSelectList { get; set; }
        public IEnumerable<SelectListItem> ApproverSelectList { get; set; }
        public int? MonthId { get; set; }
        [Required(ErrorMessage = "Year is required")]
        public int LeaveYearId { get; set; }
       public int? LeaveEmpCode { get; set; }
    }
}
