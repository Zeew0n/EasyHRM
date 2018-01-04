using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.DTOs
{
    public class EmployeeDocumentDTO
    {
        [Key]
        public int DocumentId { get; set; }

        public int DocumentEmpCode { get; set; }

        public int? DocumentCatId { get; set; }

        public string DocumentTitle { get; set; }

        public bool? DocumentOnlyAdmin { get; set; }

        public bool? DocumentVerified { get; set; }

        public string DocumentRemarks { get; set; }

        public DateTime? DocumentCreatedAt { get; set; }

        public EmployeeDTO Employee { get; set; }

        public DocumentCategoryDTO DocumentCategory { get; set; }

        public IEnumerable<SelectListItem> DocumentCategorySelectlist { get; set; }
        public object EmpImage { get; set; }
    }
    public partial class EmployeeDocumentInformation
    {
        public List<EmployeeDocumentDTO> EmployeeDocumentList { get; set; }
        public int EmpId { get; set; }
        public IEnumerable<SelectListItem> DocumentCategorySelectlist { get; set; }
    }
}