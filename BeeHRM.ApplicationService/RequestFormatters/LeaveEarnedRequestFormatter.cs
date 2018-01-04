using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.RequestFormatters
{
 public   class LeaveEarnedRequestFormatter
    {
        public static LeaveEarned LeaveEarnedDTOToDb(LeaveEarnedDTO Data)
        {
            LeaveEarned Record = new LeaveEarned
            {
                LeaveEarnedId = Data.LeaveEarnedId,
                EmpCode = Data.EmpCode,
                LeaveEarnRequestedDate = Data.LeaveEarnRequestedDate,
                RecommendedEmpCode = Data.RecommendedEmpCode,
                RecommendStatus = Data.RecommendStatus,
                RecommendStatusDate = Data.RecommendStatusDate,
                ApproverEmpCode = Data.ApproverEmpCode,
                ApproverStatus = Data.ApproverStatus,
                ApproverStatusDate = Data.ApproverStatusDate,
                WorkedStartDate = Data.WorkedStartDate,
                WorkedEndDate = Data.WorkedEndDate,
                LeaveTotalEanrnedDays = Data.LeaveTotalEanrnedDays,
                LeaveTypeId = Data.LeaveTypeId,
                EarnLeaveExpiryDate = Data.EarnLeaveExpiryDate,
                Remarks = Data.Remarks,
                LeaveYearId = Data.LeaveYearId,
            };
            return Record;
        }
    }
}
