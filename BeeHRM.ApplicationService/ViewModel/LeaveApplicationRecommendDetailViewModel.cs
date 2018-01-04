using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ViewModel
{
    public class LeaveApplicationRecommendDetailViewModel
    {
        public EmployeeDetailsViewModel LeaveApplier { get; set; }
        public LeaveApplicationViewModel LeaveDetail { get; set; }

    }
}
