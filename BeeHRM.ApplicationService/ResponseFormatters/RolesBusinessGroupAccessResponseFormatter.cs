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
   public class RolesBusinessGroupAccessResponseFormatter
    {
        public static IEnumerable<RolesBusinessGroupAccessDTO> ModelData(IEnumerable<RolesBusinessGroupAccess> modelData)
        {

            Mapper.CreateMap<RolesBusinessGroupAccess, RolesBusinessGroupAccessDTO>().ConvertUsing(

                m =>
                {
                    return new RolesBusinessGroupAccessDTO
                    {
                       BgAccessId = m.BgAccessId,
                       BusinssGroupId = m.BusinssGroupId,
                       RoleId = m.RoleId
                    };

                }
                );
            return Mapper.Map<IEnumerable<RolesBusinessGroupAccess>, IEnumerable<RolesBusinessGroupAccessDTO>>(modelData);
        }
    }
}
