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
   public class RolesResponseFormatter
    {
        public static IEnumerable<RolesDTO> ModelData(IEnumerable<Role> modelData)
        {

            Mapper.CreateMap<Role, RolesDTO>().ConvertUsing(

                m =>
                {
                    return new RolesDTO
                    {
                        RoleId = m.RoleId,
                        RoleName = m.RoleName,
                        RoleDetails = m.RoleDetails,
                        RoleDataAccessAll = m.RoleDataAccessAll

                    };

                }
                );
            return Mapper.Map<IEnumerable<Role>, IEnumerable<RolesDTO>>(modelData);
        }
    }
}
