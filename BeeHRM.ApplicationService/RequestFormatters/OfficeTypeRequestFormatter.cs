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
    public class OfficeTypeRequestFormatter
    {
        public static OfficeType ConvertRespondentInfoFromDTO(OfficeTypeDTO officeTypeDTO)
        {

            Mapper.CreateMap<OfficeTypeDTO, OfficeType>();
            return Mapper.Map<OfficeTypeDTO, OfficeType>(officeTypeDTO);
        }

        public static OfficeTypeDTO ConvertRespondentInfoToDTO(OfficeType officeType)
        {

            Mapper.CreateMap<OfficeType, OfficeTypeDTO>();
            return Mapper.Map<OfficeType, OfficeTypeDTO>(officeType);
        }
    }
}
