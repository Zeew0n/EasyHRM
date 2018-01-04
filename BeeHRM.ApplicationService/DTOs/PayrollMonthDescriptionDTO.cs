using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.DTOs
{
    public class PayrollMonthDescriptionDTO
    {
        public int Id { get; set; }
        [Required]
        public int FyId { get; set; }
        [Required]
        public int MonthIndex { get; set; }
        public string MonthNameEnglish { get; set; }
        [Display(Name = "Month")]
        public string MonthNameNepali { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> EndDate { get; set; }
        public decimal WorkingDays { get; set; }
        public FiscalDTO Fiscal { get; set; }
        public IEnumerable<SelectListItem> Fiscals { get; set; }
    }
}
