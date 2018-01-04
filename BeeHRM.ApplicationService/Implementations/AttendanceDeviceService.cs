using BeeHRM.ApplicationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.Repository;
using BeeHRM.ApplicationService.ResponseFormatters;

namespace BeeHRM.ApplicationService.Implementations
{
    public class AttendanceDeviceService : IAttendanceDeviceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendanceDeviceService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }
        public AttendanceDeviceDTO GetAttendanceDeviceByID(int attendanceDeviceId)
        {
            AttendanceDeviceDTO attendanceDevice = AttendanceDeviceRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.AttendanceDeviceRepository.GetById(attendanceDeviceId));
            return attendanceDevice;
        }

        public IEnumerable<AttendanceDeviceDTO> GetAttendanceDeviceList()
        {
            IEnumerable<AttendaceDevice> modelDatas = _unitOfWork.AttendanceDeviceRepository.All().ToList();
            return AttendanceDeviceResponseFormatter.ModelData(modelDatas);
        }

      
        public AttendanceDeviceDTO InsertAttendanceDevice(AttendanceDeviceDTO data)
        {
            AttendaceDevice attendancedevice = AttendanceDeviceRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return AttendanceDeviceRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.AttendanceDeviceRepository.Create(attendancedevice));
        }

        public int UpdateAttendanceDevice(AttendanceDeviceDTO data)
        {

            AttendaceDevice attendanceDevice = AttendanceDeviceRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return (_unitOfWork.AttendanceDeviceRepository.Update(attendanceDevice));
        }
    }
}
