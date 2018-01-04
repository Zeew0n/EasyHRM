using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class ModuleActionDTO
    {
        public int ModuleActionsID { get; set; }
        public string ActionName { get; set; }
        public Nullable<int> ModulesIDFK { get; set; }
        public string ActionIconPath { get; set; }
        public string APIEndPoint { get; set; }
        //public string CreatedBy { get; set; }
        //public string UpdatedBy { get; set; }
        //public Nullable<System.DateTime> CreatedDate { get; set; }
        //public Nullable<System.DateTime> UpdatedDate { get; set; }


        public virtual ICollection<ModuleActionPermissionDTO> ModuleActionPermissions { get; set; }
        public virtual ModuleDTOs Module { get; set; }
    }
}
