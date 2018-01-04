using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.DTOs
{
    public class HolidayDTOs
    {
        public int HolidayId { get; set; }
        [Required(ErrorMessage = "HolidayTitle is required !!")]
        public string HolidayTitle { get; set; }
        [Required(ErrorMessage = "HolidayDescription is required !!")]
        public string HolidayDescription { get; set; }
        //[Required(ErrorMessage = "HolidayDate is required !!")]
        public System.DateTime HolidayDate { get; set; }
        public string HolidayDateNP { get; set; }
        public bool HolidayYearlyOccourence { get; set; }
        public int HolidayOfficeId { get; set; }
        public int HolidayReligionId { get; set; }
        public int HolidayEthnicityId { get; set; }
        public string HolidayGender { get; set; }
        public bool HolidayStatus { get; set; }
        public string ReligionName { get; set; }
        public string EthnicityName { get; set; }

        public List<SelectListItem> BranchSelectList { get; set; }
        public List<SelectListItem> EthnicitySelectList { get; set; }
        public List<SelectListItem> ReligionSelectList { get; set; }
        public List<SelectListItem> GenderList { get; set; }
        public List<SelectListItem> SelectedOffices { get; set; }

    }
}
