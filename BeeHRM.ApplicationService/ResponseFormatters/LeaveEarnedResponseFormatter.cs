using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ResponseFormatters
{
    public class LeaveEarnedResponseFormatter
    {
        public static List<LeaveEarnedDTO> LeaveEarnedListToDtoList(List<LeaveEarned> Data)
        {
            List<LeaveEarnedDTO> ReturnRecord = new List<LeaveEarnedDTO>();
            foreach (var Row in Data)
            {
                LeaveEarnedDTO Record = new LeaveEarnedDTO
                {
                    LeaveEarnedId = Row.LeaveEarnedId,
                   
                    LeaveEarnRequestedDate = Row.LeaveEarnRequestedDate,
                    RecommendedEmpCode = Row.RecommendedEmpCode,
                    RecommendStatus = Row.RecommendStatus,
                    RecommendStatusDate = Row.RecommendStatusDate,
                    ApproverEmpCode = Row.ApproverEmpCode,
                    ApproverStatus = Row.ApproverStatus,
                    ApproverStatusDate = Row.ApproverStatusDate,
                    WorkedStartDate = Row.WorkedStartDate,
                    WorkedEndDate = Row.WorkedEndDate,
                    LeaveTotalEanrnedDays = Row.LeaveTotalEanrnedDays,
                    LeaveTypeId = Row.LeaveTypeId,
                    EarnLeaveExpiryDate = Row.EarnLeaveExpiryDate,
                    Remarks = Row.Remarks,
                    LeaveYearId = Row.LeaveYearId,
                    EmpCode=Row.EmpCode,
                    Employee = new EmployeeDTO
                    {
                        EmpCode = Row.Employee.EmpCode,
                        EmpName = Row.Employee.EmpName
                    }
                    ,
                    //RecommendateEmployee = new EmployeeDTO
                    //{
                    //    RecommendateEmpCode = Row.Employee2.EmpCode,
                    //    RecommendateEmpName = Row.Employee2.EmpName
                    //},
                    ApprovalEmployee = new EmployeeDTO
                    {
                        ApprovalEmpCode = Row.Employee1.EmpCode,
                        ApprovalEmpName = Row.Employee1.EmpName
                    },
                    LeaveType = new LeaveTypeDTO
                    {
                        LeaveTypeId = Row.LeaveType.LeaveTypeId,
                        LeaveTypeName = Row.LeaveType.LeaveTypeName

                    }
                    
               };
                 ReturnRecord.Add(Record);

            }
            return ReturnRecord;
        }

        public static LeaveEarnedDTO LeaveEarnedToDto(LeaveEarned Data)
        {
            LeaveEarnedDTO Record = new LeaveEarnedDTO
            {
                LeaveEarnedId = Data.LeaveEarnedId,
             
                LeaveEarnRequestedDate = Data.LeaveEarnRequestedDate,
                RecommendedEmpCode = Data.RecommendedEmpCode,
                RecommendStatus = Data.RecommendStatus,
                RecommendStatusDate = Data.RecommendStatusDate,
                ApproverEmpCode=Data.ApproverEmpCode,
                ApproverStatus = Data.ApproverStatus,
                ApproverStatusDate = Data.ApproverStatusDate,
                WorkedStartDate = Data.WorkedStartDate,
                WorkedEndDate = Data.WorkedEndDate,
                LeaveTotalEanrnedDays = Data.LeaveTotalEanrnedDays,
                LeaveTypeId = Data.LeaveTypeId,
                EarnLeaveExpiryDate = Data.EarnLeaveExpiryDate,
                Remarks = Data.Remarks,
                LeaveYearId = Data.LeaveYearId,
                EmpCode = Data.EmpCode,
                 Employee = new EmployeeDTO
                 {
                     EmpCode = Data.Employee.EmpCode,
                     EmpName = Data.Employee.EmpName
                 }
            };
             return Record;
        }
    }
}