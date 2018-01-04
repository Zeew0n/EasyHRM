using BeeHRM.ApplicationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.ApplicationService.ConnectDb;
using System.Data.SqlClient;
using System.Data;
using BeeHRM.ApplicationService.Common;
using BeeHRM.Repository;
using BeeHRM.ApplicationService.RequestFormatters;

namespace BeeHRM.ApplicationService.Implementations
{
    public class LeaveYearService : ILeaveYearService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LeaveYearService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public void DeleteLeaveYear(int id)
        {
            _unitOfWork.LeaveYearRepository.Delete(id);
            _unitOfWork.Save();
        }

        public LeaveYearDTO GetLeaveYearInfoById(int id)
        {
            LeaveYear info = _unitOfWork.LeaveYearRepository.Get(x => x.YearId == id).FirstOrDefault();
            return LeaveYearRequestFormatter.ConvertRespondentInfoToDTO(info);
        }

        public IEnumerable<LeaveYearDTO> GetLeaveYears()
        {
            return LeaveYearResponseFormatter.ModelData(_unitOfWork.LeaveYearRepository.All());
        }

        public LeaveYearDTO InsertLeaveYear(LeaveYearDTO newLeaveYear)
        {
            LeaveYearDTO lyd = new LeaveYearDTO();
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_LeaveYearAdd", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@YearName", newLeaveYear.YearName);
            cmd.Parameters.AddWithValue("@YearStart", newLeaveYear.YearStartDate);
            cmd.Parameters.AddWithValue("@YearEnd", newLeaveYear.YearEndDate);
            cmd.Parameters.AddWithValue("@YearStartNP", newLeaveYear.YearStartDateNp);
            cmd.Parameters.AddWithValue("@YearEndNP", newLeaveYear.YearEndDateNp);
            cmd.Parameters.AddWithValue("@IsActive", newLeaveYear.YearCurrent);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            foreach (DataRow row in dt.Rows)
            {
                return new LeaveYearDTO {
                    YearName = Convert.ToInt32(row["YearName"]),
                    YearStartDate =  Convert.ToDateTime(row["YearStartDate"].ToString()),
                    YearEndDate = Convert.ToDateTime(row["YearEndDate"].ToString()),
                    YearStartDateNp = row["YearStartDateNP"].ToString(),
                    YearEndDateNp = row["YearEndDateNP"].ToString(),
                    YearCurrent = Convert.ToBoolean(row["YearCurrent"].ToString())
                };
            }

            return lyd;
        }

        public int UpdateLeaveYear(LeaveYearDTO leaveYear)
        {
            LeaveYearDTO lyd = new LeaveYearDTO();
            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_LeaveYearEdit", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@YearId", leaveYear.YearId);
            cmd.Parameters.AddWithValue("@YearName", leaveYear.YearName);
            cmd.Parameters.AddWithValue("@YearStart", leaveYear.YearStartDate);
            cmd.Parameters.AddWithValue("@YearEnd", leaveYear.YearEndDate);
            cmd.Parameters.AddWithValue("@YearStartNP", leaveYear.YearStartDateNp);
            cmd.Parameters.AddWithValue("@YearEndNP", leaveYear.YearEndDateNp);
            cmd.Parameters.AddWithValue("@IsActive", leaveYear.YearCurrent);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            return dt.Rows.Count;
        }
    }
}
