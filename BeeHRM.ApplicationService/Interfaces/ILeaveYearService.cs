using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface ILeaveYearService
    {
        IEnumerable<LeaveYearDTO> GetLeaveYears();

        LeaveYearDTO InsertLeaveYear(LeaveYearDTO newLeaveYear);

        LeaveYearDTO GetLeaveYearInfoById(int id);

        int UpdateLeaveYear(LeaveYearDTO leaveYear);

        void DeleteLeaveYear(int id);
    }
}
