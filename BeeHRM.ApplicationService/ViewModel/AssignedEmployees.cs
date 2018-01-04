using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ViewModel
{
    public class AssignedEmployees
    {
        public int EmployeeCode{get;set;}
        public string EmployeeName{get;set;}
        public string PercentageAmount{get;set;}
        public bool Selected { get; set; } 
        public Nullable<decimal> Value { get; set; }
    }
}
