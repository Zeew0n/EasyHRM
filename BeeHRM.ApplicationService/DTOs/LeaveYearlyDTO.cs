using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.DTOs
{
    public class LeaveYearlyReportDTO
    {
        public int EmployeeCode { get; set; }
        public int LeaveId { get; set; }
        public int MonthId { get; set; }
        public string MonthName { get; set; }
        public decimal AssignedDays { get; set; }
        public decimal GotHomeLeave { get; set; }
        public decimal GotSickLeave { get; set; }
        public decimal GotCasualLeave { get; set; }
        public decimal GotExchangeLeave { get; set; }
        public decimal TakenHomeLeave { get; set; }
        public decimal TakenSickLeave { get; set; }
        public decimal TakenCasualLeave { get; set; }
        public decimal TakenExchangeLeave { get; set; }
        public decimal LeftHomeLeave { get; set; }
        public decimal LeftSickLeave { get; set; }
        public decimal LeftCasualLeave { get; set; }
        public decimal LeftExchangeLeave { get; set; }
    }


    public partial class LeaveYearlyReportWithFilter
    {
        [Required]
        [DisplayName("Employee")]
        public Nullable<int> EmployeeCode { get; set; }
        [Required]
        [DisplayName("Leave years")]
        public Nullable<int> LeaveYearId { get; set; }
        public List<LeaveYearlyReportDTO> LeaveYearlyReport { get; set; }
        public List<PayrollLeaveDeductionDTO> PayrollLeaveDeduction { get; set; }
        public IEnumerable<SelectListItem> EmployeeSelectList { get; set; }
        public IEnumerable<SelectListItem> LeaveYears { get; set; }
    }
}
