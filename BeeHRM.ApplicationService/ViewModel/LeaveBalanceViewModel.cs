using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.ViewModel
{
   public  class LeaveBalanceViewModel
    {
        public int nepali_year { get; set; }
        public List<SelectListItem> YearList { get; set; }  

        public IEnumerable<EmployeeDTO> EmpList { get; set; }

    }
}
