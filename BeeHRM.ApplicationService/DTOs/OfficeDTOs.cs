using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.DTOs
{
    public class OfficeDTOs
    {

        public int OfficeId { get; set; }
        public Nullable<int> OfficeParentId { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
        public string OfficeAddress { get; set; }
        public string OfficePhone { get; set; }
        public string OfficeGeoLocation { get; set; }
        public Nullable<bool> OfficeStatus { get; set; }
        public Nullable<int> OfficeTypeId { get; set; }
        public Nullable<int> OfficeLocationId { get; set; }
        public bool IsActive { get; set; }
        public int PayrollRemmoteSetupRemoteId { get; set; }
        public Nullable<int> OfficeHeadEmpCode { get; set; }
        public EmployeeDTO Employee { get; set; }
        public OfficeDTOs Offices1 { get; set; }
        public string parentOfficeName { get; set; }
        public PayrollRemoteSetupDTO PayrollRemoteSetup { get; set; }
        public List<SelectListItem> EmployeeList { get; set; }
        public List<SelectListItem> RemoteList { get; set; }
        public List<OfficeDTOs> branchList { get; set; }
    }
}
