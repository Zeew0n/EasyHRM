using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ViewModel
{
    public class JobHistoryViewModel
    {
        public EmployeeDetailsViewModel EmployeeDetails { get; set; }
        public EmployeeJobHistoryDTO JobHistories { get; set; }
        [Display(Name ="Update As Current Job")]
        public bool UpdateAsCurrent { get; set; }
    }
}
