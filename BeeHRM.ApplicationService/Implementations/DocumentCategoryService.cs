using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.RequestFormatters;
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
    public class DocumentCategoryService : IDocumentCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DocumentCategoryService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public DocumentCategoryDTO InsertDocumentCategory(DocumentCategoryDTO Record)
        {
            DocumentCategory ReturnRecord = DocumentCategoryRequestFormatter.ConvertRespondentInfoFromDTO(Record);
            return DocumentCategoryRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.DocumentCategoryRepository.Create(ReturnRecord));
        }

        public DocumentCategoryDTO GetDocumentCategoryById(int Id)
        {
            return DocumentCategoryRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.DocumentCategoryRepository.GetById(Id));
        }

        public int UpdateDocumentCategory(DocumentCategoryDTO Record)
        {
            DocumentCategory ReturnRecord = DocumentCategoryRequestFormatter.ConvertRespondentInfoFromDTO(Record);
            var res = _unitOfWork.DocumentCategoryRepository.Update(ReturnRecord);
            return res;
        }

        public IEnumerable<DocumentCategoryDTO> GetAllDocumentCategory()
        {
            var res = _unitOfWork.DocumentCategoryRepository.All();
            return DocumentCategoryResponseFormatter.DocumentCategoryDbListToDTOList(res);
        }

        public void DeleteDocumentCategory(int id)
        {
            _unitOfWork.DocumentCategoryRepository.Delete(id);

        }
    }
}
