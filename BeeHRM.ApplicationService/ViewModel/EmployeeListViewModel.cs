using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ViewModel
{
    public class EmployeeListViewModel
    {
        public int EmpCode { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string JoinDate { get; set; }
        public string Designation { get; set; }
        public string DepartmentName { get; set; }
        public string OfficeName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
    }
}
