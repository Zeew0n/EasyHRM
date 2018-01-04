namespace BeeHRM.ApplicationService.DTOs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InsuranceCompany")]
    public partial class InsuranceCompanyDTO
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string CompanyName { get; set; }
    }
}
