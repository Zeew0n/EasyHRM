using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Leave_Module.DTOs
{
    public class LeaveApplicationDTOs
    {
        public int LeaveId { get; set; }
        public int LeaveEmpCode { get; set; }
        public int LeaveYearId { get; set; }
        public int LeaveTypeId { get; set; }
        public string EmpName { get; set; }
        public string RecommenderName { get; set; }
        public string ApproverName { get; set; }
        public string LeaveTypeName { get; set; }
        [Display(Name = "Recommender")]
        public int RecommededEmpCode { get; set; }
        public int RecommendStatus { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Recommend Message")]

        public string RecommenderMessage { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> RecommendStatusDate { get; set; }
        [Display(Name = "Approver")]
        public int LeaveApproverEmpCode { get; set; }
        public int LeaveStatus { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Approver Message")]
        public string ApproverMessage { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> LeaveStatusDate { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "Start Date")]
        public System.DateTime LeaveStartDate { get; set; }
        public string LeaveStartDateNP { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "End Date")]
        public Nullable<DateTime>LeaveEndDate { get; set; }
        public string LeaveEndDateNP { get; set; }
        public decimal LeaveDays { get; set; }
        public bool PaidLeave { get; set; }
        [Required]
        [Display(Name = "Leave Subject")]
        public string LeaveSubject { get; set; }
        [Required]
        [Display(Name = "Leave Description")]
        [DataType(DataType.MultilineText)]
        public string LeaveDetails { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public System.DateTime LeaveAppliedDate { get; set; }
        [Display(Name = "IsHalf Leave")]
        public string LeaveDaysType { get; set; }
        [Display(Name = "Leave Day Half")]
        public string LeaveDaysPart { get; set; }
        public string LeaveGUICode { get; set; }
        public bool IsHalfDay { get; set; }
        public bool IsHalfDayAllow { get; set; }
        public virtual EmployeeDTOs EmployeeDetail { get; set; }
        public virtual EmployeeDTOs RecommenderDetails { get; set; }
        public virtual EmployeeDTOs ApproverDetails { get; set; }
        public virtual LeaveTypesDTOs Leavetypes { get; set; }
        public virtual LeaveYearsDTOs LeaveYear { get; set; }

        public IEnumerable<SelectListItem> EmpList { get; set; }
        public List<SelectListItem> LeaveStatusList { get; set; }
        public List<SelectListItem> LeaveYearList { get; set; }
        public List<SelectListItem> ApproverList { get; set; }
        public List<SelectListItem> RecommenderList { get; set; }
        public List<string> ErrorList { get; set; }

        public LeaveApplicationDTOs()
        {

            LeaveDaysType = "F";
            LeaveAppliedDate = DateTime.Now;
            LeaveStatus = 1;
            RecommendStatus = 1;
            LeaveGUICode = Guid.NewGuid().ToString();
        }

    }
}
