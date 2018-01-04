using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class ZoneDTO
    {
        public int Id { get; set; }
        public string ZoneName { get; set; }
        public ICollection<DistrictDTO> Districts { get; set; }
    }
}
