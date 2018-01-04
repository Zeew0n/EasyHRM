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
   public class EmployeeFamilyService: IEmployeeFamilyService
    {
         private readonly IUnitOfWork _unitOfWork;
        public EmployeeFamilyService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public void AddEmplyeeFamily(EmployeeFamilyDTO Record)
        {
          EmployeeFamily ReturnRecord=EmployeeFamilyRequestFormatter.EmployeeFamilyDTOToDb(Record);

            _unitOfWork.EmployeeFamilyRepository.Create(ReturnRecord);
        }

        public EmployeeFamilyDTO GetOneEmployeeFamily(int Id)
        {
         EmployeeFamily Record = _unitOfWork.EmployeeFamilyRepository.Get(x => x.FamilyId==Id).FirstOrDefault();
            EmployeeFamilyDTO ReturnRecord=ResponseFormatters.EmployeeFamilyResponseFormatter.EmplyeeFamilyDbToDTO(Record);
            return ReturnRecord;

        }

        public void UpdateEmployeeFamily(EmployeeFamilyDTO Record)
        {
            EmployeeFamily ReturnRecord = EmployeeFamilyRequestFormatter.EmployeeFamilyDTOToDb(Record);
            ReturnRecord.EmpCode = Record.EmpId;
            _unitOfWork.EmployeeFamilyRepository.Update(ReturnRecord);
        }
        //search not used
        public List<EmployeeFamilyDTO> GetEmployeeFamilyInformationByEmpCode(int? empcode)
        {
            List<EmployeeFamily> Record = new List<EmployeeFamily>();
            if (empcode == 0 || empcode == null)
            {
           Record = _unitOfWork.EmployeeFamilyRepository.All().ToList();
             }
            else
            {
             Record = _unitOfWork.EmployeeFamilyRepository.Get(x => x.EmpCode == empcode).ToList();
            }
            List<EmployeeFamilyDTO> ReturnRecord= ResponseFormatters.EmployeeFamilyResponseFormatter.EmplyoeeFamilyDbListToDTOList(Record);
             return ReturnRecord;
        }

        public List<EmployeeFamilyDTO> GetEmployeeByEmpCode(int empcode)
        {
            List<EmployeeFamily> Record = new List<EmployeeFamily>();
            Record = _unitOfWork.EmployeeFamilyRepository.Get(x => x.EmpCode == empcode).ToList();
            List<EmployeeFamilyDTO> ReturnRecord = ResponseFormatters.EmployeeFamilyResponseFormatter.EmplyoeeFamilyDbListToDTOList(Record);
            return ReturnRecord;
        }
    }
}
