using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.DTOs
{
    public class PayrollAllowanceDetailDTO
    {
        public int Id { get; set; }
        public int AllowanceMasterId { get; set; }
        public int EmployeeCode { get; set; }
        public string PercentageAmount { get; set; }
        public Nullable<decimal> Value { get; set; }
        public virtual EmployeeDTO Employee { get; set; }
        public virtual PayrollAllowanceMasterDTO PayrollAllowanceMaster { get; set; }

        public List<SelectListItem> AllowanceMasterList { get; set; }

        public string AllowanceName { get; set; }
        public string EmployeeName { get; set; }
        public List<SelectListItem> OfficeList { get; set; }
        public int OfficeId { get; set; }


    }

    public class ExportExcelModel
    {
        public int SNo { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeName { get; set; }

        public string PercentageAmount { get; set; }

        public decimal Value { get; set; }
    }
    
}
