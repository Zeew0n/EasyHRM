using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BeeHRM.ApplicationService.DTOs
{
  public  class EmployeeFamilyDTO
    {
        public int FamilyId { get; set; }
        public int EmpCode { get; set; }
        public int EmpId { get; set; }
        public string Fname { get; set; }
        public Nullable<System.DateTime> FDob { get; set; }
        public string FDobNP { get; set; }
        public string FGender { get; set; }
        public string FRelation { get; set; }
        public string FContactNumber { get; set; }
        public string FContactAddress { get; set; }
        public IEnumerable<SelectListItem> EmployeeCodeSelectlist { get; set; }
        public  EmployeeDTO Employee { get; set; }
    }
    public partial class EmployeeFamilyInformation
    {
    public List<EmployeeFamilyDTO> EmployeeFamilyList { get; set;}
        public int EmpId{get; set;}
        public IEnumerable<SelectListItem> EmployeeCodeSelectlist { get; set; }
    }
}
