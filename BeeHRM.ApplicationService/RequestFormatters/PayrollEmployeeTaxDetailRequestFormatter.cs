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
    public class PayrollEmployeeTaxDetailRequestFormatter
    {
        public static PayrollEmployeeTaxDetailDTO ConvertRespondentInfoToDTO(PayrollEmployeeTaxDetail PayrollEmployeeTaxDetail)
        {
            Mapper.CreateMap<PayrollEmployeeTaxDetail, PayrollEmployeeTaxDetailDTO>().ConvertUsing(
                m =>
                {
                    return new PayrollEmployeeTaxDetailDTO
                    {
                        Id = m.Id,
                        EmployeeCode = m.EmployeeCode,
                        MaxAmount = m.MaxAmount,
                        DeductPercentage = m.DeductPercentage,
                        OrderNumber = m.OrderNumber,
                        DeductedAmount = m.DeductedAmount
                    };
                });
            return Mapper.Map<PayrollEmployeeTaxDetail, PayrollEmployeeTaxDetailDTO>(PayrollEmployeeTaxDetail);
        }

        public static PayrollEmployeeTaxDetail ConvertRespondentInfoFromDTO(PayrollEmployeeTaxDetailDTO PayrollEmployeeTaxDetailDTO)
        {

            Mapper.CreateMap<PayrollEmployeeTaxDetailDTO, PayrollEmployeeTaxDetail>().ConvertUsing(
                m =>
                {
                    return new PayrollEmployeeTaxDetail
                    {
                        Id = m.Id,
                        EmployeeCode = m.EmployeeCode,
                        MaxAmount = m.MaxAmount,
                        DeductPercentage = m.DeductPercentage,
                        OrderNumber = m.OrderNumber,
                        DeductedAmount = m.DeductedAmount
                    };
                });
            return Mapper.Map<PayrollEmployeeTaxDetailDTO, PayrollEmployeeTaxDetail>(PayrollEmployeeTaxDetailDTO);
        }
    }
}
