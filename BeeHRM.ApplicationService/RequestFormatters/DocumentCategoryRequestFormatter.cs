using AutoMapper;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.RequestFormatters
{
    public class DocumentCategoryRequestFormatter
    {
        public static DocumentCategory ConvertRespondentInfoFromDTO(DocumentCategoryDTO documentCatDTO)
        {

            Mapper.CreateMap<DocumentCategoryDTO, DocumentCategory>().ConvertUsing(
                      m =>
                      {
                          return new DocumentCategory
                          {
                              CatId = m.CatId,
                              CatTitle = m.CatTitle
                          };

                      });
            return Mapper.Map<DocumentCategoryDTO, DocumentCategory>(documentCatDTO);
        }

        public static DocumentCategoryDTO ConvertRespondentInfoToDTO(DocumentCategory documentCatDTO)
        {

            Mapper.CreateMap<DocumentCategory, DocumentCategoryDTO>().ConvertUsing(
                      m =>
                      {
                          return new DocumentCategoryDTO
                          {
                              CatId = m.CatId,
                              CatTitle = m.CatTitle,
                          };

                      });
            return Mapper.Map<DocumentCategory, DocumentCategoryDTO>(documentCatDTO);
        }
    }
}