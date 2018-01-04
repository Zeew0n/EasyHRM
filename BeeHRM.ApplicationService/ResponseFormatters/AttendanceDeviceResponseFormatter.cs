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
   public  class AttendanceDeviceResponseFormatter
    {
        public static IEnumerable<AttendanceDeviceDTO> ModelData(IEnumerable<AttendaceDevice> modelData)
        {

            Mapper.CreateMap<AttendaceDevice, AttendanceDeviceDTO>().ConvertUsing(

                m =>
                {
                    return new AttendanceDeviceDTO
                    {
                        DeviceId = m.DeviceId,
                        DeviceName = m.DeviceName,
                        DeviceIP = m.DeviceIP,
                        DevicePort = m.DevicePort,
                        DevicePassword = m.DevicePassword,
                        DeviceMachineNo = m.DeviceMachineNo,
                        DeviceFetchStartTime = m.DeviceFetchStartTime,
                        DeviceFetchEndTime = m.DeviceFetchEndTime,
                        DeviceLastImportDate = m.DeviceLastImportDate,
                        DeviceStatus = m.DeviceStatus,
                        DeviceDataDelete = m.DeviceDataDelete,
                        DeviceType = m.DeviceType,

                    };
                });
            return Mapper.Map<IEnumerable<AttendaceDevice>, IEnumerable<AttendanceDeviceDTO>>(modelData);
        }
    }
}
