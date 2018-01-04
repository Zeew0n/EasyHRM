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
    public class PayrollMonthDescriptionResponseFormatter
    {
        public static IEnumerable<PayrollMonthDescriptionDTO> GetAllTPayrollMonths(IEnumerable<PayrollMonthDescription> Record)
        {
            Mapper.CreateMap<PayrollMonthDescription, PayrollMonthDescriptionDTO>().ConvertUsing(m =>
            {
                return new PayrollMonthDescriptionDTO
                {
                    Id = m.Id,
                    FyId = m.FyId,
                    MonthNameEnglish = m.MonthNameEnglish,
                    MonthNameNepali = m.MonthNameNepali,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    WorkingDays = m.WorkingDays
                };
            });
            return Mapper.Map<IEnumerable<PayrollMonthDescription>, IEnumerable<PayrollMonthDescriptionDTO>>(Record);
        }
    }
}
