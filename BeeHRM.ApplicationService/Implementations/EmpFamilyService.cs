using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.ApplicationService.ViewModel;
using BeeHRM.Repository;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Implementations
{
    public class EmpFamilyService : IEmpFamilyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmpFamilyService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public EmployeeFamilyViewModel GetEmpFamilyByID(int rankId)
        {
            EmployeeFamilyViewModel rank = EmpFamilyRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.EmployeeFamilyRepository.GetById(rankId));
            return rank;
        }

        public IEnumerable<EmployeeFamilyViewModel> GetEmpFamilyList()
        {
            IEnumerable<EmployeeFamily> modelDatas = _unitOfWork.EmployeeFamilyRepository.All().ToList();
            return EmpFamilyResponseFormatter.ModelData(modelDatas);
        }

        public EmployeeFamilyViewModel InsertEmpFamily(EmployeeFamilyViewModel data)
        {
            EmployeeFamily rnk = EmpFamilyRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return EmpFamilyRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.EmployeeFamilyRepository.Create(rnk));
        }

        public int UpdateEmpFamily(EmployeeFamilyViewModel data)
        {
            EmployeeFamily rnk = EmpFamilyRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return (_unitOfWork.EmployeeFamilyRepository.Update(rnk));
        }
    }
}
