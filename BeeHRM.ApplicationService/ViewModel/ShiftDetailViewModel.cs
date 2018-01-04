using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ViewModel
{
   public class ShiftDetailViewModel
    {
        public int ShiftId { get; set; }
        [Required(ErrorMessage ="Shift Name is required")]
        public string ShiftName { get; set; }
        public Nullable<bool> ShiftStatus { get; set; }
        [Required(ErrorMessage ="Delay time is required")]
        public Nullable<System.TimeSpan> ShiftDelayAllow { get; set; }
        public List<ShiftDayDTO> ShiftDay { get; set; }
    }
}
