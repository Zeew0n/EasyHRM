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
    public class RolesBusinessGroupAccessRequestFormatter
    {
        public static RolesBusinessGroupAccess ConvertRespondentInfoFromDTO(RolesBusinessGroupAccessDTO rolesBusinessGroupAccessDTO)
        {

            Mapper.CreateMap<RolesBusinessGroupAccessDTO, RolesBusinessGroupAccess>().ConvertUsing(
                      m =>
                      {
                          return new RolesBusinessGroupAccess
                          {
                              BusinssGroupId = m.BusinssGroupId,
                              BgAccessId = m.BgAccessId,
                              RoleId = m.RoleId

                          };

                      });
            return Mapper.Map<RolesBusinessGroupAccessDTO, RolesBusinessGroupAccess>(rolesBusinessGroupAccessDTO);
        }

        public static RolesBusinessGroupAccessDTO ConvertRespondentInfoToDTO(RolesBusinessGroupAccess rolesBusinessGroupAccess)
        {

            Mapper.CreateMap<RolesBusinessGroupAccess, RolesBusinessGroupAccessDTO>().ConvertUsing(
                      m =>
                      {
                          return new RolesBusinessGroupAccessDTO
                          {
                              BusinssGroupId = m.BusinssGroupId,
                              BgAccessId = m.BgAccessId,
                              RoleId = m.RoleId
                          };

                      });
            return Mapper.Map<RolesBusinessGroupAccess, RolesBusinessGroupAccessDTO>(rolesBusinessGroupAccess);
        }
    }
}
