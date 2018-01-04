using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.ViewModel
{
    public class AttendanceLogViewModel
    {
        public int logEmpCode { get; set; }
        public DateTime logDate { get; set; }
        public string logDateNP { get; set; }
        public DateTime logTime { get; set; }
        public int logTypeId { get; set; }
        public int InOutMode { get; set; }
        public int logMethodId { get; set; }

        public IEnumerable<SelectListItem> EmployeeSelectList { get; set; }
    }
}
