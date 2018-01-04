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
    class TaxSetupRequestFormatter
    {
        public static PayrollTaxSetup ConvertRespondentInfoFromDTO(TaxSetupDTO TaxSetupDTO)
        {

            Mapper.CreateMap<TaxSetupDTO, PayrollTaxSetup>().ForMember(d => d.PayrollTaxDetails, m => m.Ignore());
            return Mapper.Map<TaxSetupDTO, PayrollTaxSetup>(TaxSetupDTO);
        }

        public static TaxSetupDTO ConvertRespondentInfoToDTO(PayrollTaxSetup TaxSetup)
        {

            Mapper.CreateMap<PayrollTaxSetup, TaxSetupDTO>().ForMember(d => d.PayrollTaxDetails, m => m.Ignore());
            return Mapper.Map<PayrollTaxSetup, TaxSetupDTO>(TaxSetup);
        }
    }
}
