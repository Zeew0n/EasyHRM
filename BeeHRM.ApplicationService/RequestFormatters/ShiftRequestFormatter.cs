using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.ViewModel;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.RequestFormatters
{
    public class ShiftRequestFormatter
    {
        public static Shift ConvertRespondentInfoFromDTO(ShiftDTO shiftDTO)
        {

            Mapper.CreateMap<ShiftDTO, Shift>().ConvertUsing(
                      m =>
                      {
                          return new Shift
                          {
                              ShiftId = m.ShiftId,
                              ShiftName = m.ShiftName,
                              ShiftStatus = m.ShiftStatus,
                              ShiftDelayAllow = m.ShiftDelayAllow
                          };

                      });
            return Mapper.Map<ShiftDTO, Shift>(shiftDTO);
        }
        //public static Shift ConvertRespondentInfoFromDTO(ShiftDetailViewModel shiftDTO)
        //{


        //    Mapper.CreateMap<ShiftDetailViewModel, Shift>().ConvertUsing(
        //        m =>
        //              {
        //                  return new Shift
        //                  {
        //                      ShiftId = m.ShiftId,
        //                      ShiftName = m.ShiftName,
        //                      ShiftStatus = m.ShiftStatus,
        //                      ShiftDelayAllow = m.ShiftDelayAllow,

        //                  };

        //              });
        //    return Mapper.Map<ShiftDetailViewModel, Shift>(shiftDTO);
        //}
        public static ShiftDTO ConvertRespondentInfoToDTO(Shift shift)
        {

            Mapper.CreateMap<Shift, ShiftDTO>().ConvertUsing(
                      m =>
                      {
                          return new ShiftDTO
                          {
                              ShiftId = m.ShiftId,
                              ShiftName = m.ShiftName,
                              ShiftStatus = m.ShiftStatus,
                              ShiftDelayAllow = m.ShiftDelayAllow
                          };

                      });
            return Mapper.Map<Shift, ShiftDTO>(shift);
        }
    }
}
