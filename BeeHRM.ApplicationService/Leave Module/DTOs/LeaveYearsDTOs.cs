using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeeHRM.ApplicationService.Leave_Module.DTOs
{
    public class LeaveYearsDTOs
    {
        public LeaveYearsDTOs()
        {
            YearCurrent = false;
            Isactive = (bool)YearCurrent;

        }
        public int YearId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public int YearName { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public System.DateTime? YearStartDate { get; set; }
        [Display(Name = "End Date")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public System.DateTime YearEndDate { get; set; }
        public string YearStartDateNp { get; set; }
        public string YearEndDateNp { get; set; }
        [Display(Name = "IsCurrent")]
        public Nullable<bool> YearCurrent { get; set; }
        public bool Isactive { get; set; }
        public virtual ICollection<LeaveAssigned> LeaveAssigneds { get; set; }
        public virtual ICollection<LeaveMonthlyProcess> LeaveMonthlyProcesses { get; set; }
    }
}
