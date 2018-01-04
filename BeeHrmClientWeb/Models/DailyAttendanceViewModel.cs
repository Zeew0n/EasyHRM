using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeeHrmClientWeb.Models
{
    public class DailyAttendanceViewModel
    {
        public int? BgId { get; set; }
        public string EmpName { get; set; }
        public string EmpOffice { get; set; }
        public string EmpDesignation { get; set; }
        public string AttCheckIn { get; set; }
        public string AttCheckOut { get; set; }
        public string WorkedHours { get; set; }
        public string LateEntry { get; set; }
        public string EarlyExit { get; set; }
        public string OfficeType { get; set; }
        public string Department { get; set; }
        public DateTime? SearchDate { get; set; }
        public string AttendanceStatus { get; set; }
        public int? OfficeTypeID { get; set; }
        public int? DesgId { get; set; }
        public int? EmpCodeId { get; set; }
        public int? EmpDeptId { get; set; }
        public string BusinessGroup { get; set; }
        public DateTime AttDate { get; set; }
        public string EmpLevelName { get; set; }
        public string AttDay { get; set; }
        public string IsLateEntry { get; set; }
        public bool? IsAbsent { get; set; }
    }
}