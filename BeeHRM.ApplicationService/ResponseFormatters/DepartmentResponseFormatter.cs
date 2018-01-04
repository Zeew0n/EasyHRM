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
    public class DepartmentResponseFormatter
    {
        public static IEnumerable<DepartmentDTO> ModelData(IEnumerable<Department> modelData)
        {

            Mapper.CreateMap<Department, DepartmentDTO>().ConvertUsing(

                m =>
                {
                    return new DepartmentDTO
                    {
                        DeptCode = m.DeptCode,
                        DeptId = m.DeptId,
                        DeptName = m.DeptName

                    };

                }
                );
            return Mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentDTO>>(modelData);
        }
    }
}
