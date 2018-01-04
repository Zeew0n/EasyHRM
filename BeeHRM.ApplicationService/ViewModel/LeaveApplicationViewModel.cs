using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ViewModel
{
    public class LeaveApplicationViewModel
    {
        public int LeaveId { get; set; }
        public int LeaveStatus { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public string LeaveSubject { get; set; }
        public string LeaveDetails { get; set; }
        public DateTime LeaveAppliedDate { get; set; }
        public string LeaveYearName { get; set; }
        public string LeaveTypeName { get; set; }
        public string RecommenderName { get; set; }
        public string ApproverName { get; set; }
        public string AppliedBy { get; set; }
        public int LeaveEmpCode { get; set; }
        public int IsValid { get; set; }
        public int RecommendStatus { get; set; }
        public string RecommendMessage { get; set; }
        public int RecommendEmpCode { get; set; }
        public string ApproveMessage { get; set; }
        public string ApproveDate { get; set; }
        public string RecommendDate { get; set; }
        public decimal LeaveDays { get; set; }
        public string ApproveStatus { get; set; }
        public string EmpName { get; set; }
        public int LeaveApproverEmpcode { get; set; }
        public int LeaveRecommenderEmpcode { get; set; }


    }
}
