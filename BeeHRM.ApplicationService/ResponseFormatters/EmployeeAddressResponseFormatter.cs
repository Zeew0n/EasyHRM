using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ResponseFormatters
{
    public class EmployeeAddressResponseFormatter
    {
        public static IEnumerable<EmployeeAddressDTO> ModelData(IEnumerable<EmployeeAddress> modelData)
        {
            List<EmployeeAddressDTO> Record = new List<EmployeeAddressDTO>();
            foreach(EmployeeAddress Item in modelData)
            {
                EmployeeAddressDTO Singles = new EmployeeAddressDTO
                {
                    Id = Item.Id,
                    EmployeeCode = Item.EmployeeCode,
                    HouseNumber = Item.HouseNumber,
                    WardNumber = Item.WardNumber,
                    VDC = Item.VDC,
                    District = Item.District,
                    Zone = Item.Zone,
                    State = Item.State,
                    Country = Item.Country,
                    LandMark = Item.LandMark,
                    AddressType = Item.AddressType,
                    Employee = new EmployeeDTO
                    {
                        EmpName = Item.Employee.EmpName
                    },
                    Zone1 = new ZoneDTO
                    {
                        ZoneName = Item.Zone1.ZoneName,
                    },
                    District1 = new DistrictDTO
                    {
                        DistrictName = Item.District1.DistrictName,
                    },
                    Country1 = new CountryDTO
                    {
                        CountryName = Item.Country1.CountryName
                    }
                };
                Record.Add(Singles);
            }
            return Record;
        }
    }
}
