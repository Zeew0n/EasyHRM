using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.Repository.StoredProcedureVariable
{
   public class PayrollLeaveBalance
    {
        public decimal TotalLeaveDays { get; set; }
        public decimal BalanceDays { get; set; }    
        public int EmpCode { get; set; }
    }
}
