using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
   public class EducationLevelDTO
    {
        public int LevelId { get; set; }
        [Display(Name ="Name(*)")]
        [Required(ErrorMessage ="Level Name is required",AllowEmptyStrings =false)]
        public string LevelName { get; set; }
        [Required(ErrorMessage ="Order is required",AllowEmptyStrings =false)]
        [Display(Name ="Order")]
        public Nullable<int> LevelOrder { get; set; }
    }
}
