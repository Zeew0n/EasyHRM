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
    public class BankResponseFormatter
    {
        public static IEnumerable<BankDTO> ModelData(IEnumerable<Bank> modelData)
        {

            Mapper.CreateMap<Bank, BankDTO>().ConvertUsing(

                m =>
                {
                    return new BankDTO
                    {
                        BankId = m.BankId,
                        BankName = m.BankName,
                        BankAddedDate = m.BankAddedDate,
                        BankStatus =Convert.ToBoolean(m.BankStatus)
                    };

                }
                );
            return Mapper.Map<IEnumerable<Bank>, IEnumerable<BankDTO>>(modelData);
        }
    }
}
