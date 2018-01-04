using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ViewModel
{
   public class EmployeeExperinceViewModel
    {
        public int ExpId { get; set; }
        public int EmpCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyContact { get; set; }
        public string Designation { get; set; }
        public string JobDescription { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]

        public System.DateTime StartDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime EnDate { get; set; }
        public string RefName { get; set; }
        public string RefDesignation { get; set; }
        public string RefEmail { get; set; }
        public string RefPhone { get; set; }

        public virtual EmployeeDTO Employee { get; set; }
    }
}
