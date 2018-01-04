using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.RequestFormatters
{
    public class PayrollInsuranceRequestFormatter
    {
        public static PayrollInsurancePremium ConvertRespondentInfoFromDTO(PayrollInsurancePremiumDTO empDocDTO)
        {

            Mapper.CreateMap<PayrollInsurancePremiumDTO, PayrollInsurancePremium>().ConvertUsing(
                      m =>
                      {
                          return new PayrollInsurancePremium
                          {
                              IsuranceClaimId = m.IsuranceClaimId,
                              InsuranceCompanyId = m.InsuranceCompanyId,
                              InsuredAmount = m.InsuredAmount,
                              PremiumAmount = m.PremiumAmount,
                              AmountType = m.AmountType,
                              StartDate = Convert.ToDateTime(m.StartDate),
                              EndDate = Convert.ToDateTime(m.EndDate),
                              InsuranceClaimFyId = m.InsuranceClaimFyId,
                              EmpCode = m.EmpCode,
                              InsurancePolicyNumber = m.InsurancePolicyNumber
                          };

                      });
            return Mapper.Map<PayrollInsurancePremiumDTO, PayrollInsurancePremium>(empDocDTO);
        }
        public static PayrollInsurancePremiumDTO ConvertRespondentInfoToDTO(PayrollInsurancePremium emp)
        {

            Mapper.CreateMap<PayrollInsurancePremium, PayrollInsurancePremiumDTO>().ConvertUsing(
                      m =>
                      {
                          return new PayrollInsurancePremiumDTO
                          {
                              IsuranceClaimId = m.IsuranceClaimId,
                              InsuranceCompanyId = m.InsuranceCompanyId,
                              InsuredAmount = m.InsuredAmount,
                              PremiumAmount = m.PremiumAmount,
                              AmountType = m.AmountType,
                              StartDate = m.StartDate,
                              EndDate = m.EndDate,
                              InsuranceClaimFyId = m.InsuranceClaimFyId,
                              EmpCode = m.EmpCode,
                              InsurancePolicyNumber = m.InsurancePolicyNumber
                          };

                      });
            return Mapper.Map<PayrollInsurancePremium, PayrollInsurancePremiumDTO>(emp);
        }
    }
}
