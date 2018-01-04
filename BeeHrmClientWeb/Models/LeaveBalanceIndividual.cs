using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeeHrmClientWeb.Models
{
    public class LeaveBalanceIndividual
    {
        public List<SelectListItem> YearList { get; set; }
        public EmployeeDetailsViewModel EmpDetail { get; set; }
        public IEnumerable<LeaveStatViewModel> LeaveDetails { get; set; }
        public string LeaveuleName { get; set; }
        public IEnumerable<LeaveRuleDTO> LeaveRuleList { get; set; }
        public string CreateLink { get; set; }
        public IEnumerable<LeaveStatViewModel> UnassignLeave { get; set; }

        public int Currentyear { get; set; }
    }
}