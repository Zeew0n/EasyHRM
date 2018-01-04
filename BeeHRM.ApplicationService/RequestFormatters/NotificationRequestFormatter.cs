using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository;
using AutoMapper;

namespace BeeHRM.ApplicationService.RequestFormatters
{
    public static class NotificationRequestFormatter
    {
        public static Notification ConvertRespondentInfoFromDTO(this NotificationsDTOs ntd)
        {
            Mapper.CreateMap<NotificationsDTOs, Notification>().ConvertUsing(
                m =>
                {
                    return new Notification
                    {
                        NotificationDate = m.NotificationDate,
                        NotificationDetailURL = m.NotificationDetailURL,
                        NotificationId = m.NotificationId,
                        NotificationMessage = m.NotificationMessage,
                        NotificationSubject = m.NotificationSubject,
                        NotificationReceiverType = m.NotificationReceiverType,
                        NotificationReadDate = m.NotificationReadDate,
                        NotificationReceiverId = m.NotificationReceiverId
                    };
                });
            return Mapper.Map<NotificationsDTOs, Notification>(ntd);
       
        }



        public static NotificationsDTOs  ConvertRespondentToDTO(this  Notification ntd)
        {
            Mapper.CreateMap<Notification, NotificationsDTOs>().ConvertUsing(
               m =>
               {

                   return new  NotificationsDTOs
                   {

                       NotificationDate = m.NotificationDate,
                       NotificationDetailURL = m.NotificationDetailURL,
                       NotificationId = m.NotificationId,
                       NotificationMessage = m.NotificationMessage,
                       NotificationSubject = m.NotificationSubject,
                       NotificationReceiverType = m.NotificationReceiverType,
                       NotificationReadDate = m.NotificationReadDate,
                       NotificationReceiverId = m.NotificationReceiverId
                   };
               });
            return Mapper.Map<Notification, NotificationsDTOs>(ntd);
        }


      
    }
}
