using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.RequestFormatters
{
  public  class EmployeeFamilyRequestFormatter
    {
        public static EmployeeFamily EmployeeFamilyDTOToDb(EmployeeFamilyDTO ModelData)
        {
            EmployeeFamily Record = new EmployeeFamily
            {
                
                EmpCode = ModelData.EmpCode,
                FamilyId = ModelData.FamilyId,
                Fname = ModelData.Fname,
                FDob = ModelData.FDob,
                FGender = ModelData.FGender,
                FRelation = ModelData.FRelation,
                FContactNumber = ModelData.FContactNumber,
                FContactAddress = ModelData.FContactAddress,
           };
            return Record;
            
        }
    }
}
