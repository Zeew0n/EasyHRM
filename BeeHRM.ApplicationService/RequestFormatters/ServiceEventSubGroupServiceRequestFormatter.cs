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
    public class ServiceEventSubGroupServiceRequestFormatter
    {
        public static ServiceEventSubGroup ConvertRespondentInfoFromDTO(ServiceEventSubGroupDTO serviceEventSubGroupDTO)
        {

            Mapper.CreateMap<ServiceEventSubGroupDTO, ServiceEventSubGroup>();
            return Mapper.Map<ServiceEventSubGroupDTO, ServiceEventSubGroup>(serviceEventSubGroupDTO);
        }

        public static ServiceEventSubGroupDTO ConvertRespondentInfoToDTO(ServiceEventSubGroup serviceEventSubGroup)
        {

            Mapper.CreateMap<ServiceEventSubGroup, ServiceEventSubGroupDTO>();
            return Mapper.Map<ServiceEventSubGroup, ServiceEventSubGroupDTO>(serviceEventSubGroup);
        }
    }
}
