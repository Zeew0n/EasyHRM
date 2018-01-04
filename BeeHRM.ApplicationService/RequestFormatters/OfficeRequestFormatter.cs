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
    public class OfficeRequestFormatter
    {
        public static Office ConvertRespondentInfoFromDTO(OfficeDTOs officeDTO)
        {




            Mapper.CreateMap<OfficeDTOs, Office>().ConvertUsing(
                      m =>
                      {
                          return new Office
                          {
                              OfficeGeoLocation = m.OfficeGeoLocation,
                              OfficeAddress = m.OfficeAddress,
                              OfficeCode = m.OfficeCode,
                              OfficeId = m.OfficeId,
                              OfficeName = m.OfficeName,
                              OfficeParentId = m.OfficeParentId,
                              OfficePhone = m.OfficePhone,
                              OfficeStatus = m.OfficeStatus,
                              OfficeTypeId = m.OfficeTypeId,
                              IsActive = m.IsActive,
                              OfficeLocationId = m.OfficeLocationId,
                              PayrollRemmoteSetupRemoteId = m.PayrollRemmoteSetupRemoteId,
                              OfficeHeadEmpCode = m.OfficeHeadEmpCode

                          };

                      });
            return Mapper.Map<OfficeDTOs, Office>(officeDTO);
        }

        public static OfficeDTOs ConvertRespondentInfoToDTO(Office office)
        {

            Mapper.CreateMap<Office, OfficeDTOs>().ConvertUsing(
                      m =>
                      {
                          return new OfficeDTOs
                          {
                              OfficeGeoLocation = m.OfficeGeoLocation,
                              OfficeAddress = m.OfficeAddress,
                              OfficeCode = m.OfficeCode,
                              OfficeId = m.OfficeId,
                              OfficeName = m.OfficeName,
                              OfficeParentId = m.OfficeParentId,
                              OfficePhone = m.OfficePhone,
                              OfficeStatus = m.OfficeStatus,
                              OfficeTypeId = m.OfficeTypeId,
                              OfficeHeadEmpCode = m.OfficeHeadEmpCode,
                              IsActive = m.IsActive,
                              OfficeLocationId = m.OfficeLocationId,
                              PayrollRemmoteSetupRemoteId = m.PayrollRemmoteSetupRemoteId,
                          };

                      });
            return Mapper.Map<Office, OfficeDTOs>(office);
        }
    }
}
