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
    public class InsuranceCompanyRequestFormatter
    {
        public static InsuranceCompany ConvertRespondentInfoFromDTO(InsuranceCompanyDTO insuranceCompanyDTO)
        {

            Mapper.CreateMap<InsuranceCompanyDTO, InsuranceCompany>().ConvertUsing(
                      m =>
                      {
                          return new InsuranceCompany
                          {
                              Id = m.Id,
                              CompanyName = m.CompanyName
                          };

                      });
            return Mapper.Map<InsuranceCompanyDTO, InsuranceCompany>(insuranceCompanyDTO);
        }

        public static InsuranceCompanyDTO ConvertRespondentInfoToDTO(InsuranceCompany insuranceCompanyDTO)
        {

            Mapper.CreateMap<InsuranceCompany, InsuranceCompanyDTO>().ConvertUsing(
                      m =>
                      {
                          return new InsuranceCompanyDTO
                          {
                              Id = m.Id,
                              CompanyName = m.CompanyName
                          };

                      });
            return Mapper.Map<InsuranceCompany, InsuranceCompanyDTO>(insuranceCompanyDTO);
        }
    }
}
