using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class EmployeeJobHistoryDTO
    {
        public int HistoryId { get; set; }
        public int EmpCode { get; set; }
        [Display(Name = "Office")]
        public Nullable<int> OfficeId { get; set; }
        [Display(Name = "Department")]
        public Nullable<int> DeptId { get; set; }
        [Display(Name = "Section")]
        public Nullable<int> SectionId { get; set; }
        [Display(Name = "कायम मुकायम् मु.क.र.र ")]
        public Nullable<bool> DesgKayamMukayamMuKaRaRa { get; set; }
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
        [Display(Name = "Job Type")]
        public Nullable<int> JobTypeId { get; set; }
        public Nullable<int> ServiceEventGroupId { get; set; }
        public Nullable<int> ServiceEventSubGroupId { get; set; }
        [Display(Name = "Service Event")]
        [DataType(DataType.MultilineText)]
        public string ServiceEvent { get; set; }
        [Display(Name = "Decision Date")]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        [Required]
        public Nullable<System.DateTime> DecisionDate { get; set; }
        public string DecisionDateNP { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Letter Issued Date")]
        [Required]
        public Nullable<System.DateTime> LetterIssueDate { get; set; }
        public string LetterIssueDateNP { get; set; }
        [Display(Name = "Chalani Number")]
        [Required]
        public string LetterRefNo { get; set; }
        [Display(Name = "Effective From")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public Nullable<System.DateTime> EffectiveDate { get; set; }
        public string EffectiveDateNP { get; set; }
        [Display(Name = "Service Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public Nullable<System.DateTime> ServiceHolidingDate { get; set; }
        public string ServiceHolidingDateNP { get; set; }
        [Display(Name = "Office Join Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public Nullable<System.DateTime> OfficeJoinDate { get; set; }
        public string OfficeJoinDateNP { get; set; }
        [Display(Name = "Effective Till?")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[Required]
        public Nullable<System.DateTime> EffectiveTillDate { get; set; }
        public string EffectiveTillDateNP { get; set; }
        [Display(Name = "सदर गर्ने अधिकारी ")]
        [Required]
        public Nullable<int> SadarGarneEmployeeCode { get; set; }
        [Display(Name = "सदर मिती")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public Nullable<System.DateTime> SadarDate { get; set; }
        public string SadarDateNP { get; set; }
        [Display(Name = "Remarks")]
        //[Required]
        public string Remarks { get; set; }
        [Display(Name = "Instruction")]
        [Required]
        [DataType(DataType.MultilineText)]
        public string Instruction { get; set; }
        [Display(Name = "काजमा खटाउने Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[Required]
        public Nullable<System.DateTime> KajStartDate { get; set; }
        public string KajStartDateNP { get; set; }
        [Display(Name = "काजमा खटाउने End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[Required]
        public Nullable<System.DateTime> KajEndDate { get; set; }
        public string KajEndDateNP { get; set; }
        public string OfficeName { get; set; }
        public string SectionName { get; set; }
        public string JobTypeName { get; set; }
        public string ServiceEventName { get; set; }
        public string ServiceEventSubName { get; set; }
        public string EmployeeName { get; set; }
        public string SadarGarneName { get; set; }
        public Nullable<decimal> RankName { get; set; }
        public string DesignationName { get; set; }
        public string DeptName { get; set; }
        public string LevelName { get; set; }
        public string ChalaniNumber { get; set; }
        public string KaajType { get; set; }

        public string RemoteCode { get; set; }

    }
}
