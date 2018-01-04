using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class UsersUserRoleRelDTO
    {
        public int UsersUserRoleRel1 { get; set; }
        public Nullable<int> UsersIDFK { get; set; }
        public Nullable<int> UserRoleIDFK { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public virtual UserRoleDTO UserRole { get; set; }
        public virtual UserInfoDTO User { get; set; }
    }
}
