using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IAttendanceDeviceService
    {
        IEnumerable<AttendanceDeviceDTO> GetAttendanceDeviceList();
        AttendanceDeviceDTO InsertAttendanceDevice(AttendanceDeviceDTO data);
        AttendanceDeviceDTO GetAttendanceDeviceByID(int rankId);
        int UpdateAttendanceDevice(AttendanceDeviceDTO data);
    }
}
