using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.DTOs
{
    public class RolesAccessDTO
    {
        public int AssignId { get; set; }
        public int AssignRoleId { get; set; }
        [Required]
        public int AssignModuleId { get; set; }
        public bool AllowAdd { get; set; }
        public bool AllowEdit { get; set; }
        public bool AllowDelete { get; set; }
        public bool AllowView { get; set; }
        public string ModuleName { get; set; }
        public string ParentModuleName { get; set; }
        public string RoleName { get; set; }
        public int AssignModuleParentId { get; set; }
        public ParentModulesDTO ParentModule { get; set; }

    }
}
