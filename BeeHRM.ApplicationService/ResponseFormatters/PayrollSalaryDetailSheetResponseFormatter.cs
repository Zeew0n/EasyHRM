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
    public class PayrollSalaryDetailSheetResponseFormatter
    {


        public static IEnumerable<PayrollSalaryDetailSheetDTO> GetAllPayrollDetailSheet(IEnumerable<PayrollSalaryDetailSheet> Record)
        {

            Mapper.CreateMap<PayrollSalaryDetailSheet, PayrollSalaryDetailSheetDTO>().ConvertUsing(m =>
            {
                return new PayrollSalaryDetailSheetDTO
                {
                    Id = m.Id,
                    AllowanceId = m.AllowanceId,
                    AllowanceName = m.AllowanceName,
                    AllowanceType = m.AllowanceType,
                    CalculatedValue = m.CalculatedValue,
                    EarningDeduction = m.EarningDeduction,
                    PayrollSalaryMasterId = m.PayrollSalaryMasterId,
                    PercentageAmount = m.PercentageAmount,
                    Value = m.Value
                };
            });
            return Mapper.Map<IEnumerable<PayrollSalaryDetailSheet>, IEnumerable<PayrollSalaryDetailSheetDTO>>(Record);
        }



    }
}
