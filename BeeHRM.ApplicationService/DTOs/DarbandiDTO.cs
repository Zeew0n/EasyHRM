using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class DarbandiDTO
    {
        public int DarbandiId { get; set; }
        [Required(ErrorMessage ="Please choose a designation")]
        public Nullable<int> DarbandiDesgId { get; set; }
        [Required(ErrorMessage ="Please choose a branch")]
        public Nullable<int> DarbandiOfficeId { get; set; }
        [Required(ErrorMessage ="Number of darbandi is required")]
        public int DarbandiNumber { get; set; }
        public string DarbandiType { get; set; }
        //[Required(ErrorMessage ="Approved on date is required")]
        //[Display DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public Nullable<System.DateTime> DarbandiDate { get; set; }
        public string DarbandiDateNP { get; set; }
        [DataType(DataType.MultilineText)]
        public string DarbandiRemarks { get; set; }
        public string OfficeName { get; set; }
        public string DesgName { get; set; }

    }
}
