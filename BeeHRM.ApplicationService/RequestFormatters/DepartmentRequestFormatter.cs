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
    public static class DepartmentRequestFormatter
    {
        public static Department ConvertRespondentInfoFromDTO(DepartmentDTO deptDTO)
        {

            Mapper.CreateMap<DepartmentDTO,Department >().ConvertUsing(
                      m =>
                      {
                          return new Department
                          {
                              DeptCode = m.DeptCode,
                              DeptName = m.DeptName,
                              DeptId = m.DeptId
                              
                          };

                      });
            return Mapper.Map<DepartmentDTO, Department>(deptDTO);
        }

        public static DepartmentDTO ConvertRespondentInfoToDTO(Department dept)
        {

            Mapper.CreateMap<Department, DepartmentDTO>().ConvertUsing(
                      m =>
                      {
                          return new DepartmentDTO
                          {
                             DeptId = m.DeptId,
                             DeptCode = m.DeptCode,
                             DeptName = m.DeptName

                          };

                      });
            return Mapper.Map<Department, DepartmentDTO>(dept);
        }
    }
}
