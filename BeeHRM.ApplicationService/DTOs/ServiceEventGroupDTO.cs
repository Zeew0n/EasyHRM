using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class ServiceEventGroupDTO
    {
        public int ServiceEventId { get; set; }
        public string ServiceEventGroupName { get; set; }
        public string ServiceEventGroupDetails { get; set; }
        public Nullable<int> ServiceEventOrder { get; set; }
    }
}
