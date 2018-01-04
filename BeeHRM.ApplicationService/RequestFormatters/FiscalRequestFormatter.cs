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
   public class FiscalRequestFormatter
    {
        public static Fiscal ConvertRespondentInfoFromDTO(FiscalDTO fiscalDTO)
        {

            Mapper.CreateMap<FiscalDTO, Fiscal>().ConvertUsing(
                      m =>
                      {
                          return new Fiscal
                          {
                             FyId = m.FyId,
                             FyCurrent = m.FyCurrent,
                             FyEndDate = m.FyEndDate,
                             FyEndDateNp = m.FyEndDateNp,
                             FyName = m.FyName,
                             FyStartDate = m.FyStartDate,
                             FyStartDateNp = m.FyStartDateNp
                          };

                      });
            return Mapper.Map<FiscalDTO, Fiscal>(fiscalDTO);
        }

        public static FiscalDTO ConvertRespondentInfoToDTO(Fiscal fiscal)
        {

            Mapper.CreateMap<Fiscal, FiscalDTO>().ConvertUsing(
                      m =>
                      {
                          return new FiscalDTO
                          {
                              FyId = m.FyId,
                              FyCurrent = m.FyCurrent,
                              FyEndDate = m.FyEndDate,
                              FyEndDateNp = m.FyEndDateNp,
                              FyName = m.FyName,
                              FyStartDate = m.FyStartDate,
                              FyStartDateNp = m.FyStartDateNp
                          };

                      });
            return Mapper.Map<Fiscal, FiscalDTO>(fiscal);
        }
    }
}
