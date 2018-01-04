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
    public class PayrollAllowanceDetailResponseFormatter
    {
        public static IEnumerable<PayrollAllowanceDetailDTO> GetAllTPayrollDetailDTO(IEnumerable<PayrollAllowanceDetail> Record)
        {
            Mapper.CreateMap<PayrollAllowanceDetail, PayrollAllowanceDetailDTO>().ConvertUsing(m =>
            {
                return new PayrollAllowanceDetailDTO
                {
                     Id = m.Id,
                     EmployeeCode = m.EmployeeCode,
                     AllowanceMasterId = m.AllowanceMasterId,
                     PercentageAmount = m.PercentageAmount,
                     Value = m.Value
                };
            });
            return Mapper.Map<IEnumerable<PayrollAllowanceDetail>, IEnumerable<PayrollAllowanceDetailDTO>>(Record);
        }
    }
}
