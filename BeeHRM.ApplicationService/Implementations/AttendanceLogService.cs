using BeeHRM.ApplicationService.ConnectDb;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using BeeHRM.Repository;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Implementations
{
    public class AttendanceLogService : IAttendanceLogService
    {
        private readonly IUnitOfWork _unitOfWork;
        //private AttendanceRequest _req;


        public AttendanceLogService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public IEnumerable<AttendanceLogViewModel> GetAttendanceLog(int EmpCode, DateTime StrtDate)
        {

            // List<DailyAttendanceFilterViewModel> attlist = new List<DailyAttendanceFilterViewModel>();
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            //SqlCommand cmd = new SqlCommand("select ek.logEmpCode,ek.logDate,ek.logTime, ek.logTypeId, ek.logMethodId, s.InOutMode from AttEmployeeLog ek join AttendaceLog s on ek.logEmpCode = s.EnrollNumber AND ek.LogId = s.DeviceDataId where ek.logEmpCode = '" + EmpCode + "'" + "AND" + " ek.logDate = '" + StrtDate + "'", conn);
            SqlCommand cmd = new SqlCommand("select logEmpCode, logDate, logTime, logMethodId from AttEmployeeLog where logEmpCode = '" + EmpCode + "'" + "AND" +" logDate = '"+ StrtDate + "'", conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@StartDate", StrtDate);
            cmd.Parameters.AddWithValue("@EmpCode", EmpCode);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            foreach (DataRow row in dt.Rows)
            {
                yield return new AttendanceLogViewModel
                {
                    logEmpCode = Convert.ToInt32(row["logEmpCode"].ToString()),
                    //InOutMode = Convert.ToInt32(row["InOutMode"].ToString()),
                    logMethodId = Convert.ToInt32(row["logMethodId"].ToString()),
                    logDate = Convert.ToDateTime(row["logDate"].ToString()),
                    logTime = Convert.ToDateTime(row["logTime"].ToString()),
                    //logTypeId = Convert.ToInt32(row["logTypeId"].ToString())
                };
            }
        }
    }
}
