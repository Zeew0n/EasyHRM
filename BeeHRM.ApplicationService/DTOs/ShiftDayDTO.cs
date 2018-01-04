using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class ShiftDayDTO
    {
        public int DayId { get; set; }
        public int DayShiftId { get; set; }
        public int DayNumber { get; set; }
        public string DayName { get; set; }
        [Required(ErrorMessage ="Day Start Time is Required")]
        public Nullable<System.TimeSpan> DayStartTime { get; set; }
        [Required(ErrorMessage ="Day End Time is Required")]
        public Nullable<System.TimeSpan> DayEndTime { get; set; }
        public bool DayDual { get; set; }
        public int DayWorkingMinute { get; set; }
        public bool DayWeekend { get; set; }
    }
}
