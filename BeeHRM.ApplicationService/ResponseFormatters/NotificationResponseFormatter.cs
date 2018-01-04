using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository;
using AutoMapper;


namespace BeeHRM.ApplicationService.ResponseFormatters
{
   public  class NotificationResponseFormatter
    {

       public static IEnumerable<NotificationsDTOs> ModelData(IEnumerable<Notification> modelData)
       {

                        
           return Mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationsDTOs>>(modelData);
       }
    }
}
