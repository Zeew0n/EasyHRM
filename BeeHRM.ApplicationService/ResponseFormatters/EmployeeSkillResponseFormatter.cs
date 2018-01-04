using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ResponseFormatters
{
  public  class EmployeeSkillResponseFormatter
    {
        public static EmployeeSkillsDTO EmplyeeSkillDbToDTO(EmployeeSkill ModelData)
        {
            EmployeeSkillsDTO Record = new EmployeeSkillsDTO
            {
                Id = ModelData.Id,
                EmpCode = ModelData.EmpCode,
                EmpSkillId = ModelData.EmpSkillId,
                EmpSkillPoint = ModelData.EmpSkillPoint,
                EmpSkillStatus = ModelData.EmpSkillStatus,
               Employee  = new EmployeeDTO
                {
                    EmpCode = ModelData.Employee.EmpCode,
                    EmpName = ModelData.Employee.EmpName,
                },
               Skill=new SkillDTO
               {
                   SkillId=ModelData.Skill.SkillId,
                   SkillName=ModelData.Skill.SkillName
               }

            };
            return Record;
        }

        public static List<EmployeeSkillsDTO> EmplyoeeSkillDbListToDTOList(List<EmployeeSkill> ModelData)
        {
            List<EmployeeSkillsDTO> ReturnRecord = new List<EmployeeSkillsDTO>();
            foreach (EmployeeSkill Row in ModelData)
            {
                EmployeeSkillsDTO Record = new EmployeeSkillsDTO
                {
                    Id = Row.Id,
                    EmpCode = Row.EmpCode,
                    EmpSkillId = Row.EmpSkillId,
                    EmpSkillPoint = Row.EmpSkillPoint,
                    EmpSkillStatus = Row.EmpSkillStatus,
                    Employee = new EmployeeDTO
                    {
                        EmpCode = Row.Employee.EmpCode,
                        EmpName = Row.Employee.EmpName,
                    },
                    Skill = new SkillDTO
                    {
                        SkillId = Row.Skill.SkillId,
                        SkillName = Row.Skill.SkillName
                    }

                };
                ReturnRecord.Add(Record);
            }
            return ReturnRecord;


        }
    }
}
