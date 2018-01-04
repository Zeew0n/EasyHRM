using BeeHRM.ApplicationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using BeeHRM.Repository;
using BeeHRM.ApplicationService.ResponseFormatters;

namespace BeeHRM.ApplicationService.Implementations
{
    public class InterestGainService : IInterestGainService
    {
        private readonly IUnitOfWork _unitOfWork;
        public InterestGainService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }
        public IEnumerable<PayrollIntrestGainDTO> GetInterestGainList(int? EmpId, int? OfficeId)
        {

            IEnumerable<PayrollIntrestGain> modelDatas = _unitOfWork.InterestGainRepository.All().ToList();
            IEnumerable<PayrollIntrestGain> officeFiltered = modelDatas.Where(x => x.Employee.EmpOfficeId == OfficeId);
            IEnumerable<PayrollIntrestGain> employeeFiltered = modelDatas.Where(x => x.EmpCode == EmpId);
            IEnumerable<PayrollIntrestGain> returnRecord = modelDatas.Where(x => x.EmpCode == EmpId && x.Employee.EmpOfficeId == OfficeId);
            if (OfficeId != null && EmpId != null)
            {
                return PayrollInterestGainResponseFormatter.PayrollInterestDbListToDTOList(returnRecord);
            }
            else if (EmpId == null && OfficeId != null)
            {
                return PayrollInterestGainResponseFormatter.PayrollInterestDbListToDTOList(officeFiltered);
            }
            else if (EmpId != null && OfficeId == null)
            {
                return PayrollInterestGainResponseFormatter.PayrollInterestDbListToDTOList(employeeFiltered);
            }
            else
            {
                return PayrollInterestGainResponseFormatter.PayrollInterestDbListToDTOList(modelDatas);
            }
        }

        public PayrollIntrestGainDTO GetInterestGainByEmpCode(int EmpCode)
        {
            PayrollIntrestGain domainRecord = _unitOfWork.InterestGainRepository.Get(x => x.EmpCode == EmpCode).FirstOrDefault();
            return PayrollInterestGainResponseFormatter.PayrollInterestDbToDTO(domainRecord);
        }
    }
}
