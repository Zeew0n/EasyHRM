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
  public   class ModuleMapRequestFormtter
    {

        public static Role ConvertModuleDataFromDTO(RolesDTO rolesDTO)
        {

            Mapper.CreateMap<RolesDTO, Role>().ConvertUsing(
                      m =>
                      {
                          return new Role
                          {
                              RoleId = m.RoleId,
                              RoleName = m.RoleName,
                              RoleDetails = m.RoleDetails,
                              RoleDataAccessAll = m.RoleDataAccessAll

                          };

                      });
            return Mapper.Map<RolesDTO, Role>(rolesDTO);
        }
    }
}
