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
    public class TaxDetailService : ITaxDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TaxDetailService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }
        public IEnumerable<TaxDetailDTO> GetAllTaxDetails()
        {
            IEnumerable<PayrollTaxDetail> modelDatas = _unitOfWork.TaxDetailRepository.All().ToList();
            return TaxDetailResponseFormatter.GetAllTaxSetupDTO(modelDatas);
        }
        public void InsertIntoTaxDetail(TaxDetailDTO Record)
        {
            PayrollTaxDetail Data = TaxDetailRequestFormatter.ConvertRespondentInfoFromDTO(Record);
            _unitOfWork.TaxDetailRepository.Create(Data);
        }

        public TaxDetailDTO GetTaxDetailById(int Id)
        {
            PayrollTaxDetail TaxSetup = _unitOfWork.TaxDetailRepository.GetById(Id);




            TaxDetailDTO TaxSetupDTO = TaxDetailRequestFormatter.ConvertRespondentInfoToDTO(TaxSetup);
            return TaxSetupDTO;
        }
        public int UpdateTaxDetail(TaxDetailDTO Record) 
        {

            PayrollTaxDetail record = TaxDetailRequestFormatter.ConvertRespondentInfoFromDTO(Record);
            var response = _unitOfWork.TaxDetailRepository.Update(record);
            _unitOfWork.Save();
            return record.MasterId;
        }

        public IEnumerable<TaxDetailDTO> GetAllTaxDetailsByMasterId(int MasterId)
        {
            IEnumerable<PayrollTaxDetail> modelDatas = _unitOfWork.TaxDetailRepository.All().Where(x => x.MasterId == MasterId).ToList();
            return TaxDetailResponseFormatter.GetAllTaxSetupDTO(modelDatas);
        }
    }
}
