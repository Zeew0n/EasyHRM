using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ViewModel
{
    public class AttendanceSearchViewModel
    {
        public int SearchId { get; set; }
        public DateTime? StartDate { get; set; }
        //Nulable date
        public DateTime? EndDate { get; set; }


    }
}
