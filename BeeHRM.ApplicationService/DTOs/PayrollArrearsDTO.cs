using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.DTOs
{
    public class PayrollArrearsDTO
    {
        public int Id { get; set; }
        public int EmployeeCode { get; set; }
        public int ArrearMonth { get; set; }
        public int AdjustMonth { get; set; }
        public int FyId { get; set; }
        public string EarningDeduction { get; set; }
        public decimal NoOfDays { get; set; }
        public virtual EmployeeDTO Employee { get; set; }
        public virtual FiscalDTO Fiscal { get; set; }
        public virtual PayrollMonthDescriptionDTO PayrollMonthDescription { get; set; }
        public virtual PayrollMonthDescriptionDTO PayrollMonthDescription1 { get; set; }
        public IEnumerable<SelectListItem> EmployeeList { get; set; }
        public IEnumerable<SelectListItem> ArrearMonthList { get; set; }
        public IEnumerable<SelectListItem> AdjustMonthList { get; set; }
        public IEnumerable<SelectListItem> FiscalYearList { get; set; }
        public IEnumerable<SelectListItem> EarningDeductionList { get; set; }
    }
}
