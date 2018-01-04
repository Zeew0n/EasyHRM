using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.RequestFormatters
{
   public  class AttendanceDeviceRequestFormatter
    {
        public static AttendaceDevice ConvertRespondentInfoFromDTO(AttendanceDeviceDTO attendanceDeviceDTO)
        {

            Mapper.CreateMap<AttendanceDeviceDTO, AttendaceDevice>().ConvertUsing(
                      m =>
                      {
                          return new AttendaceDevice
                          {DeviceId=m.DeviceId,
                          DeviceName=m.DeviceName,
                          DeviceIP=m.DeviceIP,
                          DevicePort=m.DevicePort,
                          DevicePassword=m.DevicePassword,
                          DeviceMachineNo=m.DeviceMachineNo,
                          DeviceFetchStartTime=m.DeviceFetchStartTime,
                          DeviceFetchEndTime=m.DeviceFetchEndTime,
                          DeviceLastImportDate=m.DeviceLastImportDate,
                          DeviceStatus=m.DeviceStatus,
                          DeviceDataDelete=m.DeviceDataDelete,
                          DeviceType=m.DeviceType,


                          };

                      });
            return Mapper.Map<AttendanceDeviceDTO, AttendaceDevice>(attendanceDeviceDTO);
        }

        public static AttendanceDeviceDTO ConvertRespondentInfoToDTO(AttendaceDevice attendancedevice)
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
            return Mapper.Map<AttendaceDevice, AttendanceDeviceDTO>(attendancedevice);
        }
    }
}
