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
    public class BankRequestFormatter
    {
        public static Bank ConvertRespondentInfoFromDTO(BankDTO bankDTO)
        {

            Mapper.CreateMap<BankDTO, Bank>().ConvertUsing(
                      m =>
                      {
                          return new Bank
                          {
                              BankId = m.BankId,
                              BankName = m.BankName,
                              BankAddedDate = m.BankAddedDate,
                              BankStatus = m.BankStatus
                          };

                      });
            return Mapper.Map<BankDTO, Bank>(bankDTO);
        }

        public static BankDTO ConvertRespondentInfoToDTO(Bank bank)
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

                      });
            return Mapper.Map<Bank, BankDTO>(bank);
        }
    }
}
