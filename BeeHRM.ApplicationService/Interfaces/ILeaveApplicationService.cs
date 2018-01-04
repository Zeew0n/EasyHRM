using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface ILeaveApplicationService
    {
        //LeaveApplicationDTO InsertLeaveApplication(LeaveApplicationDTO leaveApplDTO);
        IEnumerable<LeaveApplicationViewModel> GetLeaveApplicationsList(int empCode, int? monthID, int? year);
        IEnumerable<LeaveApplicationViewModel> GetLeaveApplicationsListAdmin(int EmpCode, int? monthID, int? year);
        IEnumerable<YearViewModel> GetLeaveApplicationsYear();
        IEnumerable<LeaveStatViewModel> GetLeaveStatus(int id, int leaveYearId);
        IEnumerable<LeaveTypeDTO> GetValidLeaveTypes(int empCode);
        IEnumerable<EmployeeDTO> GetRecommenderList(int empCode);
        IEnumerable<EmployeeDTO> GetApproverList(int empCode);
        LeaveYearDTO GetActiveYear();
        int GetLeaveYearId(int? Year);
        LeaveApplicationDTO InsertLeaveApplication(LeaveApplicationDTO newLeave);
        int DeleteLeaveApplication(int empCode, int leaveApplicationId);
        IEnumerable<LeaveApplicationViewModel> GetLeavesByRecommenderId(int recId, int? monthID, int? year);
        LeaveApplicationViewModel GetRecommendedLeaveDetail(int empCode, int leaveId);
        void UpdateRecommendStatus(int leaveId, int status, string message);
        IEnumerable<LeaveApplicationViewModel> GetLeavesByApproverId(int appId, int? monthID, int? year);
        LeaveApplicationViewModel GetApproveLeaveDetail(int empCode, int leaveId);
        void UpdateApproveStatus(int leaveId, int status, string message);
        LeaveApplicationViewModel LeaveDetail(int Empcode, int LeaveId);
        LeaveApplicationViewModel LeaveDetailAdmin(int LeaveId);
        int UpdateLeaveAssign(int Empcode, int LeaveRuleId);
        IEnumerable<LeaveStatViewModel> UnassignedLeave(int Empcode, int LeaveRuleId, int YearId);
        double LeaveDayCalculations(int LeaveId, DateTime Sdate, DateTime Edate);
        List<LeaveMonthlyProcessDTO> GetLeaveYearList();
        void ProcessMonthlyByProcessId(int id);
        List<SelectListItem> GetLeaveYearSelectList();
        List<LeaveYearlyReportDTO> GetLeaveYearlyReport(LeaveYearlyReportWithFilter Record);
        List<PayrollLeaveDeductionDTO> GetPayrollLeaveDeduction(LeaveYearlyReportWithFilter Record);
    }
}
