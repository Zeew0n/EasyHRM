using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;

namespace BeeHRM.ApplicationService.RequestFormatters
{
    public class PayrollLeaveDeductionRequestFormatter
    {
        public static PayrollLeaveDeduction PayrollLeaveDeductionDtoToDb(PayrollLeaveDeductionDTO Data)
        {
            PayrollLeaveDeduction Record = new PayrollLeaveDeduction
            {
                DeductionId = Data.DeductionId,
                EmpCode = Data.EmpCode,
                LeaveTypeId = Data.LeaveTypeId,
                LeaveDays = Data.LeaveDays,
                DeductionType = Data.DeductionType,
                LeaveYearId = Data.LeaveYearId,
                LeaveDate = Data.LeaveDate,
                Details = Data.Details,
                isEncashed = Data.isEncashed,
            };
            return Record;
        }
    }
}
