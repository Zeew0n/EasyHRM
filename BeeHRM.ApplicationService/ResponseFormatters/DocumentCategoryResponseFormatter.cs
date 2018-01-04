using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ResponseFormatters
{
    public class DocumentCategoryResponseFormatter
    {
        public static DocumentCategoryDTO DocumentCategoryDbToDTO(DocumentCategory ModelData)
        {
            DocumentCategoryDTO Record = new DocumentCategoryDTO
            {
                CatId = ModelData.CatId,
                CatTitle = ModelData.CatTitle,
            };
            return Record;
        }

        public static IEnumerable<DocumentCategoryDTO> DocumentCategoryDbListToDTOList(IEnumerable<DocumentCategory> ModelData)
        {
            List<DocumentCategoryDTO> ReturnRecord = new List<DocumentCategoryDTO>();
            foreach (DocumentCategory Row in ModelData)
            {
                DocumentCategoryDTO Record = new DocumentCategoryDTO
                {

                    CatId = Row.CatId,
                    CatTitle = Row.CatTitle

                };
                ReturnRecord.Add(Record);
            }
            return ReturnRecord;


        }
    }
}
