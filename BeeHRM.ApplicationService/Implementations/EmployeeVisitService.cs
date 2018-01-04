using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.Repository;

namespace BeeHRM.ApplicationService.Implementations
{
    public class EmployeeVisitService: IEmployeeVisitService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeVisitService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public void DeleteVisit(int id)
        {
            _unitOfWork.EmployeeVisitRepository.Delete(id);
            _unitOfWork.Save();
        }

        public IEnumerable<EmployeeVisitDTO> GetAllVisitByEmpId(int id)
        {
            var list = _unitOfWork.EmployeeVisitRepository.All().Where(x => x.VisitEmpCode == id);
            IEnumerable<EmployeeVisitDTO> data = EmployeeVisitResponseFormatter.ModelData(list);
            foreach (var row in data)
            {
                row.CountryName = _unitOfWork.CountryReposityory.GetById(row.VisitCoutryId).CountryName;
                row.DesgName = _unitOfWork.DesignationRepository.GetById(row.EmpDesignationId).DsgName;
            }
            return data;
        }

        public EmployeeVisitDTO GetVisitById(int id)
        {
            EmployeeVisitDTO res = new EmployeeVisitDTO();
            res = EmployeeVisitRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.EmployeeVisitRepository.GetById(id));
            return res;
        }

        public EmployeeVisitDTO InsertVisit(EmployeeVisitDTO data)
        {
            EmployeeVisit dataToInsert = new EmployeeVisit();
            dataToInsert = EmployeeVisitRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return EmployeeVisitRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.EmployeeVisitRepository.Create(dataToInsert));
        }

        public int UpdateVisit(EmployeeVisitDTO data)
        {
            EmployeeVisit dataToUpdate = new EmployeeVisit();
            dataToUpdate = EmployeeVisitRequestFormatter.ConvertRespondentInfoFromDTO(data);
            var res = _unitOfWork.EmployeeVisitRepository.Update(dataToUpdate);
            return res;
        }
    }
}
