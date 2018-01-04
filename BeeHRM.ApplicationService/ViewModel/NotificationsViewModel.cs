using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ViewModel
{
   public class NotificationsViewModel
    {
        public string NotificationID { get; set; }
        public string ReceiverID { get; set; }
        public string ReceiverType { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Date { get; set; }
        public string  ReadDate { get; set; }
        public string DetailUrl { get; set; }
        public string EmpName { get; set; }
        public string EmpPhoto { get; set; }
        public string TimeAgo { get; set; }
    }
}
