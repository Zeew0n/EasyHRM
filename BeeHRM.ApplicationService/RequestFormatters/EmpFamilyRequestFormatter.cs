using AutoMapper;
using BeeHRM.ApplicationService.ViewModel;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.RequestFormatters
{
    public class EmpFamilyRequestFormatter
    {
        public static EmployeeFamily ConvertRespondentInfoFromDTO(EmployeeFamilyViewModel empFamily)
        {

            Mapper.CreateMap<EmployeeFamilyViewModel, EmployeeFamily>().ConvertUsing(
                      m =>
                      {
                          return new EmployeeFamily
                          {
                              EmpCode = m.EmpCode,
                              FamilyId = m.FamilyId,
                              FContactAddress = m.FContactAddress,
                              FContactNumber = m.FContactNumber,
                              FDob = m.FDob,
                              FGender = m.FGender,
                              Fname = m.Fname,
                              FRelation = m.FRelation

                          };

                      });
            return Mapper.Map<EmployeeFamilyViewModel, EmployeeFamily>(empFamily);
        }

        public static EmployeeFamilyViewModel ConvertRespondentInfoToDTO(EmployeeFamily family)
        {

            Mapper.CreateMap<EmployeeFamily, EmployeeFamilyViewModel>().ConvertUsing(
                      m =>
                      {
                          return new EmployeeFamilyViewModel
                          {
                              EmpCode = m.EmpCode,
                              FamilyId = m.FamilyId,
                              FContactAddress = m.FContactAddress,
                              FContactNumber = m.FContactNumber,
                              FDob =Convert.ToDateTime(m.FDob),
                              FGender = m.FGender,
                              Fname = m.Fname,
                              FRelation = m.FRelation

                          };

                      });
            return Mapper.Map<EmployeeFamily, EmployeeFamilyViewModel>(family);
        }
    }
}
