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
   public class EmployeeTrainingRequestFormatter
    {
        //public static EmployeeTraining ConvertRespondentInfoFromDTO(EmployeeTrainingDTO employeeTrainingDTO)
        //{

        //    Mapper.CreateMap<EmployeeTrainingDTO, EmployeeTraining>().ConvertUsing(
        //        m=>
        //        {
        //            return new EmployeeTraining
        //            {
        //                CountryId = m.CountryId,
        //                //CountryName = m.Country.CountryName,
        //                EmpCode = m.EmpCode,
        //                TrainingDays = m.TrainingDays,
        //                TrainingDescription = m.TrainingDescription,
        //                TrainingEndDate = m.TrainingEndDate,
        //                TrainingId = m.TrainingId,
        //                TrainingName = m.TrainingName,
        //                TrainingProvidedBy = m.TrainingProvidedBy,
        //                TrainingStartDate = m.TrainingStartDate,
        //                TrainingSubject = m.TrainingSubject,
        //                TrainingYear = m.TrainingYear,
        //                CurrentDesignation = m.CurrentDesignation,
        //                CurrentRank = m.CurrentRank,
        //                CurrentOffice = m.CurrentOffice,
        //                AssignedBy = m.AssignedBy,
        //                Sponsorship = m.Sponsorship,
        //                NationalInternational = m.NationalInternational,
        //                Venue = m.Venue
        //            };
        //        }
                
        //        );
        //    return Mapper.Map<EmployeeTrainingDTO, EmployeeTraining>(employeeTrainingDTO);
        //}

        //public static EmployeeTrainingDTO ConvertRespondentInfoToDTO(EmployeeTraining employeeTraining)
        //{

        //    Mapper.CreateMap<EmployeeTraining, EmployeeTrainingDTO>().ConvertUsing(
        //      m =>
        //      {
        //          return new EmployeeTrainingDTO
        //          {
        //              CountryId = m.CountryId,
        //              CountryName = m.Country.CountryName,
        //              EmpCode = m.EmpCode,
        //              TrainingDays = m.TrainingDays,
        //              TrainingDescription = m.TrainingDescription,
        //              TrainingEndDate = m.TrainingEndDate,
        //              TrainingId = m.TrainingId,
        //              TrainingName = m.TrainingName,
        //              TrainingProvidedBy = m.TrainingProvidedBy,
        //              TrainingStartDate = m.TrainingStartDate,
        //              TrainingSubject = m.TrainingSubject,
        //              TrainingYear = m.TrainingYear,
        //              CurrentDesignation = m.CurrentDesignation,
        //              CurrentRank = m.CurrentRank,
        //              CurrentOffice = m.CurrentOffice,
        //              AssignedBy = m.AssignedBy,
        //              Sponsorship = m.Sponsorship,
        //              NationalInternational = m.NationalInternational,
        //              Venue = m.Venue
        //          };
        //      });
        //    return Mapper.Map<EmployeeTraining, EmployeeTrainingDTO>(employeeTraining);
        //}
    }
}
