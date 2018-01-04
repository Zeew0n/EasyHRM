using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ViewModel
{
    public class EmployeeJobHistoryViewModel
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
        public string DateOfBirth { get; set; }
        public string PAN { get; set; }
        public string PFNUmber { get; set; }
        public string CITNumber { get; set; }
        public string RemoteArea { get; set; }
        public string MarriageAnniversary { get; set; }
        public string BloodGroup { get; set; }

        public string PhotoName { get; set; }
        public string JoinDate { get; set; }
        public int HistoryId { get; set; }
        public int EmpCode { get; set; }
        [Display(Name ="Office")]
        public Nullable<int> OfficeId { get; set; }
        [Display(Name = "Department")]
        public Nullable<int> DeptId { get; set; }
        [Display(Name = "Section")]
        public Nullable<int> SectionId { get; set; }
        [Display(Name = "कायम मुकायम् मु.क.र.र ")]
        public bool DesgKayamMukayamMuKaRaRa { get; set; }
        [Display(Name = "Designation")]
        public Nullable<int> DesgId { get; set; }
        [Display(Name = "Rank")]
        public Nullable<int> RankId { get; set; }
        [Display(Name = "Level")]
        public Nullable<int> LevelId { get; set; }
        [Display(Name = "Business Group")]
        public Nullable<int> BusinessGroupId { get; set; }
        [Display(Name = "Shift")]
        public Nullable<int> ShiftId { get; set; }
        [Display(Name = "Remote")]
        public Nullable<int> RemoteId { get; set; }
        [Display(Name="Job Type")]
        public Nullable<int> JobTypeId { get; set; }
        public Nullable<int> ServiceEventGroupId { get; set; }
        public Nullable<int> ServiceEventSubGroupId { get; set; }
        [Display(Name ="Service Event")]
        [DataType(DataType.MultilineText)]
        public string ServiceEvent { get; set; }
        [Display(Name = "Decision Date")]
        //[Required]
        public Nullable<System.DateTime> DecisionDate { get; set; }
        public string DecisionDateNP { get; set; }
        [Display(Name = "Letter Issued Date")]
        //[Required]
        public Nullable<System.DateTime> LetterIssueDate { get; set; }
        public string LetterIssueDateNP { get; set; }
        [Display(Name = "Chalani Number")]
        [Required]
        public string LetterChalaniNumber { get; set; }
        [Display(Name = "Effective From")]
        //[Required]
        public Nullable<System.DateTime> EffectiveDate { get; set; }
        public string EffectiveDateNP { get; set; }
        [Display(Name = "Service Start Date")]
        //[Required]
        public Nullable<System.DateTime> ServiceCountingFromDate { get; set; }
        public string ServiceCountingFromDateNP { get; set; }
        [Display(Name = "Office Join Date")]
        //[Required]
        public Nullable<System.DateTime> OfficeJoinDate { get; set; }
        public string OfficeJoinDateNP { get; set; }
        [Display(Name = "Effective Till?")]
        //[Required]
        public Nullable<System.DateTime> EffectiveTillDate { get; set; }
        public string EffectiveTillDateNP { get; set; }
        [Display(Name = "सदर गर्ने अधिकारी ")]
        [Required]
        public Nullable<int> SadarGarneEmployeeCode { get; set; }
        [Display(Name = "सदर मिती")]
        //[Required]
        public Nullable<System.DateTime> SadarDate { get; set; }
        public string SadarDateNP { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Remarks")]
        //[Required]
        public string Remarks { get; set; }
        [Display(Name = "Instruction")]
        [Required]
        [DataType(DataType.MultilineText)]
        public string Instruction { get; set; }
        [Display(Name = "काज Start Date")]
        //[Required]
        public Nullable<System.DateTime> KajStartDate { get; set; }
        public string KajStartDateNP { get; set; }
        [Display(Name = "काज End Date")]
        //[Required]
        public Nullable<System.DateTime> KajEndDate { get; set; }
        public string KajEndDateNP { get; set; }
        public string RemoteCode { get; set; }



    }
}
