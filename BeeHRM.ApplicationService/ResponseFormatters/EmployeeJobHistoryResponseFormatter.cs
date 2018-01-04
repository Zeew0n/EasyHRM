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
    public class EmployeeJobHistoryResponseFormatter
    {
        public static List<EmployeeJobHistoryDTO> ModelData(List<EmployeeJobHistory> modelData)
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
                        ServiceEventSubName = m.ServiceEventSubGroup.ServiceEventSubGroupName,
                        ServiceEvent = m.ServiceEvent,
                        ServiceEventSubGroupId = m.ServiceEventSubGroupId,
                        ShiftId = m.ShiftId,
                        KaajType = m.KaajType
                    };

                });
            return Mapper.Map<List<EmployeeJobHistory>, List<EmployeeJobHistoryDTO>>(modelData);
        }
    }
}
