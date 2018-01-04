using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IBusinessGroupService
    {
        IEnumerable<BusinessGroupDTO> GetBusinessGroupList();
        IEnumerable<BusinessGroupDTO> GetBusinessGroupByEmpRoleId(int roleId);
        BusinessGroupDTO InsertDepartment(BusinessGroupDTO data);
        BusinessGroupDTO GetBusinessGroupById(int bgID);
        int UpdateBusinessGroup(BusinessGroupDTO data);
    }
}
