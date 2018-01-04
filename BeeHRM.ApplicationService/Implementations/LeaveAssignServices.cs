using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.Repository;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.ApplicationService.ConnectDb;
using System.Data.SqlClient;
using System.Data;

namespace BeeHRM.ApplicationService.Implementations
{
    public class LeaveAssignServices : ILeaveAssigned
    {
        private readonly IUnitOfWork _unitOfWork;

        public LeaveAssignServices(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();

        }
        public int InsertLeaveAssigned(LeaveAssignedDTO data)
        {
            try
            {
                LeaveAssigned dataToInsert = new LeaveAssigned();
                dataToInsert = LeaveAssignRequestFormatters.ConvertRespondentInfoFromDTO(data);
                LeaveAssignRequestFormatters.ConvertRespondentInfoToDTO(_unitOfWork.LeaveAssignedRepository.Create(dataToInsert));
                return 1;
            }
            catch (Exception Ex)
            {
                return 0;
            }

        }

        public int DeleteLeaveAssignRules(int AssiginId)
        {

            try
            {
                var conn = DbConnectHelper.GetConnection();
                conn.Open();
                SqlCommand cmd = new SqlCommand("Delete From LeaveAssigned where AssignedId=" + AssiginId, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@AssiginedId", AssiginId);
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

        public int CheckLeaveExistence(int EmpCode, int LeaveTypeId, int LeaveYearId)
        {
            try
            {
                var conn = DbConnectHelper.GetConnection();
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from LeaveApplications Where LeaveEmpCode="+ EmpCode + " and LeaveTypeId="+ LeaveTypeId+" and LeaveYearId="+ LeaveYearId, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@LeaveEmpCode", EmpCode);
                cmd.Parameters.AddWithValue("@LeaveTypeId", LeaveTypeId);
                cmd.Parameters.AddWithValue("@LeaveYearId", LeaveYearId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                if (dt.Rows.Count > 0)
                {
                    return 0;
                }
                else
                    return 1;
               
            }


            catch (Exception ex)
            {
                return 0;
            }
        }

        public int UpdateLeaveAssigned(LeaveAssignedDTO data)
        {
            try
            {
                var conn = DbConnectHelper.GetConnection();
                conn.Open();
               
                SqlCommand cmd = new SqlCommand("UPDATE LeaveAssigned SET AssignedDays="+data.AssignedDays+" Where AssignEmpCode=" + data.AssignEmpCode + " and AssignLeaveTypeId=" + data.AssignLeaveTypeId + " and AssignedLeaveYearId=" + data.AssignedLeaveYearId, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                if (dt.Rows.Count > 0)
                {
                    return 0;
                }
                else
                    return 1;

            }


            catch (Exception ex)
            {
                return 0;
            }
           
        }

        public int UpdateLeaveAssignedDaysInital(int assiginId, decimal LeaveDays)
        {
            try
            {
                var conn = DbConnectHelper.GetConnection();
                conn.Open();
                SqlCommand cmd = new SqlCommand("update LeaveAssigned set AssignedDays=" + LeaveDays +  " Where AssignedId="+ assiginId, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
                if (dt.Rows.Count > 0)
                {
                    return 0;
                }
                else
                    return 1;

            }


            catch (Exception ex)
            {
                return 0;
            }

        }
    }
    }
  

