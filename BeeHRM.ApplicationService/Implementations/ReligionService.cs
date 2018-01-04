using BeeHRM.ApplicationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;

namespace BeeHRM.ApplicationService.Implementations
{
    public class ReligionService : IReligionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReligionService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public ReligionDTO GetReligionById(int id)
        {
            var rel = _unitOfWork.ReligionRepository.All().Where(x=> x.ReligionId == id);
            ReligionDTO r = new ReligionDTO();
            r.ReligionId = rel.FirstOrDefault().ReligionId;
            r.ReligionId = rel.FirstOrDefault().ReligionId;
            return r;


        }

        public IEnumerable<ReligionDTO> GetReligionList()
        {
            return ReligionResponseFormatter.ModelData(_unitOfWork.ReligionRepository.All());
        }
    }
}
