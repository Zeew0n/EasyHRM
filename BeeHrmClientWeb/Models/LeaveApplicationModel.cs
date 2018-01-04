using BeeHRM.ApplicationService.Leave_Module.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeeHrmClientWeb.Models
{
    public class LeaveApplicationModel
    {
        public LeaveApplicationDTOs LeaveApplication { get; set; }
        public IEnumerable<LeaveApplicationDTOs> LeaveApplicationDetails { get; set; }
        public int MonthId { get; set; }
        public IEnumerable<SelectListItem> YearSelectList { get; set; }
        public IEnumerable<SelectListItem> LeaveStatusList { get; set; }
        
    }
}