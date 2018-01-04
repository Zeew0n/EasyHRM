using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.Repository;
using BeeHRM.ApplicationService.RequestFormatters;

namespace BeeHRM.ApplicationService.Implementations
{
    public class EducationLevelService: IEducationLevelService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EducationLevelService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public void DeleteEducationLevel(int id)
        {
            _unitOfWork.EducationLevelRepository.Delete(id);
            _unitOfWork.Save();
        }

        public IEnumerable<EducationLevelDTO> GetEducationLevel()
        {
            return EducationLevelResponseFormatter.ModelData(_unitOfWork.EducationLevelRepository.All());
        }

        public EducationLevelDTO GetEducationLevelById(int id)
        {
            return EducationLevelRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.EducationLevelRepository.GetById(id));
        }

        public EducationLevelDTO InsertEducationLevel(EducationLevelDTO data)
        {
            EducationLevel dataToInsert = EducationLevelRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return EducationLevelRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.EducationLevelRepository.Create(dataToInsert));
        }

        public int UpdateEducationLevel(EducationLevelDTO data)
        {
            EducationLevel dataToUpdate = EducationLevelRequestFormatter.ConvertRespondentInfoFromDTO(data);
            var res = _unitOfWork.EducationLevelRepository.Update(dataToUpdate);
            _unitOfWork.Save();
            return res;
        }
    }
}
