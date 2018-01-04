using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.Repository;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System.Collections.Generic;


namespace BeeHRM.ApplicationService.Implementations
{
    public class EthnicityService : IEthnicityService
    {
        private readonly IUnitOfWork _unitOfWork;
        // private EthnicityService _ethnicity;



        public EthnicityService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
            //  _ethnicity = new Ethnicity();
        }
        public IEnumerable<EthnicityDTO> GetEthnicityList()
        {
            IEnumerable<Ethnicity> modelDatas = _unitOfWork.EthnicityRepository.All();
            return EthnicityResponseFormatter.ModelData(modelDatas);
        }

        public EthnicityDTO InsertEthnicity(EthnicityDTO data)
        {
            Ethnicity dataToInsert = new Ethnicity();
            dataToInsert = EthnicityRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return EthnicityRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.EthnicityRepository.Create(dataToInsert));
        }

        public EthnicityDTO GetEthnicityById(int id)
        {
            return EthnicityRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.EthnicityRepository.GetById(id));
        }

        public int UpdateEthnicity(EthnicityDTO data)
        {
            Ethnicity dataToUpdate = EthnicityRequestFormatter.ConvertRespondentInfoFromDTO(data);
            int res = _unitOfWork.EthnicityRepository.Update(dataToUpdate);
            _unitOfWork.Save();
            return res;
        }

        public void DeleteEthnicityById(int id)
        {
            _unitOfWork.EthnicityRepository.Delete(id);
            _unitOfWork.Save();
        }
    }
}
