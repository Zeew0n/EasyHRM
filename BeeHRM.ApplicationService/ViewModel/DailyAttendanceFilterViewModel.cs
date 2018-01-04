using System;

namespace BeeHRM.ApplicationService.ViewModel
{
    public class DailyAttendanceFilterViewModel
    {
        public int? EmpCode { get; set; }
        public int searchid { get; set; }
        public string code { get; set; }
        public string EmpName { get; set; }
        public string AttDate { get; set; }
        public string AttCheckIn { get; set; }
        public string AttCheckOut { get; set; }
        public string Isleave { get; set; }
        public string IsAbsent { get; set; }
        public string IsWeekend { get; set; }
        public string IsTraining { get; set; }
        public string IsHoliday { get; set; }

        public string IsOfficialVisit { get; set; }
        public int? DsgId { get; set; }
        public string DsgName { get; set; }
        public string OfficeName { get; set; }
        public string Worked_Hour { get; set; }
        public string ShiftName { get; set; }
        public string GroupName { get; set; }

        public int? officeid { get; set; }
        public int? dprtid { get; set; }
        public int? emptypeid { get; set; }
        public DateTime searchdate { get; set; }
        public string searchdateNP { get; set; }

        public string LateEntry { get; set; }

        public string EarlyExit { get; set; }

        public string ShiftDelayAllow { get; set; }
        public string AttShiftStart { get; set; }
        public string AttShiftEnd { get; set; }

        public string Level { get; set; }
        public string Department { get; set; }

        public DateTime startdate { get; set; }
        public string startdateNP { get; set; }
        public DateTime Enddate { get; set; }
        public string EnddateNP { get; set; }
        public string Attid { get; set; }

        public int EmpSearchCode { get; set; }
        public int AdminEmpCode { get; set; }
    }
}
