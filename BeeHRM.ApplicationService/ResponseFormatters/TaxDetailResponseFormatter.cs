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
    public class TaxDetailResponseFormatter
    {
        public static IEnumerable<TaxDetailDTO> GetAllTaxSetupDTO(IEnumerable<PayrollTaxDetail> Record)
        {
            Mapper.CreateMap<PayrollTaxDetail, TaxDetailDTO>().ConvertUsing(m => { 
            return new TaxDetailDTO
                    {
                        DetailId = m.DetailId,
                        MasterId = m.MasterId,
                        MaxAmount = m.MaxAmount,
                        OrderNumber = m.OrderNumber,
                        Percentage = m.Percentage,
                    };
            
            });
            return Mapper.Map<IEnumerable<PayrollTaxDetail>, IEnumerable<TaxDetailDTO>>(Record);
        }
    }
}
