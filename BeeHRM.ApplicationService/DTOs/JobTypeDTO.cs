using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class JobTypeDTO
    {
        public int JobtypeId { get; set; }
        [Required(ErrorMessage ="Name is required")]
        [Display(Name ="Name")]
        public string JobTypeName { get; set; }
        [Display(Name ="Payroll Type")]
        public string PayRollType { get; set; }
        [Display(Name ="Job Period (in month)")]
        public int JobPeriodMonth { get; set; }
        [Display(Name = "Job Appoinment Type")]
        public byte JobAppointmentType { get; set; }
        [Display(Name = "PF Allow")]
        public bool PfAllow { get; set; }
        public virtual ICollection<EmployeeJobHistoryDTO> EmployeeJobHistories { get; set; }
        public virtual ICollection<EmployeeDTO> Employees { get; set; }
    }
}
