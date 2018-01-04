using BeeHRM.ApplicationService.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IReportsServices
    {
        List<int> GetYearList();
        List<SelectListItem> NepaliMonthList();

        void GetStartAndEndDate(int month, int year, out string StartDate, out string EndDate);
        DataTable AttendanceMonthlySummary(DateTime sdate, DateTime enddate, int office);

        DataTable AttendanceLeaveReports(DateTime sdate, DateTime enddate, int office);

        AttendanceEntireYearViewModel AttendanceEntireYearReport(int Empcode, int FiscalYear, int NepaliMonth);
        AttendanceEntireYearSummaryViewModel AttendanceEntireYearReportSummary(int Empcode, int FiscalYear, int NepaliMonth);
    }
}
