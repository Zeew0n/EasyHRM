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
    public class BusinessGroupResponseFormatter
    {
        public static IEnumerable<BusinessGroupDTO> ModelData(IEnumerable<BusinessGroup> modelData)
        {
            Mapper.CreateMap<BusinessGroup, BusinessGroupDTO>().ConvertUsing(

                m =>
                {
                    return new BusinessGroupDTO
                    {
                        BgId = m.BgId,
                        BgName = m.BgName
                    };

                }
                );
            return Mapper.Map<IEnumerable<BusinessGroup>, IEnumerable<BusinessGroupDTO>>(modelData);
        }
    }
}
