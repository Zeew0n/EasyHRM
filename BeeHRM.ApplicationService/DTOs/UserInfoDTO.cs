using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class UserInfoDTO
    {
        public int UsersID { get; set; }
        public string UserName { get; set; }
        public string UserEmailID { get; set; }
        public Nullable<bool> IsSocialLogin { get; set; }
        //public string CreatedBy { get; set; }
        //public string UpdatedBy { get; set; }
        //public Nullable<System.DateTime> CreatedDate { get; set; }
        //public Nullable<System.DateTime> UpdatedDate { get; set; }

        // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsersUserRoleRelDTO> UsersUserRoleRels { get; set; }
    }
}
