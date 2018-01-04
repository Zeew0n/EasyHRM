using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
   public class TenantDTO
    {

        public int TenantID { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Logo { get; set; }
        public int CultureID { get; set; }
        public string Culture { get; set; }
        public string CultureCode { get; set; }
        public int CountryID { get; set; }
        public string Country { get; set; }
    }
}
