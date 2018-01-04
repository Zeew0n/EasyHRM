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
    public class OfficeResponseFormatter
    {
        public static IEnumerable<OfficeDTOs> ModelData(IEnumerable<Office> modelData)
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
                      
                        Employee = new EmployeeDTO
                        {
                            EmpName = m.Employee.EmpName,
                            EmpCode = m.Employee.EmpCode
                        },
                        PayrollRemoteSetup = new PayrollRemoteSetupDTO
                        {
                            RemoteName = m.PayrollRemoteSetup.RemoteName,
                            RemoteId = m.PayrollRemoteSetup.RemoteId
                        }

                    };

                }
                );
            return Mapper.Map<IEnumerable<Office>, IEnumerable<OfficeDTOs>>(modelData);
        }
    }
}
