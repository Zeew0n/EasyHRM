using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
     public interface ILeaveAssigned
    {
        int InsertLeaveAssigned(LeaveAssignedDTO data);
        int DeleteLeaveAssignRules(int AssiginId);
        int CheckLeaveExistence(int EmpCode, int LeaveTypeId, int LeaveYearId);
        int UpdateLeaveAssigned(LeaveAssignedDTO data);
        int UpdateLeaveAssignedDaysInital(int assiginId, decimal LeaveDays);
    }
}
