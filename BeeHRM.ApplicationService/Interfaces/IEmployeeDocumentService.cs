using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IEmployeeDocumentService
    {
        EmployeeDocumentDTO InsertEmployeeDocument(EmployeeDocumentDTO newEmployeeDocument);
        EmployeeDocumentDTO GetOneEmployeeDocument(int Id);
        void UpdateEmployeeDocument(EmployeeDocumentDTO Record);
        int GetEmpCode(int Id);
        void DeleteDocument(int id);
        List<EmployeeDocumentDTO> GetEmployeeDocumentInformationByEmpCode(int? empcode);
        IEnumerable<SelectListItem> GetDocumentCategorySelectList();
        List<EmployeeDocumentDTO> GetEmployeeByEmpCode(int empcode);
    }
}
