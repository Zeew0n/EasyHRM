using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
   public class AttDailyDTOs
    {
        public int DailyId { get; set; }
        public DateTime? DailyDate { get; set; }
        public bool DailyStatus { get; set; }
        public DateTime? DailyCreatedAt { get; set; }
    }
}
