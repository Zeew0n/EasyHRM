using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.DTOs
{
   public class AttendanceReportsDTO
    {

        public int nepali_year { get; set; }
        public int month_code { get; set; }
        public string eng_start_date { get; set; }
        public Nullable<int> no_days { get; set; }
        public string eng_end_date { get; set; }
        public int OfficeId { get; set; }
        public DateTime ExcelStartDate { get; set; }
        public DateTime ExcelEndDate { get; set; }
        public List<SelectListItem> BranchSelectList { get; set; }

        public List<SelectListItem> YearList { get; set; }
        public List<SelectListItem> MonthList { get; set; }
    }
}
