using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.RequestFormatters;
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
    public class EmployeeDocumentService : IEmployeeDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeDocumentService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public EmployeeDocumentDTO InsertEmployeeDocument(EmployeeDocumentDTO Record)
        {
            EmployeeDocument ReturnRecord = EmployeeDocumentRequestFormatter.ConvertRespondentInfoFromDTO(Record);

            return EmployeeDocumentRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.EmployeeDocumentRepository.Create(ReturnRecord));
        }

        public EmployeeDocumentDTO GetOneEmployeeDocument(int Id)
        {
            EmployeeDocument Record = _unitOfWork.EmployeeDocumentRepository.Get(x => x.DocumentId == Id).FirstOrDefault();
            EmployeeDocumentDTO ReturnRecord = ResponseFormatters.EmployeeDocumentResponseFormatter.EmplyeeFamilyDbToDTO(Record);
            return ReturnRecord;

        }

        public void UpdateEmployeeDocument(EmployeeDocumentDTO Record)
        {
            EmployeeDocument ReturnRecord = EmployeeDocumentRequestFormatter.ConvertRespondentInfoFromDTO(Record);
            //ReturnRecord.DocumentEmpCode = Record.DocumentEmpCode;
            _unitOfWork.EmployeeDocumentRepository.Update(ReturnRecord);
        }
        //search not used
        public List<EmployeeDocumentDTO> GetEmployeeDocumentInformationByEmpCode(int? empcode)
        {
            List<EmployeeDocument> Record = new List<EmployeeDocument>();
            if (empcode == 0 || empcode == null)
            {
                Record = _unitOfWork.EmployeeDocumentRepository.All().ToList();
            }
            else
            {
                Record = _unitOfWork.EmployeeDocumentRepository.Get(x => x.DocumentEmpCode == empcode).ToList();
            }
            List<EmployeeDocumentDTO> ReturnRecord = ResponseFormatters.EmployeeDocumentResponseFormatter.EmplyoeeFamilyDbListToDTOList(Record);
            return ReturnRecord;
        }

        public IEnumerable<SelectListItem> GetDocumentCategorySelectList()
        {
            IEnumerable<DocumentCategory> Modellist = _unitOfWork.DocumentCategoryRepository.All().ToList();
            List<SelectListItem> ReturnRecord = new List<SelectListItem>();
            foreach (var row in Modellist)
            {
                ReturnRecord.Add(new SelectListItem
                {
                    Text = row.CatTitle,
                    Value = row.CatId.ToString()
                });
            }
            return ReturnRecord;
        }

        public List<EmployeeDocumentDTO> GetEmployeeByEmpCode(int empcode)
        {
            List<EmployeeDocument> Record = new List<EmployeeDocument>();
            Record = _unitOfWork.EmployeeDocumentRepository.Get(x => x.DocumentEmpCode == empcode).ToList();
            List<EmployeeDocumentDTO> ReturnRecord = ResponseFormatters.EmployeeDocumentResponseFormatter.EmplyoeeFamilyDbListToDTOList(Record);
            return ReturnRecord;
        }

        public int GetEmpCode(int Id)
        {
            int EmpCode = _unitOfWork.EmployeeDocumentRepository.Get(x => x.DocumentId == Id).Select(x => x.DocumentEmpCode).FirstOrDefault();
            return EmpCode;
        }

        public void DeleteDocument(int id)
        {
            _unitOfWork.EmployeeDocumentRepository.Delete(id);
        }
    }
}
