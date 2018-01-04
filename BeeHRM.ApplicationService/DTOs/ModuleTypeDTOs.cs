using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class ModuleTypeDTOs
    {
        public int ModuleId { get; set; }
        public int ModuleParentId { get; set; }
        public string MOduleName { get; set; }
        public string ModuleCssClass { get; set; }
        public string MduleLink { get; set; }
        public Nullable<int> Order { get; set; }

    }
}
