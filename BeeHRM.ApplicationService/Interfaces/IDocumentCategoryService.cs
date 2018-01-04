using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IDocumentCategoryService
    {
        IEnumerable<DocumentCategoryDTO> GetAllDocumentCategory();
        DocumentCategoryDTO InsertDocumentCategory(DocumentCategoryDTO data);
        DocumentCategoryDTO GetDocumentCategoryById(int id);
        int UpdateDocumentCategory(DocumentCategoryDTO data);
        void DeleteDocumentCategory(int id);
    }
}
