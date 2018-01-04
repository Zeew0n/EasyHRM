using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BeeHRM.ApplicationService.Interfaces
{
    public interface INotification
    {
        NotificationsDTOs InsertNotification(NotificationsDTOs ntd);

        IEnumerable<NotificationsViewModel> Notificationlist(int id);
        IEnumerable<NotificationsViewModel> SingleNotification(int id);



        int UpdateNotification(NotificationsDTOs ntd);
    }
}
