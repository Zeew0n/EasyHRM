using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IAttendanceDailyService
    {
        IEnumerable<AttendanceDailyDTO> GetDailyAttendance(int id);

        IEnumerable<DailyAttendanceFilterViewModel> GetAttendanceDaily(int? EmpOfficeId, int? EmpDesgId, int? EmpBgId, int? EmpDeptId, int? EmpCode, DateTime StartDate);

        IEnumerable<DailyAttendanceFilterViewModel> GetAttendanceByRangeAndID(int Id, DateTime? startDate, DateTime? endDate);

        //IEnumerable<AttendanceFilterViewModel> GetAttendanceByFilter(int id, string status);
        IEnumerable<AttEmployeeLogDTO> GetAttEmployeeLog(int id, DateTime date);

        IEnumerable<DailyAttendanceFilterViewModel> GetAttendanceDailyStatus(int AdminEmpCode, DateTime date, int? code, int? degid, int? officeid);

        DataTable GetMonthlyAttendanceAll(DateTime sdate, DateTime enddate, int office);
        DataTable AttendanceTotalDaysSummary(DateTime sdate, DateTime enddate, int office);

        AttendanceRequestDTO InsertAttendanceRequest(AttendanceRequestDTO attenddances);

        IEnumerable<AttendanceRequestsListViewModel> GetRequestAttendanceList(int? empcode, int? approverid, int? recommenderid, int? requestid, int? recommendstatus);

        IEnumerable<AttendanceRequestsListViewModel> GetrequestAttendanceListByParms(int? officeid, int? empcode, DateTime startdate, DateTime enddate, int? reccode, int? appcode, int? recommendstatus, int? approvestatus);

        int UpdateAttendanceRequest(AttendanceRequestDTO attd);
        void DeleteAttendancerequest(int id);
        void GetenareDailyAttendance(int requestid);
        int UpdateIndividualAttendanceDaily(AttendanceDailyViewModel atd);

        int RejectApprovedAttendance(AttendanceRequestDTO atd);

        int InsertKaajAttendance(int Empcode, string sdate, string enddate, string type);



    }
}
