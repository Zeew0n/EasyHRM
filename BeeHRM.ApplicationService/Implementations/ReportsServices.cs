using BeeHRM.ApplicationService.ConnectDb;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Implementations
{
    public class ReportsServices : IReportsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILeaveTypeService _leave;

        public ReportsServices(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
            _leave = new LeaveTypeService();
        }

        public DataTable AttendanceLeaveReports(DateTime sdate, DateTime enddate, int office)
        {
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_AttendanceLeaveReports", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StartDate", sdate);
            cmd.Parameters.AddWithValue("@EndDate", enddate);
            cmd.Parameters.AddWithValue("@officeId", office);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            return dt;
        }

        public DataTable AttendanceMonthlySummary(DateTime sdate, DateTime enddate, int office)
        {

            try
            {
                SqlConnection conn = DbConnectHelper.GetConnection();
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_AttendanceTotalDaysReportsSummary", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@startdate", sdate);
                cmd.Parameters.AddWithValue("@endate", enddate);
                cmd.Parameters.AddWithValue("@officeId", office);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);



                SqlCommand cmd1 = new SqlCommand("sp_AttendanceLeaveReports", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@StartDate", sdate);
                cmd1.Parameters.AddWithValue("@EndDate", enddate);
                cmd1.Parameters.AddWithValue("@officeId", office);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                IEnumerable<LeaveTypeDTO> ltd = _leave.GetLeaveTypes();
                foreach (var row in ltd)
                {
                    dt.Columns.Add(row.LeaveTypeName);
                }
                int x = dt1.Rows.Count;
                string[] array = new string[x];
                int i = 0;

                foreach (var row in ltd)
                {
                    foreach (DataRow item in dt1.Rows)
                    {
                        array[i] = item[row.LeaveTypeName].ToString();
                        i++;
                    }
                    i = 0;
                    foreach (DataRow item in dt.Rows)
                    {
                        if (string.IsNullOrEmpty(array[i]))
                        {
                            array[i] = 0.ToString();
                        }
                        item[row.LeaveTypeName] = array[i];
                        i++;
                    }

                    i = 0;
                }




                da.Dispose();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();

                return dt;
            }
            catch (Exception Mg)
            {
                return null;
            }


        }

        public void GetStartAndEndDate(int month, int year, out string StartDate, out string EndDate)
        {
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_GetStartAndEndDate", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@month", month);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            string sdate = null;
            string edate = null;

            foreach (DataRow row in dt.Rows)
            {

                sdate = row["eng_start_date"].ToString();
                edate = row["eng_end_date"].ToString();
            }
            da.Dispose();
            cmd.Dispose();
            StartDate = sdate;
            EndDate = edate;
            conn.Close();
            conn.Dispose();
        }

        public List<int> GetYearList()
        {
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_NepaliFiscalYearList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<int> Year = new List<int>();
            foreach (DataRow item in dt.Rows)
            {
                Year.Add(Convert.ToInt32(item["nepali_year"]));
            }
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            return Year;
        }

        public List<SelectListItem> NepaliMonthList()
        {
            List<SelectListItem> MonthList = new List<SelectListItem>();


            MonthList.Add(new SelectListItem
            {
                Text = "बैशाख",
                Value = "1"
            });
            MonthList.Add(new SelectListItem
            {
                Text = "जेठ",
                Value = "2"
            });
            MonthList.Add(new SelectListItem
            {
                Text = "आषाढ",
                Value = "3"
            });
            MonthList.Add(new SelectListItem
            {
                Text = "साउन",
                Value = "4"
            });
            MonthList.Add(new SelectListItem
            {
                Text = "भाद्र",
                Value = "5"
            });
            MonthList.Add(new SelectListItem
            {
                Text = "आश्विन",
                Value = "06"
            });
            MonthList.Add(new SelectListItem
            {
                Text = "कार्तिक",
                Value = "07"
            });
            MonthList.Add(new SelectListItem
            {
                Text = "मंसिर",
                Value = "08"
            });
            MonthList.Add(new SelectListItem
            {
                Text = "पौष",
                Value = "09"
            });
            MonthList.Add(new SelectListItem
            {
                Text = "माघ",
                Value = "10"
            });
            MonthList.Add(new SelectListItem
            {
                Text = "फाल्गुण",
                Value = "11"
            });
            MonthList.Add(new SelectListItem
            {
                Text = "चैत्र",
                Value = "12"
            });

            return MonthList;
        }

        public AttendanceEntireYearViewModel AttendanceEntireYearReport(int Empcode, int FiscalYear, int NepaliMonth)
        {


            AttendanceEntireYearViewModel EntireData = new AttendanceEntireYearViewModel();




            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_AttendanceEntireYearReport", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FiscalYear", FiscalYear);
            cmd.Parameters.AddWithValue("@NepaliMonth", NepaliMonth);
            cmd.Parameters.AddWithValue("@Empcode", Empcode);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            string sdate = null;
            string edate = null;

            List<AttendanceDailyDetails> DailyData = new List<AttendanceDailyDetails>();
            foreach (DataRow row in dt.Rows)
            {

                // sdate = row["eng_start_date"].ToString();
                // edate = row["eng_end_date"].ToString();

                //EntireData.Empcode = Convert.ToInt32(row["AttEmpCode"].ToString());

                AttendanceDailyDetails single = new AttendanceDailyDetails();
                var attstatus = "";
                single.AttDate = Convert.ToDateTime(row["AttDate"].ToString());
                single.CheckInTime = row["AttCheckIn"].ToString();
                single.CheckOutTime = row["AttCheckOut"].ToString();
                single.Holiday = (row["IsHoliday"].ToString() == "") ? "0" : row["IsHoliday"].ToString();
                single.Weekend = (row["IsWeekend"].ToString() == "") ? "0" : row["IsWeekend"].ToString();
                single.Leave = (row["IsLeave"].ToString() == "") ? "0" : row["IsLeave"].ToString();
                single.Training = (row["IsTraining"].ToString() == "") ? "0" : row["IsTraining"].ToString();
                single.OfficialVisit = (row["IsOfficialVisit"].ToString() == "") ? "0" : row["IsOfficialVisit"].ToString();
                single.CheckAbsent = row["CheckAbsent"].ToString();
                single.Isabsent = (row["Isabsent"].ToString() == "") ? "0" : row["Isabsent"].ToString();
                single.LeaveTypeName = (row["leaveTpeName"].ToString() == "") ? "" : row["leaveTpeName"].ToString();
                if (Convert.ToInt32(single.Holiday) > 0)
                {
                    attstatus = "H";
                }
                if (Convert.ToBoolean(single.Weekend))
                {
                    attstatus = attstatus + "W";
                }
                if (Convert.ToInt32(single.Leave) > 0)
                {
                    attstatus = attstatus + "L";
                }
                if (Convert.ToBoolean(single.Training))
                {
                    attstatus = attstatus + "T";
                }
                if (Convert.ToInt32(single.OfficialVisit) > 0)
                {
                    attstatus = attstatus + "K";
                }
                if (Convert.ToBoolean(single.Isabsent) == true && single.CheckAbsent == "0")
                {
                    attstatus = "A";
                }
                single.AttStatus = attstatus;



                DailyData.Add(single);


            }
            EntireData.DailyData = DailyData;

            da.Dispose();
            cmd.Dispose();

            conn.Close();
            conn.Dispose();






            return EntireData;
        }


        public AttendanceEntireYearSummaryViewModel AttendanceEntireYearReportSummary(int Empcode, int FiscalYear, int NepaliMonth)
        {

            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_AttendanceEntireYearReportSummary", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FiscalYear", FiscalYear);
            cmd.Parameters.AddWithValue("@NepaliMonth", NepaliMonth);
            cmd.Parameters.AddWithValue("@Empcode", Empcode);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            AttendanceEntireYearSummaryViewModel monthlyData = new AttendanceEntireYearSummaryViewModel();


            foreach (DataRow row in dt.Rows)
            {

                monthlyData.earlyexit = Convert.ToInt32(row["EarlyExit"].ToString());
                monthlyData.holiday = Convert.ToInt32(row["Holiday"].ToString());
                monthlyData.latentry = Convert.ToInt32(row["LateEntry"].ToString());
                monthlyData.leave = Convert.ToInt32(row["Leave"].ToString());
                monthlyData.officialvisit = Convert.ToInt32(row["OfficialVisit"].ToString());
                monthlyData.training = Convert.ToInt32(row["Training"].ToString());
                monthlyData.weekend = Convert.ToInt32(row["Weekend"].ToString());
                monthlyData.totalDays = Convert.ToInt32(row["Totaldays"].ToString());
                monthlyData.totalAbsent = Convert.ToInt32(row["TotalAbsent"].ToString());
                monthlyData.presentdays = Convert.ToInt32(row["presentdays"].ToString());

            }
            return monthlyData;
        }

    }
}
