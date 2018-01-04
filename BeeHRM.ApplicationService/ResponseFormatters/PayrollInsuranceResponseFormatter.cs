using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ResponseFormatters
{
    public class PayrollInsuranceResponseFormatter
    {
        public static PayrollInsurancePremiumDTO PayrollInsuranceDbToDTO(PayrollInsurancePremium ModelData)
        {
            PayrollInsurancePremiumDTO Record = new PayrollInsurancePremiumDTO
            {
                IsuranceClaimId = ModelData.IsuranceClaimId,
                InsuranceCompanyId = ModelData.InsuranceCompanyId,
                InsuredAmount = ModelData.InsuredAmount,
                PremiumAmount = ModelData.PremiumAmount,
                AmountType = ModelData.AmountType,
                StartDate = ModelData.StartDate,
                EndDate = ModelData.EndDate,
                InsuranceClaimFyId = ModelData.InsuranceClaimFyId,
                EmpCode = ModelData.EmpCode,
                InsurancePolicyNumber = ModelData.InsurancePolicyNumber,
                Employee = new EmployeeDTO
                {
                    EmpCode = ModelData.Employee.EmpCode,
                    EmpName = ModelData.Employee.EmpName,
                },
                InsuranceCompany = new InsuranceCompanyDTO
                {
                    Id = ModelData.InsuranceCompany.Id,
                    CompanyName = ModelData.InsuranceCompany.CompanyName
                }

            };
            return Record;
        }

        public static IEnumerable<PayrollInsurancePremiumDTO> PayrollInsuranceDbListToDTOList(IEnumerable<PayrollInsurancePremium> ModelData)
        {
            List<PayrollInsurancePremiumDTO> ReturnRecord = new List<PayrollInsurancePremiumDTO>();
            foreach (PayrollInsurancePremium Row in ModelData)
            {
                PayrollInsurancePremiumDTO Record = new PayrollInsurancePremiumDTO
                {

                    IsuranceClaimId = Row.IsuranceClaimId,
                    InsuranceCompanyId = Row.InsuranceCompanyId,
                    InsuredAmount = Row.InsuredAmount,
                    PremiumAmount = Row.PremiumAmount,
                    AmountType = Row.AmountType,
                    StartDate = Row.StartDate,
                    EndDate = Row.EndDate,
                    InsuranceClaimFyId = Row.InsuranceClaimFyId,
                    EmpCode = Row.EmpCode,
                    InsurancePolicyNumber = Row.InsurancePolicyNumber,
                    Employee = new EmployeeDTO
                    {
                        EmpCode = Row.Employee.EmpCode,
                        EmpName = Row.Employee.EmpName,
                    },
                    InsuranceCompany = new InsuranceCompanyDTO
                    {
                        Id = Row.InsuranceCompany.Id,
                        CompanyName = Row.InsuranceCompany.CompanyName
                    }

                };
                ReturnRecord.Add(Record);
            }
            return ReturnRecord;


        }
    }
}
