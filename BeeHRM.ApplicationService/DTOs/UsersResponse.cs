using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class UsersResponse
    {
        public UsersResponse()
        {
            Users = new List<UsersDTO>();
        }

        public List<UsersDTO> Users { get; set; }
    }
}
