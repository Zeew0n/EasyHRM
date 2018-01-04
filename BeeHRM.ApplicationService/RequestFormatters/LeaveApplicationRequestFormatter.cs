using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.RequestFormatters
{
    public sealed class LeaveApplicationRequestFormatter
    {
        public static LeaveApplication ConvertRespondentInfoFromDTO(LeaveApplicationDTO leaveApplicationDTO)
        {

            Mapper.CreateMap<LeaveApplicationDTO, LeaveApplication>().ConvertUsing(
                      m =>
                      {
                          return new LeaveApplication
                          {
                                LeaveAppliedDate = m.LeaveAppliedDate,
                                LeaveStartDate = m.LeaveStartDate,
                                LeaveApproverEmpCode = m.LeaveApproverEmpCode,
                                LeaveDays = m.LeaveDays,
                                LeaveDaysType = m.LeaveDaysType,
                                LeaveDetails = m.LeaveDetails,
                                LeaveEmpCode = m.LeaveEmpCode,
                                LeaveGUICode = m.LeaveGUICode,
                                LeaveId = m.LeaveId,
                                LeaveStatus = m.LeaveStatus,
                                LeaveEndDate = m.LeaveEndDate,
                                LeaveStatusDate = m.LeaveStatusDate,
                                LeaveSubject = m.LeaveSubject,
                                LeaveTypeId = m.LeaveTypeId,
                                LeaveYearId = m.LeaveYearId,
                                PaidLeave = m.PaidLeave,
                                LeaveDaysPart = m.LeaveDaysPart,
                                RecommededEmpCode = m.RecommededEmpCode,
                                RecommendStatus = m.RecommendStatus,
                                RecommendStatusDate = m.RecommendStatusDate
                          };

                      });
            return Mapper.Map<LeaveApplicationDTO, LeaveApplication>(leaveApplicationDTO);
        }

        public static LeaveApplicationDTO ConvertRespondentInfoToDTO(LeaveApplication leave)
        {

            Mapper.CreateMap<LeaveApplication, LeaveApplicationDTO>().ConvertUsing(
                      m =>
                      {
                          return new LeaveApplicationDTO
                          {
                              LeaveAppliedDate = m.LeaveAppliedDate,
                              LeaveStartDate = m.LeaveStartDate,
                              LeaveApproverEmpCode = m.LeaveApproverEmpCode,
                              LeaveDays = m.LeaveDays,
                              LeaveDaysType = m.LeaveDaysType,
                              LeaveDetails = m.LeaveDetails,
                              LeaveEmpCode = m.LeaveEmpCode,
                              LeaveGUICode = m.LeaveGUICode,
                              LeaveId = m.LeaveId,
                              LeaveStatus = m.LeaveStatus,
                              LeaveEndDate = m.LeaveEndDate,
                              LeaveStatusDate = m.LeaveStatusDate,
                              LeaveSubject = m.LeaveSubject,
                              LeaveTypeId = m.LeaveTypeId,
                              LeaveYearId = m.LeaveYearId,
                              PaidLeave = m.PaidLeave,
                              LeaveDaysPart = m.LeaveDaysPart,
                              RecommededEmpCode = m.RecommededEmpCode,
                              RecommendStatus = m.RecommendStatus,
                              RecommendStatusDate = m.RecommendStatusDate
                          };
                      });
            return Mapper.Map<LeaveApplication, LeaveApplicationDTO>(leave);
        }

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
                        RecommendateEmpCode = Row.Employee2.EmpCode,
                        RecommendateEmpName = Row.Employee2.EmpName
                    },
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
                Employee = new EmployeeDTO
                {
                    EmpCode = modelData.Employee.EmpCode,
                    EmpName = modelData.Employee.EmpName
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
                }



            };
            return Record;

        }

    }
}
