using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeeHRM.Api.Models
{
    public class OfficeModel
    {
        public int OfficeId { get; set; }
        public Nullable<int> OfficeParentId { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
        public string OfficeAddress { get; set; }
        public string OfficePhone { get; set; }
        public string OfficeGeloLocation { get; set; }
        public Nullable<bool> OfficeStatus { get; set; }
        public Nullable<int> OfficeTypeId { get; set; }
    }
}