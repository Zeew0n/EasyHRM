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
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Implementations
{
   public class EmployeeSkillService : IEmployeeSkillService
    {
         private readonly IUnitOfWork _unitOfWork;
        public EmployeeSkillService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public void AddEmployeeSkills(EmployeeSkillsDTO Record)
        {
          EmployeeSkill ReturnRecord=EmployeeSkillRequestFormatter.EmployeeSkillDTOToDb(Record);
            ReturnRecord.EmpSkillStatus = true;
            _unitOfWork.EmployeeSkillRepository.Create(ReturnRecord);
        }

        public EmployeeSkillsDTO GetOneEmployeeSkills(int Id)
        {
            EmployeeSkill Record = _unitOfWork.EmployeeSkillRepository.Get(x => x.Id == Id).FirstOrDefault();
            EmployeeSkillsDTO ReturnRecord = ResponseFormatters.EmployeeSkillResponseFormatter.EmplyeeSkillDbToDTO(Record);
            return ReturnRecord;

        }

        public void UpdateEmployeeSkill(EmployeeSkillsDTO Record)
        {
            EmployeeSkill ReturnRecord = EmployeeSkillRequestFormatter.EmployeeSkillDTOToDb(Record);
            ReturnRecord.EmpCode = Record.EmpId;
            ReturnRecord.EmpSkillStatus = true;
            _unitOfWork.EmployeeSkillRepository.Update(ReturnRecord);
        }


        public List<EmployeeSkillsDTO> GetEmployeeByEmpCode(int empcode)
        {
            List<EmployeeSkill> Record = new List<EmployeeSkill>();
            Record = _unitOfWork.EmployeeSkillRepository.Get(x => x.EmpCode == empcode).ToList();
            List<EmployeeSkillsDTO> ReturnRecord = ResponseFormatters.EmployeeSkillResponseFormatter.EmplyoeeSkillDbListToDTOList(Record);
            return ReturnRecord;
        }

        public int GetEmpCode(int Id)
        {
            int EmpId = _unitOfWork.EmployeeSkillRepository.Get(x => x.Id == Id).Select(x=>x.EmpCode).FirstOrDefault();
            return EmpId;
        }

        public IEnumerable<SelectListItem> GetSkillSelectList()
        {
            IEnumerable<Skill> Modellist = _unitOfWork.SkillRepository.All().ToList();
            List<SelectListItem> ReturnRecord = new List<SelectListItem>();
            foreach (var row in Modellist)
            {
                ReturnRecord.Add(new SelectListItem
                {
                    Text = row.SkillName,
                    Value = row.SkillId.ToString()
                });
            }
            return ReturnRecord;
        }

        public void Delete(int id)
        {
            _unitOfWork.SkillRepository.Delete(id);
            
       }
    }
}
