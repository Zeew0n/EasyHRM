using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IEmployeeTrainingService
    {
       
        List<EmpTrainingDTO> GetAllTrainingOfEmployeeWithId(int id);
        
        int CreateEmployeeTraining(EmpTrainingDTO record);
        EmpTrainingDTO GetEmployeeTrainingById(int id);
        void UpdateEmployeeTraining(EmpTrainingDTO record);
        void DeleteTraining(int id);
        void UpdateAttendaceonTraining(int id, string mode);
    }
}
