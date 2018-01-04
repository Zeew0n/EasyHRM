using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.DTOs
{
    public class RolesDTO
    {
        
        public int RoleId { get; set; }
        [Display(Name ="Name(*)")]
        public string RoleName { get; set; }
        [Display(Name = "Details")]
        public string RoleDetails { get; set; }
        [Display(Name = "Access All?")]
        public bool RoleDataAccessAll { get; set; }
        //public string[] actualBusinessGroup { get; set; }
        [Display(Name ="Business Group")]
        //public List<SelectListItem> BusinessGroup { get; set; }
        public int[] BusinessGroup { get; set; }
        //public string[]
        [Display(Name ="Offices")]
        //public List<SelectListItem> Office { get; set; }
        public int[] Office { get; set; }
    }
}
