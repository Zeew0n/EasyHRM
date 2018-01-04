using BeeHRM.ApplicationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using BeeHRM.Repository;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.ApplicationService.ConnectDb;
using System.Data.SqlClient;
using System.Data;
using BeeHRM.ApplicationService.ViewModel;

namespace BeeHRM.ApplicationService.Implementations
{
    public class LeaveRuleDetailService : ILeaveRuleDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LeaveRuleDetailService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public IEnumerable<LeaveRuleDetailDTO> GetLeaveRuleDetails(int leaveRuleId)
        {
            return LeaveRuleDetailResponseFormatter.ModelData(_unitOfWork.LeaveRuleDetailRepository.Get(x => x.LeaveRuleId == leaveRuleId));
        }

        public LeaveRuleDetailDTO InsertLeaveRuleDetail(LeaveRuleDetailDTO leaveRuleDetail)
        {
            LeaveRuleDetail lr = LeaveRuleDetailRequestFormatter.ConvertRespondentInfoFromDTO(leaveRuleDetail);
            return LeaveRuleDetailRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.LeaveRuleDetailRepository.Create(lr));
        }

        public int UpdateLeaveRuleDetails(LeaveRuleDetailDTO editLRD)
        {
            
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_InsertLeaveRuleDetails", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DetailID", editLRD.DetailId);
            cmd.Parameters.AddWithValue("@LeaveRuleId", editLRD.LeaveRuleId);
            cmd.Parameters.AddWithValue("@LeaveTypeId", editLRD.LeaveTypeId);
            cmd.Parameters.AddWithValue("@LeaveDays", editLRD.LeaveDays);
            int abc = cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            return abc;
        }

      
    }
}
