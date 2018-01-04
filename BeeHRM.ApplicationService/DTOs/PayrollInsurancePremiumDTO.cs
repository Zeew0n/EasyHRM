namespace BeeHRM.ApplicationService.DTOs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;
    [Table("PayrollInsurancePremium")]
    public partial class PayrollInsurancePremiumDTO
    {
        [Key]
        public int IsuranceClaimId { get; set; }

        public int InsuranceCompanyId { get; set; }

        public decimal InsuredAmount { get; set; }

        public decimal? PremiumAmount { get; set; }

        [StringLength(1)]
        public string AmountType { get; set; }

        [Column(TypeName = "date")]
        public Nullable<DateTime> StartDate { get; set; }
        public string StartDateNP { get; set; }

        [Column(TypeName = "date")]
        public Nullable<DateTime> EndDate { get; set; }
        public string EndDateNP { get; set; }

        public int? InsuranceClaimFyId { get; set; }

        public int EmpCode { get; set; }

        [StringLength(50)]
        public string InsurancePolicyNumber { get; set; }

        public EmployeeDTO Employee { get; set; }

        public InsuranceCompanyDTO InsuranceCompany { get; set; }

        public IEnumerable<SelectListItem> InsuranceCompanySelectlist { get; set; }
        public IEnumerable<SelectListItem> AmountTypeSelectList { get; set; }
    }
    public partial class PayrollInsuranceInformation
    {
        public List<PayrollInsurancePremiumDTO> PayrollInsurancePremiumList { get; set; }
        public int EmpId { get; set; }
        public IEnumerable<SelectListItem> InsuranceCompanySelectlist { get; set; }
    }
}
