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
   public class FiscalResponseFormatter
    {
        public static IEnumerable<FiscalDTO> ModelData(IEnumerable<Fiscal> modelData)
        {
            Mapper.CreateMap<Fiscal, FiscalDTO>().ConvertUsing(

                m =>
                {
                    return new FiscalDTO
                    {
                        FyId = m.FyId,
                        FyName = m.FyName,
                        FyCurrent = m.FyCurrent,
                        FyEndDate = m.FyEndDate,
                        FyEndDateNp = m.FyEndDateNp,
                        FyStartDate = m.FyStartDate,
                        FyStartDateNp = m.FyStartDateNp
                    };

                }
                );
            return Mapper.Map<IEnumerable<Fiscal>, IEnumerable<FiscalDTO>>(modelData);
        }
    }
}
