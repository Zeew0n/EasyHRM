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
    public class PayrollAllowanceDetailRequestFormatter
    {



        public static PayrollAllowanceDetail ConvertRespondentInfoFromDTO(PayrollAllowanceDetailDTO PayrollAllowanceMasterDTO)
        {

            Mapper.CreateMap<PayrollAllowanceDetailDTO, PayrollAllowanceDetail>().ConvertUsing(
                      m =>
                      {
                          return new PayrollAllowanceDetail
                          {
                              Id = m.Id,
                              EmployeeCode = m.EmployeeCode,
                              AllowanceMasterId = m. AllowanceMasterId,
                              PercentageAmount = m.PercentageAmount,
                              Value = m.Value
                          };

                      });
            return Mapper.Map<PayrollAllowanceDetailDTO, PayrollAllowanceDetail>(PayrollAllowanceMasterDTO);
        }

        public static PayrollAllowanceDetailDTO ConvertRespondentInfoToDTO(PayrollAllowanceDetail PayrollAllowanceMaster)
        {
            AutoMapper.Mapper.CreateMap<PayrollAllowanceDetail, PayrollAllowanceDetailDTO>().ConvertUsing(
                m =>
                {
                    return new PayrollAllowanceDetailDTO
                    {
                        Id = m.Id,
                        EmployeeCode = m.EmployeeCode,
                        AllowanceMasterId = m.AllowanceMasterId,
                        PercentageAmount = m.PercentageAmount,
                        Value = m.Value
                    };

                });
            return AutoMapper.Mapper.Map<PayrollAllowanceDetail, PayrollAllowanceDetailDTO>(PayrollAllowanceMaster);
        }


    }
}
