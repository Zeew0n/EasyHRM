using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class DesignationDTO
    {
        public int DsgId { get; set; }
        [Required(ErrorMessage ="Designation Name is required")]
        [DisplayName("Designation")]
        public string DsgName { get; set; }
        public int DsgParentId { get; set; }
        [Required(ErrorMessage = "Designation Code is required")]
        public string DesgCode { get; set; }
        [Required(ErrorMessage = "Designation short Code is required")]
        public string DesgShortCode { get; set; }
        public Nullable<int> DesgOrder { get; set; }
        public Nullable<bool> LeaveRecommendation { get; set; }
        public Nullable<bool> LeaveApprove { get; set; }

        public Nullable<bool> AttendanceRecommendation { get; set; }
        public Nullable<bool> AttendanceApprove { get; set; }
        [Required(ErrorMessage = "Designation Max period is required")]
        public Nullable<int> DesgMaxPeriodDays { get; set; }
        public Nullable<int> DesgLevelId { get; set; }
        public string LevelName { get; set; }
        [Required(ErrorMessage = "Designation Salary is required")]
        public Nullable<decimal> DesgSalary { get; set; }
        [Required(ErrorMessage = "Designation Per Grade is required")]
        public Nullable<decimal> DesgPerGradeAmount { get; set; }
        public Nullable<int> DesgMaxGrade { get; set; }
        [Required(ErrorMessage = "Designation special allowance is required")]
        public Nullable<decimal> DesgSpecialAllowances { get; set; }
        [Required(ErrorMessage = "Designation dress amount is required")]
        public Nullable<decimal> DesgDressAmount { get; set; }
        [Required(ErrorMessage = "Designation other allowance is required")]
        public Nullable<decimal> DesgOtherAllowance { get; set; }
        [Required(ErrorMessage = "Designation dearness allowance is required")]
        public Nullable<decimal> DesgDearnessAllowance { get; set; }
      
    }
}
