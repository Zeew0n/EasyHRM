using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class AttendanceDailyDTO
    {
        public long AttId { get; set; }
        public int AttEmpCode { get; set; }
        public Nullable<int> AttOfficeId { get; set; }
        public Nullable<int> AttJobHistoryId { get; set; }
        public Nullable<System.DateTime> AttDate { get; set; }
        public string AttDateNp { get; set; }
        public Nullable<System.DateTime> AttCheckIn { get; set; }
        public Nullable<System.DateTime> AttCheckOut { get; set; }
        public Nullable<int> AttShiftId { get; set; }
        public Nullable<System.TimeSpan> AttShiftStart { get; set; }
        public Nullable<System.TimeSpan> AttShiftEnd { get; set; }
        public Nullable<bool> IsAbsent { get; set; }
        public Nullable<decimal> PayableDay { get; set; }
        public Nullable<bool> IsWeekend { get; set; }
        public Nullable<int> IsHoliday { get; set; }
        public Nullable<int> isOfficialVisit { get; set; }
        public Nullable<int> isLeave { get; set; }
        public string AttRemarks { get; set; }
    }
}
