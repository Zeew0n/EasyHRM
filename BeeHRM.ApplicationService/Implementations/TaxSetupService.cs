using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.Repository;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace BeeHRM.ApplicationService.Implementations
{
    public class TaxSetupService : ITaxSetupService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TaxSetupService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }


        public IEnumerable<TaxSetupDTO> GetAllTaxSetup()
        {
            IEnumerable<PayrollTaxSetup> modelDatas = _unitOfWork.TaxSetupRepository.All().ToList();
            return TaxSetupResponseFormatter.GetAllTaxSetupDTO(modelDatas);
        }
        public void InsertIntoTaxSetup(TaxSetupDTO Record)
        {
            PayrollTaxSetup Data = TaxSetupRequestFormatter.ConvertRespondentInfoFromDTO(Record);
            _unitOfWork.TaxSetupRepository.Create(Data);
        }

        public TaxSetupDTO GetTaxSetupById(int Id)
        {
            PayrollTaxSetup TaxSetup = _unitOfWork.TaxSetupRepository.GetById(Id);
            TaxSetupDTO TaxSetupDTO = TaxSetupRequestFormatter.ConvertRespondentInfoToDTO(TaxSetup);
            return TaxSetupDTO;
        }
        public int UpdateTaxSetup(TaxSetupDTO Record)
        {

            PayrollTaxSetup record = TaxSetupRequestFormatter.ConvertRespondentInfoFromDTO(Record);
            var response = _unitOfWork.TaxSetupRepository.Update(record);
            _unitOfWork.Save();
            return record.MasterId;
        }
    }
}
