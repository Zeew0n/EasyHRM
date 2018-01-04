using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeeHRM.ApplicationService.DTOs
{
    public class TaxDetailDTO
    {
        public int DetailId { get; set; }
        public int MasterId { get; set; }
        public decimal MaxAmount { get; set; }
        public int OrderNumber { get; set; }
        public decimal Percentage { get; set; }
        public virtual TaxSetupDTO PayrollTaxSetup { get; set; }
    }
}
