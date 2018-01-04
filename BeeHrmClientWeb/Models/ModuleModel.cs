using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeeHrmClientWeb.Models
{
    public class ModuleModules
    {

        public int ModuleId { get; set; }
        public int ModuleParentId { get; set; }
        public string MOduleName { get; set; }
        public string ModuleCssClass { get; set; }
        public string MduleLink { get; set; }
        public Nullable<int> Order { get; set; }
        public string Controller { get; set; }

    }
}