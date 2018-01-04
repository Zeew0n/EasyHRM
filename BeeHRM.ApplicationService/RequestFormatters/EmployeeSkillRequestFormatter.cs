using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.RequestFormatters
{
  public  class EmployeeSkillRequestFormatter
    {
        public static EmployeeSkill EmployeeSkillDTOToDb(EmployeeSkillsDTO ModelData)
        {
            EmployeeSkill Record = new EmployeeSkill
            {

                Id = ModelData.Id,     
                EmpCode= ModelData.EmpCode,
                EmpSkillId= ModelData.EmpSkillId,     
                EmpSkillPoint= ModelData.EmpSkillPoint,     
                EmpSkillStatus= ModelData.EmpSkillStatus
           };
            return Record;
            
        }
    }
}
