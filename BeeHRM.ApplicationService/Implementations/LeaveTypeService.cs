using BeeHRM.ApplicationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using BeeHRM.Repository;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.ApplicationService.RequestFormatters;

namespace BeeHRM.ApplicationService.Implementations
{
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LeaveTypeService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public IEnumerable<LeaveTypeDTO> GetLeaveTypes()
        {
            IEnumerable<LeaveType> modelDatas = _unitOfWork.LeaveTypeRepository.All();
            return LeaveTypeResponseFormatter.ModelData(modelDatas);
        }

        public bool LeaveTypeExists(LeaveTypeDTO newLeaveType)
        {
            LeaveType leave = LeaveTypeRequestFormatter.ConvertRespondentInfoFromDTO(newLeaveType);
            return _unitOfWork.LeaveTypeRepository.Exists(leave);
        }

        public LeaveTypeDTO InsertLeaveType(LeaveTypeDTO leaveType)
        {
            LeaveType leave = LeaveTypeRequestFormatter.ConvertRespondentInfoFromDTO(leaveType);
            return LeaveTypeRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.LeaveTypeRepository.Create(leave));
        }

        public LeaveTypeDTO GetLeaveTypeById(int leaveTypeId)
        {
            var leaveType = _unitOfWork.LeaveTypeRepository.All().Where(x => x.LeaveTypeId == leaveTypeId).FirstOrDefault();
            return LeaveTypeRequestFormatter.ConvertRespondentInfoToDTO(leaveType);
        }
        public int UpdateLeaveType(LeaveTypeDTO editLeaveType)
        {
            var leaveType = LeaveTypeRequestFormatter.ConvertRespondentInfoFromDTO(editLeaveType);
            int response = _unitOfWork.LeaveTypeRepository.Update(leaveType);
            //_unitOfWork.Save();
            return response;
        }

        public void DeleteLeaveType(int leaveTypeId)
        {
            _unitOfWork.LeaveTypeRepository.Delete(leaveTypeId);
            _unitOfWork.Save();
        }
    }
}
