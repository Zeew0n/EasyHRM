using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.ViewModel
{
    public class EmployeeDetailsViewModel
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public int Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public string MaritalStatus { get; set; }
        public string Incapacitated { get; set; }
        public string Email { get; set; }
        public string OfficeName { get; set; }
        public string BusinessGroup { get; set; }
        public string Department { get; set; }
        public string Rank { get; set; }
        public string Shift { get; set; }
        public string Designation { get; set; }
        public string Level { get; set; }
        public string JobType { get; set; }
        public string Section { get; set; }
        public string Group { get; set; }

        public string ContractExpiryDate { get; set; }
        public string AttendanceIgnore { get; set; }
        public string Contact { get; set; }
        public string ServiceStatus { get; set; }
        public string DateOfBirth{ get; set; }
        public string PAN{ get; set; }
        public string PFNUmber{ get; set; }
        public string CITNumber{ get; set; }
        public string RemoteArea{ get; set; }
        public string MarriageAnniversary{ get; set; }
        public string BloodGroup{ get; set; }

        public string PhotoName { get; set; }
        public string JoinDate { get; set; }

        public int OfficeId { get; set; }
        public string NomineeName { get; set; }
        public string NomineeAddress { get; set; }
        public string NomineePhoto { get; set; }
        public System.DateTime NomineeDob { get; set; }
        public string NomineeRelation { get; set; }
        public int LeaveRuleId { get; set; }
        public virtual IEnumerable<EmployeeAppointmentDetailsViewModel> EmployeeAppointmentDetail { get; set; }
        public string RemoteCode { get; set; }

    }

    public class EmployeeAppointmentDetailsViewModel
    {
        public string AppointDate { get; set; }
        public string ExpiryDate { get; set; }
        public string AppointBranch { get; set; }
        public string AppointDepartment { get; set; }
        public string AppointDesignation { get; set; }
        public string AppointRank { get; set; }
        public string AppointLevel { get; set; }
        public string AppointShift { get; set; }
        public string AppointGroup { get; set; }
        public string AppointService { get; set; }
    }
}
