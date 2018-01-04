using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ResponseFormatters
{
    public class EmployeeDocumentResponseFormatter
    {
        public static EmployeeDocumentDTO EmplyeeFamilyDbToDTO(EmployeeDocument ModelData)
        {
            EmployeeDocumentDTO Record = new EmployeeDocumentDTO
            {
                DocumentCatId = ModelData.DocumentCatId,
                DocumentCreatedAt = ModelData.DocumentCreatedAt,
                DocumentEmpCode = ModelData.DocumentEmpCode,
                DocumentId = ModelData.DocumentId,
                DocumentOnlyAdmin = ModelData.DocumentOnlyAdmin,
                DocumentRemarks = ModelData.DocumentRemarks,
                DocumentTitle = ModelData.DocumentTitle,
                DocumentVerified = ModelData.DocumentVerified,
                Employee = new EmployeeDTO
                {
                    EmpCode = ModelData.Employee.EmpCode,
                    EmpName = ModelData.Employee.EmpName,
                }

            };
            return Record;
        }

        public static List<EmployeeDocumentDTO> EmplyoeeFamilyDbListToDTOList(List<EmployeeDocument> ModelData)
        {
            List<EmployeeDocumentDTO> ReturnRecord = new List<EmployeeDocumentDTO>();
            foreach (EmployeeDocument Row in ModelData)
            {
                EmployeeDocumentDTO Record = new EmployeeDocumentDTO
                {

                    DocumentCatId = Row.DocumentCatId,
                    DocumentCreatedAt = Row.DocumentCreatedAt,
                    DocumentEmpCode = Row.DocumentEmpCode,
                    DocumentId = Row.DocumentId,
                    DocumentOnlyAdmin = Row.DocumentOnlyAdmin,
                    DocumentRemarks = Row.DocumentRemarks,
                    DocumentTitle = Row.DocumentTitle,
                    DocumentVerified = Row.DocumentVerified,
                    Employee = new EmployeeDTO
                    {
                        EmpCode = Row.Employee.EmpCode,
                        EmpName = Row.Employee.EmpName
                    },
                    DocumentCategory = new DocumentCategoryDTO
                    {
                         CatId = Row.DocumentCategory.CatId,
                        CatTitle = Row.DocumentCategory.CatTitle
                    }

                };
                ReturnRecord.Add(Record);
            }
            return ReturnRecord;


        }
    }
}
