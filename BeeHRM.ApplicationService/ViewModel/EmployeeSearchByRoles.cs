using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.ViewModel
{
   public class EmployeeSearchByRoles
    {
        public IEnumerable<EmployeeDTO> Employee { get; set; }
        public IEnumerable<RolesDTO> Roles { get; set; }
        public IEnumerable<OfficeDTOs> Office { get; set; }
        public IEnumerable<SelectListItem> BranchSelectList { get; set; }
        public IEnumerable<SelectListItem> RolesSelectList { get; set; }
        public IEnumerable<SelectListItem> EmployeeSelectList { get; set; }
        public int? OfficeId { get; set; }
        public int ? RoleId { get; set; }

    }
}
