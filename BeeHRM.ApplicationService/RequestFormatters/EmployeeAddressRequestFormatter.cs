using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.RequestFormatters
{
    public static class EmployeeAddressRequestFormatter
    {
        public static EmployeeAddress ConvertEmployeeAddressFromDTO(EmployeeAddressDTO Item)
        {
            EmployeeAddress Record = new EmployeeAddress
            {
                Id = Item.Id,
                EmployeeCode = Item.EmployeeCode,
                HouseNumber = Convert.ToInt32(Item.HouseNumber),
                WardNumber = Convert.ToInt32(Item.WardNumber),
                VDC = Item.VDC,
                District = Item.District,
                Zone = Item.Zone,
                State = Item.State,
                Country = Item.Country,
                LandMark = Item.LandMark,
                AddressType = Item.AddressType,
            };
            return Record;
        }
        public static EmployeeAddressDTO ConvertEmployeeAddressToDTO(EmployeeAddress Item)
        {
            EmployeeAddressDTO Record = new EmployeeAddressDTO
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
            return Record;
        }
    }
}
