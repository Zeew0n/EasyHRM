using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IJobTypeService
    {
        IEnumerable<JobTypeDTO> GetJobTypeList();
        JobTypeDTO InsertJobType(JobTypeDTO data);
        JobTypeDTO GetJobTypeId(int id);
        int UpdateJobType(JobTypeDTO data);
        void DeleteJobTypeById(int id);
    }
}
