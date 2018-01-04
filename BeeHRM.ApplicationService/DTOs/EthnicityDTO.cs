using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class EthnicityDTO
    {
        [Display(Name ="Id")]
        public int EthnicityId { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage ="Ethnicity name is required")]
        public string EthnicityName { get; set; }
    }
}
