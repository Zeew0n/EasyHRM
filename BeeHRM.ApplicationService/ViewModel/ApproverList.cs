using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.ViewModel
{
   public class ApproverList
    {
        public int EmpCode { get; set; }
        public int OfficeId { get; set; }
        public IEnumerable<EmployeeDTO> Employee { get; set; }
        public string Degisnation { get; set; }
        public List<int> EmpId { get; set; }
        public List<int> OfficeListId { get; set; }
        public IEnumerable<OfficeDTOs> Office { get; set; }
        public IEnumerable<SelectListItem> BranchSelectList { get; set; }
    }
}
