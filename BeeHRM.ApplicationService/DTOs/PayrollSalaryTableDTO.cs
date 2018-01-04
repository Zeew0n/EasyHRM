using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.DTOs
{


    public class MyPayrollSalaryTableDTO
    {
        public int Id { get; set; }
        [DisplayName("Fiscal Year")]
        public string FyId { get; set; }
        [DisplayName("Month")]
        public string PayrollMonth { get; set; }

        public decimal RankAndGradeSalary { get; set; }
        public decimal RankAllowances { get; set; }

        public decimal TotalAllowances { get; set; }
        public decimal MyProperty { get; set; }
        public decimal PfSelf { get; set; }
        public decimal PfCompany { get; set; }

        public decimal PfExtra { get; set; }


        public decimal GrossSalary { get; set; }

        public decimal PF { get; set; }

        public decimal CIT { get; set; }
        public decimal Tax { get; set; }
        public decimal CashInHand { get; set; }
        [DisplayName("Confirmed")]
        public bool SalaryConfirmed { get; set; }
        public virtual PayrollMonthDescriptionDTO PayrollMonthDescription { get; set; }
    }

    public class PayrollSalaryTableDTO
    {
        public int Id { get; set; }
        [DisplayName("Fiscal Year")]
        public int FyId { get; set; }
        [DisplayName("Month")]
        public int PayrollMonthId { get; set; }
        [DisplayName("Created by")]
        public int CreatorId { get; set; }
        [DisplayName("Office")]
        public int OfficeId { get; set; }
        public Nullable<int> BgId { get; set; }
        public string Details { get; set; }
        [DisplayName("Confirmed")]
        public bool SalaryConfirmed { get; set; }
        public virtual FiscalDTO Fiscal { get; set; }
        public virtual OfficeDTOs Office { get; set; }
        public virtual PayrollMonthDescriptionDTO PayrollMonthDescription { get; set; }
        public virtual IEnumerable<PayrollSalaryMasterSheetDTO> PayrollSalaryMasterSheets { get; set; }
        public IEnumerable<SelectListItem> MonthSelectList { get; set; }
        public IEnumerable<SelectListItem> OfficeList { get; set; }
        public IEnumerable<SelectListItem> FiscalYearList { get; set; }
        public EmployeeDTO Employee { get; set; }
        [DisplayName("Update Existing")]  
        public bool UpdateExisting { get; set; }
        public bool AllowGeneration { get; set; }
        public PayrollAllowanceMasterDTO PayrollAllowanceMasterDTO { get; set; }
    }


}
