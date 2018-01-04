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
   public class EmpEducationRequestFormatter
    {
        public static EmployeeEducation ConvertRespondentInfoFromDTO(EmpEducationDTO employeeEducationDTO)
        {

            Mapper.CreateMap<EmpEducationDTO, EmployeeEducation>().ConvertUsing(

               m =>
               {
                   return new EmployeeEducation
                   {
                       EduId = m.EduId,
                       CountryId = m.CountryId,
                       EducationStatus = m.EducationStatus,
                       EmpCode = m.EmpCode,
                       DegreeName = m.DegreeName,
                       EmpEduLevelId = m.EmpEduLevelId,
                       FacultyName = m.FacultyName,
                       MarkingSystem = m.MarkingSystem,
                       ObtainedMark = m.ObtainedMark,
                       PassedDate = Convert.ToDateTime(m.PassedDate),
                       //ScanDocument = m.ScanDocument,
                       UniversityName = m.UniversityName,
                       //Division = m.Division
                       //Country = new CountryDTO
                       //{
                       //    CountryName = m.Country.CountryName,
                       //    CountryId = m.Country.CountryId
                       //},
                       //EducationLevel = new EducationLevelDTO
                       //{
                       //    LevelName = m.EducationLevel.LevelName,
                       //    LevelId = m.EducationLevel.LevelId
                       //}
                   };

               }
               );
            return Mapper.Map<EmpEducationDTO, EmployeeEducation>(employeeEducationDTO);
        }

        public static EmpEducationDTO ConvertRespondentInfoToDTO(EmployeeEducation employeeEducation)
        {

            Mapper.CreateMap<EmployeeEducation, EmpEducationDTO>().ConvertUsing(

               m =>
               {
                   return new EmpEducationDTO
                   {
                       EduId = m.EduId,
                       CountryId = m.CountryId,
                       EducationStatus = m.EducationStatus,
                       EmpCode = m.EmpCode,
                       DegreeName = m.DegreeName,
                       EmpEduLevelId = m.EmpEduLevelId,
                       FacultyName = m.FacultyName,
                       MarkingSystem = m.MarkingSystem,
                       ObtainedMark = m.ObtainedMark,
                       PassedDate = Convert.ToDateTime(m.PassedDate),
                       ScanDocument = m.ScanDocument,
                       UniversityName = m.UniversityName,
                       Division = m.Division
                       //Country = new CountryDTO
                       //{
                       //    CountryName = m.Country.CountryName,
                       //    CountryId = m.Country.CountryId
                       //},
                       //EducationLevel = new EducationLevelDTO
                       //{
                       //    LevelName = m.EducationLevel.LevelName,
                       //    LevelId = m.EducationLevel.LevelId
                       //}
                   };

               }
               );
            return Mapper.Map<EmployeeEducation, EmpEducationDTO>(employeeEducation);
        }
    }
}
