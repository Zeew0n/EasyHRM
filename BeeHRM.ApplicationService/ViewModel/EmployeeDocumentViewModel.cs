using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ViewModel
{
    public class EmployeeDocumentViewModel
    {
        public int DocumentId { get; set; }
        public int DocumentEmpCode { get; set; }
        public Nullable<int> DocumentCatId { get; set; }
        public string CatTitle { get; set; }
        public string DocumentTitle { get; set; }
        public Nullable<bool> DocumentOnlyAdmin { get; set; }
        public bool DocumentVerified { get; set; }
        public string DocumentRemarks { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime DocumentCreatedAt { get; set; }
        //public virtual DocumentCategory DocumentCategory { get; set; }
    }
}
