using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
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
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private Department _dept;

        public DepartmentService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
            _dept = new Department();
        }

        public IEnumerable<DepartmentDTO> GetDepartmentlist()
        {

            IEnumerable<Department> modelDatas = _unitOfWork.DepartmentRepository.All().ToList();
            return DepartmentResponseFormatter.ModelData(modelDatas);
        }
        
        public DepartmentDTO InsertDepartment(DepartmentDTO data)
        {
            Department dept = DepartmentRequestFormatter.ConvertRespondentInfoFromDTO(data);
            _dept.DeptId = dept.DeptId;
            _dept.DeptCode = dept.DeptCode;
            _dept.DeptName = dept.DeptName;
            
            return DepartmentRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.DepartmentRepository.Create(dept));
        }

        public DepartmentDTO GetDepartmentById(int deptId)
        {
            var departmentList = _unitOfWork.DepartmentRepository.All();
            DepartmentDTO list = (from dept in departmentList
                                                         select new DepartmentDTO
                                                         {
                                                             DeptCode = dept.DeptCode,
                                                             DeptId = dept.DeptId,
                                                             DeptName = dept.DeptName

                                                         }).Where(x=>x.DeptId == deptId).FirstOrDefault();
            return list;
        }

        public int UpdateDepartment(DepartmentDTO data)
        {
            Department dept = DepartmentRequestFormatter.ConvertRespondentInfoFromDTO(data);
            var response = _unitOfWork.DepartmentRepository.Update(dept);
            _unitOfWork.Save();
            return response;
        }
    }
}
