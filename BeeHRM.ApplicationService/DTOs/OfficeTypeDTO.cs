using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class OfficeTypeDTO
    {
        public int OfficeTypeId { get; set; }
        [Display(Name ="Name")]
        [Required(ErrorMessage ="Name is required")]
        public string OfficeTypeName { get; set; }
    }
}
