using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class DepartmentDTO
    {
        public int DeptId { get; set; }
        [Required(ErrorMessage ="Department Name is required")]
        [DisplayName("Department")]
        public string DeptName { get; set; }
        public string DeptCode { get; set; }
    }
}
