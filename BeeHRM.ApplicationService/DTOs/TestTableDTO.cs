using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BeeHRM.ApplicationService.DTOs
{
    public partial class TestTableDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string EMail { get; set; }
    }
}
