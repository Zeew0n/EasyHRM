using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class RoleDTOs
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDetails { get; set; }
        public Nullable<bool> RoleDataAccessAll { get; set; }
    }
}
