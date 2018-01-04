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
   public class RemoteAreaResponseFormatter
    {
        public static IEnumerable<RemoteAreasDTO> ModelData(IEnumerable<RemoteArea> modelData)
        {

            Mapper.CreateMap<RemoteArea, RemoteAreasDTO>();
            return Mapper.Map<IEnumerable<RemoteArea>, IEnumerable<RemoteAreasDTO>>(modelData);
        }
    }
}
