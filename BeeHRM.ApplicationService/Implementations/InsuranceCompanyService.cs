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
    public class InsuranceCompanyService : IInsuranceCompanyService
    {
        public readonly IUnitOfWork _unitOfWork;
        public InsuranceCompanyService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public InsuranceCompanyDTO InsertInsuranceCompany(InsuranceCompanyDTO Record)
        {
            InsuranceCompany ReturnRecord = InsuranceCompanyRequestFormatter.ConvertRespondentInfoFromDTO(Record);
            return InsuranceCompanyRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.InsuranceCompanyRepository.Create(ReturnRecord));
        }

        public InsuranceCompanyDTO GetInsuranceCompanyById(int Id)
        {
            return InsuranceCompanyRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.InsuranceCompanyRepository.GetById(Id));
        }

        public int UpdateInsuranceCompany(InsuranceCompanyDTO Record)
        {
            InsuranceCompany ReturnRecord = InsuranceCompanyRequestFormatter.ConvertRespondentInfoFromDTO(Record);
            var res = _unitOfWork.InsuranceCompanyRepository.Update(ReturnRecord);
            return res;
        }

        public IEnumerable<InsuranceCompanyDTO> GetAllInsuranceCompany()
        {
            var res = _unitOfWork.InsuranceCompanyRepository.All();
            return InsuranceCompanyResponseFormatter.InsuranceCompanyDbListToDTOList(res);
        }

        public void DeleteInsuranceCompany(int id)
        {
            _unitOfWork.InsuranceCompanyRepository.Delete(id);

        }
    }
}
