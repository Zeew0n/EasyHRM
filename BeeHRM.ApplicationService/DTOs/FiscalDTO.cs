using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class FiscalDTO
    {
        [Display(Name ="SN")]
        public int FyId { get; set; }
        [Display(Name ="Name")]
        [Required(ErrorMessage ="Fiscal name is required")]
        public string FyName { get; set; }
        [Display(Name ="Start Date")]
        [Required(ErrorMessage ="Start date is required")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MMM-dd}")]
        public System.DateTime FyStartDate { get; set; }
        [Display(Name ="End Date")]
        [Required(ErrorMessage ="End date is required")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MMM-dd}")]
        public System.DateTime FyEndDate { get; set; }
        [Display(Name ="Start Date in Nepali")]
        public string FyStartDateNp { get; set; }
        [Display(Name = "End Date in Nepali")]
        public string FyEndDateNp { get; set; }
        [Display(Name ="Current Fiscal Year")]
        public bool FyCurrent { get; set; }
    }
}
