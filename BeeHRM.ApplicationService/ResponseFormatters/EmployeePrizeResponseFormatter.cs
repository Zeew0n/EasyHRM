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
    public class EmployeePrizeResponseFormatter
    {
        public static IEnumerable<EmployeePrizeDTO> ModelData(IEnumerable<EmployeePrize> modelData)
        {

            Mapper.CreateMap<EmployeePrize, EmployeePrizeDTO>().ConvertUsing(m =>
            {
                return new EmployeePrizeDTO
                {
                    CreatedAt = m.CreatedAt,
                    DesignationName = m.Designation.DsgName,
                    PrizeDate =  m.PrizeDate,
                    PrizeDesignationId = m.PrizeDesignationId,
                    PrizeDetails = m.PrizeDetails,
                    PrizeEmpCode = m.PrizeEmpCode,
                    PrizeId = m.PrizeId,
                    PrizeName = m.PrizeName,
                    PrizeType = m.PrizeType
                };

            });
            return Mapper.Map<IEnumerable<EmployeePrize>, IEnumerable<EmployeePrizeDTO>>(modelData);
        }
    }
}
