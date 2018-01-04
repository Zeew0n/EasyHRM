using BeeHRM.ApplicationService.Leave_Module.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Leave_Module.Mapper
{
    public class LeaveEarnedMapper
    {
        public static LeaveEarnedDTOs LeaveEadredToLeaveEarnedDTOs(LeaveEarned Record)
        {
            LeaveEarnedDTOs result = new LeaveEarnedDTOs()
            {
                LeaveEarnedId = Record.LeaveEarnedId,
                ApproverEmpCode= Record.ApproverEmpCode,
                ApproverStatus= Record.ApproverStatus,
                ApproverStatusDate= Record.ApproverStatusDate,
                EarnLeaveExpiryDate= Record.EarnLeaveExpiryDate,
                EmpCode= Record.EmpCode,
                LeaveEarnRequestedDate= Record.LeaveEarnRequestedDate,
                LeaveTotalEanrnedDays=Record.LeaveTotalEanrnedDays,
                LeaveTypeId= Record.LeaveTypeId,
                LeaveYearId= Record.LeaveYearId,
                RecommendedEmpCode= Record.RecommendedEmpCode,
                RecommendStatus= Record.RecommendStatus,
                RecommendStatusDate= Record.RecommendStatusDate,
                Remarks= Record.Remarks,
                WorkedEndDate= Record.WorkedEndDate,
                WorkedStartDate= Record.WorkedStartDate
            };
            return result;
        }
        public static LeaveEarned LeavernedDtoToLeaveEarned(LeaveEarnedDTOs Record)
        {
            LeaveEarned result = new LeaveEarned()
            {
                LeaveEarnedId = Record.LeaveEarnedId,
                ApproverEmpCode = Record.ApproverEmpCode,
                ApproverStatus = Record.ApproverStatus,
                ApproverStatusDate = Record.ApproverStatusDate,
                EarnLeaveExpiryDate = Record.EarnLeaveExpiryDate,
                EmpCode = Record.EmpCode,
                LeaveEarnRequestedDate = Record.LeaveEarnRequestedDate,
                LeaveTotalEanrnedDays = Record.LeaveTotalEanrnedDays,
                LeaveTypeId = Record.LeaveTypeId,
                LeaveYearId = Record.LeaveYearId,
                RecommendedEmpCode = Record.RecommendedEmpCode,
                RecommendStatus = Record.RecommendStatus,
                RecommendStatusDate = Record.RecommendStatusDate,
                Remarks = Record.Remarks,
                WorkedEndDate = Record.WorkedEndDate,
                WorkedStartDate = Record.WorkedStartDate
            };
            return result;
        }
        public static List<LeaveEarnedDTOs> LeaveEarnedListToListLeaveErnedDTO(List<LeaveEarned> Record)
        {
            List<LeaveEarnedDTOs> Result = new List<LeaveEarnedDTOs>();
            foreach(var Item in Record)
            {
                LeaveEarnedDTOs single = new LeaveEarnedDTOs()
                {
                    LeaveEarnedId = Item.LeaveEarnedId,
                    ApproverEmpCode = Item.ApproverEmpCode,
                    ApproverStatus = Item.ApproverStatus,
                    ApproverStatusDate = Item.ApproverStatusDate,
                    EarnLeaveExpiryDate = Item.EarnLeaveExpiryDate,
                    EmpCode = Item.EmpCode,
                    LeaveEarnRequestedDate = Item.LeaveEarnRequestedDate,
                    LeaveTotalEanrnedDays = Item.LeaveTotalEanrnedDays,
                    LeaveTypeId = Item.LeaveTypeId,
                    LeaveYearId = Item.LeaveYearId,
                    RecommendedEmpCode = Item.RecommendedEmpCode,
                    RecommendStatus = Item.RecommendStatus,
                    RecommendStatusDate = Item.RecommendStatusDate,
                    Remarks = Item.Remarks,
                    WorkedEndDate = Item.WorkedEndDate,
                    WorkedStartDate = Item.WorkedStartDate
                };
               Result.Add(single);
            }
            return Result;
        }
    }
}
