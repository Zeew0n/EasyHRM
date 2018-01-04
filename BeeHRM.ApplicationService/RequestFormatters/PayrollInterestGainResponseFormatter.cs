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
    public class PayrollInterestGainResponseFormatter
    {
        public static PayrollIntrestGain ConvertRespondentInfoFromDTO(PayrollIntrestGainDTO PayrollAllowanceMasterDTO)
        {

            Mapper.CreateMap<PayrollIntrestGainDTO, PayrollIntrestGain>().ConvertUsing(
                      m =>
                      {
                          return new PayrollIntrestGain
                          {
                              Id = m.Id,
                              CustomerId = m.CustomerId,
                              EmpCode = m.EmpCode,
                              FyId = m.FyId,
                              InterestGain = m.InterestGain,
                              MonthIndex = m.MonthIndex
                              //AllowanceMasterId = m.AllowanceMasterId,
                              //PercentageAmount = m.PercentageAmount,
                              //Value = m.Value
                          };

                      });
            return Mapper.Map<PayrollIntrestGainDTO, PayrollIntrestGain>(PayrollAllowanceMasterDTO);
        }

        public static PayrollIntrestGainDTO ConvertRespondentInfoToDTO(PayrollIntrestGain PayrollAllowanceMaster)
        {
            AutoMapper.Mapper.CreateMap<PayrollIntrestGain, PayrollIntrestGainDTO>().ConvertUsing(
                m =>
                {
                    return new PayrollIntrestGainDTO
                    {
                        Id = m.Id,
                        CustomerId = m.CustomerId,
                        EmpCode = m.EmpCode,
                        FyId = m.FyId,
                        InterestGain = m.InterestGain,
                        MonthIndex = m.MonthIndex
                        //EmployeeCode = m.EmployeeCode,
                        //AllowanceMasterId = m.AllowanceMasterId,
                        //PercentageAmount = m.PercentageAmount,
                        //Value = m.Value
                    };

                });
            return AutoMapper.Mapper.Map<PayrollIntrestGain, PayrollIntrestGainDTO>(PayrollAllowanceMaster);
        }

    }
}
