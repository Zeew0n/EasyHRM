using BeeHRM.ApplicationService.Leave_Module.DTOs;
using BeeHRM.ApplicationService.ViewModel;
using System.Collections.Generic;

namespace BeeHRM.ApplicationService.Leave_Module.Serivices.Interface
{
    public interface ILeaveApplicationServices
    {
        #region LeaveBalance
        IEnumerable<LeaveBalance> LeaveBalanceList(int? YearId, int EmpCode);
        LeaveBalance LeaveBalanceSearch();
        #endregion
        #region LeaveApplication
        LeaveApplicationDTOs LeaveDetails(int LeaveId, int empcode);
        IEnumerable<LeaveApplicationDTOs> LeaveapplicationList();
        LeaveApplicationDTOs LeaveApplicationSearch();
        LeaveApplicationDTOs CreateLeaveApplicationInformation(int empcode, int leaveid, string recommendType);
        void LeaveApplicationCreate(LeaveApplicationDTOs Record);
        void LeaveApplicationUpdae(LeaveApplicationDTOs Record);
        void RejectEmployeeLeave(int id, int Empcode);
        void CancelMyLeave(int id, int Empcode);
        #endregion
        #region Leave Assign
        LeaveAssignedDTOs LeaveAssignCreateInfo(int empcode);
        void CreateLeaveAssign(LeaveAssignedDTOs data);
        #endregion


        LeaveYearlyReportViewModel YearlyOfficewiseLeaveReport(int empCode, int LeaveTypeId, int LeaveYearId);

    }
}
