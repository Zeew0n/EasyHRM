using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeeHrmClientWeb.Models
{
    public class AttendanceMonthlyReport
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime ExcelStartDate { get; set; }
        public DateTime ExcelEndDate { get; set; }
    }
}