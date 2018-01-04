using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Implementations
{
    public class RemoteAreaService: IRemoteAreaService
    {
        private readonly IUnitOfWork _unitOfWork;
       

        public RemoteAreaService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
            
        }

        public IEnumerable<RemoteAreasDTO> GetRemoteList()
        {
            return RemoteAreaResponseFormatter.ModelData(_unitOfWork.RemoteAreaRepository.All());
        }
    }
}
