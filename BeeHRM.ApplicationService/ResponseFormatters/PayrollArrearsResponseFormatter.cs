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
    public class PayrollArrearsResponseFormatter
    {
        public static IEnumerable<PayrollArrearsDTO> GetPayrollArrears(IEnumerable<PayrollArrear> Record)
        {
            Mapper.CreateMap<PayrollArrear, PayrollArrearsDTO>().ConvertUsing(m =>
            {
                return new PayrollArrearsDTO
                {
                    Id = m.Id,
                    EmployeeCode = m.EmployeeCode,
                    EarningDeduction = m.EarningDeduction,
                    FyId = m.FyId,
                    NoOfDays = m.NoOfDays,
                    AdjustMonth = m.AdjustMonth,
                    ArrearMonth = m.ArrearMonth
                };
            });
            return Mapper.Map<IEnumerable<PayrollArrear>, IEnumerable<PayrollArrearsDTO>>(Record);
        }
    }
}
