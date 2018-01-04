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
    public class EmployeeVisitRequestFormatter
    {
        public static EmployeeVisit ConvertRespondentInfoFromDTO(EmployeeVisitDTO employeeVisitDTO)
        {

            Mapper.CreateMap<EmployeeVisitDTO, EmployeeVisit>();
            return Mapper.Map<EmployeeVisitDTO, EmployeeVisit>(employeeVisitDTO);
        }

        public static EmployeeVisitDTO ConvertRespondentInfoToDTO(EmployeeVisit employeeVisit)
        {

            Mapper.CreateMap<EmployeeVisit, EmployeeVisitDTO>();
            return Mapper.Map<EmployeeVisit, EmployeeVisitDTO>(employeeVisit);
        }
    }
}
