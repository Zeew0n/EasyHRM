using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BeeHRM.ApplicationService.Leave_Module.DTOs
{
    public class LeaveAssignedDTOs
    {
        public int AssignedId { get; set; }
        public int AssignEmpCode { get; set; }
        public int AssignLeaveTypeId { get; set; }
        public decimal AssignedDays { get; set; }
        public int AssignedLeaveYearId { get; set; }
        public string AssignedRemarks { get; set; }
        public int LeaveGainedMonth { get; set; }

        public virtual LeaveType LeaveType { get; set; }
        public virtual LeaveYear LeaveYear { get; set; }

        //LeaveAssign Model
        public decimal HomeBalance { get; set; }
        public decimal HomeAssignment { get; set; }
        public int HomeMonthId { get; set; }
        public decimal SickLeaveBalance { get; set; }
        public decimal SickLeaveNew { get; set; }
        public decimal Casual { get; set; }
        public List<SelectListItem> NepaliMonth { get; set; }
        public List<LeaveAssignedList> LeaveAssignedList { get; set; }

    }

    public class LeaveAssignedList
    {
        public int MonthId { get; set; }
        [Display(Name="Month")]
        public string MonthName { get; set; }
        [Display(Name="Home leave")]
        public decimal HomeLeaveBalance { get; set; }
        [Display(Name="Sick leave")]
        public decimal SickLeaveBalance { get; set; }
        [Display(Name="Casual leave")]
        public decimal CasualLeaveBalance { get; set; }
        public int LeaveGainedMonth { get; set; }
    }
}
