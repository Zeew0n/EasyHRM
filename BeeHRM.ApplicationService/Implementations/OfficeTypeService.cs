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
    public class OfficeTypeService:IOfficeTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OfficeTypeService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public void DeleteOfficeType(int id)
        {
            _unitOfWork.OfficeTypeRepository.Delete(id);
            _unitOfWork.Save();
        }

        public OfficeTypeDTO GetOfficeTypeById(int id)
        {
            return OfficeTypeRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.OfficeTypeRepository.GetById(id));
        }

        public IEnumerable<OfficeTypeDTO> GetOfficeTypes()
        {
            IEnumerable<OfficeType> modelDatas = _unitOfWork.OfficeTypeRepository.All().ToList();
            return OfficeTypeResponseFormatter.ModelData(modelDatas);
        }

        public OfficeTypeDTO InsertOfficeType(OfficeTypeDTO data)
        {
            OfficeType dataToInsert = new OfficeType();
            dataToInsert = OfficeTypeRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return OfficeTypeRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.OfficeTypeRepository.Create(dataToInsert));
        }

        public int UpdateOfficeType(OfficeTypeDTO data)
        {
            OfficeType dataToUpdate = new OfficeType();
            dataToUpdate = OfficeTypeRequestFormatter.ConvertRespondentInfoFromDTO(data);
            var response = _unitOfWork.OfficeTypeRepository.Update(dataToUpdate);
            return response;
        }
    }
}
