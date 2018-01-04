using BeeHRM.ApplicationService.ConnectDb;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.ApplicationService.ViewModel;
using BeeHRM.Repository;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.StoredProcedureVariable;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Implementations
{
    public class LeaveApplicationService : ILeaveApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ILeaveService _leaveService;
        public LeaveApplicationService(IUnitOfWork unitOfWork = null, ILeaveService leaveService = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
            _leaveService = leaveService ?? new LeaveService();
        }
        public IEnumerable<LeaveApplicationViewModel> GetLeaveApplicationsList(int empCode, int? monthID, int? year)
        {
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_LeaveApplicationsUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@empCode", empCode);
            cmd.Parameters.AddWithValue("@monthId", monthID);
            cmd.Parameters.AddWithValue("@Year", year);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            foreach (DataRow dr in dt.Rows)
            {
                yield return new LeaveApplicationViewModel
                {
                    LeaveId = Convert.ToInt32(dr["LeaveId"].ToString()),
                    LeaveStatus = Convert.ToInt32(dr["LeaveStatus"].ToString()),
                    LeaveStartDate = Convert.ToDateTime(dr["LeaveStartDate"].ToString()),
                    LeaveEndDate = Convert.ToDateTime(dr["LeaveEndDate"]),
                    LeaveSubject = dr["LeaveSubject"].ToString(),
                    LeaveAppliedDate = Convert.ToDateTime(dr["LeaveAppliedDate"].ToString()),
                    LeaveYearName = dr["YearName"].ToString(),
                    LeaveTypeName = dr["LeaveTypeName"].ToString(),
                    RecommenderName = dr["RecommendorName"].ToString(),
                    ApproverName = dr["ApproverName"].ToString(),
                    RecommendStatus = Convert.ToInt32(dr["RecommendStatus"].ToString())
                };
            }
        }
        public IEnumerable<LeaveApplicationViewModel> GetLeaveApplicationsListAdmin(int Empcode, int? monthID, int? year)
        {
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("[sp_LeaveApplicationsAdmin]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@monthId", monthID);
            cmd.Parameters.AddWithValue("@EmpCode", Empcode);
            cmd.Parameters.AddWithValue("@Year", year);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            foreach (DataRow dr in dt.Rows)
            {
                yield return new LeaveApplicationViewModel
                {
                    LeaveId = Convert.ToInt32(dr["LeaveId"].ToString()),
                    LeaveStatus = Convert.ToInt32(dr["LeaveStatus"].ToString()),
                    LeaveStartDate = Convert.ToDateTime(dr["LeaveStartDate"].ToString()),
                    LeaveEndDate = Convert.ToDateTime(dr["LeaveEndDate"]),
                    LeaveSubject = dr["LeaveSubject"].ToString(),
                    LeaveAppliedDate = Convert.ToDateTime(dr["LeaveAppliedDate"].ToString()),
                    LeaveYearName = dr["YearName"].ToString(),
                    LeaveTypeName = dr["LeaveTypeName"].ToString(),
                    RecommenderName = dr["RecommendorName"].ToString(),
                    ApproverName = dr["ApproverName"].ToString(),
                    RecommendStatus = Convert.ToInt32(dr["RecommendStatus"].ToString()),
                    EmpName = dr["EmpName"].ToString()
                };
            }
        }
        public IEnumerable<YearViewModel> GetLeaveApplicationsYear()
        {
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_LeaveApplicationYearUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            foreach (DataRow dr in dt.Rows)
            {
                yield return new YearViewModel
                {
                    YearName = Convert.ToInt32(dr["YearName"].ToString()),
                };
            }
        }
        public IEnumerable<LeaveStatViewModel> GetLeaveStatus(int empCode, int leaveYearId)
        {
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_LeaveApplicationLeaveRemTable", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpCode", empCode);
            cmd.Parameters.AddWithValue("@LeaveYearId", leaveYearId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            foreach (DataRow dr in dt.Rows)
            {
                yield return new LeaveStatViewModel
                {

                    EmpCode = dr["EmpCode"].ToString() != null ? Convert.ToInt32(dr["EmpCode"].ToString()) : 0,
                    EmpLeaveRuleId = dr["EmpLeaveRuleId"].ToString() != "" ? Convert.ToInt32(dr["EmpLeaveRuleId"].ToString()) : 0,
                    LeaveTypeId = dr["LeaveTypeId"].ToString() != null ? Convert.ToInt32(dr["LeaveTypeId"].ToString()) : 0,
                    LeaveDays = dr["LeaveDays"].ToString() != "" ? Convert.ToDecimal(dr["LeaveDays"]) : 0,
                    LeaveTypeName = dr["LeaveTypeName"].ToString(),
                    LeaveApplyBeforeDays = dr["LeaveApplyBefore"].ToString() != "" ? Convert.ToInt32(dr["LeaveApplyBefore"].ToString()) : 0,
                    MonthlyQuantity = dr["MonthlyQty"].ToString() != "" ? Convert.ToInt32(dr["MonthlyQty"].ToString()) : 0,
                    TotalLeaveTakenDays = dr["TOTAL_DAYS"].ToString() != null ? Convert.ToDecimal(dr["TOTAL_DAYS"].ToString()) : 0,
                    TotalRemainingDays = dr["REM_DAYS"].ToString() != null ? Convert.ToDecimal(dr["REM_DAYS"].ToString()) : 0,
                    IsPayable = Convert.ToBoolean(dr["IsPayable"].ToString()),
                    LeaveYearId = Convert.ToInt32(dr["AssignedLeaveYearId"].ToString()),
                    LeaveAssiginId = Convert.ToInt32(dr["AssignedId"].ToString())

                };
            }
        }
        public IEnumerable<LeaveTypeDTO> GetValidLeaveTypes(int empCode)
        {
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("[sp_LeaveApplicationValidLeaveType]", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpCode", empCode);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            foreach (DataRow dr in dt.Rows)
            {
                yield return new LeaveTypeDTO
                {
                    LeaveTypeId = Convert.ToInt32(dr["LeaveTypeId"].ToString()),
                    LeaveTypeName = dr["LeaveTypeName"].ToString()
                };
            }
        }
        public IEnumerable<EmployeeDTO> GetRecommenderList(int empCode)
        {
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("[sp_Myrecommender]", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpCode", empCode);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            foreach (DataRow dr in dt.Rows)
            {
                yield return new EmployeeDTO
                {
                    EmpCode = Convert.ToInt32(dr["EmpCode"].ToString()),
                    EmpName = dr["EmpName"].ToString()
                };
            }
        }
        public IEnumerable<EmployeeDTO> GetApproverList(int empCode)
        {
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("[sp_MyApprover]", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpCode", empCode);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            foreach (DataRow dr in dt.Rows)
            {
                yield return new EmployeeDTO
                {
                    EmpCode = Convert.ToInt32(dr["EmpCode"].ToString()),
                    EmpName = dr["EmpName"].ToString()
                };
            }
        }
        public LeaveYearDTO GetActiveYear()
        {
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_GetActiveYear", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            return new LeaveYearDTO
            {
                YearStartDate = Convert.ToDateTime(dt.Rows[0]["YearStartDate"].ToString()),
                YearEndDate = Convert.ToDateTime(dt.Rows[0]["YearEndDate"].ToString()),
                YearId = Convert.ToInt32(dt.Rows[0]["YearId"].ToString()),
                YearEndDateNp = dt.Rows[0]["YearEndDateNp"].ToString(),
                YearName = Convert.ToInt32(dt.Rows[0]["YearName"]),
                YearStartDateNp = dt.Rows[0]["YearStartDateNp"].ToString()
            };

        }
        public int GetLeaveYearId(int? year)
        {
            string first = year.ToString();
            int second = Convert.ToInt32(year + 1) - 2000;
            int? yearname = Convert.ToInt32(first);
            IEnumerable<LeaveYear> lvy = _unitOfWork.LeaveYearRepository.All().Where(x => x.YearName == yearname);
            int id = lvy.FirstOrDefault().YearId;
            return id;
        }
        public LeaveApplicationDTO InsertLeaveApplication(LeaveApplicationDTO newLeave)
        {
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_LeaveApplicationInsert", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LeaveEmpCode", newLeave.LeaveEmpCode);
            cmd.Parameters.AddWithValue("@LeaveYearId", newLeave.LeaveYearId);
            cmd.Parameters.AddWithValue("@LeaveTypeId", newLeave.LeaveTypeId);
            cmd.Parameters.AddWithValue("@RecommendedEmpCode", newLeave.RecommededEmpCode);
            cmd.Parameters.AddWithValue("@RecommendStatus", newLeave.RecommendStatus);
            cmd.Parameters.AddWithValue("@LeaveApproverEmpCode", newLeave.LeaveApproverEmpCode);
            cmd.Parameters.AddWithValue("@LeaveStatus", newLeave.LeaveStatus);
            cmd.Parameters.AddWithValue("@LeaveStartDate", newLeave.LeaveStartDate);
            cmd.Parameters.AddWithValue("@LeaveEndDate", newLeave.LeaveEndDate);
            cmd.Parameters.AddWithValue("@LeaveDays", newLeave.LeaveDays);
            cmd.Parameters.AddWithValue("@PaidLeave", newLeave.PaidLeave);
            cmd.Parameters.AddWithValue("@LeaveSubject", newLeave.LeaveSubject);
            cmd.Parameters.AddWithValue("@LeaveDetails", newLeave.LeaveDetails);
            cmd.Parameters.AddWithValue("@LeaveAppliedDate", newLeave.LeaveAppliedDate);
            cmd.Parameters.AddWithValue("@LeaveDaysType", newLeave.LeaveDaysType);
            cmd.Parameters.AddWithValue("@LeaveDaysPart", newLeave.LeaveDaysPart);
            cmd.Parameters.AddWithValue("@LeaveGUICode", newLeave.LeaveGUICode);
            cmd.Parameters.AddWithValue("@RecommenderMessage", newLeave.RecommenderMessage);
            cmd.Parameters.AddWithValue("@RecommendStatusDate", newLeave.RecommendStatusDate);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            //if (dt.Rows.Count > 0)
            //{
            //    return new LeaveApplicationDTO
            //    {
            //        LeaveId = Convert.ToInt32(dt.Rows[0]["LeaveId"].ToString()),
            //        LeaveEmpCode = Convert.ToInt32(dt.Rows[0]["LeaveEmpCode"].ToString()),
            //        LeaveYearId = Convert.ToInt32(dt.Rows[0]["LeaveYearId"].ToString()),
            //        LeaveTypeId = Convert.ToInt32(dt.Rows[0]["LeaveTypeId"].ToString()),
            //        RecommededEmpCode = Convert.ToInt32(dt.Rows[0]["RecommededEmpCode"].ToString()),
            //        RecommendStatus = Convert.ToByte(dt.Rows[0]["RecommendStatus"].ToString()),
            //        LeaveApproverEmpCode = Convert.ToInt32(dt.Rows[0]["LeaveApproverEmpCode"].ToString()),
            //        LeaveStatus = Convert.ToByte(dt.Rows[0]["LeaveId"].ToString()),
            //        LeaveStartDate = Convert.ToDateTime(dt.Rows[0]["LeaveStartDate"].ToString()),
            //        LeaveEndDate = Convert.ToDateTime(dt.Rows[0]["LeaveEndDate"].ToString()),
            //        LeaveDays = Convert.ToDecimal(dt.Rows[0]["LeaveDays"].ToString()),
            //        PaidLeave = Convert.ToBoolean(dt.Rows[0]["PaidLeave"].ToString()),
            //        LeaveSubject = dt.Rows[0]["LeaveSubject"].ToString(),
            //        LeaveDetails = dt.Rows[0]["LeaveDetails"].ToString(),
            //        LeaveAppliedDate = Convert.ToDateTime(dt.Rows[0]["LeaveAppliedDate"].ToString()),
            //        LeaveDaysType = dt.Rows[0]["LeaveDaysType"].ToString(),
            //        LeaveDaysPart = dt.Rows[0]["LeaveDaysPart"].ToString(),
            //        LeaveGUICode = dt.Rows[0]["LeaveGUICode"].ToString(),
            //        RecommenderMessage = dt.Rows[0]["RecommenderMessage"].ToString(),
            //        RecommendStatusDate = Convert.ToDateTime(dt.Rows[0]["RecommendStatusDate"].ToString())
            //    };
            //}
            //else
            //{
            return new LeaveApplicationDTO { };
            //}

        }
        public LeaveApplicationDTO GetLeaveApplicationById(int empCode, int leaveId)
        {
            LeaveApplicationDTO leave = LeaveApplicationRequestFormatter
                .ConvertRespondentInfoToDTO(_unitOfWork.LeaveApplicationRepository
                .Get(x => x.LeaveEmpCode == empCode && x.LeaveId == leaveId).FirstOrDefault());
            return leave;
        }
        public int DeleteLeaveApplication(int empCode, int leaveApplicationId)
        {
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_LeaveApplicationDelete", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpCode", empCode);
            cmd.Parameters.AddWithValue("@LeaveIdy", leaveApplicationId);


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            return 1;

        }
        public IEnumerable<LeaveApplicationViewModel> GetLeavesByRecommenderId(int recId, int? monthID, int? year)
        {
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("[sp_LeaveApplicationsRecommend]", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@empCode", recId);
            cmd.Parameters.AddWithValue("@monthId", monthID);
            cmd.Parameters.AddWithValue("@Year", year);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            foreach (DataRow dr in dt.Rows)
            {
                yield return new LeaveApplicationViewModel
                {
                    LeaveId = Convert.ToInt32(dr["LeaveId"].ToString()),
                    LeaveStatus = Convert.ToInt32(dr["LeaveStatus"].ToString()),
                    LeaveStartDate = Convert.ToDateTime(dr["LeaveStartDate"].ToString()),
                    LeaveEndDate = Convert.ToDateTime(dr["LeaveEndDate"]),
                    LeaveSubject = dr["LeaveSubject"].ToString(),
                    LeaveAppliedDate = Convert.ToDateTime(dr["LeaveAppliedDate"].ToString()),
                    LeaveYearName = dr["YearName"].ToString(),
                    LeaveTypeName = dr["LeaveTypeName"].ToString(),
                    RecommenderName = dr["RecommendorName"].ToString(),
                    ApproverName = dr["ApproverName"].ToString(),
                    LeaveEmpCode = Convert.ToInt32(dr["LeaveEmpCode"].ToString()),
                    AppliedBy = dr["AppliedBy"].ToString(),
                    RecommendStatus = Convert.ToInt32(dr["RecommendStatus"].ToString())
                };
            }
        }
        public LeaveApplicationViewModel GetRecommendedLeaveDetail(int empCode, int leaveId)
        {
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("[sp_LeaveApplicationDetailById]", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpCode", empCode);
            cmd.Parameters.AddWithValue("@LeaveId", leaveId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            if (dt.Rows.Count != 0)
            {
                return new LeaveApplicationViewModel
                {
                    LeaveId = Convert.ToInt32(dt.Rows[0]["LeaveId"].ToString()),
                    LeaveStatus = Convert.ToInt32(dt.Rows[0]["LeaveStatus"].ToString()),
                    LeaveStartDate = Convert.ToDateTime(dt.Rows[0]["LeaveStartDate"].ToString()),
                    LeaveEndDate = Convert.ToDateTime(dt.Rows[0]["LeaveEndDate"]),
                    LeaveSubject = dt.Rows[0]["LeaveSubject"].ToString(),
                    LeaveAppliedDate = Convert.ToDateTime(dt.Rows[0]["LeaveAppliedDate"].ToString()),
                    LeaveYearName = dt.Rows[0]["YearName"].ToString(),
                    LeaveTypeName = dt.Rows[0]["LeaveTypeName"].ToString(),
                    RecommenderName = dt.Rows[0]["RecommendorName"].ToString(),
                    ApproverName = dt.Rows[0]["ApproverName"].ToString(),
                    LeaveEmpCode = Convert.ToInt32(dt.Rows[0]["LeaveEmpCode"].ToString()),
                    LeaveDays = Convert.ToDecimal(dt.Rows[0]["LeaveDays"]),
                    AppliedBy = dt.Rows[0]["AppliedBy"].ToString(),
                    IsValid = Convert.ToInt32(dt.Rows[0]["IsValid"].ToString()),
                    RecommendMessage = dt.Rows[0]["RecommenderMessage"].ToString(),
                    ApproveMessage = dt.Rows[0]["ApproverMessage"].ToString(),
                    RecommendStatus = Convert.ToInt32(dt.Rows[0]["RecommendStatus"].ToString())
                };
            }
            else
            {
                return new LeaveApplicationViewModel();
            }


        }
        public void UpdateRecommendStatus(int leaveId, int status, string message)
        {
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("[sp_LeaveApplicationRecommendStatusChange]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LeaveIdy", leaveId);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@Message", message);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
        public IEnumerable<LeaveApplicationViewModel> GetLeavesByApproverId(int appId, int? monthID, int? year)
        {
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("[sp_LeaveApplicationsApprove]", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@empCode", appId);
            cmd.Parameters.AddWithValue("@monthId", monthID);
            cmd.Parameters.AddWithValue("@Year", year);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            foreach (DataRow dr in dt.Rows)
            {
                yield return new LeaveApplicationViewModel
                {
                    LeaveId = Convert.ToInt32(dr["LeaveId"].ToString()),
                    LeaveStatus = Convert.ToInt32(dr["LeaveStatus"].ToString()),
                    LeaveStartDate = Convert.ToDateTime(dr["LeaveStartDate"].ToString()),
                    LeaveEndDate = Convert.ToDateTime(dr["LeaveEndDate"]),
                    LeaveSubject = dr["LeaveSubject"].ToString(),
                    LeaveAppliedDate = Convert.ToDateTime(dr["LeaveAppliedDate"].ToString()),
                    LeaveYearName = dr["YearName"].ToString(),
                    LeaveTypeName = dr["LeaveTypeName"].ToString(),
                    LeaveDays = Convert.ToDecimal(dr["LeaveDays"]),
                    RecommenderName = dr["RecommendorName"].ToString(),
                    //ApproverName = dr["ApproverName"].ToString(),
                    LeaveEmpCode = Convert.ToInt32(dr["LeaveEmpCode"].ToString()),
                    AppliedBy = dr["AppliedBy"].ToString(),
                    RecommendStatus = Convert.ToInt32(dr["RecommendStatus"].ToString())
                };
            }
        }
        public LeaveApplicationViewModel GetApproveLeaveDetail(int empCode, int leaveId)
        {
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("[sp_LeaveApplicationApproveDetailById]", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpCode", empCode);
            cmd.Parameters.AddWithValue("@LeaveId", leaveId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            if (dt.Rows.Count != 0)
            {
                return new LeaveApplicationViewModel
                {
                    LeaveId = Convert.ToInt32(dt.Rows[0]["LeaveId"].ToString()),
                    LeaveStatus = Convert.ToInt32(dt.Rows[0]["LeaveStatus"].ToString()),
                    LeaveStartDate = Convert.ToDateTime(dt.Rows[0]["LeaveStartDate"].ToString()),
                    LeaveEndDate = Convert.ToDateTime(dt.Rows[0]["LeaveEndDate"]),
                    LeaveSubject = dt.Rows[0]["LeaveSubject"].ToString(),
                    LeaveAppliedDate = Convert.ToDateTime(dt.Rows[0]["LeaveAppliedDate"].ToString()),
                    LeaveYearName = dt.Rows[0]["YearName"].ToString(),
                    LeaveTypeName = dt.Rows[0]["LeaveTypeName"].ToString(),
                    LeaveDays = Convert.ToDecimal(dt.Rows[0]["LeaveDays"]),
                    RecommenderName = dt.Rows[0]["RecommendorName"].ToString(),
                    ApproverName = dt.Rows[0]["ApproverName"].ToString(),
                    LeaveEmpCode = Convert.ToInt32(dt.Rows[0]["LeaveEmpCode"].ToString()),
                    AppliedBy = dt.Rows[0]["AppliedBy"].ToString(),
                    IsValid = Convert.ToInt32(dt.Rows[0]["IsValid"].ToString())
                };
            }
            else
            {
                return new LeaveApplicationViewModel();
            }
        }
        public void UpdateApproveStatus(int leaveId, int status, string message)
        {
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("[sp_LeaveApplicationApproveStatusChange]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LeaveIdy", leaveId);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@Message", message);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
        }
        public LeaveApplicationViewModel LeaveDetail(int Empcode, int LeaveId)
        {
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("[sp_LeaveApplicationDetail]", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpCode", Empcode);
            cmd.Parameters.AddWithValue("@LeaveId", LeaveId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            if (dt.Rows.Count != 0)
            {
                return new LeaveApplicationViewModel
                {
                    LeaveId = Convert.ToInt32(dt.Rows[0]["LeaveId"].ToString()),
                    LeaveStatus = Convert.ToInt32(dt.Rows[0]["LeaveStatus"].ToString()),
                    LeaveStartDate = Convert.ToDateTime(dt.Rows[0]["LeaveStartDate"].ToString()),
                    LeaveEndDate = Convert.ToDateTime(dt.Rows[0]["LeaveEndDate"]),
                    LeaveSubject = dt.Rows[0]["LeaveSubject"].ToString(),
                    LeaveAppliedDate = Convert.ToDateTime(dt.Rows[0]["LeaveAppliedDate"].ToString()),
                    LeaveYearName = dt.Rows[0]["YearName"].ToString(),
                    LeaveTypeName = dt.Rows[0]["LeaveTypeName"].ToString(),
                    RecommenderName = dt.Rows[0]["RecommendorName"].ToString(),
                    ApproverName = dt.Rows[0]["ApproverName"].ToString(),
                    LeaveEmpCode = Convert.ToInt32(dt.Rows[0]["LeaveEmpCode"].ToString()),
                    AppliedBy = dt.Rows[0]["AppliedBy"].ToString(),
                    IsValid = Convert.ToInt32(dt.Rows[0]["IsValid"].ToString()),
                    RecommendMessage = dt.Rows[0]["RecommenderMessage"].ToString(),
                    ApproveMessage = dt.Rows[0]["ApproverMessage"].ToString(),
                    RecommendStatus = Convert.ToInt32(dt.Rows[0]["RecommendStatus"].ToString()),
                    RecommendDate = dt.Rows[0]["RecommendStatusDate"].ToString(),
                    ApproveDate = dt.Rows[0]["LeaveStatusDate"].ToString(),
                    LeaveRecommenderEmpcode = Convert.ToInt32(dt.Rows[0]["RecommededEmpCode"].ToString()),
                    LeaveApproverEmpcode = Convert.ToInt32(dt.Rows[0]["LeaveApproverEmpCode"].ToString())
                };
            }
            else
            {
                return new LeaveApplicationViewModel();
            }

        }
        public LeaveApplicationViewModel LeaveDetailAdmin(int LeaveId)
        {
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("[sp_LeaveApplicationDetailadmin]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LeaveId", LeaveId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            if (dt.Rows.Count != 0)
            {
                return new LeaveApplicationViewModel
                {
                    LeaveId = Convert.ToInt32(dt.Rows[0]["LeaveId"].ToString()),
                    LeaveStatus = Convert.ToInt32(dt.Rows[0]["LeaveStatus"].ToString()),
                    LeaveStartDate = Convert.ToDateTime(dt.Rows[0]["LeaveStartDate"].ToString()),
                    LeaveEndDate = Convert.ToDateTime(dt.Rows[0]["LeaveEndDate"]),
                    LeaveSubject = dt.Rows[0]["LeaveSubject"].ToString(),
                    LeaveAppliedDate = Convert.ToDateTime(dt.Rows[0]["LeaveAppliedDate"].ToString()),
                    LeaveYearName = dt.Rows[0]["YearName"].ToString(),
                    LeaveTypeName = dt.Rows[0]["LeaveTypeName"].ToString(),
                    RecommenderName = dt.Rows[0]["RecommendorName"].ToString(),
                    ApproverName = dt.Rows[0]["ApproverName"].ToString(),
                    LeaveEmpCode = Convert.ToInt32(dt.Rows[0]["LeaveEmpCode"].ToString()),
                    LeaveDays = Convert.ToDecimal(dt.Rows[0]["LeaveDays"]),
                    AppliedBy = dt.Rows[0]["AppliedBy"].ToString(),
                    RecommendMessage = dt.Rows[0]["RecommenderMessage"].ToString(),
                    ApproveMessage = dt.Rows[0]["ApproverMessage"].ToString(),
                    RecommendStatus = Convert.ToInt32(dt.Rows[0]["RecommendStatus"].ToString()),
                    RecommendDate = dt.Rows[0]["RecommendStatusDate"].ToString(),
                    ApproveDate = dt.Rows[0]["LeaveStatusDate"].ToString(),
                    ApproveStatus = dt.Rows[0]["LeaveStatus"].ToString(),
                    LeaveRecommenderEmpcode = Convert.ToInt32(dt.Rows[0]["RecommededEmpCode"].ToString()),
                    LeaveApproverEmpcode = Convert.ToInt32(dt.Rows[0]["LeaveApproverEmpCode"].ToString())
                };
            }
            else
            {
                return new LeaveApplicationViewModel();
            }
        }
        public int UpdateLeaveAssign(int Empcode, int LeaveRuleId)
        {

            try
            {
                SqlConnection conn = DbConnectHelper.GetConnection();
                conn.Open();
                SqlCommand cmd = new SqlCommand("[sp_LeaveRuleEmployeeSetup]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpCode", Empcode);
                cmd.Parameters.AddWithValue("@leaveYearId", LeaveRuleId);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                return 1;
            }
            catch
            {
                return 0;
            }

        }
        public IEnumerable<LeaveStatViewModel> UnassignedLeave(int Empcode, int LeaveRuleId, int YearId)
        {


            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("[sp_GetUnassignedLeave]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Empcode", Empcode);
            cmd.Parameters.AddWithValue("@LeaveRuleId", LeaveRuleId);
            cmd.Parameters.AddWithValue("@yearId", YearId);
            cmd.ExecuteNonQuery();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            foreach (DataRow dr in dt.Rows)
            {
                yield return new LeaveStatViewModel
                {
                    LeaveTypeId = Convert.ToInt32(dr["LeaveTypeId"].ToString()),
                    LeaveTypeName = dr["LeaveTypeName"].ToString(),
                    LeaveYearId = YearId

                };
            }
        }
        public double LeaveDayCalculations(int LeaveId, DateTime Sdate, DateTime Edate)
        {
            double LeaveDays;
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("[sp_CountLeaveAppliedDays]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Sdate", Sdate);
            cmd.Parameters.AddWithValue("@EndDate", Edate);
            cmd.Parameters.AddWithValue("@LeaveId", LeaveId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            LeaveDays = Convert.ToDouble(dt.Rows[0]["FinalCounts"].ToString());
            return LeaveDays;

        }
        public List<LeaveMonthlyProcessDTO> GetLeaveYearList()
        {
            List<LeaveMonthlyProcess> Record = new List<LeaveMonthlyProcess>();
            LeaveYear CurrentYear = _unitOfWork.LeaveYearRepository.Get(x => x.YearCurrent == true).FirstOrDefault();
            if (CurrentYear != null)
            {
                Record = _unitOfWork.LeaveMonthlyProcessRepository.Get(x => x.LeaveYearId == CurrentYear.YearId).ToList();
            }
            List<LeaveMonthlyProcessDTO> ReturnRecord = LeaveMonthlyProcessRequestFormatter.DomainToModelList(Record);
            return ReturnRecord;
        }
        public void ProcessMonthlyByProcessId(int id)
        {
            LeaveMonthlyProcess Record = _unitOfWork.LeaveMonthlyProcessRepository.GetById(id);
            Record.ProcessByEmpCode = Convert.ToInt32(HttpContext.Current.Session["EmpCode"]); ;
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("[sp_LeaveProcessMonthly]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@leaveYearId", Record.LeaveYearId);
            cmd.Parameters.AddWithValue("@MonthNumber", Record.MonthNumber);
            cmd.Parameters.AddWithValue("@updatedByEmpCode", Record.ProcessByEmpCode);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
        }


        public List<SelectListItem> GetLeaveYearSelectList()
        {
            List<LeaveYear> Domain = _unitOfWork.LeaveYearRepository.All().ToList();
            List<SelectListItem> Record = new List<SelectListItem>();
            foreach (LeaveYear Item in Domain)
            {
                SelectListItem Single = new SelectListItem
                {
                    Text = Item.YearName.ToString(),
                    Value = Item.YearId.ToString()
                };
                Record.Add(Single);
            }
            return Record;
        }
        //public List<LeaveYearlyReportDTO> GetLeaveYearlyReport(LeaveYearlyReportWithFilter Record)
        //{
        //    List<LeaveYearlyReportDTO> ReturnRecord = new List<LeaveYearlyReportDTO>();
        //    List<LeaveAssigned> LeaveAssignedList = _unitOfWork.LeaveAssignedRepository.Get(x => x.AssignEmpCode == Record.EmployeeCode && x.AssignedLeaveYearId == Record.LeaveYearId).OrderBy(x => x.LeaveGainedMonth).ToList();
        //    int LeaveGainedMonth = 0;
        //    LeaveYearlyReportDTO Singles = new LeaveYearlyReportDTO();
        //    decimal TotalRemainingHomeLeave = 0;
        //    decimal TotalRemainingSickLeave = 0;
        //    decimal TotalRemainingCasualLeave = 0;
        //    decimal TotalRemainingExchangeLeave = 0;


        //    decimal PastRemainingHomeLeave = 0;
        //    decimal PastRemainingSickLeave = 0;
        //    decimal PastRemainingCasualLeave = 0;
        //    decimal PastRemainingExchangeLeave = 0;

        //    foreach (LeaveAssigned Item in LeaveAssignedList)
        //    {


        //        if (LeaveGainedMonth != Item.LeaveGainedMonth)
        //        {

        //            if (LeaveGainedMonth == 1)
        //                Singles.MonthName = "बैशाख";
        //            if (LeaveGainedMonth == 2)
        //                Singles.MonthName = "जेठ";
        //            if (LeaveGainedMonth == 3)
        //                Singles.MonthName = "असार";
        //            if (LeaveGainedMonth == 4)
        //                Singles.MonthName = "श्रावण";
        //            if (LeaveGainedMonth == 5)
        //                Singles.MonthName = "भाद्र";
        //            if (LeaveGainedMonth == 6)
        //                Singles.MonthName = "असोज";
        //            if (LeaveGainedMonth == 7)
        //                Singles.MonthName = "कार्तिक";
        //            if (LeaveGainedMonth == 8)
        //                Singles.MonthName = "मंसिर";
        //            if (LeaveGainedMonth == 9)
        //                Singles.MonthName = "पुष";
        //            if (LeaveGainedMonth == 10)
        //                Singles.MonthName = "माघ";
        //            if (LeaveGainedMonth == 11)
        //                Singles.MonthName = "फाल्गुन";
        //            if (LeaveGainedMonth == 12)
        //                Singles.MonthName = "चैत";

        //            LeaveGainedMonth = Convert.ToInt32(Item.LeaveGainedMonth);
        //            ReturnRecord.Add(Singles);

        //            Singles = new LeaveYearlyReportDTO();
        //        }

        //        /// for previous year leave
        //        if (Item.LeaveGainedMonth == 0)
        //        {
        //            if (Item.AssignLeaveTypeId == 1)
        //            {
        //                Singles.TakenHomeLeave = Convert.ToDecimal(Item.AssignedDays);
        //                TotalRemainingHomeLeave = TotalRemainingHomeLeave + Convert.ToDecimal(Item.AssignedDays);
        //            }

        //            if (Item.AssignLeaveTypeId == 2)
        //            {
        //                Singles.TakenSickLeave = Convert.ToDecimal(Item.AssignedDays);
        //                TotalRemainingSickLeave = TotalRemainingSickLeave + Convert.ToDecimal(Item.AssignedDays);
        //            }

        //            if (Item.AssignLeaveTypeId == 3)
        //            {
        //                Singles.TakenCasualLeave = Convert.ToDecimal(Item.AssignedDays);
        //                TotalRemainingCasualLeave = TotalRemainingCasualLeave + Convert.ToDecimal(Item.AssignedDays);
        //            }

        //            if (Item.AssignLeaveTypeId == 9)
        //            {
        //                Singles.TakenExchangeLeave = Convert.ToDecimal(Item.AssignedDays);
        //                TotalRemainingExchangeLeave = TotalRemainingExchangeLeave + Convert.ToDecimal(Item.AssignedDays);
        //            }
        //        }
        //        else
        //        {
        //            LeaveYear Year = _unitOfWork.LeaveYearRepository.Get(x => x.YearId == Record.LeaveYearId).FirstOrDefault();
        //            LeaveYearlyReport monthlyTaken = new LeaveYearlyReport();
        //            decimal LeaveTaken = 0;
        //            if (Year != null)
        //            {
        //                SqlParameter[] parameter =
        //                {
        //                    new SqlParameter("@Empcode", Record.EmployeeCode),
        //                    new SqlParameter("@nepaliYear",  Convert.ToInt32(Year.YearName)),
        //                    new SqlParameter("@Monthcode", Item.LeaveGainedMonth),
        //                    new SqlParameter("@leaveTypeId", Item.AssignLeaveTypeId)
        //                };
        //                monthlyTaken = _unitOfWork.LeaveYearlyReportRepository.ExecuteSPwithParameterForList("sp_EmployeeMonthlyTakenLeave @Empcode,@nepaliYear,@Monthcode,@leaveTypeId", parameter).FirstOrDefault();

        //                LeaveTaken = monthlyTaken.LeaveTaken;
        //            }

        //            if (Item.AssignLeaveTypeId == 1)
        //            {
        //                Singles.GotHomeLeave = Convert.ToDecimal(Item.AssignedDays);
        //                Singles.TakenHomeLeave = LeaveTaken;
        //                TotalRemainingHomeLeave = TotalRemainingHomeLeave + Convert.ToDecimal(Item.AssignedDays) - LeaveTaken;
        //                Singles.LeftHomeLeave = TotalRemainingHomeLeave;

        //            }
        //            if (Item.AssignLeaveTypeId == 2)
        //            {
        //                Singles.GotSickLeave = Convert.ToDecimal(Item.AssignedDays);
        //                Singles.TakenSickLeave = LeaveTaken;
        //                TotalRemainingSickLeave = TotalRemainingSickLeave + Convert.ToDecimal(Item.AssignedDays) - LeaveTaken;
        //                Singles.LeftSickLeave = TotalRemainingSickLeave;
        //            }
        //            if (Item.AssignLeaveTypeId == 3)
        //            {
        //                Singles.GotCasualLeave = Convert.ToDecimal(Item.AssignedDays);
        //                Singles.TakenCasualLeave = LeaveTaken;
        //                TotalRemainingCasualLeave = TotalRemainingCasualLeave + Convert.ToDecimal(Item.AssignedDays) - LeaveTaken;
        //                Singles.LeftCasualLeave = TotalRemainingCasualLeave;
        //            }
        //            if (Item.AssignLeaveTypeId == 9)
        //            {
        //                decimal LeaveEarned = 0;
        //                /*
        //                For leave type  = 9 We need to get earned days from LeaveEarned table
        //                */
        //                if (Year != null && Item.LeaveGainedMonth > 0)
        //                {
        //                    SqlParameter[] parameter =
        //                    {
        //                    new SqlParameter("@Empcode", Record.EmployeeCode),
        //                    new SqlParameter("@nepaliYear",  Convert.ToInt32(Year.YearName)),
        //                    new SqlParameter("@Monthcode", Item.LeaveGainedMonth),
        //                    new SqlParameter("@leaveTypeId", Item.AssignLeaveTypeId)
        //                };
        //                    monthlyTaken = _unitOfWork.LeaveYearlyReportRepository.ExecuteSPwithParameterForList("sp_EmployeeLeaveEarnedMonthly @Empcode,@nepaliYear,@Monthcode,@leaveTypeId", parameter).FirstOrDefault();

        //                    LeaveEarned = monthlyTaken.LeaveTaken;
        //                }

        //                Singles.GotExchangeLeave = Convert.ToDecimal(LeaveEarned);
        //                Singles.TakenExchangeLeave = LeaveTaken;
        //                TotalRemainingExchangeLeave = TotalRemainingExchangeLeave + Convert.ToDecimal(LeaveEarned) - LeaveTaken;
        //                Singles.LeftExchangeLeave = TotalRemainingExchangeLeave;
        //            }


        //        }


        //    }

        //    if (LeaveGainedMonth == 1)
        //        Singles.MonthName = "बैशाख";
        //    if (LeaveGainedMonth == 2)
        //        Singles.MonthName = "जेठ";
        //    if (LeaveGainedMonth == 3)
        //        Singles.MonthName = "असार";
        //    if (LeaveGainedMonth == 4)
        //        Singles.MonthName = "श्रावण";
        //    if (LeaveGainedMonth == 5)
        //        Singles.MonthName = "भाद्र";
        //    if (LeaveGainedMonth == 6)
        //        Singles.MonthName = "असोज";
        //    if (LeaveGainedMonth == 7)
        //        Singles.MonthName = "कार्तिक";
        //    if (LeaveGainedMonth == 8)
        //        Singles.MonthName = "मंसिर";
        //    if (LeaveGainedMonth == 9)
        //        Singles.MonthName = "पुष";
        //    if (LeaveGainedMonth == 10)
        //        Singles.MonthName = "माघ";
        //    if (LeaveGainedMonth == 11)
        //        Singles.MonthName = "फाल्गुन";
        //    if (LeaveGainedMonth == 12)
        //        Singles.MonthName = "चैत";
        //    ReturnRecord.Add(Singles);
        //    return ReturnRecord;


        //}

        public List<LeaveYearlyReportDTO> GetLeaveYearlyReport(LeaveYearlyReportWithFilter Record)
        {

            List<LeaveYearlyReportDTO> ReturnRecord = new List<LeaveYearlyReportDTO>();

            int LeaveGainedMonth = 0;



            decimal TotalRemainingHomeLeave = 0;
            decimal TotalRemainingSickLeave = 0;
            decimal TotalRemainingCasualLeave = 0;
            decimal TotalRemainingExchangeLeave = 0;

            decimal PastRemainingHomeLeave = 0;
            decimal PastRemainingSickLeave = 0;
            decimal PastRemainingCasualLeave = 0;
            decimal PastRemainingExchangeLeave = 0;

            LeaveYear Year = _unitOfWork.LeaveYearRepository.Get(x => x.YearId == Record.LeaveYearId).FirstOrDefault();

            for (int i = 0; i <= 12; i++)
            {
                LeaveYearlyReportDTO Singles = new LeaveYearlyReportDTO();

                List<LeaveAssigned> LeaveAssignedList = _unitOfWork.LeaveAssignedRepository.Get(x => x.AssignEmpCode == Record.EmployeeCode
                    && x.AssignedLeaveYearId == Record.LeaveYearId
                    && x.LeaveGainedMonth == i).ToList();
                if (i == 0)
                {
                    foreach (LeaveAssigned Item in LeaveAssignedList)
                    {
                        Singles.MonthName = "";

                        if (Item.AssignLeaveTypeId == 1)
                        {
                            Singles.TakenHomeLeave = Convert.ToDecimal(Item.AssignedDays);
                            TotalRemainingHomeLeave = TotalRemainingHomeLeave + Convert.ToDecimal(Item.AssignedDays);
                        }

                        if (Item.AssignLeaveTypeId == 2)
                        {
                            Singles.TakenSickLeave = Convert.ToDecimal(Item.AssignedDays);
                            TotalRemainingSickLeave = TotalRemainingSickLeave + Convert.ToDecimal(Item.AssignedDays);
                        }

                        if (Item.AssignLeaveTypeId == 3)
                        {
                            Singles.TakenCasualLeave = Convert.ToDecimal(Item.AssignedDays);
                            TotalRemainingCasualLeave = TotalRemainingCasualLeave + Convert.ToDecimal(Item.AssignedDays);
                        }

                        if (Item.AssignLeaveTypeId == 9)
                        {
                            Singles.TakenExchangeLeave = Convert.ToDecimal(Item.AssignedDays);
                            TotalRemainingExchangeLeave = TotalRemainingExchangeLeave + Convert.ToDecimal(Item.AssignedDays);
                        }


                    }
                    ReturnRecord.Add(Singles);

                }
                else
                {

                    if (i == 1)
                        Singles.MonthName = "बैशाख";
                    if (i == 2)
                        Singles.MonthName = "जेठ";
                    if (i == 3)
                        Singles.MonthName = "असार";
                    if (i == 4)
                        Singles.MonthName = "श्रावण";
                    if (i == 5)
                        Singles.MonthName = "भाद्र";
                    if (i == 6)
                        Singles.MonthName = "असोज";
                    if (i == 7)
                        Singles.MonthName = "कार्तिक";
                    if (i == 8)
                        Singles.MonthName = "मंसिर";
                    if (i == 9)
                        Singles.MonthName = "पुष";
                    if (i == 10)
                        Singles.MonthName = "माघ";
                    if (i == 11)
                        Singles.MonthName = "फाल्गुन";
                    if (i == 12)
                        Singles.MonthName = "चैत";

                    int[] LeaveTypeIdArray = new int[] { 1, 2, 3, 9 };

                    foreach (int TypeId in LeaveTypeIdArray)
                    {

                        var MonthlyAsign = _unitOfWork.LeaveAssignedRepository.Get(x => x.AssignEmpCode == Record.EmployeeCode
                     && x.AssignedLeaveYearId == Record.LeaveYearId
                     && x.LeaveGainedMonth == i
                     && x.AssignLeaveTypeId == TypeId
                     ).FirstOrDefault();

                        decimal AssignedDays = 0;

                        if (MonthlyAsign != null)
                        {
                            AssignedDays = Convert.ToDecimal(MonthlyAsign.AssignedDays);
                        }


                        LeaveYearlyReport monthlyTaken = new LeaveYearlyReport();
                        decimal LeaveTaken = 0;

                        if (Year != null)
                        {
                            SqlParameter[] parameter =
                            {
                            new SqlParameter("@Empcode", Record.EmployeeCode),
                            new SqlParameter("@nepaliYear",  Convert.ToInt32(Year.YearName)),
                            new SqlParameter("@Monthcode", i),
                            new SqlParameter("@leaveTypeId", TypeId)
                        };
                            monthlyTaken = _unitOfWork.LeaveYearlyReportRepository.ExecuteSPwithParameterForList("sp_EmployeeMonthlyTakenLeave @Empcode,@nepaliYear,@Monthcode,@leaveTypeId", parameter).FirstOrDefault();

                            LeaveTaken = monthlyTaken.LeaveTaken;
                        }
                        if (TypeId == 1)
                        {
                            Singles.GotHomeLeave = AssignedDays;
                            Singles.TakenHomeLeave = LeaveTaken;
                            TotalRemainingHomeLeave = TotalRemainingHomeLeave + Convert.ToDecimal(AssignedDays) - LeaveTaken;
                            Singles.LeftHomeLeave = TotalRemainingHomeLeave;

                        }
                        if (TypeId == 2)
                        {
                            Singles.GotSickLeave = AssignedDays;
                            Singles.TakenSickLeave = LeaveTaken;
                            TotalRemainingSickLeave = TotalRemainingSickLeave + AssignedDays - LeaveTaken;
                            Singles.LeftSickLeave = TotalRemainingSickLeave;
                        }
                        if (TypeId == 3)
                        {
                            Singles.GotCasualLeave = AssignedDays;
                            Singles.TakenCasualLeave = LeaveTaken;
                            TotalRemainingCasualLeave = TotalRemainingCasualLeave + AssignedDays - LeaveTaken;
                            Singles.LeftCasualLeave = TotalRemainingCasualLeave;
                        }
                        if (TypeId == 9)
                        {
                            decimal LeaveEarned = 0;
                            /*
                            For leave type  = 9 We need to get earned days from LeaveEarned table
                            */
                            if (Year != null && i > 0)
                            {
                                SqlParameter[] parameter =
                                {
                                new SqlParameter("@Empcode", Record.EmployeeCode),
                                 new SqlParameter("@nepaliYear",  Convert.ToInt32(Year.YearName)),
                              new SqlParameter("@Monthcode", i),
                              new SqlParameter("@leaveTypeId", TypeId)
                            };
                                monthlyTaken = _unitOfWork.LeaveYearlyReportRepository.ExecuteSPwithParameterForList("sp_EmployeeLeaveEarnedMonthly @Empcode,@nepaliYear,@Monthcode,@leaveTypeId", parameter).FirstOrDefault();

                                LeaveEarned = monthlyTaken.LeaveTaken;
                            }

                            Singles.GotExchangeLeave = Convert.ToDecimal(LeaveEarned);
                            Singles.TakenExchangeLeave = LeaveTaken;
                            TotalRemainingExchangeLeave = TotalRemainingExchangeLeave + Convert.ToDecimal(LeaveEarned) - LeaveTaken;
                            Singles.LeftExchangeLeave = TotalRemainingExchangeLeave;
                        }



                    }
                    ReturnRecord.Add(Singles);



                }


            }


            return ReturnRecord;


        }
        public List<PayrollLeaveDeductionDTO> GetPayrollLeaveDeduction(LeaveYearlyReportWithFilter Record)
        {
            List<PayrollLeaveDeductionDTO> ReturnRecord = new List<PayrollLeaveDeductionDTO>();
            List<PayrollLeaveDeduction> PayrollLeaveDeductions = _unitOfWork.PayrollLeaveDeductionRepository.Get(x => x.EmpCode == Record.EmployeeCode && x.LeaveYearId == Record.LeaveYearId).ToList();
            foreach (PayrollLeaveDeduction Item in PayrollLeaveDeductions)
            {
                PayrollLeaveDeductionDTO Singles = new PayrollLeaveDeductionDTO();
                Singles.LeaveTypeId = Item.LeaveTypeId;
                Singles.LeaveName = _unitOfWork.LeaveTypeRepository.GetById(Item.LeaveTypeId).LeaveTypeName;
                Singles.LeaveDays = Item.LeaveDays;
                Singles.DeductionType = Item.DeductionType == "P" ? "Penalty" : "Encashment";
                Singles.LeaveDate = Convert.ToDateTime(Item.LeaveDate);
                ReturnRecord.Add(Singles);
            }
            return ReturnRecord;
        }


    }
}


