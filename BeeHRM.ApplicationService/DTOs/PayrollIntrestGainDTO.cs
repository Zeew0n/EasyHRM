namespace BeeHRM.ApplicationService.DTOs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;
    public class PayrollIntrestGainDTO
    {
        public int Id { get; set; }

        public int? EmpCode { get; set; }

        [StringLength(50)]
        public string CustomerId { get; set; }

        public int? FyId { get; set; }

        public int? MonthIndex { get; set; }

        public decimal? InterestGain { get; set; }
        public int OfficeId { get; set; }
        public List<SelectListItem> OfficeList { get; set; }
        public string EmployeeName { get; set; }
    }

    public class InterestExportExcelModel
    {
        public int SNo { get; set; }
        public int EmployeeCode { get; set; }
        public string CustomerId { get; set; }
        public string EmployeeName { get; set; }

        //public string PercentageAmount { get; set; }

        public decimal Value { get; set; }
    }
}
