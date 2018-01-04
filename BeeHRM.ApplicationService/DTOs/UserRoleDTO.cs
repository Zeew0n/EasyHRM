using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class UserRoleDTO
    {
        public int UserRoleID { get; set; }
        public string RoleName { get; set; }
        public Nullable<bool> IsDefault { get; set; }
        //public string CreatedBy { get; set; }
        //public string UpdatedBy { get; set; }
        //public Nullable<System.DateTime> CreatedDate { get; set; }
        //public Nullable<System.DateTime> UpdatedDate { get; set; }


        public virtual ICollection<ModuleActionPermissionDTO> ModuleActionPermissions { get; set; }

        public virtual ICollection<ModuleActionPermissionDTO> ModulePermissions { get; set; }

        // public virtual ICollection<UsersUserRoleRelDTO> UsersUserRoleRels { get; set; }
    }
}
