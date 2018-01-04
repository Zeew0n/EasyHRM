using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ResponseFormatters
{
    public static class EmpTrainingResponseFormatter
    {
        public static IEnumerable<EmpTrainingDTO> ModelListData(IEnumerable<EmpTraining> modelData)
        {
            List<EmpTrainingDTO> Record = new List<EmpTrainingDTO>();
            foreach (EmpTraining Item in modelData)
            {
                EmpTrainingDTO Singles = new EmpTrainingDTO {
                    Id = Item.Id,
                    EmpCode = Item.EmpCode,
                    Designation = Item.Designation,
                    Rank = Item.Rank,
                    Office = Item.Office,
                    TrainingTitle = Item.TrainingTitle,
                    TrainingFocus = Item.TrainingFocus,
                    TrainingStartDate = Item.TrainingStartDate,
                    TrainingEndDate = Item.TrainingEndDate,
                    TrainingVisitStartDate = Item.TrainingVisitStartDate,
                    TrainingVisitEndDate = Item.TrainingVisitEndDate,
                    TrainingOrganization = Item.TrainingOrganization,
                    NationalInternational = Item.NationalInternational,
                    TrainingCountry = Item.TrainingCountry,
                    TrainingVenue = Item.TrainingVenue,
                    Sponsorship = Item.Sponsorship,
                    SponsorerOrganizationName=Item.SponsorerOrganizationName,
                    Approver = Item.Approver,
                    Designation1 = new DesignationDTO
                    {

                    },
                    Employee = new EmployeeDTO
                    {

                    },
                    Office1 = new OfficeDTOs
                    {

                    },
                    Rank1 = new RankDTO
                    {

                    }
                };
                Record.Add(Singles);
            }
            return Record;
        }
        public static EmpTrainingDTO ModelData(EmpTraining Record)
        {
            EmpTrainingDTO ReturnRecord = new EmpTrainingDTO
            {
                Id = Record.Id,
                EmpCode = Record.EmpCode,
                Designation = Record.Designation,
                Rank = Record.Rank,
                Office = Record.Office,
                TrainingTitle = Record.TrainingTitle,
                TrainingFocus = Record.TrainingFocus,
                TrainingStartDate = Record.TrainingStartDate,
                TrainingEndDate = Record.TrainingEndDate,
                LetterDate=Record.LetterDate,
                TrainingVisitStartDate = Record.TrainingVisitStartDate,
                TrainingVisitEndDate = Record.TrainingVisitEndDate,
                TrainingOrganization = Record.TrainingOrganization,
                NationalInternational = Record.NationalInternational,
                TrainingCountry = Record.TrainingCountry,
                TrainingVenue = Record.TrainingVenue,
                Sponsorship = Record.Sponsorship,
                SponsorerOrganizationName = Record.SponsorerOrganizationName,
                Approver = Record.Approver,
                Designation1 = new DesignationDTO
                {

                },
                Employee = new EmployeeDTO
                {

                },
                Office1 = new OfficeDTOs
                {

                },
                Rank1 = new RankDTO
                {

                }
            };
            return ReturnRecord;
        }
    }
}
