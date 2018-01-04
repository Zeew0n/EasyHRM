using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeeHrmClientWeb.Models
{
    public class EmployeeAssignRole
    {
          public EmployeeDetailsViewModel EmpDetail { get; set; }
            public IEnumerable<RolesDTO> Rolelist { get; set; }
        public int RoleId { get; set; }
    }
}