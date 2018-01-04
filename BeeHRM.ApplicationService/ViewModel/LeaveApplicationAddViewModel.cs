using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ViewModel
{
    public sealed class LeaveApplicationAddViewModel
    {
        public bool IsHalfDayLeave { get; set; }
        [Required(ErrorMessage ="The Subject is required for Leave Application.")]
        public string LeaveSubject { get; set; }
        [Required(ErrorMessage ="The leave type is a required field.")]
        public int LeaveTypeId { get; set; }
        [Required(ErrorMessage ="Recommender is Required.")]
        public int LeaveRecommenderId { get; set; }
        [Required(ErrorMessage = "Approver is Required.")]
        public int LeaveApproverId { get; set; }
        public string HalfDayLeaveValue { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public string LeaveDescription { get; set; }
        public string SerializedActiveYearData { get; set; }
        public string SerializedStatData { get; set; }
        public IEnumerable<EmployeeDTO> ApproverList { get; set; }
        public IEnumerable<EmployeeDTO> RecommenderList { get; set; }
        public IEnumerable<LeaveTypeDTO> ValidLeaveTypes{ get; set; }
        public IEnumerable<EmployeeDTO> Employees { get; set; }
        public IEnumerable<LeaveStatViewModel> LeaveStats { get; set; }

        public int YearID { get; set; }
    }
}
