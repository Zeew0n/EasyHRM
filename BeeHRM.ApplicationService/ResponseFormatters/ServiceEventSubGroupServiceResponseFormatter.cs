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
    public class ServiceEventSubGroupServiceResponseFormatter
    {
        public static IEnumerable<ServiceEventSubGroupDTO> ModelData(IEnumerable<ServiceEventSubGroup> modelData)
        {

            Mapper.CreateMap<ServiceEventSubGroup, ServiceEventSubGroupDTO>();
            return Mapper.Map<IEnumerable<ServiceEventSubGroup>, IEnumerable<ServiceEventSubGroupDTO>>(modelData);
        }
    }
}
