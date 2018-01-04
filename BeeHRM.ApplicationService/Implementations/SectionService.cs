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
    
    public class SectionService : ISectionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SectionService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public void DeleteSection(int sectionId)
        {
            _unitOfWork.SectionRepository.Delete(sectionId);
            _unitOfWork.Save();
        }

        public SectionDTO GetSectionById(int sectionId)
        {
            return SectionRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.SectionRepository.GetById(sectionId));
        }

        public IEnumerable<SectionDTO> GetSectionList()
        {
            IEnumerable<Section> modelDatas = _unitOfWork.SectionRepository.All().ToList();
            return SectionResponseFormatter.ModelData(modelDatas);
        }

        public SectionDTO InsertSection(SectionDTO data)
        {
            Section dataToInsert = SectionRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return SectionRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.SectionRepository.Create(dataToInsert));
        }

        public int UpdateSectionById(SectionDTO data)
        {
            Section dataToUpdate = SectionRequestFormatter.ConvertRespondentInfoFromDTO(data);
            var response = _unitOfWork.SectionRepository.Update(dataToUpdate);
            return response;
        }
    }
}
