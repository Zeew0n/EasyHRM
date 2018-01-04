using BeeHRM.ApplicationService.ConnectDb;
using BeeHRM.ApplicationService.Leave_Module.DTOs;
using BeeHRM.ApplicationService.Leave_Module.Serivices.Implementation;
using BeeHRM.ApplicationService.Leave_Module.Serivices.Interface;
using BeeHrmInterface.GlobalSelectLists;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BeeHRM.ApplicationService.Leave_Module.Helper
{
    public interface ILeaveValidateHelper
    {
        LeaveApplicationDTOs ValidateLeave(LeaveApplicationDTOs Record);
    }
    public class LeaveValidateHelper : ILeaveValidateHelper
    {
        private ILeaveApplicationServices _LeaveApp;
        private ILeaveSetupservices _LeaveSetUp;
        private IDynamicSelectList _DynamicSelectList;
        public LeaveValidateHelper()
        {
            this._LeaveApp = new LeaveApplicationServices();
            this._LeaveSetUp = new LeaveSetupservices();
            this._DynamicSelectList = new DynamicSelectList();
        }
        public LeaveApplicationDTOs ValidateLeave(LeaveApplicationDTOs Record)
        {
            List<string> Error = new List<string>();
            IEnumerable<LeaveBalance> data = _LeaveApp.LeaveBalanceList(null, Record.LeaveEmpCode);
            LeaveYearsDTOs LeaveYear = _LeaveSetUp.LeaveYearList().Where(X => X.YearCurrent == true).FirstOrDefault();
            decimal Applydays = Convert.ToDecimal((Convert.ToDateTime(Record.LeaveEndDate) - Record.LeaveStartDate).TotalDays) + 1;
            decimal leavebalance = Convert.ToDecimal(data.Where(x => x.LeaveTypeId == Record.LeaveTypeId).Select(x => x.Leave_Balance).FirstOrDefault());
            Record.LeaveDays = Applydays;
            if (Record.IsHalfDay == true)
            {
                Record.LeaveDaysType = "H";
                Applydays = Convert.ToDecimal(0.5);
                Record.LeaveDays = Applydays;
                if (String.IsNullOrEmpty(Record.LeaveDaysPart))
                {
                    Error.Add("Leave Day Part is not selected.");

                }
            }
            if (Applydays > leavebalance)
            {
                Error.Add("You do not have enough leave balance for this leave type.");
            }
            if (Record.LeaveStartDate > Record.LeaveEndDate)
            {
                Error.Add("The start Date is greater than the End Date.");

            }
            if (Record.LeaveStartDate < LeaveYear.YearStartDate || Record.LeaveStartDate > LeaveYear.YearEndDate)
            {
                Error.Add("Leave Start date and end date should be between " + Convert.ToDateTime(LeaveYear.YearStartDate).ToShortDateString() + " and " + Convert.ToDateTime(LeaveYear.YearEndDate).ToShortDateString());
            }
            else if (Record.LeaveEndDate < LeaveYear.YearStartDate || Record.LeaveEndDate > LeaveYear.YearEndDate)
            {
                Error.Add("Leave Start date and end date should be between " + Convert.ToDateTime(LeaveYear.YearStartDate).ToShortDateString() + " and " + Convert.ToDateTime(LeaveYear.YearEndDate).ToShortDateString());
            }
            if (Record.LeaveApproverEmpCode == 0 || Record.RecommededEmpCode == 0)
            {
                Error.Add("Please choose both Recommender and Approver to apply leave");
            }
            if (Record.LeaveApproverEmpCode == Record.RecommededEmpCode)
            {
                //  Error.Add("Recommender and Approver can not be same person");
            }
            /**
            select * from LeaveApplications where LeaveEmpCode = 6300 AND RecommendStatus IN(1,2) and LeaveStatus in(1,2) 
                AND(('start_date' between LeaveStartDate and LeaveEndDate) OR ('end_date' between LeaveStartDate and LeaveEndDate) )

            **/

            SqlConnection conn = DbConnectHelper.GetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_LeaveApplyDuplicationCheck", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@empCode", Record.LeaveEmpCode);
            cmd.Parameters.AddWithValue("@startdate", Record.LeaveStartDate);
            cmd.Parameters.AddWithValue("@endDate", Record.LeaveEndDate);

            var cnt = Convert.ToInt32(cmd.ExecuteScalar());
            if (cnt > 0)
            {
                Error.Add("You have alread applied leave for these dates");
            }

            Record.LeaveYearId = LeaveYear.YearId;
            if (Record.RecommendStatus == 2)
            {
                Record.RecommendStatusDate = DateTime.Now;
            }
            Record.ApproverList = _DynamicSelectList.GetApproverSelectList(Record.LeaveEmpCode).ToList();
            Record.PaidLeave = Convert.ToBoolean(_LeaveSetUp.LeaveTypeList().Where(x => x.LeaveTypeId == Record.LeaveTypeId).Select(x => x.IsPayable).FirstOrDefault());
            Record.ErrorList = Error;
            return Record;
        }
    }
}
