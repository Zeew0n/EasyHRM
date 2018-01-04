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
    public class PayrollSalaryDetailSheetRequestFormatter
    {
        public static PayrollSalaryDetailSheet ConvertRespondentInfoFromDTO(PayrollSalaryDetailSheetDTO PayrollSalaryDetailSheetDTO)
        {


            Mapper.CreateMap<PayrollSalaryDetailSheetDTO, PayrollSalaryDetailSheet>().ConvertUsing(
                m =>
                {
                    return new PayrollSalaryDetailSheet
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
            return Mapper.Map<PayrollSalaryDetailSheetDTO, PayrollSalaryDetailSheet>(PayrollSalaryDetailSheetDTO);
        }

        public static PayrollSalaryDetailSheetDTO ConvertRespondentInfoToDTO(PayrollSalaryDetailSheet PayrollSalaryDetailSheet)
        {
            Mapper.CreateMap<PayrollSalaryDetailSheet, PayrollSalaryDetailSheetDTO>().ConvertUsing(
                m =>
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
            return Mapper.Map<PayrollSalaryDetailSheet, PayrollSalaryDetailSheetDTO>(PayrollSalaryDetailSheet);
        }



    }
}
