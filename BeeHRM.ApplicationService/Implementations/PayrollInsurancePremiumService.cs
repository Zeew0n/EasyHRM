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
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Implementations
{
    public class PayrollInsurancePremiumService : IPayrollInsurancePremiumService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PayrollInsurancePremiumService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }
        public IEnumerable<PayrollInsurancePremiumDTO> GetAllPayrollInsurancePremium()
        {
            var res = _unitOfWork.PayrollInsurancePremiumRepository.All();
            return PayrollInsuranceResponseFormatter.PayrollInsuranceDbListToDTOList(res);
        }
        public PayrollInsurancePremiumDTO InsertPayrollInsurancePremium(PayrollInsurancePremiumDTO Record)
        {
            PayrollInsurancePremium ReturnRecord = PayrollInsuranceRequestFormatter.ConvertRespondentInfoFromDTO(Record);

            return PayrollInsuranceRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.PayrollInsurancePremiumRepository.Create(ReturnRecord));
        }

        public PayrollInsurancePremiumDTO GetOnePayrollInsurancePremium(int Id)
        {
            PayrollInsurancePremium Record = _unitOfWork.PayrollInsurancePremiumRepository.Get(x => x.IsuranceClaimId == Id).FirstOrDefault();
            PayrollInsurancePremiumDTO ReturnRecord = ResponseFormatters.PayrollInsuranceResponseFormatter.PayrollInsuranceDbToDTO(Record);
            return ReturnRecord;

        }

        public void UpdatePayrollInsurancePremium(PayrollInsurancePremiumDTO Record)
        {
            PayrollInsurancePremium ReturnRecord = PayrollInsuranceRequestFormatter.ConvertRespondentInfoFromDTO(Record);
            _unitOfWork.PayrollInsurancePremiumRepository.Update(ReturnRecord);
        }
        //search not used
        //public IEnumerable<PayrollInsurancePremiumDTO> GetPayrollInsurancePremiumInformationByEmpCode(int? empcode)
        //{
        //    var res = _unitOfWork.PayrollInsurancePremiumRepository.All();
        //    return PayrollInsuranceResponseFormatter.PayrollInsuranceDbListToDTOList(res);
        //}

        public IEnumerable<SelectListItem> GetInsuranceCompanySelectList()
        {
            IEnumerable<InsuranceCompany> Modellist = _unitOfWork.InsuranceCompanyRepository.All().ToList();
            List<SelectListItem> ReturnRecord = new List<SelectListItem>();
            foreach (var row in Modellist)
            {
                ReturnRecord.Add(new SelectListItem
                {
                    Text = row.CompanyName,
                    Value = row.Id.ToString()
                });
            }
            return ReturnRecord;
        }

        //public IEnumerable<PayrollInsurancePremiumDTO> GetEmployeeByEmpCode(int empcode)
        //{
        //    //List<PayrollInsurancePremium> Record = new List<PayrollInsurancePremium>();
        //    var Record = _unitOfWork.PayrollInsurancePremiumRepository.Get(x => x.EmpCode == empcode).ToList();
        //    IEnumerable<PayrollInsurancePremiumDTO> ReturnRecord = PayrollInsuranceResponseFormatter.PayrollInsuranceDbListToDTOList(Record);
        //    return ReturnRecord;
        //}

        public int GetEmpCode(int Id)
        {
            int EmpCode = _unitOfWork.PayrollInsurancePremiumRepository.Get(x => x.IsuranceClaimId == Id).Select(x => x.EmpCode).FirstOrDefault();
            return EmpCode;
        }

        public void DeletePayrollInsurancePremium(int id)
        {
            _unitOfWork.PayrollInsurancePremiumRepository.Delete(id);
        }

        public IEnumerable<PayrollInsurancePremiumDTO> GetInsuranceInfobyEmpCode(int Empcode, int fyid)
        {
            var res = _unitOfWork.PayrollInsurancePremiumRepository.All().Where(x => x.EmpCode == Empcode ).Where(x=>x.InsuranceClaimFyId==fyid);
            return PayrollInsuranceResponseFormatter.PayrollInsuranceDbListToDTOList(res);
        }
    }
}
