using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ViewModel
{
   public class HolidayDetailsViewModel
    {
        public int HolidayId { get; set; }
        public string HolidayName { get; set; }
        public string Religion { get; set; }       
        public bool HolidayStatus { get; set; }
        public IEnumerable<HolidayOfficesViewModel> OfficesList { get; set; }
        public string HolidayDescription { get; set; }
        public string HolidayTitle { get; set; }
        public System.DateTime HolidayDate { get; set; }
        public bool HolidayYearlyOccourence { get; set; }
        public int HolidayOfficeId { get; set; }
        public int HolidayReligionId { get; set; }
        public int HolidayEthnicityId { get; set; }
        public string HolidayGender { get; set; }
        public string ReligionName { get; set; }
        public string EthnicityName { get; set; }
        public string HolidayOffices { get; set; }



    }
}
