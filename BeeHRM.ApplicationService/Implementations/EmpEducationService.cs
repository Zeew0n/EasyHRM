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
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Implementations
{
    public class EmpEducationService : IEmpEducationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmpEducationService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
            
        }

        public void DeleteEducation(int eduId)
        {
            _unitOfWork.EmployeeEducationRepository.Delete(eduId);
            _unitOfWork.Save();
        }

        public IEnumerable<EmpEducationDTO> GetAllEducationById(int id)
        {
            IEnumerable<EmpEducationDTO> empEducationData = EmpEducationResponseFormatter.ModelData(_unitOfWork.EmployeeEducationRepository.All().Where(x => x.EmpCode == id).ToList());
            foreach (var row in empEducationData)
            {
                row.CountryName = _unitOfWork.CountryReposityory.GetById(row.CountryId).CountryName;
                row.EducationLevelName = _unitOfWork.EducationLevelRepository.GetById(row.EmpEduLevelId).LevelName;
            }
            return empEducationData;
        }

        public EmpEducationDTO GetEducationByEduId(int eduId)
        {
            return EmpEducationRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.EmployeeEducationRepository.GetById(eduId));
        }

        public EmpEducationDTO InsertEmpEducation(EmpEducationDTO data)
        {
            EmployeeEducation dataToInsert = EmpEducationRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return EmpEducationRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.EmployeeEducationRepository.Create(dataToInsert));
        }

        public int UpdateEducation(EmpEducationDTO data)
        {
            EmployeeEducation dataToUpdate = EmpEducationRequestFormatter.ConvertRespondentInfoFromDTO(data);
            var res = _unitOfWork.EmployeeEducationRepository.Update(dataToUpdate);
            return res;
        }
        public IEnumerable<SelectListItem> GetCountryList()
        {
            List<SelectListItem> Record = new List<SelectListItem>();
            List<Country> CountryList = _unitOfWork.CountryReposityory.Get().ToList();
            foreach (Country item in CountryList)
            {
                SelectListItem selectData = new SelectListItem();
                selectData.Text = item.CountryName.ToString();
                selectData.Value = item.CountryId.ToString();
                Record.Add(selectData);
            }
            return Record;
        }
        public IEnumerable<SelectListItem> GetEducationLevelList()
        {
            List<SelectListItem> Record = new List<SelectListItem>();
            List<EducationLevel> EducationLevelList = _unitOfWork.EducationLevelRepository.Get().ToList();
            foreach (EducationLevel item in EducationLevelList)
            {
                SelectListItem selectData = new SelectListItem();
                selectData.Text = item.LevelName.ToString();
                selectData.Value = item.LevelId.ToString();
                Record.Add(selectData);
            }
            return Record;
        }
    }
}