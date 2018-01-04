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
    public class EmployeePrizeRequestFormatter
    {
        public static EmployeePrize ConvertRespondentInfoFromDTO(EmployeePrizeDTO employeePrizeDTO)
        {

            Mapper.CreateMap<EmployeePrizeDTO, EmployeePrize>().ConvertUsing(m =>
            {
                return new EmployeePrize
                {
                    CreatedAt = m.CreatedAt,
                    PrizeDate = m.PrizeDate,
                    PrizeDesignationId = m.PrizeDesignationId,
                    PrizeDetails = m.PrizeDetails,
                    PrizeEmpCode = m.PrizeEmpCode,
                    PrizeName = m.PrizeName,
                    PrizeType = m.PrizeType,
                    PrizeId = m.PrizeId

                };

            });
            return Mapper.Map<EmployeePrizeDTO, EmployeePrize>(employeePrizeDTO);
        }

        public static EmployeePrizeDTO ConvertRespondentInfoToDTO(EmployeePrize employeePrize)
        {

            Mapper.CreateMap<EmployeePrize, EmployeePrizeDTO>().ConvertUsing(m =>
            {
                return new EmployeePrizeDTO
                {
                    CreatedAt = m.CreatedAt,
                    PrizeDate = m.PrizeDate,
                    PrizeDesignationId = m.PrizeDesignationId,
                    PrizeDetails = m.PrizeDetails,
                    PrizeEmpCode = m.PrizeEmpCode,
                    PrizeName = m.PrizeName,
                    PrizeType = m.PrizeType,
                    PrizeId = m.PrizeId                                  
                };

            });
            return Mapper.Map<EmployeePrize, EmployeePrizeDTO>(employeePrize);
        }
    }
}
