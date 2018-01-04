using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;

namespace BeeHRM.ApplicationService.RequestFormatters
{
    public class PayrollMonthDescriptionRequestFormatter
    {

        public static PayrollMonthDescription ConvertRespondentInfoFromDTO(PayrollMonthDescriptionDTO PayrollMonthDescriptionDTO)
        {

            Mapper.CreateMap<PayrollMonthDescriptionDTO, PayrollMonthDescription>().ConvertUsing(
                      m =>
                      {
                          return new PayrollMonthDescription
                          {
                              Id = m.Id,
                              FyId = m.FyId,
                              MonthIndex = m.MonthIndex,
                              MonthNameEnglish = m.MonthNameEnglish,
                              MonthNameNepali = m.MonthNameNepali,
                              StartDate = m.StartDate,
                              EndDate = m.EndDate,
                              WorkingDays = m.WorkingDays
                          };

                      });
            return Mapper.Map<PayrollMonthDescriptionDTO, PayrollMonthDescription>(PayrollMonthDescriptionDTO);
        }

        public static PayrollMonthDescriptionDTO ConvertRespondentInfoToDTO(PayrollMonthDescription PayrollMonthDescription)
        {
            AutoMapper.Mapper.CreateMap<PayrollMonthDescription, PayrollMonthDescriptionDTO>().ConvertUsing(
                m =>
                {
                    return new PayrollMonthDescriptionDTO
                    {
                        Id = m.Id,
                        FyId = m.FyId,
                        MonthIndex = m.MonthIndex,
                        MonthNameEnglish = m.MonthNameEnglish,
                        MonthNameNepali = m.MonthNameNepali,
                        StartDate = m.StartDate,
                        EndDate = m.EndDate,
                        WorkingDays = m.WorkingDays
                    };

                });
            return AutoMapper.Mapper.Map<PayrollMonthDescription, PayrollMonthDescriptionDTO>(PayrollMonthDescription);
        }


    }
}
