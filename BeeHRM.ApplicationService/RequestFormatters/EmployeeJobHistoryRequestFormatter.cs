using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;

namespace BeeHRM.ApplicationService.RequestFormatters
{
    public class EmployeeJobHistoryRequestFormatter
    {
        public static EmployeeJobHistory ConvertRespondentInfoFromDTO(EmployeeJobHistoryDTO employeeJobHistoryDTO)
        {

            Mapper.CreateMap<EmployeeJobHistoryDTO, EmployeeJobHistory>();
            return Mapper.Map<EmployeeJobHistoryDTO, EmployeeJobHistory>(employeeJobHistoryDTO);
        }

        public static EmployeeJobHistoryDTO ConvertRespondentInfoToDTO(EmployeeJobHistory employeeJobHistory)

        {

            Mapper.CreateMap<EmployeeJobHistory, EmployeeJobHistoryDTO>().ConvertUsing(
                m =>
                {
                    return new EmployeeJobHistoryDTO
                    {
                        BusinessGroupId = m.BusinessGroupId,
                        EffectiveDate = m.EffectiveDate,
                        DecisionDate = m.DecisionDate,
                        DeptId = m.DeptId,
                        DesgId = m.DesgId,
                        DesgKayamMukayamMuKaRaRa = m.DesgKayamMukayamMuKaRaRa,
                        EffectiveTillDate = m.EffectiveTillDate,
                        EmpCode = m.EmpCode,
                        HistoryId = m.HistoryId,
                        Instruction = m.Instruction,
                        JobTypeId = m.JobTypeId,
                        KajEndDate = m.KajEndDate,
                        JobTypeName = m.JobType.JobTypeName,
                        KajStartDate = m.KajStartDate,
                        LetterIssueDate = m.LetterIssueDate,
                        LetterRefNo = m.LetterRefNo,
                        LevelId = m.LevelId,
                        OfficeId = m.OfficeId,
                        OfficeJoinDate = m.OfficeJoinDate,
                        OfficeName = m.Office.OfficeName,
                        RankId = m.RankId,
                        Remarks = m.Remarks,
                        RemoteId = m.RemoteId,
                        SadarDate = m.SadarDate,
                        SadarGarneEmployeeCode = m.SadarGarneEmployeeCode,
                        SectionId = m.SectionId,
                        SectionName = m.Section.SectionName,
                        ServiceEventGroupId = m.ServiceEventGroupId,
                        ServiceHolidingDate = m.ServiceHolidingDate,
                        ServiceEventName = m.ServiceEventGroup.ServiceEventGroupName,
                        ServiceEvent = m.ServiceEvent,
                        ServiceEventSubGroupId = m.ServiceEventSubGroupId,
                        ServiceEventSubName = m.ServiceEventSubGroup.ServiceEventSubGroupName,
                        ShiftId = m.ShiftId,
                        DeptName = m.Department.DeptName,
                        DesignationName = m.Designation.DsgName,

                       // SadarGarneName = m.Employee.EmpName,
                        LevelName = m.Level.LevelName,
                        //RankName = m.Rank.RankName,
                        EmployeeName = m.Employee1.EmpName,
                        KaajType = m.KaajType,
                        ChalaniNumber=m.ChalaniNumber,
                        RemoteCode=m.RemoteCode,
                    };
                });
            return Mapper.Map<EmployeeJobHistory, EmployeeJobHistoryDTO>(employeeJobHistory);
        }
    }
}
