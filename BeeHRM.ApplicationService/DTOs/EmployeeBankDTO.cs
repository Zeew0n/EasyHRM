using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.DTOs
{
    public class EmployeeBankDTO
    {
        public int Id { get; set; }
        [Display(Name ="Employee")]
        public Nullable<int> EmpCode { get; set; }
        [Display(Name ="Bank")]
        public Nullable<int> BankId { get; set; }
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }
        [Display(Name = "Account Primary")]
        public bool AccountPrimary { get; set; }

        public virtual BankDTO Bank { get; set; }
        public virtual EmployeeDTO Employee { get; set; }
        public IEnumerable<SelectListItem> EmployeeList { get; set; }
        public IEnumerable<SelectListItem> BankList { get; set; }
    }
}
