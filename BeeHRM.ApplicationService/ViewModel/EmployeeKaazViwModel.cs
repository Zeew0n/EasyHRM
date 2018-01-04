using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ViewModel
{
   public  class EmployeeKaazViwModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
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
        public int EmpCode { get; set; }
        [Display(Name = "Letter Issued Date")]
        //[Required]
        public Nullable<System.DateTime> LetterIssueDate { get; set; }
        public string LetterIssueDateNP { get; set; }
        [Display(Name = "सदर गर्ने अधिकारी ")]
        [Required]
        public Nullable<int> SadarGarneEmployeeCode { get; set; }
        
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
        [Display(Name = "Chalani Number")]
        [Required]
        public string LetterChalaniNumber { get; set; }
      
        public string LetterRefNo { get; set; }

        [Required]
        public string KaajType { get; set; }
    }
}
