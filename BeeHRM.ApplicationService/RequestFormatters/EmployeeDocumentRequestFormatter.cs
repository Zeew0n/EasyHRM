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
    public class EmployeeDocumentRequestFormatter
    {
        public static EmployeeDocument ConvertRespondentInfoFromDTO(EmployeeDocumentDTO empDocDTO)
        {

            Mapper.CreateMap<EmployeeDocumentDTO, EmployeeDocument>().ConvertUsing(
                      m =>
                      {
                          return new EmployeeDocument
                          {
                              DocumentCatId = m.DocumentCatId,
                              DocumentCreatedAt = m.DocumentCreatedAt,
                              DocumentEmpCode = m.DocumentEmpCode,
                              DocumentId = m.DocumentId,
                              DocumentOnlyAdmin = m.DocumentOnlyAdmin,                             
                              DocumentRemarks = m.DocumentRemarks,
                              DocumentTitle = m.DocumentTitle,
                              DocumentVerified = m.DocumentVerified,
                          };

                      });
            return Mapper.Map<EmployeeDocumentDTO, EmployeeDocument>(empDocDTO);
        }
        public static EmployeeDocumentDTO ConvertRespondentInfoToDTO(EmployeeDocument emp)
        {

            Mapper.CreateMap<EmployeeDocument, EmployeeDocumentDTO>().ConvertUsing(
                      m =>
                      {
                          return new EmployeeDocumentDTO
                          {
                              DocumentCatId = m.DocumentCatId,
                              DocumentCreatedAt = m.DocumentCreatedAt,
                              DocumentEmpCode = m.DocumentEmpCode,
                              DocumentId = m.DocumentId,
                              DocumentOnlyAdmin = m.DocumentOnlyAdmin,
                              DocumentRemarks = m.DocumentRemarks,
                              DocumentTitle = m.DocumentTitle,
                              DocumentVerified = m.DocumentVerified,

                          };

                      });
            return Mapper.Map<EmployeeDocument, EmployeeDocumentDTO>(emp);
        }
    }
}
