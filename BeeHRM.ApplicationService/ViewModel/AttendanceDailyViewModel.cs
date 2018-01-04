using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ViewModel
{
    public class AttendanceDailyViewModel
    {
        public int EmpCode { get; set; }
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
        public int? BgId { get; set; }
        public string ShiftName { get; set; }
        public int? EmpCodeId { get; set; }
        public int? EmpDeptId { get; set; }
        public string BusinessGroup { get; set; }
        public DateTime? AttDate { get; set; }
        public string EmpLevelName { get; set; }
        public string AttDay { get; set; }
        public string IsLateEntry { get; set; }
        public bool IsWeekend { get; set; }
        public int IsHoliday { get; set; }
        public int IsOfficalVisit { get; set; }
        public int IsLeave { get; set; }

        public int IsAbsent { get; set; }
        public string SearchStartdate { get; set; }
        public string SearchEndDate { get; set; }




    }
}
