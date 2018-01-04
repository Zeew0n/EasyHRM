namespace BeeHRM.ApplicationService.ViewModel
{
    public class LeaveYearlyReportViewModel
    {
        public decimal TotalLeaveDays { get; set; }
        public decimal BalanceDays { get; set; }
        public int EmpCode { get; set; }
        public decimal TotalTaken { get; set; }
        public int LeaveYearId { get; set; }
        public int LeaveTypeId { get; set; }

        public string EmpName { get; set; }
        public string Designation { get; set; }
        public string OfficeName { get; set; }
        public decimal PrevYearBalance { get; set; }
        public decimal ThisYearEarned { get; set; }


    }

    public class MultipleLeaveYearlyReportViewModel
    {

        public int EmpCode { get; set; }
        public decimal H_TotalLeaveDays { get; set; }
        public decimal H_PrevYearLeaveDays { get; set; }
        public decimal H_ThisYearLeaveDays { get; set; }
        public decimal H_BalanceDays { get; set; }

        public decimal H_TotalTaken { get; set; }

        public decimal S_TotalLeaveDays { get; set; }
        public decimal S_BalanceDays { get; set; }

        public decimal S_TotalTaken { get; set; }


        public decimal Ca_TotalLeaveDays { get; set; }
        public decimal Ca_BalanceDays { get; set; }

        public decimal Ca_TotalTaken { get; set; }

        public decimal Co_TotalLeaveDays { get; set; }
        public decimal Co_BalanceDays { get; set; }

        public decimal Co_TotalTaken { get; set; }


        public string EmpName { get; set; }
        public string OfficeName { get; set; }
        public string DesgName { get; set; }


    }
}
