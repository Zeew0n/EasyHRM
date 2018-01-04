using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeeHrmClientWeb.Models
{
    public class ChangePassword
    {

        public int EmpCode { get; set; }
        [Required(ErrorMessage = "Old Password is required !!")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "New password is required !!")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password is required !!")]
        public string ConfirmPassword { get; set; }
    }
}