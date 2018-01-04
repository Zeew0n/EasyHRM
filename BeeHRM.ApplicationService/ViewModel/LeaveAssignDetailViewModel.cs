using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ViewModel
{
   public class LeaveAssignDetailViewModel
    {

        public int LeaveTypeId { get; set; }
        public int AssignedId { get; set; }
        public bool IsEnable { get; set; }
        public decimal AddDays { get; set; }
        public bool IsAlreadyAssigned { get; set; }
        public int LeaveYearId { get; set; }
        public int LeaveTakenDays { get; set; }

    }
}
