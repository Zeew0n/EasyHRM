using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.Repository;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Implementations
{
    public class ServiceEventGroupService : IServiceEventService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServiceEventGroupService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public IEnumerable<ServiceEventGroupDTO> GetServiceEventList()
        {
            IEnumerable<ServiceEventGroup> modelDatas = _unitOfWork.ServiceEventRepository.All().ToList();
            return ServiceEventGroupResponseFormatter.ModelData(modelDatas);
        }
    }
}
