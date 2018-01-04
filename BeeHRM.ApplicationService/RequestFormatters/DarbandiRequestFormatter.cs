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
    public class DarbandiRequestFormatter
    {
        public static Darbandi ConvertRespondentInfoFromDTO(DarbandiDTO darbandiDTO)
        {

            Mapper.CreateMap<DarbandiDTO, Darbandi>().ConvertUsing(
                      m =>
                      {
                          return new Darbandi
                          {
                              DarbandiDate = m.DarbandiDate,
                              DarbandiDesgId = m.DarbandiDesgId,
                              DarbandiId = m.DarbandiId,
                              DarbandiNumber = m.DarbandiNumber,
                              DarbandiOfficeId = m.DarbandiOfficeId,
                              DarbandiRemarks = m.DarbandiRemarks,
                              DarbandiType = m.DarbandiType

                          };

                      });
            return Mapper.Map<DarbandiDTO, Darbandi>(darbandiDTO);
        }

        public static DarbandiDTO ConvertRespondentInfoToDTO(Darbandi darbandi)
        {

            Mapper.CreateMap<Darbandi, DarbandiDTO>().ConvertUsing(
                      m =>
                      {
                          return new DarbandiDTO
                          {
                              DarbandiDate = m.DarbandiDate,
                              DarbandiDesgId = m.DarbandiDesgId,
                              DarbandiId = m.DarbandiId,
                              DarbandiNumber = m.DarbandiNumber,
                              DarbandiOfficeId = m.DarbandiOfficeId,
                              DarbandiRemarks = m.DarbandiRemarks,
                              DarbandiType = m.DarbandiType
                          };

                      });
            return Mapper.Map<Darbandi, DarbandiDTO>(darbandi);
        }
    }
}
