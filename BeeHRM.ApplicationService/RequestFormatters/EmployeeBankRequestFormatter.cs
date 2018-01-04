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
    public class EmployeeBankRequestFormatter
    {
        public static EmployeeBank ConvertRespondentInfoFromDTO(EmployeeBankDTO employeeBankDTO)
        {

            Mapper.CreateMap<EmployeeBankDTO, EmployeeBank>().ConvertUsing(
                      m =>
                      {
                          return new EmployeeBank
                          {
                              AccountName = m.AccountName,
                              AccountNumber = m.AccountNumber,
                              AccountPrimary = m.AccountPrimary,
                              BankId = m.BankId,
                              EmpCode = m.EmpCode,
                              Id = m.Id
                          };

                      });
            return Mapper.Map<EmployeeBankDTO, EmployeeBank>(employeeBankDTO);
        }

        public static EmployeeBankDTO ConvertRespondentInfoToDTO(EmployeeBank employeeBank)
        {

            Mapper.CreateMap<EmployeeBank, EmployeeBankDTO>().ConvertUsing(
                      m =>
                      {
                          return new EmployeeBankDTO
                          {
                              AccountName = m.AccountName,
                              AccountNumber = m.AccountNumber,
                              AccountPrimary =Convert.ToBoolean(m.AccountPrimary),
                              BankId = m.BankId,
                              EmpCode = m.EmpCode,
                              Id = m.Id
                          };

                      });
            return Mapper.Map<EmployeeBank, EmployeeBankDTO>(employeeBank);
        }
    }
}
