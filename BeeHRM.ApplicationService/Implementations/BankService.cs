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
    public class BankService : IBankService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BankService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();

        }

        public void DeleteBankById(int id)
        {
            _unitOfWork.BankRepository.Delete(id);
            _unitOfWork.Save();
        }

        public IEnumerable<BankDTO> GetBankList()
        {
            return BankResponseFormatter.ModelData(_unitOfWork.BankRepository.All());
        }

        public BankDTO GetBankId(int id)
        {
            return BankRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.BankRepository.GetById(id));
        }

        public BankDTO InsertBank(BankDTO data)
        {
            Bank dataToInsert = new Bank();
            dataToInsert = BankRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return BankRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.BankRepository.Create(dataToInsert));
        }

        public int UpdateBank(BankDTO data)
        {
            Bank dataToUpdate = BankRequestFormatter.ConvertRespondentInfoFromDTO(data);
            var res = _unitOfWork.BankRepository.Update(dataToUpdate);
            return res;
        }
    }
}
