using BeeHRM.ApplicationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using BeeHRM.ApplicationService.RequestFormatters;

namespace BeeHRM.ApplicationService.Implementations
{
    public class LeaveService : ILeaveService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LeaveService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }
        public LeaveAssignedDTO InsertLeaveAssigned(LeaveAssignedDTO lAsignDTO)
        {
            LeaveAssigned la = _unitOfWork.LeaveAssignedRepository.Create(LeaveRequestFormatter.ConvertRespondentInfoFromDTO(lAsignDTO));
            return LeaveRequestFormatter.ConvertRespondentInfoToDTO(la);
        }
    }
}
