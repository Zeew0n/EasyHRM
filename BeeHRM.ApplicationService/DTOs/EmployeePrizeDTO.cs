using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class EmployeePrizeDTO
    {
        public int PrizeId { get; set; }
        public Nullable<int> PrizeEmpCode { get; set; }
        [Display(Name ="Name")]
        public string PrizeName { get; set; }
        [Display(Name = "Type")]
        public string PrizeType { get; set; }
        [Display(Name = "Designation")]
        public int PrizeDesignationId { get; set; }
        [Display(Name = "Received Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> PrizeDate { get; set; }
        public string PrizeDateNP { get; set; }
        [Display(Name = "Details")]
        public string PrizeDetails { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public string DesignationName { get; set; }
       
    }
}
