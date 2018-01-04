using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class EmployeeVisitDTO
    {
        public int VisitId { get; set; }
        public int VisitEmpCode { get; set; }
        [Display(Name ="Country")]
        public int VisitCoutryId { get; set; }
        [Display(Name ="City")]
        public string VisitCity { get; set; }
        [Display(Name ="Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> VisitStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="End Date")]
        public Nullable<System.DateTime> VisitEndDate { get; set; }
        [Display(Name ="Designation")]
        public int EmpDesignationId { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime CreatedAt { get; set; }
        public string CountryName { get; set; }
        public string DesgName { get; set; }
    }
}
