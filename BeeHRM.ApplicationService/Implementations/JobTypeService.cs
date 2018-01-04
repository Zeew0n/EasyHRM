using BeeHRM.ApplicationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using BeeHRM.Repository;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.ApplicationService.RequestFormatters;

namespace BeeHRM.ApplicationService.Implementations
{
    public class JobTypeService : IJobTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public JobTypeService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public void DeleteJobTypeById(int id)
        {
            _unitOfWork.JobTypeRepository.Delete(id);
            _unitOfWork.Save();
        }

        public JobTypeDTO GetJobTypeId(int id)
        {
            return JobTypeRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.JobTypeRepository.GetById(id));
        }

        public IEnumerable<JobTypeDTO> GetJobTypeList()
        {
            IEnumerable<JobType> modelDatas = _unitOfWork.JobTypeRepository.All().ToList();
            return JobTypeResponseFormatter.ModelData(modelDatas);
        }

        public JobTypeDTO InsertJobType(JobTypeDTO data)
        {
            JobType dataToInsert = new JobType();
            dataToInsert = JobTypeRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return JobTypeRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.JobTypeRepository.Create(dataToInsert));
        }

        public int UpdateJobType(JobTypeDTO data)
        {
            JobType dataToUpdate = JobTypeRequestFormatter.ConvertRespondentInfoFromDTO(data);
            int res = _unitOfWork.JobTypeRepository.Update(dataToUpdate);
            _unitOfWork.Save();
            return res;
        }
    }
}
