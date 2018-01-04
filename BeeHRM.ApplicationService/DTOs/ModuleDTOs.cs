using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
     public class ModuleDTOs
    {
            [Key]
            public int ModuleId { get; set; }
            public int ModuleParentId { get; set; }
            public string MOduleName { get; set; }
            public string ModuleCssClass { get; set; }
            public string MduleLink { get; set; }
             public int? Order { get; set; }
            public string Controller { get; set; }
            public int Level { get; set; }
            public bool Views { get; set; }
            public bool Add { get; set; }
            public bool Edit { get; set; }
            public bool Delete { get; set; }
            public int RoleId { get; set; }
            public bool IsDefault { get; set; }
            public  ParentModulesDTO ParentModule { get; set; }
            public  IEnumerable<RoleAccessDTOs> RolesAccesses { get; set; }
    }




     public class TreeModules
     {
         [Key]
         public int Id { get; set; }
         public int ModuleParentId { get; set; }
         public string MOduleName { get; set; }
         public string ModuleCssClass { get; set; }
         public int Level { get; set; }
         public bool Views { get; set; }
         public bool Add { get; set; }
         public bool Edit { get; set; }
         public bool Delete { get; set; }
         public int RoleId { get; set; }
     }

    public class ModularLists
    {
        public List<TreeModules> ListOfModules { get; set; }
    }

    public class ParentModulesDTO
    {
        public int ParentModulesId { get; set; }
        public string parentModuleName { get; set; }
        public string ParentModulesCssClass { get; set; }
        public string ParentModuleLink { get; set; }
        public Nullable<int> ParentModuleOrder { get; set; }
        public bool IsDefault { get; set; }
        public string Controller { get; set; }


    }

    public class ModuleDTOsForparent
    {

        public int ModuleId { get; set; }
        public int ModuleParentId { get; set; }
        public string MOduleName { get; set; }
        public string ModuleCssClass { get; set; }
        public string MduleLink { get; set; }
        public int? Order { get; set; }
        public bool? IsDefault { get; set; }
        public string Controller { get; set; }
        public bool ModuleStatus { get; set; }
    }

}
