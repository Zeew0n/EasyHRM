using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ResponseFormatters
{
  public  class EmployeeFamilyResponseFormatter
    {
        public static EmployeeFamilyDTO EmplyeeFamilyDbToDTO(EmployeeFamily ModelData)
        {
            EmployeeFamilyDTO Record = new EmployeeFamilyDTO
            {
                EmpCode = ModelData.EmpCode,
                EmpId=ModelData.EmpCode,
                FamilyId = ModelData.FamilyId,
                Fname = ModelData.Fname,
                FDob = ModelData.FDob,
                FGender = ModelData.FGender,
                FRelation = ModelData.FRelation,
                FContactNumber = ModelData.FContactNumber,
                FContactAddress = ModelData.FContactAddress,
                Employee = new EmployeeDTO
                {
                    EmpCode = ModelData.Employee.EmpCode,
                    EmpName = ModelData.Employee.EmpName,
                }

            };
            return Record;
        }

        public static List<EmployeeFamilyDTO> EmplyoeeFamilyDbListToDTOList(List<EmployeeFamily> ModelData)
        {
            List<EmployeeFamilyDTO> ReturnRecord = new List<EmployeeFamilyDTO>();
            foreach (EmployeeFamily Row in ModelData)
            {
                EmployeeFamilyDTO Record = new EmployeeFamilyDTO
                {
                    
                    EmpCode = Row.EmpCode,
                    EmpId=Row.EmpCode,
                    FamilyId = Row.FamilyId,
                    Fname = Row.Fname,
                    FDob = Row.FDob,
                    FGender = Row.FGender,
                    FRelation = Row.FRelation,
                    FContactNumber = Row.FContactNumber,
                    FContactAddress = Row.FContactAddress,
                    Employee = new EmployeeDTO
                    {
                        EmpCode = Row.Employee.EmpCode,
                        EmpName = Row.Employee.EmpName
                    }

                };
                ReturnRecord.Add(Record);
            }
            return ReturnRecord;


        }
    }
}
