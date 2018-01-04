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
    class TaxDetailRequestFormatter
    {
        public static PayrollTaxDetail ConvertRespondentInfoFromDTO(TaxDetailDTO TaxSetupDTO)
        {

            Mapper.CreateMap<TaxDetailDTO, PayrollTaxDetail>().ForMember(d => d.PayrollTaxSetup, m => m.Ignore());
            return Mapper.Map<TaxDetailDTO, PayrollTaxDetail>(TaxSetupDTO);
        }

        public static TaxDetailDTO ConvertRespondentInfoToDTO(PayrollTaxDetail TaxDetail)
        {

            Mapper.CreateMap<PayrollTaxDetail, TaxDetailDTO>().ForMember(d => d.PayrollTaxSetup, m => m.Ignore());
            return Mapper.Map<PayrollTaxDetail, TaxDetailDTO>(TaxDetail);
        }
    }
}
