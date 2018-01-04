using BeeHRM.ApplicationService.Leave_Module.DTOs;
using BeeHRM.ApplicationService.ViewModel;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;

namespace BeeHRM.ApplicationService.Leave_Module.Mapper
{
    public class LeaveBalanceMapper
    {


        public static LeaveYearlyReportViewModel SpLeaveBalanceListToYearlyLeaveBalanceList(sp_LeaveApplicationEmployeeBalance_Result Record, int Empcode, int YearId)
        {

            LeaveYearlyReportViewModel single = new LeaveYearlyReportViewModel
            {
                LeaveTypeId = Convert.ToInt32(Record.LeaveTypeId),

                BalanceDays = Convert.ToDecimal(Record.BalanceDays),

                TotalTaken = Convert.ToDecimal(Record.TotalTaken),
                TotalLeaveDays = Convert.ToDecimal(Record.TotalLeaveDays),
                EmpCode = Empcode,
                LeaveYearId = YearId,
                PrevYearBalance = Convert.ToDecimal(Record.PrevYearBalance),
                ThisYearEarned = Convert.ToDecimal(Record.ThisYearEarned)
            };

            return single;
        }

        public static List<LeaveBalance> SpLeaveBalanceListToLeaveBalanceList(List<sp_LeaveApplicationEmployeeBalance_Result> Record, int Empcode, int YearId)
        {
            List<LeaveBalance> Result = new List<LeaveBalance>();
            foreach (var item in Record)
            {
                LeaveBalance single = new LeaveBalance
                {
                    LeaveTypeId = Convert.ToInt32(item.LeaveTypeId),
                    LeaveTypeName = item.LeaveTypeName,
                    Leave_Balance = Convert.ToDecimal(item.BalanceDays),
                    LeaveTypeAssignment = item.LeaveTypeAssignment,
                    TotalTaken = Convert.ToDecimal(item.TotalTaken),
                    TotalLeaveAssigned = Convert.ToDecimal(item.TotalLeaveDays),
                    EmpCode = Empcode,
                    LeaveYearId = YearId,
                    PrevYearBalance = Convert.ToDecimal(item.PrevYearBalance),
                    ThisYearEarned = Convert.ToDecimal(item.ThisYearEarned)

                };
                Result.Add(single);
            }
            return Result;
        }
        public static LeaveBalance SpLeaveBalanceToLeaveBalance(sp_LeaveApplicationEmployeeBalance_Result Record, int Empcode, int YearId)
        {
            LeaveBalance Result = new LeaveBalance
            {
                LeaveTypeId = Convert.ToInt32(Record.LeaveTypeId),
                LeaveTypeName = Record.LeaveTypeName,
                Leave_Balance = Convert.ToDecimal(Record.BalanceDays),
                LeaveTypeAssignment = Record.LeaveTypeAssignment,
                TotalTaken = Convert.ToDecimal(Record.TotalTaken),
                TotalLeaveAssigned = Convert.ToDecimal(Record.TotalLeaveDays),
                EmpCode = Empcode,
                LeaveYearId = YearId,
            };
            return Result;
        }
    }
}
