using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System.Collections.Generic;

namespace BeeHRM.ApplicationService.ResponseFormatters
{
    public class PayrollEmployeeTaxDetailResponseFormatter
    {
        public static IEnumerable<PayrollEmployeeTaxDetailDTO> GetAllTaxDetailSheet(IEnumerable<PayrollEmployeeTaxDetail> Record)
        {
            Mapper.CreateMap<PayrollEmployeeTaxDetail, PayrollEmployeeTaxDetailDTO>().ConvertUsing(m =>
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
            return Mapper.Map<IEnumerable<PayrollEmployeeTaxDetail>, IEnumerable<PayrollEmployeeTaxDetailDTO>>(Record);
        }


    }
}
