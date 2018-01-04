using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ResponseFormatters
{
    public class AttEmployeeDailyLogResponseFormatter
    {
        public static IEnumerable<AttEmployeeLogDTO> ModelData(IEnumerable<AttEmployeeLog> modelData)
        {

            Mapper.CreateMap<AttEmployeeLog, AttEmployeeLogDTO>().ConvertUsing(

                m =>
                {
                    return new AttEmployeeLogDTO
                    {
                        logDate = m.logDate,
                        logEmpCode = m.logEmpCode,
                        logMethodId = m.logMethodId,
                        logIpAddress = m.logIpAddress,
                        logTime = m.logTime,
                        logTypeID = m.logTypeID,
                        LogId = m.LogId
                    };
                }
                );
            return Mapper.Map<IEnumerable<AttEmployeeLog>, IEnumerable<AttEmployeeLogDTO>>(modelData);
        }

    }


}
