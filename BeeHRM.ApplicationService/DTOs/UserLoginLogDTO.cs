using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class UserLoginLogDTO
    {
        public int Id { get; set; }
        public Nullable<int> LoginUser { get; set; }
        public string LoginUserGUID { get; set; }
        public Nullable<System.DateTime> LoginDate { get; set; }
    }
}

