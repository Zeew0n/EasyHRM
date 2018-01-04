using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class PayrollSalaryDetailSheetDTO
    {
        public int Id { get; set; }
        public int AllowanceId { get; set; }
        public string AllowanceName { get; set; }
        public string AllowanceType { get; set; }
        public string EarningDeduction { get; set; }
        public string PercentageAmount { get; set; }
        public decimal Value { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal CalculatedValue { get; set; }
        public int PayrollSalaryMasterId { get; set; }
        public virtual PayrollSalaryMasterSheetDTO PayrollSalaryMasterSheet { get; set; }
    }
}
