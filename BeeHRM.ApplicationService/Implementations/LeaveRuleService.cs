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
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.ApplicationService.ConnectDb;
using System.Data.SqlClient;
using System.Data;
using BeeHRM.ApplicationService.Common;
using BeeHRM.ApplicationService.ViewModel;

namespace BeeHRM.ApplicationService.Implementations
{
    public class LeaveRuleService : ILeaveRuleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LeaveRuleService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public int DeleteLeaveRule(int id)
        {
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_LeaveRuleDelete", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LeaveRuleId", id);
            int abc = cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            return abc;
        }

        public LeaveRuleDTO GetLeaveRuleById(int id)
        {
            LeaveRule lr = _unitOfWork.LeaveRuleRepository.Get(x => x.LeaveRuleId == id).FirstOrDefault();
            return LeaveRuleRequestFormatter.ConvertRespondentInfoToDTO(lr);
        }

        public IEnumerable<LeaveRuleDTO> GetLeaveRulesList()
        {
            IEnumerable<LeaveRule> modelDatas = _unitOfWork.LeaveRuleRepository.All().ToList();
            return LeaveRuleResponseFormatter.ModelData(modelDatas);
        }

        public LeaveRuleDTO InsertLeaveRule(LeaveRuleDTO leaveRule)
        {
            LeaveRule lr = LeaveRuleRequestFormatter.ConvertRespondentInfoFromDTO(leaveRule);
            return LeaveRuleRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.LeaveRuleRepository.Create(lr));
        }

        public int UpdateLeaveRule(LeaveRuleDTO editLeaveRule)
        {
            LeaveRule editLR = LeaveRuleRequestFormatter.ConvertRespondentInfoFromDTO(editLeaveRule);
            return _unitOfWork.LeaveRuleRepository.Update(editLR);
        }

        public IEnumerable<LeaveAssignedViewModel> LeaveAssignDetail(int empcode)
        {
           
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("select la.AssignedId,la.AssignLeaveTypeId,(select LeaveTypeName from LeaveTypes where LeaveTypeId = la.AssignLeaveTypeId)LeaveTypeName, la.AssignedDays, (la.AssignedDays-la.LeaveTakenDays)AssignedRemainingDays  from LeaveAssigned la where AssignEmpCode =" + empcode, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@EmpCode", empcode);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            foreach (DataRow row in dt.Rows)
            {
                yield return new LeaveAssignedViewModel
                {
                    LeaveTypeId = Convert.ToInt32(row["AssignLeaveTypeId"].ToString()),
                    AssignedId= Convert.ToInt32(row["AssignedId"].ToString()),
                    LeaveTypename =row["LeaveTypeName"].ToString(),
                    TotalAssignDay=Convert.ToDecimal(row["AssignedDays"].ToString()),
                    TotalRemainingDay=Convert.ToDecimal(row["AssignedRemainingDays"].ToString()),



                };


            }
            
        }
    }
}
