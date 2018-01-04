using System;
using System.ComponentModel.DataAnnotations;

namespace BeeHRM.ApplicationService.DTOs
{
    public class LeaveYearDTO
    {
        public int YearId { get; set; }
        [Required(ErrorMessage = "The Year Name is Required!")]
        public Nullable<int> YearName { get; set; }
        //[Required(ErrorMessage = "The Year Start Date is Required!")]
        public Nullable<System.DateTime> YearStartDate { get; set; }
        //[Required(ErrorMessage = "The Year End Date is Required!")]
        public Nullable<System.DateTime> YearEndDate { get; set; }
        [Required(ErrorMessage = "The Year Start Date is Required!")]
        public string YearStartDateNp { get; set; }
        [Required(ErrorMessage = "The Year End Date is Required!")]
        public string YearEndDateNp { get; set; }
        public Nullable<bool> YearCurrent { get; set; }

    }
}
