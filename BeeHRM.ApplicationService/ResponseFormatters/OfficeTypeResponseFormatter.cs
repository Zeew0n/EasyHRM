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
    public class OfficeTypeResponseFormatter
    {
        public static IEnumerable<OfficeTypeDTO> ModelData(IEnumerable<OfficeType> modelData)
        {

            Mapper.CreateMap<OfficeType, OfficeTypeDTO>().ConvertUsing(

                m =>
                {
                    return new OfficeTypeDTO
                    {
                        OfficeTypeId = m.OfficeTypeId,
                        OfficeTypeName = m.OfficeTypeName
                    };
                }
            );
            return Mapper.Map<IEnumerable<OfficeType>, IEnumerable<OfficeTypeDTO>>(modelData);
        }
    }
}
