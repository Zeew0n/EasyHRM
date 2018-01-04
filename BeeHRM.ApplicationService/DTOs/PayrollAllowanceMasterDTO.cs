using BeeHRM.ApplicationService.ViewModel;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.DTOs
{
    public class PayrollAllowanceMasterDTO
    {
        public int AllowanceMasterId { get; set; }
        public string AllowanceName { get; set; }
        public string AllowanceType { get; set; }
        public string EarningDeduction { get; set; }
        public string PercentageAmount { get; set; }
        public decimal Value { get; set; }
        public bool IsAlwaysAplicable { get; set; }
        public bool ApplyToAllEmployee { get; set; }
        public bool IsPerDayDeductable { get; set; }
        public bool SameForAllEmployee { get; set; }
        public bool GivenOnlyIfPresent { get; set; }
        public bool IsTaxable { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public Nullable<System.DateTime> ApplicableFromDate { get; set; }
        public Nullable<System.DateTime> ApplicableToDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
         public IEnumerable<AssignedEmployees> AssignedEmployees{get;set;}
        public  IEnumerable<PayrollAllowanceDetailDTO> PayrollAllowanceDetails { get; set; }
        public IEnumerable<SelectListItem> SelectedEmployeeList { get; set; }
        public IEnumerable<SelectListItem> EmployeeList { get; set; }

        //modified after retirement fund is created

        public IEnumerable<SelectListItem> EarningDeductionSelectList { get; set; }
        public IEnumerable<SelectListItem> AllowanceTypeSelectList { get; set; }
        public IEnumerable<SelectListItem> PercentageAmountSelectList { get; set; }



    }
}
