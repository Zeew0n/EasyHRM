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
    class PayrollRemoteAllowancesRequestFormatter
    {
        public static PayrollRemoteAllowance ConvertRespondentInfoFromDTO(PayrollRemoteAllowancesDTO AttDTO)
        {
            Mapper.CreateMap<PayrollRemoteAllowancesDTO, PayrollRemoteAllowance>().ConvertUsing(
                m => {
                    return new PayrollRemoteAllowance
                    {
                        RAId = m.RAId,
                        RARankId = m.RARankId,
                        RARemoteId = m.RARemoteId,
                        RAType = m.RAType,
                        RemoteAllowance = m.RemoteAllowance
                    };
                });
            return Mapper.Map<PayrollRemoteAllowancesDTO, PayrollRemoteAllowance>(AttDTO);
        }

        public static PayrollRemoteAllowancesDTO ConvertRespondentToDTO(PayrollRemoteAllowance AttDTO)
        {

            Mapper.CreateMap<PayrollRemoteAllowance, PayrollRemoteAllowancesDTO>().ConvertUsing(
                m =>
                {

                    return new PayrollRemoteAllowancesDTO
                    {
                        RAId = m.RAId,
                        RARankId = m.RARankId,
                        RARemoteId = m.RARemoteId,
                        RAType = m.RAType,
                        RemoteAllowance = m.RemoteAllowance
                    };
                });
            return Mapper.Map<PayrollRemoteAllowance, PayrollRemoteAllowancesDTO>(AttDTO);
        }
    }
}
