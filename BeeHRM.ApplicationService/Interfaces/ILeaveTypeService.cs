using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface ILeaveTypeService
    {
        IEnumerable<LeaveTypeDTO> GetLeaveTypes();
        LeaveTypeDTO InsertLeaveType(LeaveTypeDTO leaveType);
        bool LeaveTypeExists(LeaveTypeDTO newEmp);
        LeaveTypeDTO GetLeaveTypeById(int leaveTypeId);
        int UpdateLeaveType(LeaveTypeDTO editLeaveType);
        void DeleteLeaveType(int leaveTypeId);
    }
}
