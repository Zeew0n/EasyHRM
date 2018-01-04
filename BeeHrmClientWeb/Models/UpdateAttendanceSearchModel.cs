using BeeHRM.ApplicationService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeeHrmClientWeb.Models
{
    public class UpdateAttendanceSearchModel
    {
        public IEnumerable<DailyAttendanceFilterViewModel> AttendanceList { get; set; }
        public string Name { get; set; }
        public int Attid { get; set; }
        public string    Branch { get; set; }
        public string Department { get; set; }
        public int Empcode { get; set; }
        public string Shift { get; set; }
        public string Designation { get; set; }
        public string Group { get; set; }
        public string Level { get; set; }
        public string SearchStartdate { get; set; }
        public string SearchStartdateNP { get; set; }
        public string SeacrchEnddate { get; set; }
        public string SeacrchEnddateNP { get; set; }
    }
}