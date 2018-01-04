using System;
using System.Collections.Generic;

namespace BeeHRM.ApplicationService.ViewModel
{
    public class AttendanceEntireYearViewModel
    {
        public int Empcode { get; set; }

        public List<AttendanceDailyDetails> DailyData { get; set; }


    }

    public class AttendanceDailyDetails
    {
        public DateTime AttDate { get; set; }
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }
        public string LateEntry { get; set; }
        public string EarlyExit { get; set; }
        public string Holiday { get; set; }
        public string Weekend { get; set; }
        public string OfficialVisit { get; set; }
        public string Training { get; set; }
        public string Leave { get; set; }
        public string AttStatus { get; set; }
        public string logDateNP { get; set; }
        public string CheckAbsent { get; set; }
        public string Isabsent { get; set; }
        public string LeaveTypeName { get; set; }

    }
}
