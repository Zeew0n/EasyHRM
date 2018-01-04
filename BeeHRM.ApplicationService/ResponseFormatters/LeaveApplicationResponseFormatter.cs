using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ResponseFormatters
{
  public  class LeaveApplicationResponseFormatter
    {

        public static List<LeaveApplicationDTO> LeaveApplicationDbListToModelList(List<LeaveApplication> modelData)
        {
            List<LeaveApplicationDTO> ReturnRecord = new List<LeaveApplicationDTO>();
            foreach (var Row in modelData)
            {
                LeaveApplicationDTO Record = new LeaveApplicationDTO
                {
                    LeaveId = Row.LeaveId,
                    LeaveEmpCode = Row.LeaveEmpCode,
                    LeaveYearId = Row.LeaveYearId,
                    LeaveTypeId = Row.LeaveTypeId,
                    RecommededEmpCode = Row.RecommededEmpCode,
                    RecommendStatus = Row.RecommendStatus,
                    RecommenderMessage = Row.RecommenderMessage,
                    RecommendStatusDate = Row.RecommendStatusDate,
                    LeaveApproverEmpCode = Row.LeaveApproverEmpCode,
                    LeaveStatus = Row.LeaveStatus,
                    ApproverMessage = Row.ApproverMessage,
                    LeaveStatusDate = Row.LeaveStatusDate,
                    LeaveStartDate = Row.LeaveStartDate,
                    LeaveEndDate = Row.LeaveEndDate,
                    LeaveDays = Row.LeaveDays,
                    PaidLeave = Row.PaidLeave,
                    LeaveSubject = Row.LeaveSubject,
                    LeaveDetails = Row.LeaveDetails,
                    LeaveAppliedDate = Row.LeaveAppliedDate,
                    LeaveDaysType = Row.LeaveDaysType,
                    LeaveDaysPart = Row.LeaveDaysPart,
                    LeaveGUICode = Row.LeaveGUICode,
                    Employee = new EmployeeDTO
                    {
                        EmpCode = Row.Employee.EmpCode,
                        EmpName = Row.Employee.EmpName
                    },
                    RecommendateEmployee = new EmployeeDTO
                    {
                        RecommendateEmpCode = Row.Employee1.EmpCode,
                        RecommendateEmpName = Row.Employee1.EmpName
                    },
                    ApprovalEmployee = new EmployeeDTO
                    {
                        ApprovalEmpCode = Row.Employee2.EmpCode,
                        ApprovalEmpName = Row.Employee2.EmpName
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

        public static LeaveApplicationDTO LeaveApplicationDbToModel(LeaveApplication modelData)
        {

            LeaveApplicationDTO Record = new LeaveApplicationDTO
            {
                LeaveId = modelData.LeaveId,
                LeaveEmpCode = modelData.LeaveEmpCode,
                LeaveYearId = modelData.LeaveYearId,
                LeaveTypeId = modelData.LeaveTypeId,
                RecommededEmpCode = modelData.RecommededEmpCode,
                RecommendStatus = modelData.RecommendStatus,
                RecommenderMessage = modelData.RecommenderMessage,
                RecommendStatusDate = modelData.RecommendStatusDate,
                LeaveApproverEmpCode = modelData.LeaveApproverEmpCode,
                LeaveStatus = modelData.LeaveStatus,
                ApproverMessage = modelData.ApproverMessage,
                LeaveStatusDate = modelData.LeaveStatusDate,
                LeaveStartDate = modelData.LeaveStartDate,
                LeaveEndDate = modelData.LeaveEndDate,
                LeaveDays = modelData.LeaveDays,
                PaidLeave = modelData.PaidLeave,
                LeaveSubject = modelData.LeaveSubject,
                LeaveDetails = modelData.LeaveDetails,
                LeaveAppliedDate = modelData.LeaveAppliedDate,
                LeaveDaysType = modelData.LeaveDaysType,
                LeaveDaysPart = modelData.LeaveDaysPart,
                LeaveGUICode = modelData.LeaveGUICode,
                LeaveType = new LeaveTypeDTO
                {
                    LeaveTypeId = modelData.LeaveType.LeaveTypeId,
                    LeaveTypeName = modelData.LeaveType.LeaveTypeName
                },
                Employee=new EmployeeDTO
                {
                    EmpCode=modelData.Employee.EmpCode,
                    EmpName=modelData.Employee.EmpName
                },
                RecommendateEmployee = new EmployeeDTO
                {
                    RecommendateEmpCode = modelData.Employee1.EmpCode,
                    RecommendateEmpName = modelData.Employee1.EmpName
                },
                ApprovalEmployee = new EmployeeDTO
                {
                    ApprovalEmpCode = modelData.Employee2.EmpCode,
                    ApprovalEmpName = modelData.Employee2.EmpName
                },



            };
                return Record;
                
            }


    }
}

