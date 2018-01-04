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
    public class PayrollArrearsRequestFormatter
    {
        public static PayrollArrear ConvertRespondentInfoFromDTO(PayrollArrearsDTO PayrollArrearsDTO)
        {

            Mapper.CreateMap<PayrollArrearsDTO, PayrollArrear>().ConvertUsing(
                m =>
                {
                    return new PayrollArrear
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
            return Mapper.Map<PayrollArrearsDTO, PayrollArrear>(PayrollArrearsDTO);
        }

        public static PayrollArrearsDTO ConvertRespondentInfoToDTO(PayrollArrear PayrollArrear)
        {
            Mapper.CreateMap<PayrollArrear, PayrollArrearsDTO>().ConvertUsing(
                m =>
                {
                    return new PayrollArrearsDTO
                    {
                        Id = m.Id,
                        EmployeeCode = m.EmployeeCode,
                        EarningDeduction = m.EarningDeduction,
                        FyId = m.FyId,
                        NoOfDays = m.NoOfDays,
                        AdjustMonth = m.AdjustMonth,
                        ArrearMonth = m.ArrearMonth,
                        PayrollMonthDescription = new PayrollMonthDescriptionDTO
                        {
                            Id = m.PayrollMonthDescription.Id,
                            MonthNameNepali = m.PayrollMonthDescription.MonthNameNepali,
                            MonthNameEnglish = m.PayrollMonthDescription.MonthNameEnglish,
                        },
                        PayrollMonthDescription1 = new PayrollMonthDescriptionDTO
                        {
                            Id = m.PayrollMonthDescription1.Id,
                            MonthNameNepali = m.PayrollMonthDescription1.MonthNameNepali,
                            MonthNameEnglish = m.PayrollMonthDescription1.MonthNameEnglish,
                        },
                        Fiscal = new FiscalDTO
                        {
                            FyId = m.Fiscal.FyId,
                            FyName = m.Fiscal.FyName
                        }
                    };
                });
            return Mapper.Map<PayrollArrear, PayrollArrearsDTO>(PayrollArrear);
        }
    }
}
