using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class BankDTO
    {
        public int BankId { get; set; }
        [Display(Name ="Bank Name")]
        [Required]
        public string BankName { get; set; }
        [Display(Name ="Bank Status")]
        public bool BankStatus { get; set; }
        [Display(Name ="Bank Added Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> BankAddedDate { get; set; }
        [Required]
        public string BankAddedDateNP { get; set; }
    }
}
