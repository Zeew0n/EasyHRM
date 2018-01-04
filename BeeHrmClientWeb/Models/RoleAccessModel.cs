using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeeHrmClientWeb.Models
{
    public class RoleAccessModel
    {
        public int AssignId { get; set; }
        public Nullable<int> AssignRoleId { get; set; }
        public Nullable<int> AssignModuleId { get; set; }
        public Nullable<bool> AllowAdd { get; set; }
        public Nullable<bool> AllowEdit { get; set; }
        public Nullable<bool> AllowDelete { get; set; }
        public Nullable<bool> AllowView { get; set; }

       // public ModuleModules ModuleData { get; set; }

       // public  RoleData { get; set; }
    }
}