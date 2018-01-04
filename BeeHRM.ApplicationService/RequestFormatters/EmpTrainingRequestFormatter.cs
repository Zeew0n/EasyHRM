using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.RequestFormatters
{
    public static class EmpTrainingRequestFormatter
    {
        public static EmpTraining ConvertRespondentInfoFromDTO(EmpTrainingDTO Record)
        {
            EmpTraining ReturnRecord = new EmpTraining
            {
                Id = Record.Id,
                EmpCode = Record.EmpCode,
                Designation = Record.Designation,
                
                Rank = Record.Rank,
                Office = Record.Office,
                TrainingTitle = Record.TrainingTitle,
                TrainingFocus = Record.TrainingFocus,
                TrainingStartDate = Convert.ToDateTime(Record.TrainingStartDate),
                TrainingEndDate = Convert.ToDateTime(Record.TrainingEndDate),
                TrainingVisitStartDate = Convert.ToDateTime(Record.TrainingVisitStartDate),
                TrainingVisitEndDate = Convert.ToDateTime(Record.TrainingVisitEndDate),
                TrainingOrganization = Record.TrainingOrganization,
                NationalInternational = Record.NationalInternational,
                LetterDate=Record.LetterDate,
                TrainingCountry = Record.TrainingCountry,
                TrainingVenue = Record.TrainingVenue,
                Sponsorship = Record.Sponsorship,
                SponsorerOrganizationName=Record.SponsorerOrganizationName,
                Approver = Record.Approver,
            };
            return ReturnRecord;
        }
    }
}
