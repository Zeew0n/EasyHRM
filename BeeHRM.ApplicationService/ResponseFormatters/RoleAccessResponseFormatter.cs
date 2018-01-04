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
    public static class RoleAccessResponseFormatter
    {
        public static List<RoleAccessDTOs> GetAllRolesAndAccess(this List<RolesAccess> roleAccess)
        {
            Mapper.CreateMap<RolesAccess, RoleAccessDTOs>().ConvertUsing(

                x =>
                {
                    return new RoleAccessDTOs
                    {
                       AssignId = x.AssignId,
                       AssignRoleId = x.AssignRoleId,
                        AssignModuleId = x.AssignModuleId,
                        AllowAdd = x.AllowAdd,
                        AllowEdit = x.AllowEdit,
                        AllowDelete = x.AllowDelete,
                       AllowView = x.AllowView,
                       ModuleData = new ModuleDTOs
                       {
                           MduleLink = x.Module.MduleLink,
                           ModuleCssClass = x.Module.ModuleCssClass,
                           ModuleId = x.Module.ModuleId,
                           MOduleName = x.Module.MOduleName,
                           ModuleParentId = x.Module.ModuleParentId,
                           Order = x.Module.Order,
                           IsDefault = x.Module.IsDefault

                       },

                        RoleData = new RoleDTOs
                        {
                            RoleDataAccessAll = x.Role.RoleDataAccessAll,
                            RoleDetails = x.Role.RoleDetails,
                            RoleId = x.Role.RoleId,
                            RoleName = x.Role.RoleName
                        }
                    };

                });
            return Mapper.Map<List<RolesAccess>, List<RoleAccessDTOs>>(roleAccess);
        }

        public static IEnumerable<RolesAccessDTO> ModelData(IEnumerable<RolesAccess> modelData)
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
                        AllowView = m.AllowView,
                        AllowEdit = m.AllowEdit,
                        AllowDelete = m.AllowDelete,
                        RoleName = m.Role.RoleName,
                        ModuleName = m.Module.MOduleName
                       

                    };
                }
          );
            return Mapper.Map<IEnumerable<RolesAccess>, IEnumerable<RolesAccessDTO>>(modelData);
        }


        public static RoleAccessDTOs RoleAccessModuleData(RolesAccess roleAccessModuleData)
        {
            Mapper.CreateMap<RolesAccess, RolesAccessDTO>();
            return Mapper.Map<RolesAccess, RoleAccessDTOs>(roleAccessModuleData);


        }
    }
}


  
