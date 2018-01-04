using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
   public class NotificationsDTOs
    {
        public long NotificationId { get; set; }
        public int NotificationReceiverId { get; set; }
        public string NotificationReceiverType { get; set; }
        public string NotificationMessage { get; set; }
        public string NotificationSubject { get; set; }
        public Nullable<System.DateTime> NotificationDate { get; set; }
        public Nullable<System.DateTime> NotificationReadDate { get; set; }
        public string NotificationDetailURL { get; set; }
    }

}
