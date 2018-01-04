using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace BeeHRM.ApplicationService.Implementations
{
    public class ServiceEventSubGroupService : IServiceEventSubGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServiceEventSubGroupService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public IEnumerable<ServiceEventSubGroupDTO> GetSubGroupById(int v)
        {
            var list = _unitOfWork.ServiceEventSubGroup.All().Where(x => x.ServiceEventGroupId == v);
            return ServiceEventSubGroupServiceResponseFormatter.ModelData(list);
        }

    }
}
