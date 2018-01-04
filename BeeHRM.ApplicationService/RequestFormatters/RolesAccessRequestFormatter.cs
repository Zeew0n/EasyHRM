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
   public class RolesAccessRequestFormatter
    {
        public static RolesAccess ConvertRespondentInfoFromDTO(RolesAccessDTO rolesaccess)
        {
           Mapper.CreateMap<RolesAccessDTO, RolesAccess>().ConvertUsing(
                m =>
                {
                    return new RolesAccess
                    {
                        AssignId = m.AssignId,
                       AssignRoleId = m.AssignRoleId,
                       AssignModuleId = m.AssignModuleId,
                       AllowAdd = m.AllowAdd,
                       AllowEdit = m.AllowEdit,
                       AllowDelete = m.AllowDelete,
                       AllowView = m.AllowView
                    };
                });
            return Mapper.Map<RolesAccessDTO, RolesAccess>(rolesaccess);
        }
        public static RolesAccessDTO ConvertRespondentInfoToDTO(RolesAccess rolesaccess)
        {
            Mapper.CreateMap<RolesAccess, RolesAccessDTO>().ConvertUsing(
                m =>
                {
                    return new RolesAccessDTO
                    {
                        AssignId = m.AssignId,
                        AssignRoleId = m.AssignRoleId,
                        AssignModuleId = m.AssignModuleId,
                        AllowAdd = m.AllowAdd,
                        AllowEdit = m.AllowEdit,
                        AllowDelete = m.AllowDelete,
                        AllowView = m.AllowView,
                        RoleName = m.Role.RoleName
                    };

                });
            return Mapper.Map<RolesAccess, RolesAccessDTO>(rolesaccess);
        }

        public static RolesAccessDTO ConvertRespondent(RolesAccess rolesaccess)
        {
            Mapper.CreateMap<RolesAccess, RolesAccessDTO>().ConvertUsing(
                m =>
                {
                    return new RolesAccessDTO
                    {
                        AssignId = m.AssignId,
                        AssignRoleId = m.AssignRoleId,
                        AssignModuleId = m.AssignModuleId,
                        AllowAdd = m.AllowAdd,
                        AllowEdit = m.AllowEdit,
                        AllowDelete = m.AllowDelete,
                        AllowView = m.AllowView,
                       
                    };

                });
            return Mapper.Map<RolesAccess, RolesAccessDTO>(rolesaccess);
        }
    }
}
