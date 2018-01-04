using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class ServiceEventSubGroupDTO
    {
        public int ServiceEventSubGroupId { get; set; }
        public int ServiceEventGroupId { get; set; }
        public string ServiceEventSubGroupName { get; set; }
    }
}
