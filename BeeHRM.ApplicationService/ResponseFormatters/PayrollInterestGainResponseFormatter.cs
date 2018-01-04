using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ResponseFormatters
{
    public class PayrollInterestGainResponseFormatter
    {
        //public static IEnumerable<PayrollIntrestGainDTO> GetAllTPayrollInterestGainDTO(IEnumerable<PayrollIntrestGain> Record)
        //{
        //    Mapper.CreateMap<PayrollIntrestGain, PayrollIntrestGainDTO>().ConvertUsing(m =>
        //    {
        //        return new PayrollIntrestGainDTO
        //        {
        //            Id = m.Id,
        //            EmpCode = m.EmpCode,
        //            CustomerId = m.CustomerId,
        //            EmployeeName = m.Employee.EmpName,
        //            FyId = m.FyId,
        //            MonthIndex = m.MonthIndex
        //        };
        //    });
        //    return Mapper.Map<IEnumerable<PayrollIntrestGain>, IEnumerable<PayrollIntrestGainDTO>>(Record);
        //}

        public static PayrollIntrestGainDTO PayrollInterestDbToDTO(PayrollIntrestGain ModelData)
        {
            PayrollIntrestGainDTO Record = new PayrollIntrestGainDTO
            {
                Id = ModelData.Id,
                EmpCode = ModelData.EmpCode,
                CustomerId = ModelData.CustomerId,
                EmployeeName = ModelData.Employee.EmpName,
                FyId = ModelData.FyId,
                MonthIndex = ModelData.MonthIndex
                //IsuranceClaimId = ModelData.IsuranceClaimId,
                //InsuranceCompanyId = ModelData.InsuranceCompanyId,
                //InsuredAmount = ModelData.InsuredAmount,
                //PremiumAmount = ModelData.PremiumAmount,
                //AmountType = ModelData.AmountType,
                //StartDate = ModelData.StartDate,
                //EndDate = ModelData.EndDate,
                //InsuranceClaimFyId = ModelData.InsuranceClaimFyId,
                //EmpCode = ModelData.EmpCode,
                //InsurancePolicyNumber = ModelData.InsurancePolicyNumber,
                //Employee = new EmployeeDTO
                //{
                //    EmpCode = ModelData.Employee.EmpCode,
                //    EmpName = ModelData.Employee.EmpName,
                //},
                //InsuranceCompany = new InsuranceCompanyDTO
                //{
                //    Id = ModelData.InsuranceCompany.Id,
                //    CompanyName = ModelData.InsuranceCompany.CompanyName
                //}

            };
            return Record;
        }

        public static IEnumerable<PayrollIntrestGainDTO> PayrollInterestDbListToDTOList(IEnumerable<PayrollIntrestGain> ModelData)
        {
            List<PayrollIntrestGainDTO> ReturnRecord = new List<PayrollIntrestGainDTO>();
            foreach (PayrollIntrestGain Row in ModelData)
            {
                PayrollIntrestGainDTO Record = new PayrollIntrestGainDTO
                {
                    Id = Row.Id,
                    EmpCode = Row.EmpCode,
                    CustomerId = Row.CustomerId,
                    EmployeeName = Row.Employee.EmpName,
                    InterestGain = Row.InterestGain,
                    FyId = Row.FyId,
                    MonthIndex = Row.MonthIndex
                    //IsuranceClaimId = Row.IsuranceClaimId,
                    //InsuranceCompanyId = Row.InsuranceCompanyId,
                    //InsuredAmount = Row.InsuredAmount,
                    //PremiumAmount = Row.PremiumAmount,
                    //AmountType = Row.AmountType,
                    //StartDate = Row.StartDate,
                    //EndDate = Row.EndDate,
                    //InsuranceClaimFyId = Row.InsuranceClaimFyId,
                    //EmpCode = Row.EmpCode,
                    //InsurancePolicyNumber = Row.InsurancePolicyNumber,
                    //Employee = new EmployeeDTO
                    //{
                    //    EmpCode = Row.Employee.EmpCode,
                    //    EmpName = Row.Employee.EmpName,
                    //},
                    //InsuranceCompany = new InsuranceCompanyDTO
                    //{
                    //    Id = Row.InsuranceCompany.Id,
                    //    CompanyName = Row.InsuranceCompany.CompanyName
                    //}

                };
                ReturnRecord.Add(Record);
            }
            return ReturnRecord;


        }
    }
}
