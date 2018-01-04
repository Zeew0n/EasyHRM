using BeeHRM.ApplicationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.Repository;

namespace BeeHRM.ApplicationService.Implementations
{
    public class EmployeePrizeService : IEmployeePrizeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeePrizeService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();

        }

        public void DeleteEmployeePrize(int prizeId)
        {
            _unitOfWork.EmployeePrizeRepository.Delete(prizeId);
            _unitOfWork.Save();
        }

        public IEnumerable<EmployeePrizeDTO> GetAllPrizeOfEmployee(int id)
        {
            var res = _unitOfWork.EmployeePrizeRepository.All().Where(e => e.PrizeEmpCode == id);
            return EmployeePrizeResponseFormatter.ModelData(res);
        }

        public EmployeePrizeDTO GetPrizeById(int id)
        {
            return EmployeePrizeRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.EmployeePrizeRepository.GetById(id));
        }

        public EmployeePrizeDTO InsertPrizeOfEmployee(EmployeePrizeDTO data)
        {
            EmployeePrize dataToInsert = EmployeePrizeRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return EmployeePrizeRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.EmployeePrizeRepository.Create(dataToInsert));
        }

        public int UpdateEmployeePrize(EmployeePrizeDTO data)
        {
            EmployeePrize dataToUpdate = EmployeePrizeRequestFormatter.ConvertRespondentInfoFromDTO(data);
            var res = _unitOfWork.EmployeePrizeRepository.Update(dataToUpdate);
            return res;
        }
    }
}
