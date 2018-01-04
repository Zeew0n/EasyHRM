using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface ILeaveService
    {
        LeaveAssignedDTO InsertLeaveAssigned(LeaveAssignedDTO lAsignDTO);
    }
}
