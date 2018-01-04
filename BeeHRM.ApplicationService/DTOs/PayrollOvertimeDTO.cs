using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.DTOs
{
    public class PayrollOvertimeDTO
    {
        public int OvertimeId { get; set; }
        public int EmpCode { get; set; }
        //[Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> OvertimeDate { get; set; }
        public string OvertimeDateNP { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }
        public Nullable<int> ApprovedById { get; set; }
        public bool ApproveStatus { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> ApproveStatusDate { get; set; }
        public string ApproveStatusDateNP { get; set; }
        public Nullable<decimal> OvertimeAmount { get; set; }
        public decimal OvertimeHours { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> OvertimeAppliedDate { get; set; }
        public string OvertimeAppliedDateNP { get; set; }
        public virtual EmployeeDTO Employee { get; set; }


        public string ApproverName { get; set; }
        public IEnumerable<SelectListItem> EmployeeList { get; set; }

        public IEnumerable<SelectListItem> ApproverList { get; set; }
    }
}
