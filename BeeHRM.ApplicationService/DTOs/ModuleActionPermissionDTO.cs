using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class ModuleActionPermissionDTO
    {
        public int ModuleActionPermissionID { get; set; }
        public Nullable<int> UserRoleIDFK { get; set; }
        public Nullable<int> ModuleActionsIDFK { get; set; }
        public Nullable<bool> HasAccess { get; set; }
        //public string CreatedBy { get; set; }
        //public string UpdatedBy { get; set; }
        //public Nullable<System.DateTime> CreatedDate { get; set; }
        //public Nullable<System.DateTime> UpdatedDate { get; set; }

        public virtual ModuleActionDTO ModuleAction { get; set; }
        // public virtual UserRoleDTO UserRole { get; set; }
    }
}
