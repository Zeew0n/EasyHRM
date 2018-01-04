using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Leave_Module.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Leave_Module.Mapper
{
    public class LeaveApplicationMapper
    {
        public static LeaveApplicationDTOs LeaveApplicationToLeaveApplicationDTOss(LeaveApplication Record)
        {
            LeaveApplicationDTOs result = new LeaveApplicationDTOs()
            {
                LeaveId = Record.LeaveId,
                LeaveTypeId = Record.LeaveTypeId,
                ApproverMessage = Record.ApproverMessage,
                LeaveAppliedDate = Record.LeaveAppliedDate,
                LeaveApproverEmpCode = Record.LeaveApproverEmpCode,
                LeaveDays = Record.LeaveDays,
                LeaveDaysPart = Record.LeaveDaysPart,
                LeaveDaysType = Record.LeaveDaysType,
                LeaveDetails = Record.LeaveDetails,
                LeaveEmpCode = Record.LeaveEmpCode,
                LeaveEndDate = Record.LeaveEndDate,
                LeaveGUICode = Record.LeaveGUICode,
                LeaveStartDate = Record.LeaveStartDate,
                LeaveStatus = Record.LeaveStatus,
                LeaveStatusDate = Record.LeaveStatusDate,
                LeaveSubject = Record.LeaveSubject,
                LeaveYearId = Record.LeaveYearId,
                PaidLeave = Record.PaidLeave,
                RecommededEmpCode = Record.RecommededEmpCode,
                RecommenderMessage = Record.RecommenderMessage,
                RecommendStatus = Record.RecommendStatus,
                RecommendStatusDate = Record.RecommendStatusDate,
                EmpName = Record.Employee.EmpName,
                RecommenderName = Record.Employee1.EmpName,
                ApproverName = Record.Employee2.EmpName,
                EmployeeDetail = new EmployeeDTOs
                {
                    EmpCode = Record.Employee.EmpCode,
                    EmpName = Record.Employee.EmpName,
                    EmpPhoto = Record.Employee.EmpPhoto,
                    Designation = Record.Employee.Designation,
                    EmpLevelId = Record.Employee.EmpLevelId,
                    Level = Record.Employee.Level,
                    CurrentGrade = Record.Employee.CurrentGrade,
                    Department = Record.Employee.Department,
                    EmpEmail = Record.Employee.EmpEmail,
                    EmpGender = Record.Employee.EmpGender,
                    Office = Record.Employee.Office,
                    EmpOfficeId = Record.Employee.EmpOfficeId,
                    EmpUserName = Record.Employee.EmpUserName,
                    JobType = Record.Employee.JobType,
                    EmpAppointmentDate = Record.Employee.EmpAppointmentDate,
                    EmpCreatedDate = Record.Employee.EmpCreatedDate,
                },
                ApproverDetails = new EmployeeDTOs
                {
                    EmpCode = Record.Employee2.EmpCode,
                    EmpName = Record.Employee2.EmpName,
                    EmpPhoto = Record.Employee2.EmpPhoto,
                    Designation = Record.Employee2.Designation,
                    EmpLevelId = Record.Employee2.EmpLevelId,
                    Level = Record.Employee2.Level,
                    CurrentGrade = Record.Employee2.CurrentGrade,
                    Department = Record.Employee2.Department,
                    EmpEmail = Record.Employee2.EmpEmail,
                    EmpGender = Record.Employee2.EmpGender,
                    Office = Record.Employee2.Office,
                    EmpOfficeId = Record.Employee2.EmpOfficeId,
                    EmpUserName = Record.Employee2.EmpUserName,
                    JobType = Record.Employee2.JobType,
                    EmpAppointmentDate = Record.Employee2.EmpAppointmentDate,
                    EmpCreatedDate = Record.Employee2.EmpCreatedDate,

                },
                
                RecommenderDetails = new EmployeeDTOs
                {
                    EmpCode = Record.Employee1.EmpCode,
                    EmpName = Record.Employee1.EmpName,
                    EmpPhoto = Record.Employee1.EmpPhoto,
                    Designation = Record.Employee1.Designation,
                    EmpLevelId = Record.Employee1.EmpLevelId,
                    Level = Record.Employee1.Level,
                    CurrentGrade = Record.Employee1.CurrentGrade,
                    Department = Record.Employee1.Department,
                    EmpEmail = Record.Employee1.EmpEmail,
                    EmpGender = Record.Employee1.EmpGender,
                    Office = Record.Employee1.Office,
                    EmpOfficeId = Record.Employee1.EmpOfficeId,
                    EmpUserName = Record.Employee1.EmpUserName,
                    JobType = Record.Employee1.JobType,
                    EmpAppointmentDate = Record.Employee1.EmpAppointmentDate,
                    EmpCreatedDate = Record.Employee1.EmpCreatedDate,
                },
                Leavetypes = new LeaveTypesDTOs
                {
                    LeaveTypeId = Record.LeaveType.LeaveTypeId,
                    LeaveTypeName = Record.LeaveType.LeaveTypeName,
                },
                LeaveYear = new LeaveYearsDTOs
                {
                    YearId = Record.LeaveYear.YearId,
                    YearName =Convert.ToInt32( Record.LeaveYear.YearName),
                    YearStartDate = Record.LeaveYear.YearStartDate,
                    YearEndDate = Convert.ToDateTime(Record.LeaveYear.YearEndDate),
                    YearStartDateNp = Record.LeaveYear.YearStartDateNp,
                    YearEndDateNp = Record.LeaveYear.YearEndDateNp,
                    YearCurrent = Record.LeaveYear.YearCurrent,
                }
            };
            return result;

        }
        public static LeaveApplication LeaveApplicationDTOsToLeaveApplication(LeaveApplicationDTOs Record)
        {
            LeaveApplication result = new LeaveApplication()
            {
                LeaveId = Record.LeaveId,
                LeaveTypeId = Record.LeaveTypeId,
                ApproverMessage = Record.ApproverMessage,
                LeaveAppliedDate = Record.LeaveAppliedDate,
                LeaveApproverEmpCode = Record.LeaveApproverEmpCode,
                LeaveDays = Record.LeaveDays,
                LeaveDaysPart = Record.LeaveDaysPart,
                LeaveDaysType = Record.LeaveDaysType,
                LeaveDetails = Record.LeaveDetails,
                LeaveEmpCode = Record.LeaveEmpCode,
                LeaveEndDate = Convert.ToDateTime(Record.LeaveEndDate),
                LeaveGUICode = Record.LeaveGUICode,
                LeaveStartDate = Record.LeaveStartDate,
                LeaveStatus = Record.LeaveStatus,
                LeaveStatusDate = Record.LeaveStatusDate,
                LeaveSubject = Record.LeaveSubject,
                LeaveYearId = Record.LeaveYearId,
                PaidLeave = Record.PaidLeave,
                RecommededEmpCode = Record.RecommededEmpCode,
                RecommenderMessage = Record.RecommenderMessage,
                RecommendStatus = Record.RecommendStatus,
                RecommendStatusDate = Record.RecommendStatusDate,
                
            };
            return result;
        }
        public static List<LeaveApplicationDTOs> ListLeaveApplicationToLeaveApplicationDTOssList(List<LeaveApplication> Record)
        {
            List<LeaveApplicationDTOs> result = new List<LeaveApplicationDTOs>();
            foreach (var item in Record)
            {
                LeaveApplicationDTOs single = new LeaveApplicationDTOs()
                {
                    LeaveId = item.LeaveId,
                    LeaveTypeId = item.LeaveTypeId,
                    ApproverMessage = item.ApproverMessage,
                    LeaveAppliedDate = item.LeaveAppliedDate,
                    LeaveApproverEmpCode = item.LeaveApproverEmpCode,
                    LeaveDays = item.LeaveDays,
                    LeaveDaysPart = item.LeaveDaysPart,
                    LeaveDaysType = item.LeaveDaysType,
                    LeaveDetails = item.LeaveDetails,
                    LeaveEmpCode = item.LeaveEmpCode,
                    LeaveEndDate = item.LeaveEndDate,
                    LeaveGUICode = item.LeaveGUICode,
                    LeaveStartDate = item.LeaveStartDate,
                    LeaveStatus = item.LeaveStatus,
                    LeaveStatusDate = item.LeaveStatusDate,
                    LeaveSubject = item.LeaveSubject,
                    LeaveYearId = item.LeaveYearId,
                    PaidLeave = item.PaidLeave,
                    RecommededEmpCode = item.RecommededEmpCode,
                    RecommenderMessage = item.RecommenderMessage,
                    RecommendStatus = item.RecommendStatus,
                    RecommendStatusDate = item.RecommendStatusDate,
                    EmpName = item.Employee.EmpName,
                    RecommenderName = item.Employee1.EmpName,
                    ApproverName = item.Employee2.EmpName,
                    LeaveTypeName = item.LeaveType.LeaveTypeName,
                    EmployeeDetail = new EmployeeDTOs
                    {
                        EmpCode = item.Employee.EmpCode,
                        EmpName = item.Employee.EmpName,
                        EmpPhoto = item.Employee.EmpPhoto,
                        Designation = item.Employee.Designation,
                        EmpLevelId = item.Employee.EmpLevelId,
                        Level = item.Employee.Level,
                        CurrentGrade = item.Employee.CurrentGrade,
                        Department = item.Employee.Department,
                        EmpEmail = item.Employee.EmpEmail,
                        EmpGender = item.Employee.EmpGender,
                        Office = item.Employee.Office,
                        EmpOfficeId = item.Employee.EmpOfficeId,
                        EmpUserName = item.Employee.EmpUserName,
                        JobType = item.Employee.JobType,
                        EmpAppointmentDate = item.Employee.EmpAppointmentDate,
                        EmpCreatedDate = item.Employee.EmpCreatedDate,
                    },
                    RecommenderDetails = new EmployeeDTOs
                    {
                        EmpCode = item.Employee1.EmpCode,
                        EmpName = item.Employee1.EmpName,
                        EmpPhoto = item.Employee1.EmpPhoto,
                        Designation = item.Employee1.Designation,
                        EmpLevelId = item.Employee1.EmpLevelId,
                        Level = item.Employee1.Level,
                        CurrentGrade = item.Employee1.CurrentGrade,
                        Department = item.Employee1.Department,
                        EmpEmail = item.Employee1.EmpEmail,
                        EmpGender = item.Employee1.EmpGender,
                        Office = item.Employee1.Office,
                        EmpOfficeId = item.Employee1.EmpOfficeId,
                        EmpUserName = item.Employee1.EmpUserName,
                        JobType = item.Employee1.JobType,
                        EmpAppointmentDate = item.Employee1.EmpAppointmentDate,
                        EmpCreatedDate = item.Employee1.EmpCreatedDate,
                    },
                    ApproverDetails= new EmployeeDTOs
                    {
                        EmpCode = item.Employee2.EmpCode,
                        EmpName = item.Employee2.EmpName,
                        EmpPhoto = item.Employee2.EmpPhoto,
                        Designation = item.Employee2.Designation,
                        EmpLevelId = item.Employee2.EmpLevelId,
                        Level = item.Employee2.Level,
                        CurrentGrade = item.Employee2.CurrentGrade,
                        Department = item.Employee2.Department,
                        EmpEmail = item.Employee2.EmpEmail,
                        EmpGender = item.Employee2.EmpGender,
                        Office = item.Employee2.Office,
                        EmpOfficeId = item.Employee2.EmpOfficeId,
                        EmpUserName = item.Employee2.EmpUserName,
                        JobType = item.Employee2.JobType,
                        EmpAppointmentDate = item.Employee2.EmpAppointmentDate,
                        EmpCreatedDate = item.Employee2.EmpCreatedDate,
                    },
                    Leavetypes = new LeaveTypesDTOs
                    {
                        LeaveTypeId = item.LeaveType.LeaveTypeId,
                        LeaveTypeName = item.LeaveType.LeaveTypeName,
                    },
                    LeaveYear = new LeaveYearsDTOs
                    {
                        YearId = item.LeaveYear.YearId,
                        YearName =Convert.ToInt32(item.LeaveYear.YearName),
                        YearStartDate = item.LeaveYear.YearStartDate,
                        YearEndDate = Convert.ToDateTime(item.LeaveYear.YearEndDate),
                        YearStartDateNp = item.LeaveYear.YearStartDateNp,
                        YearEndDateNp = item.LeaveYear.YearEndDateNp,
                        YearCurrent = item.LeaveYear.YearCurrent,
                    }

                };
                result.Add(single);

            }
            return result;
        }
    }
}
