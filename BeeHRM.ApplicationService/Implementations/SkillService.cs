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
    class SkillService:ISkillService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SkillService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();

        }

        public void DeleteSkill(int id)
        {
            _unitOfWork.SkillRepository.Delete(id);
            _unitOfWork.Save();
        }

        public IEnumerable<SkillDTO> GetAllSkill()
        {
            return SkillReponseFormatter.ModelData(_unitOfWork.SkillRepository.All());
        }

        public SkillDTO GetSkillById(int id)
        {
            return SkillRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.SkillRepository.GetById(id));
        }

        public SkillDTO InsertSkill(SkillDTO data)
        {
            Skill dataToInsert = new Skill();
            dataToInsert = SkillRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return SkillRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.SkillRepository.Create(dataToInsert));
        }

        public int UpdateSkill(SkillDTO data)
        {
            Skill dataToUpdate = SkillRequestFormatter.ConvertRespondentInfoFromDTO(data);
            var res = _unitOfWork.SkillRepository.Update(dataToUpdate);
            return res;
        }
    }
}
