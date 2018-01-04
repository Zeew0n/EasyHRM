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
  public   class AttDailyResponseFormatter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginData"></param>
        /// <returns></returns>
        public  AttDailyDTOs GetAttDailyDTOs (AttDaily loginData)
        {

            Mapper.CreateMap<AttDaily, AttDailyDTOs>().ConvertUsing(
                
                m =>
                {
                    return new AttDailyDTOs
                    {
                        DailyCreatedAt = m.DailyCreatedAt,
                        DailyStatus = m.DailyStatus
                    };

                }
                );
            return Mapper.Map <AttDaily, AttDailyDTOs> (loginData);
        }
    }
}
