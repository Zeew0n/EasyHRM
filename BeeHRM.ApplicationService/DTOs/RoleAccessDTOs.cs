using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class RoleAccessDTOs
    {
        public int AssignId { get; set; }
        public Nullable<int> AssignRoleId { get; set; }
        public Nullable<int> AssignModuleId { get; set; }
        public Nullable<bool> AllowAdd { get; set; }
        public Nullable<bool> AllowEdit { get; set; }
        public Nullable<bool> AllowDelete { get; set; }
        public Nullable<bool> AllowView { get; set; }
        public  ModuleDTOs ModuleData { get; set; }
        public RoleDTOs RoleData { get; set; }
    }
}
