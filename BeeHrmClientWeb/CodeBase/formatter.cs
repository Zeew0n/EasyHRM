using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHrmClientWeb.Models;
using AutoMapper;

namespace BeeHrmClientWeb.CodeBase
{
    public class formatter
    {
        public static ModuleDTOs ConvertModuleDataFromDTO(ModuleModules rolesDTO)
        {
            Mapper.CreateMap<ModuleModules, ModuleDTOs>().ConvertUsing(
                      m =>
                      {
                          return new ModuleDTOs
                          {
                              MduleLink =m.MduleLink,
                              ModuleId = m.ModuleId,
                              ModuleCssClass = m.ModuleCssClass,
                              MOduleName = m.MOduleName,
                              ModuleParentId = m.ModuleParentId
                          };

                      });
            return Mapper.Map<ModuleModules, ModuleDTOs>(rolesDTO);
        }
        public static RoleAccessDTOs ConvertAccessModelFromDTO(RoleAccessModel roleAccess)
        {
            Mapper.CreateMap<RoleAccessModel, RoleAccessDTOs>().ConvertUsing(
                m =>
                {
                    return new RoleAccessDTOs
                    {
                        AllowAdd = m.AllowAdd,
                        AllowDelete = m.AllowDelete,
                        AllowEdit = m.AllowEdit,
                        AllowView = m.AllowView,
                        AssignId = m.AssignId
                    };

                });
            return Mapper.Map<RoleAccessModel, RoleAccessDTOs>(roleAccess);
        }
    }

  
}