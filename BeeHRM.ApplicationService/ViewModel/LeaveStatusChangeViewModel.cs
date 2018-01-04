using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ViewModel
{
    public class LeaveStatusChangeViewModel
    {
        public int LeaveId { get; set; }
        public int LeaveStatus { get; set; }
        [Required(ErrorMessage = "This field is required !! please write some message..")]
        [DataType(DataType.MultilineText)]
        public string StatusChangeMessage { get; set; }
    }
}
