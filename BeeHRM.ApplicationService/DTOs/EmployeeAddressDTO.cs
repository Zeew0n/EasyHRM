using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.DTOs
{
    public partial class EmployeeAddressDTO
    {
        public int Id { get; set; }
        public int EmployeeCode { get; set; }
        [Required]
        public Nullable<int> HouseNumber { get; set; }
        [Required]
        public Nullable<int> WardNumber { get; set; }
        [Required]
        public string VDC { get; set; }
        [Required]
        public int District { get; set; }
        [Required]
        public int Zone { get; set; }
        public string State { get; set; }
        [Required]
        public int Country { get; set; }
        public string LandMark { get; set; }
        [Required]
        public string AddressType { get; set; }
        public EmployeeDTO Employee { get; set; }
        public DistrictDTO District1 { get; set; }
        public ZoneDTO Zone1 { get; set; }
        public CountryDTO Country1 { get; set; }
        public List<SelectListItem> DistrictSelectList { get; set; }
        public List<SelectListItem> ZoneSelectList { get; set; }
        public List<SelectListItem> CountrySelectList { get; set; }
        public List<SelectListItem> AddressTypeSelectList { get; set; }
    }
}
