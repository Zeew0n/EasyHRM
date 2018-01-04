using BeeHRM.ApplicationService.DTOs;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface ILeaveEarnedService
    {
        List<LeaveEarnedDTO> GetAllLeaveEarnedList(int EmpCode);
        IEnumerable<SelectListItem> GetEarnedLeaveTypeList();
        IEnumerable<SelectListItem> GetSelectedBranchEmployeeSelectList(int curentemp, int EmpCode);
        IEnumerable<SelectListItem> GetSelectedBranchApproveEmployeeSelectList(int curentemp, int ApproverEmpCode);
        IEnumerable<SelectListItem> GetRecommederSelectList(int EmpCode, int SeletecEmpCode);


        void AddLeaveEarned(LeaveEarnedDTO Record);
        LeaveEarnedDTO GetOneLeaveEarned(int id);
        void UpdateLeaveEarned(LeaveEarnedDTO Record);

        void Delete(int id);
        decimal YearlyEarnedLeave(int empCode, int leaveYearId, int leaveTypeId);

        IEnumerable<SelectListItem> GetBrancheEmployeeSelectList(int EmpCode);
        List<LeaveEarnedDTO> GetAllLeaveEarnedListByYearIdAndEmpCode(LeaveEarnedInfomationDTO Record);

    }
}
