using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.DTOs
{
   public class PayrollRemoteAllowancesDTO
    {
        public int RAId { get; set; }
        public int RARankId { get; set; }
        public int RARemoteId { get; set; }
        public decimal RemoteAllowance { get; set; }
        public string RAType { get; set; }
        public decimal RemoteTaxExemption { get; set; }
        public virtual PayrollRemoteSetupDTO PayrollRemoteSetup { get; set; }
        public virtual RankDTO Rank { get; set; }
        public IEnumerable<SelectListItem> RankList { get; set; }
        public IEnumerable<SelectListItem> RemoteList { get; set; }
    }
}
