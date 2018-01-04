using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ViewModel
{
    public class EmployeeFamilyViewModel
    {
        public int FamilyId { get; set; }
        public int EmpCode { get; set; }
        public string Fname { get; set; }
        //[Required]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> FDob { get; set; }
        public string FDobNP { get; set; }
        public string FGender { get; set; }
        public string FRelation { get; set; }
        public string FContactNumber { get; set; }
        public string FContactAddress { get; set; }

    }
}
