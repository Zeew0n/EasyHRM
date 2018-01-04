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
    public class TaxSetupResponseFormatter
    {
        public static IEnumerable<TaxSetupDTO> GetAllTaxSetupDTO(IEnumerable<PayrollTaxSetup> Record)
        {
            Mapper.CreateMap<PayrollTaxSetup, TaxSetupDTO>().ForMember(d => d.PayrollTaxDetails, m => m.Ignore());
            return Mapper.Map<IEnumerable<PayrollTaxSetup>, IEnumerable<TaxSetupDTO>>(Record);
        }
    }
}
