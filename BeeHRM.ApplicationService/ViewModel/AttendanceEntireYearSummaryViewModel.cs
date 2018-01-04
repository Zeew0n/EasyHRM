namespace BeeHRM.ApplicationService.ViewModel
{
    public class AttendanceEntireYearSummaryViewModel
    {
        public int empcode { get; set; }
        public int totalDays { get; set; }
        public int weekend { get; set; }
        public int holiday { get; set; }
        public int leave { get; set; }

        public int latentry { get; set; }
        public int earlyexit { get; set; }
        public int officialvisit { get; set; }
        public int training { get; set; }
        public int totalAbsent { get; set; }
        public int presentdays { get; set; }

    }
}
