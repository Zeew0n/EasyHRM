using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class TaxSetupDTO
    {
        public int MasterId { get; set; }
        public string TaxName { get; set; }
        public string TaxDescription { get; set; }
        public bool TaxStatus { get; set; }
        public  IEnumerable<TaxDetailDTO> PayrollTaxDetails { get; set; }
    }
}
