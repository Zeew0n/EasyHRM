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
   public class EmployeeVisitResponseFormatter
    {
        public static IEnumerable<EmployeeVisitDTO> ModelData(IEnumerable<EmployeeVisit> modelData)
        {

            Mapper.CreateMap<EmployeeVisit, EmployeeVisitDTO>();
            return Mapper.Map<IEnumerable<EmployeeVisit>, IEnumerable<EmployeeVisitDTO>>(modelData);
        }
    }
}
