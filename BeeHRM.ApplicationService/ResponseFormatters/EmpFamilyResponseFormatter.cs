using AutoMapper;
using BeeHRM.ApplicationService.ViewModel;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ResponseFormatters
{
    public class EmpFamilyResponseFormatter
    {
        public static IEnumerable<EmployeeFamilyViewModel> ModelData(IEnumerable<EmployeeFamily> modelData)
        {

            Mapper.CreateMap<EmployeeFamily, EmployeeFamilyViewModel>().ConvertUsing(

                m =>
                {
                    return new EmployeeFamilyViewModel
                    {
                        EmpCode = m.EmpCode,
                        FamilyId = m.FamilyId,
                        FRelation = m.FRelation,
                        Fname = m.Fname,
                        FGender = m.FGender,
                        FContactAddress = m.FContactAddress,
                        FContactNumber = m.FContactNumber,
                        FDob =Convert.ToDateTime(m.FDob),
                        
                    };

                }
                );
            return Mapper.Map<IEnumerable<EmployeeFamily>, IEnumerable<EmployeeFamilyViewModel>>(modelData);
        }
    }
}
