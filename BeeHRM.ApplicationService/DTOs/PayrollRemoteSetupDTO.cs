using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class PayrollRemoteSetupDTO
    {
        public int RemoteId { get; set; }
        public string RemoteName { get; set; }
        public string RemoteDescription { get; set; }
    }
}
