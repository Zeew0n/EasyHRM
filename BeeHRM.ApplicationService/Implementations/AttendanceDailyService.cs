using BeeHRM.ApplicationService.ConnectDb;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.ApplicationService.ViewModel;
using BeeHRM.Repository;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BeeHRM.ApplicationService.Implementations
{
    public class AttendanceDailyService : IAttendanceDailyService
    {
        private readonly IUnitOfWork _unitOfWork;


        public AttendanceDailyService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }
        #region Daily AttendanceFilter
        public IEnumerable<DailyAttendanceFilterViewModel> GetAttendanceDaily(int? EmpOfficeId, int? EmpDesgId, int? BgId, int? EmpDeptId, int? EmpCode, DateTime StrtDate)
        {


            // List<DailyAttendanceFilterViewModel> attlist = new List<DailyAttendanceFilterViewModel>();
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_AttendanceDaily", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@StartDate", StrtDate);
            cmd.Parameters.AddWithValue("@OfficeID", EmpOfficeId);
            cmd.Parameters.AddWithValue("@DegID", EmpDesgId);
            cmd.Parameters.AddWithValue("@EmpCode", EmpCode);
            cmd.Parameters.AddWithValue("@EmpTypeId", BgId);
            cmd.Parameters.AddWithValue("@DeptId", EmpDeptId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            foreach (DataRow row in dt.Rows)
            {
                yield return new DailyAttendanceFilterViewModel
                {
                    code = row["EmpCode"].ToString(),
                    AttDate = row["AttDate"].ToString(),
                    AttCheckIn = row["AttCheckIn"].ToString(),
                    AttCheckOut = row["AttCheckOut"].ToString(),
                    Isleave = row["IsLeave"].ToString(),
                    IsAbsent = row["IsAbsent"].ToString(),
                    IsWeekend = row["IsWeekend"].ToString(),
                    IsHoliday = row["IsHoliday"].ToString(),
                    IsOfficialVisit = row["IsOfficialVisit"].ToString(),
                    IsTraining = row["IsTraining"].ToString(),
                    EmpName = row["EmpName"].ToString(),
                    DsgName = row["DsgName"].ToString(),
                    OfficeName = row["OfficeName"].ToString(),
                    Worked_Hour = row["Worked_Hour"].ToString(),
                    ShiftName = row["ShiftName"].ToString(),
                    GroupName = row["GroupName"].ToString(),
                    ShiftDelayAllow = row["ShiftDelayAllow"].ToString(),
                    AttShiftEnd = row["AttShiftEnd"].ToString(),
                    AttShiftStart = row["AttShiftStart"].ToString()

                };


            }

        }
        #endregion

        private TimeSpan? FormatLateEntry(DateTime? date, TimeSpan? shiftStart)
        {
            TimeSpan ret = new TimeSpan();
            TimeSpan? checkInTime = date != null ? TimeSpan.Parse(date.Value.ToString("hh:mm:ss")) : ret;
            TimeSpan? lateTime = shiftStart - checkInTime;
            if (lateTime == TimeSpan.Parse("10:00:00"))
            {
                return TimeSpan.Parse("00:00:00");
            }
            return lateTime;
        }

        private TimeSpan? FormatEarlyExit(DateTime? checkOut, TimeSpan? shiftEnd)
        {
            TimeSpan? checkOutTime = checkOut != null ? TimeSpan.Parse(checkOut.Value.ToString("hh:mm:ss")) + TimeSpan.Parse("12:00:00") : new TimeSpan();
            TimeSpan? earlyExit = checkOutTime - shiftEnd;
            if (earlyExit == TimeSpan.Parse("-18:00:00"))
            {
                return TimeSpan.Parse("00:00:00");
            }
            return earlyExit;
        }

        public IEnumerable<AttendanceDailyDTO> GetDailyAttendance(int id)
        {
            IEnumerable<AttendaceDaily> att = _unitOfWork.AttendanceDailyRepository.Filter(x => x.AttEmpCode == id);
            var reatt = AttendanceDailyResponseFormatter.ModelData(att);
            return reatt;
        }

        //This function pulls the attendance based on two date ranges. If they are null, then it pulls out the 
        //attendance for the current month.
        public IEnumerable<DailyAttendanceFilterViewModel> GetAttendanceByRangeAndID(int Id, DateTime? startDate, DateTime? endDate)
        {
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            if (startDate == null && endDate == null)
            {
                SqlCommand cmd = new SqlCommand("sp_AttendanceDailyById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpCode", Id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();


                foreach (DataRow row in dt.Rows)
                {
                    yield return new DailyAttendanceFilterViewModel
                    {
                        code = row["EmpCode"].ToString(),
                        AttDate = row["AttDate"].ToString(),
                        AttCheckIn = row["AttCheckIn"].ToString(),
                        AttCheckOut = row["AttCheckOut"].ToString(),
                        Isleave = row["IsLeave"].ToString(),
                        IsAbsent = row["IsAbsent"].ToString(),
                        IsWeekend = row["IsWeekend"].ToString(),
                        IsHoliday = row["IsHoliday"].ToString(),
                        IsOfficialVisit = row["IsOfficialVisit"].ToString(),
                        IsTraining = row["IsTraining"].ToString(),
                        EmpName = row["EmpName"].ToString(),
                        DsgName = row["DsgName"].ToString(),
                        OfficeName = row["OfficeName"].ToString(),
                        Worked_Hour = row["Worked_Hour"].ToString(),
                        ShiftName = row["ShiftName"].ToString(),
                        GroupName = row["GroupName"].ToString(),
                        ShiftDelayAllow = row["ShiftDelayAllow"].ToString(),
                        AttShiftEnd = row["AttShiftEnd"].ToString(),
                        AttShiftStart = row["AttShiftStart"].ToString(),
                        Department = row["DeptName"].ToString(),
                        Level = row["LevelName"].ToString(),
                        Attid = row["AttId"].ToString()

                    };


                }

            }

            else
            {
                SqlCommand cmd = new SqlCommand("sp_AttendanceDailyByDateRange", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpCode", Id);
                cmd.Parameters.AddWithValue("@SearchStartdate", startDate);
                cmd.Parameters.AddWithValue("@SearchEnddate", endDate);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();


                foreach (DataRow row in dt.Rows)
                {
                    yield return new DailyAttendanceFilterViewModel
                    {
                        code = row["EmpCode"].ToString(),
                        AttDate = row["AttDate"].ToString(),
                        AttCheckIn = row["AttCheckIn"].ToString(),
                        AttCheckOut = row["AttCheckOut"].ToString(),
                        Isleave = row["IsLeave"].ToString(),
                        IsAbsent = row["IsAbsent"].ToString(),
                        IsWeekend = row["IsWeekend"].ToString(),
                        IsHoliday = row["IsHoliday"].ToString(),
                        IsTraining = row["IsTraining"].ToString(),
                        IsOfficialVisit = row["IsOfficialVisit"].ToString(),
                        EmpName = row["EmpName"].ToString(),
                        DsgName = row["DsgName"].ToString(),
                        OfficeName = row["OfficeName"].ToString(),
                        Worked_Hour = row["Worked_Hour"].ToString(),
                        ShiftName = row["ShiftName"].ToString(),
                        GroupName = row["GroupName"].ToString(),
                        ShiftDelayAllow = row["ShiftDelayAllow"].ToString(),
                        AttShiftEnd = row["AttShiftEnd"].ToString(),
                        AttShiftStart = row["AttShiftStart"].ToString(),
                        Department = row["DeptName"].ToString(),
                        Level = row["LevelName"].ToString()

                    };


                }

            }


            conn.Close();
            conn.Dispose();
        }

        public IEnumerable<AttEmployeeLogDTO> GetAttEmployeeLog(int id, DateTime date)
        {
            var attdaily = _unitOfWork.AttEmployeeLogRepository.All();
            IEnumerable<AttEmployeeLog> attemplog = _unitOfWork.AttEmployeeLogRepository.Get(x => x.logEmpCode == id && x.logDate == date);
            var loglist = AttEmployeeDailyLogResponseFormatter.ModelData(attemplog);
            return loglist;
        }

        public IEnumerable<DailyAttendanceFilterViewModel> GetAttendanceDailyStatus(int AdminEmpCode, DateTime date, int? code, int? degid, int? officeid)
        {

            var conn = DbConnectHelper.GetConnection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("sp_EmployeeDailyAttendanceStatus", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            DateTime date1 = DateTime.Now;
            cmd.Parameters.AddWithValue("@AdminEmpCode", AdminEmpCode);
            cmd.Parameters.AddWithValue("@Searchdate", date);
            cmd.Parameters.AddWithValue("@OfficeId", officeid);
            cmd.Parameters.AddWithValue("@DegID", degid);
            cmd.Parameters.AddWithValue("@EmpCode", code);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();


            foreach (DataRow row in dt.Rows)
            {
                yield return new DailyAttendanceFilterViewModel
                {
                    code = row["EmpCode"].ToString(),
                    AttDate = row["AttDate"].ToString(),
                    AttCheckIn = row["AttCheckIn"].ToString(),
                    AttCheckOut = row["AttCheckOut"].ToString(),
                    Isleave = row["IsLeave"].ToString(),
                    IsAbsent = row["IsAbsent"].ToString(),
                    IsWeekend = row["IsWeekend"].ToString(),
                    IsHoliday = row["IsHoliday"].ToString(),
                    IsTraining = row["IsTraining"].ToString(),
                    IsOfficialVisit = row["IsOfficialVisit"].ToString(),
                    EmpName = row["EmpName"].ToString(),
                    DsgName = row["DsgName"].ToString(),
                    OfficeName = row["OfficeName"].ToString(),
                    Worked_Hour = row["Worked_Hour"].ToString(),
                    ShiftDelayAllow = row["ShiftDelayAllow"].ToString(),
                    AttShiftEnd = row["AttShiftEnd"].ToString(),
                    AttShiftStart = row["AttShiftStart"].ToString()

                };


            }


            conn.Close();
            conn.Dispose();
        }

        public DataTable GetMonthlyAttendanceAll(DateTime sdate, DateTime enddate, int office)
        {

            try
            {
                SqlConnection conn = DbConnectHelper.GetConnection();
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_AttendancePivot", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@startdate", sdate);
                cmd.Parameters.AddWithValue("@enddate", enddate);
                cmd.Parameters.AddWithValue("@officeid", office.ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dt.Columns.Add("Present");
                dt.Columns.Add("Leave");
                dt.Columns.Add("Holiday");
                dt.Columns.Add("Weekend");
                dt.Columns.Add("LateEntry");
                dt.Columns.Add("EarlyExit");
                dt.Columns.Add("OfficialVisit");
                dt.Columns.Add("Training");


                SqlCommand cmd1 = new SqlCommand("sp_AttendanceTotalDaysReports", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@startdate", sdate);
                cmd1.Parameters.AddWithValue("@endate", enddate);
                cmd1.Parameters.AddWithValue("@officeId", office);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);



                SqlCommand cmd2 = new SqlCommand("sp_AttDate", conn);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@sdate", sdate);
                cmd2.Parameters.AddWithValue("@enddate", enddate);
                cmd2.Parameters.AddWithValue("@OfficeId", office);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);

                int iCount = dt1.Rows.Count;

                DateTime start = sdate;
                DateTime end = enddate;


                int x = dt1.Rows.Count;
                string[] present = new string[x];
                string[] leave = new string[x];
                string[] holiday = new string[x];
                string[] weekend = new string[x];
                string[] LateEntry = new string[x];
                string[] EarlyExit = new string[x];
                string[] OfficialVisit = new string[x];
                string[] training = new string[x];
                int i = 0;
                foreach (DataRow row in dt1.Rows)
                {

                    present[i] = Convert.ToString(row["present"]);
                    leave[i] = Convert.ToString(row["leave"]);
                    holiday[i] = Convert.ToString(row["holiday"]);
                    weekend[i] = Convert.ToString(row["weekend"]);
                    LateEntry[i] = Convert.ToString(row["LateEntry"]);
                    EarlyExit[i] = Convert.ToString(row["EarlyExit"]);
                    EarlyExit[i] = Convert.ToString(row["EarlyExit"]);
                    OfficialVisit[i] = Convert.ToString(row["OfficialVisit"]);
                    training[i] = Convert.ToString(row["Training"]);
                    i++;
                }
                //IEnumerable<AttendanceReportDateViewModel> atd=from 
                foreach (DataRow value in dt2.Rows)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        DateTime attdate = Convert.ToDateTime(value["ATTDATE"]);
                        string date = attdate.ToString("yyyy-MM-dd");
                        try
                        {
                            if (item[date].ToString() == "1")
                            {

                                SqlCommand cmd3 = new SqlCommand("sp_AttendanceStatusBydateEmpcode", conn);
                                cmd3.CommandType = CommandType.StoredProcedure;
                                cmd3.Parameters.AddWithValue("@date", date);
                                cmd3.Parameters.AddWithValue("@EmpCode", item["AttEmpCode"]);
                                SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                                DataTable dt3 = new DataTable();
                                da3.Fill(dt3);
                                foreach (DataRow test in dt3.Rows)
                                {
                                    item[date] = test["Empattendance"];
                                }

                            }
                            else

                            if (String.IsNullOrEmpty(item[date].ToString()))
                            {

                                SqlCommand cmd3 = new SqlCommand("sp_AttendanceStatusBydateEmpcode", conn);
                                cmd3.CommandType = CommandType.StoredProcedure;
                                cmd3.Parameters.AddWithValue("@date", date);
                                cmd3.Parameters.AddWithValue("@EmpCode", item["AttEmpCode"]);
                                SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                                DataTable dt3 = new DataTable();
                                da3.Fill(dt3);
                                foreach (DataRow test in dt3.Rows)
                                {
                                    item[date] = test["Empattendance"];
                                    if (String.IsNullOrEmpty(item[date].ToString()))
                                    {
                                        item[date] = "p";
                                    }
                                }


                            }
                            else
                            {
                                SqlCommand cmd3 = new SqlCommand("sp_AttendanceStatusBydateEmpcode", conn);
                                cmd3.CommandType = CommandType.StoredProcedure;
                                cmd3.Parameters.AddWithValue("@date", date);
                                cmd3.Parameters.AddWithValue("@EmpCode", item["AttEmpCode"]);
                                SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                                DataTable dt3 = new DataTable();
                                da3.Fill(dt3);
                                foreach (DataRow test in dt3.Rows)
                                {
                                    item[date] = "P " + test["Empattendance"].ToString();
                                }
                            }
                        }
                        catch (Exception Ex)
                        {
                            throw Ex.InnerException;
                        }



                        iCount++;

                    }



                }
                int j = 0;
                foreach (DataRow item in dt.Rows)
                {

                    item["Present"] = present[j];
                    item["Leave"] = leave[j];
                    item["Holiday"] = holiday[j];
                    item["Weekend"] = weekend[j];
                    item["LateEntry"] = LateEntry[j];
                    item["EarlyExit"] = EarlyExit[j];
                    item["OfficialVisit"] = OfficialVisit[j];
                    item["Training"] = training[j];
                    j++;
                }


                dt.AcceptChanges();


                da.Dispose();
                cmd1.Dispose();
                da1.Dispose();
                cmd2.Dispose();
                da2.Dispose();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            finally
            {

            }

        }

        public AttendanceRequestDTO InsertAttendanceRequest(AttendanceRequestDTO attenddances)
        {
            AttendanceRequest att = AttendanceRequestFormatter.ConvertRespondentInfoFromDTO(attenddances);

            return AttendanceRequestFormatter.ConvertRespondentToDTO(_unitOfWork.AttendanceRequestRepository.Create(att));
        }

        public IEnumerable<AttendanceRequestsListViewModel> GetRequestAttendanceList(int? empcode, int? approverid, int? recommenderid, int? requestid, int? recommendstatus)
        {
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_AttendanceRequestList", conn);
            cmd.Parameters.AddWithValue("@Empcode", empcode);
            cmd.Parameters.AddWithValue("@RecommenderId", recommenderid);
            cmd.Parameters.AddWithValue("@ApproverId", approverid);
            cmd.Parameters.AddWithValue("@RequestedId", requestid);
            cmd.Parameters.AddWithValue("@RecommendStatus", recommendstatus);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();


            foreach (DataRow row in dt.Rows)
            {
                yield return new AttendanceRequestsListViewModel
                {
                    EmpCode = row["RequestEmpCode"].ToString(),
                    RequestId = row["RequestId"].ToString(),
                    EmpName = row["EmpName"].ToString(),
                    Description = row["RequestDescription"].ToString(),
                    ApproveDate = row["ApproveStatusDate"].ToString(),
                    ApproveStatus = row["ApproveStatus"].ToString(),
                    RecommendeDate = row["RecommendStatusDate"].ToString(),
                    RecommendStatus = row["RecommendStatus"].ToString(),
                    Designation = row["DsgName"].ToString(),
                    RequestDate = row["RequestedDate"].ToString(),
                    RequestType = row["RequestType"].ToString(),
                    ApproveMessage = row["ApproverMessage"].ToString(),
                    RecommendMessage = row["RecommedarMessage"].ToString(),
                    CheckIn_Date = row["CheckInDate"].ToString(),
                    CheckOut_Date = row["CheckOutDate"].ToString(),
                    RecommenderEmpCode = row["RecommendarEmpCode"].ToString(),
                    ApproverEmpCode = row["ApproverEmpCode"].ToString(),
                    EmpEmail = row["EmpEmail"].ToString(),
                    JoinDate = row["EmpAppointmentDate"].ToString(),
                    Recommender = row["Recommender"].ToString(),
                    Approver = row["Approver"].ToString(),
                    IpAddress = row["RequestIPAddress"].ToString(),
                    ApproverStatus = Convert.ToInt32(row["ApproveStatus"].ToString())
                };
                conn.Close();
                conn.Dispose();

            }

        }

        public IEnumerable<AttendanceRequestsListViewModel> GetrequestAttendanceListByParms(int? officeid, int? empcode, DateTime startdate, DateTime enddate, int? reccode, int? appcode, int? recomendstatus, int? approvestatus)
        {
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_AttendanceRequestListByParms", conn);
            cmd.Parameters.AddWithValue("@StartDate", startdate);
            cmd.Parameters.AddWithValue("@EndDate", enddate);
            cmd.Parameters.AddWithValue("@OfficeId", officeid);
            cmd.Parameters.AddWithValue("@EmpCode", empcode);
            cmd.Parameters.AddWithValue("@ApproverCode", appcode);
            cmd.Parameters.AddWithValue("@RecEmpCode", reccode);
            cmd.Parameters.AddWithValue("@Recommendstatus", recomendstatus);
            cmd.Parameters.AddWithValue("@approvestatus", approvestatus);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();


            foreach (DataRow row in dt.Rows)
            {
                yield return new AttendanceRequestsListViewModel
                {
                    EmpCode = row["RequestEmpCode"].ToString(),
                    RequestId = row["RequestId"].ToString(),
                    EmpName = row["EmpName"].ToString(),
                    Description = row["RequestDescription"].ToString(),
                    ApproveDate = row["ApproveStatusDate"].ToString(),
                    ApproveStatus = row["ApproveStatus"].ToString(),
                    RecommendeDate = row["RecommendStatusDate"].ToString(),
                    RecommendStatus = row["RecommendStatus"].ToString(),
                    Designation = row["DsgName"].ToString(),
                    RequestDate = row["RequestedDate"].ToString(),
                    RequestType = row["RequestType"].ToString(),
                    ApproveMessage = row["ApproverMessage"].ToString(),
                    RecommendMessage = row["RecommedarMessage"].ToString(),
                    CheckIn_Date = row["CheckInDate"].ToString(),
                    CheckOut_Date = row["CheckOutDate"].ToString(),
                    RecommenderEmpCode = row["RecommendarEmpCode"].ToString(),
                    ApproverEmpCode = row["ApproverEmpCode"].ToString()

                };
                conn.Close();
                conn.Dispose();

            }

        }

        public int UpdateAttendanceRequest(AttendanceRequestDTO attd)
        {
            AttendanceRequest att = AttendanceRequestFormatter.ConvertRespondentInfoFromDTO(attd);

            return (_unitOfWork.AttendanceRequestRepository.Update(att));
        }

        public void DeleteAttendancerequest(int id)
        {


            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_deleteAttendance", conn);
            cmd.Parameters.AddWithValue("@requestId", id);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
        }

        public void GetenareDailyAttendance(int requestid)
        {
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_AttendanceRequestUpdate ", conn);
            cmd.Parameters.AddWithValue("@RequestID", requestid);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
        }

        public int UpdateIndividualAttendanceDaily(AttendanceDailyViewModel atd)
        {
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_updateDailyattendance", conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@checkIn", atd.AttCheckIn);
            cmd.Parameters.AddWithValue("@CheckOut", atd.AttCheckOut);
            cmd.Parameters.AddWithValue("@IsAbsent", atd.IsAbsent);
            cmd.Parameters.AddWithValue("@empcode", atd.EmpCode);
            cmd.Parameters.AddWithValue("@Date", atd.AttDate);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            return 0;
        }

        public int RejectApprovedAttendance(AttendanceRequestDTO atd)
        {

            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_AttendanceRequestReject ", conn);
            cmd.Parameters.AddWithValue("@RequestID", atd.RequestId);
            cmd.Parameters.AddWithValue("@ApproveMessage", atd.ApproverMessage);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            return 0;
        }

        public DataTable AttendanceTotalDaysSummary(DateTime sdate, DateTime enddate, int office)
        {
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_AttendanceTotalDaysReports", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@startdate", sdate);
            cmd.Parameters.AddWithValue("@endate", enddate);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            return dt;
        }

        public int InsertKaajAttendance(int Empcode, string sdate, string enddate, string type)
        {
            try
            {
                SqlConnection conn = DbConnectHelper.GetConnection();
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_Insertkaajattendance", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@startdate", sdate);
                cmd.Parameters.AddWithValue("@Empcode", Empcode);
                cmd.Parameters.AddWithValue("@enddate", enddate);
                cmd.Parameters.AddWithValue("@type", type);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }



        }
    }
}

