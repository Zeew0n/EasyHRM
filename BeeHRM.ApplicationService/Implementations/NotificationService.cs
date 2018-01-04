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
using BeeHRM.ApplicationService.ViewModel;
using BeeHRM.ApplicationService.ConnectDb;
using BeeHRM.ApplicationService.Common;
using System.Data.SqlClient;
using System.Data;
//using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.ApplicationService.RequestFormatters;


namespace BeeHRM.ApplicationService.Implementations
{
    public class NotificationService : INotification
    {
        private readonly IUnitOfWork _unitOfWork;
        public NotificationService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public NotificationsDTOs InsertNotification(NotificationsDTOs ntd)
        {

            Notification nttd = NotificationRequestFormatter.ConvertRespondentInfoFromDTO(ntd);
            return NotificationRequestFormatter.ConvertRespondentToDTO(_unitOfWork.NotificationRepository.Create(nttd));
        }

        public IEnumerable<NotificationsViewModel> Notificationlist(int id)
        {
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_Notification", conn);
            cmd.Parameters.AddWithValue("@EmpCode", id);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            foreach (DataRow row in dt.Rows)
            {
                yield return new NotificationsViewModel
                {
                    NotificationID = row["NotificationId"].ToString(),
                    ReceiverID = row["NotificationReceiverId"].ToString(),
                    ReceiverType = row["NotificationReceiverType"].ToString(),
                    Subject = row["NotificationSubject"].ToString(),
                    Message = row["NotificationMessage"].ToString(),
                    Date = row["NotificationDate"].ToString(),
                    ReadDate = row["NotificationReadDate"].ToString(),
                    DetailUrl = row["NotificationDetailURL"].ToString(),
                    EmpName = row["EmpName"].ToString(),
                    EmpPhoto = row["EmpPhoto"].ToString()

                };
            }
        }

        public IEnumerable<NotificationsViewModel> SingleNotification(int id)
        {
            var conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_NotificationSingle", conn);
            cmd.Parameters.AddWithValue("@NotificationId", id);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
            foreach (DataRow row in dt.Rows)
            {
                yield return new NotificationsViewModel
                {
                    NotificationID = row["NotificationId"].ToString(),
                    ReceiverID = row["NotificationReceiverId"].ToString(),
                    ReceiverType = row["NotificationReceiverType"].ToString(),
                    Subject = row["NotificationSubject"].ToString(),
                    Message = row["NotificationMessage"].ToString(),
                    Date = row["NotificationDate"].ToString(),
                    ReadDate = row["NotificationReadDate"].ToString(),
                    DetailUrl = row["NotificationDetailURL"].ToString(),
                    EmpName = row["EmpName"].ToString(),
                    EmpPhoto = row["EmpPhoto"].ToString()

                };
            }
        }

        public int UpdateNotification(NotificationsDTOs ntd)
        {
            Notification ntf = NotificationRequestFormatter.ConvertRespondentInfoFromDTO(ntd);

            return (_unitOfWork.NotificationRepository.Update(ntf));
        }
    }
}
