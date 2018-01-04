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
    class PayrollRemoteAllowancesResponseFormatter
    {
        public static IEnumerable<PayrollRemoteAllowancesDTO> ModelData(IEnumerable<PayrollRemoteAllowance> modelData)
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
                        RemoteAllowance = m.RemoteAllowance,
                        Rank = new RankDTO
                        {
                            RankName = m.Rank.RankName,
                            RankId = m.Rank.RankId
                        },
                        PayrollRemoteSetup = new PayrollRemoteSetupDTO
                        {
                            RemoteId = m.PayrollRemoteSetup.RemoteId,
                            RemoteName = m.PayrollRemoteSetup.RemoteName
                        }
                    };
                }
                );
            return Mapper.Map<IEnumerable<PayrollRemoteAllowance>, IEnumerable<PayrollRemoteAllowancesDTO>>(modelData);
        }
    }
}
