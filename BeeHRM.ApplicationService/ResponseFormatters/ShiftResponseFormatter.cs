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
    public class ShiftResponseFormatter
    {
        public static IEnumerable<ShiftDTO> ModelData(IEnumerable<Shift> modelData)
        {

            Mapper.CreateMap<Shift, ShiftDTO>().ConvertUsing(

                m =>
                {
                    return new ShiftDTO
                    {
                        ShiftId = m.ShiftId,
                        ShiftDelayAllow = m.ShiftDelayAllow,
                        ShiftName = m.ShiftName,
                        ShiftStatus = m.ShiftStatus
                    };

                }
                );
            return Mapper.Map<IEnumerable<Shift>, IEnumerable<ShiftDTO>>(modelData);
        }
    }
}
