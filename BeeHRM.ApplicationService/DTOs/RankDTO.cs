using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.DTOs
{
    public class RankDTO
    {
        public int RankId { get; set; }
        [RegularExpression(@"^\d+.\d{0,2}$")]
        [Range(0, 100, ErrorMessage = "Please enter a valid decimal Number with 2 decimal places")]
        public Nullable<decimal> RankName { get; set; }
        public string RankDescription { get; set; }
        [RegularExpression(@"^\d+.\d{0,2}$")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Please enter a valid decimal Number with 2 decimal places")]
        public Nullable<decimal> RankSalary { get; set; }
        [RegularExpression(@"^\d+.\d{0,2}$")]
        [Range(0, 100, ErrorMessage = "Please enter a valid decimal Number with 2 decimal places")]
        public Nullable<decimal> RankMaxGrade { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Please enter a valid decimal Number with 2 decimal places")]
        public Nullable<decimal> RankPerGradeAmount { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Please enter a valid decimal Number with 2 decimal places")]
        public Nullable<decimal> BankAllowance { get; set; }
        [RegularExpression(@"^\d+.\d{0,2}$")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Please enter a valid decimal Number with 2 decimal places")]
        public Nullable<decimal> Incentive { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Please enter a valid decimal Number with 2 decimal places")]
        public Nullable<decimal> SiteInchargeship { get; set; }
        [RegularExpression(@"^\d+.\d{0,2}$")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Please enter a valid decimal Number with 2 decimal places")]
        public Nullable<decimal> UniformAllowance { get; set; }
        [RegularExpression(@"^\d+.\d{0,2}$")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Please enter a valid decimal Number with 2 decimal places")]
        public Nullable<decimal> RankSpecialAllowance { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Please enter a valid decimal Number with 2 decimal places")]
        public Nullable<decimal> RankInchargeShipAllowance { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Please enter a valid decimal Number with 2 decimal places")]
        public Nullable<decimal> RankOtherAllowances { get; set; }
        [RegularExpression(@"^\d+.\d{0,2}$")]
        [Range(0, 9999999999999999.99, ErrorMessage = "Please enter a valid decimal Number with 2 decimal places")]
        public Nullable<decimal> OverTimeRate { get; set; }
    }
}
