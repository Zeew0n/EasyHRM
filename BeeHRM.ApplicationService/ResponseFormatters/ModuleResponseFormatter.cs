using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository.Implementations;
using AutoMapper;
using BeeHRM.Repository;

namespace BeeHRM.ApplicationService.ResponseFormatters
{
    public class ModuleResponseFormatter
    {
        //public static IEnumerable<ParentModulesDTO> ParentModelData (IEnumerable<ParentModule> modelData)
        //{
        //    Mapper.CreateMap<ParentModule, ParentModulesDTO>().ConvertUsing(
        //        m =>
        //        {
        //            return new ParentModulesDTO
        //            {
        //                ParentModuleLink = m.ParentModuleLink,
        //                ParentModulesCssClass = m.ParentModulesCssClass,
        //                 ParentModulesId = m.ParentModulesId,
        //                 parentModuleName = m.parentModuleName,
        //                 IsDefault = m.IsDefault,
        //                 ParentModuleOrder = m.ParentModuleOrder
        //            };
        //        }
        //        );
        //    return Mapper.Map<IEnumerable<ParentModule>, IEnumerable<ParentModulesDTO>>(modelData);
        //}

        public static IEnumerable<ModuleDTOs> ModelData(IEnumerable<Module> modelData)
        {
            Mapper.CreateMap<Module, ModuleDTOs>().ConvertUsing(
                m =>
                {
                    return new ModuleDTOs
                    {
                        MduleLink = m.MduleLink,
                        ModuleCssClass = m.ModuleCssClass,
                        ModuleId = m.ModuleId,
                        MOduleName = m.MOduleName,
                        Order = m.Order,
                        ModuleParentId = m.ModuleParentId,
                        Controller = m.Controller
                    };
                }
                );
            return Mapper.Map<IEnumerable<Module>, IEnumerable<ModuleDTOs>>(modelData);
        }

        public static ModuleDTOs ModelData1( ParentModulesDTO modelData)
        {

          ModuleDTOs  ModuleDTOs = new ModuleDTOs()
            {
              MduleLink = modelData.ParentModuleLink,
              ModuleId = modelData.ParentModulesId,
              MOduleName = modelData.parentModuleName,
              Order =  modelData.ParentModuleOrder,
              IsDefault = modelData.IsDefault
              

          };
            return ModuleDTOs;
        }

        public static ModuleDTOs ConvertModuleDataFromDTO(ModuleDTOsForparent rolesDTO)
        {
            Mapper.CreateMap<ModuleDTOsForparent, ModuleDTOs>().ConvertUsing(
                      m =>
                      {
                          return new ModuleDTOs
                          {
                              MduleLink = m.MduleLink,
                              ModuleId = m.ModuleId,
                              ModuleCssClass = m.ModuleCssClass,
                              MOduleName = m.MOduleName,
                              ModuleParentId = m.ModuleParentId
                          };

                      });
            return Mapper.Map<ModuleDTOsForparent, ModuleDTOs>(rolesDTO);
        }

        public static ModuleDTOsForparent ConvertModuleDatatoParentModel(ModuleDTOsForparent rolesDTO)
        {
            Mapper.CreateMap<ModuleDTOsForparent, ModuleDTOsForparent>().ConvertUsing(
                      m =>
                      {
                          return new ModuleDTOsForparent
                          {
                              MduleLink = m.MduleLink,
                              ModuleId = m.ModuleId,
                              ModuleCssClass = m.ModuleCssClass,
                              MOduleName = m.MOduleName,
                              ModuleParentId = m.ModuleParentId
                          };

                      });
            return Mapper.Map<ModuleDTOsForparent, ModuleDTOsForparent>(rolesDTO);
        }

        //public static List<ModuleDTOs> ModelAccessData(IEnumerable<Module> modelData)
        //{
        //    Mapper.CreateMap<List<Module>, List<ModuleDTOs>>().ConvertUsing(
        //        m =>
        //        {
        //            return new ModuleDTOs
        //            {
                        
        //                ModuleId = m.
        //                ModuleCssClass = m.ModuleCssClass,
        //                MOduleName = m.MOduleName,
        //                ModuleParentId = m.ModuleParentId
        //            };
        //        });

        //    return Mapper.Map<List<Module>,List<ModuleDTOs>>(modelData);
        //}
    }
}
