using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class LevelDTO
    {
        public int LevelId { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string LevelName { get; set; }
        [Required(ErrorMessage ="Order is required")]
        public Nullable<int> LevelOrder { get; set; }
    }
}
