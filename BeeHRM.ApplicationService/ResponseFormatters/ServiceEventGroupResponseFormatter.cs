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
    public class ServiceEventGroupResponseFormatter
    {
        public static IEnumerable<ServiceEventGroupDTO> ModelData(IEnumerable<ServiceEventGroup> modelData)
        {

            Mapper.CreateMap<ServiceEventGroup, ServiceEventGroupDTO>().ConvertUsing(

                m =>
                {
                    return new ServiceEventGroupDTO
                    {
                        ServiceEventGroupName = m.ServiceEventGroupName,
                        ServiceEventGroupDetails = m.ServiceEventGroupDetails,
                        ServiceEventId = m.ServiceEventId,
                        ServiceEventOrder = m.ServiceEventOrder
                    };

                }
                );
            return Mapper.Map<IEnumerable<ServiceEventGroup>, IEnumerable<ServiceEventGroupDTO>>(modelData);
        }
    }
}
