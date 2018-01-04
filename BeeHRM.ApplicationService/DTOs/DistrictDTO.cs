using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class DistrictDTO
    {
        public int Id { get; set; }
        public int ZoneId { get; set; }
        public string DistrictName { get; set; }
        public ZoneDTO Zone { get; set; }
    }
}
