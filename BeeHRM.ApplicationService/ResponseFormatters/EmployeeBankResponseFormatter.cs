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
    public class EmployeeBankResponseFormatter
    {
        public static IEnumerable<EmployeeBankDTO> ModelData(IEnumerable<EmployeeBank> modelData)
        {

            Mapper.CreateMap<EmployeeBank, EmployeeBankDTO>().ConvertUsing(

                m =>
                {
                    return new EmployeeBankDTO
                    {
                        Id = m.Id,
                        AccountName = m.AccountName,
                        AccountNumber = m.AccountNumber,
                        AccountPrimary =Convert.ToBoolean(m.AccountPrimary),
                        BankId = m.BankId,
                        EmpCode = m.EmpCode,
                        Bank = new BankDTO
                        {
                            BankName = m.Bank.BankName,
                            BankId = m.Bank.BankId
                        },
                        Employee = new EmployeeDTO
                        {
                            EmpName = m.Employee.EmpName,
                            EmpCode = m.Employee.EmpCode
                        }
                    };

                }
                );
            return Mapper.Map<IEnumerable<EmployeeBank>, IEnumerable<EmployeeBankDTO>>(modelData);
        }
    }
}
