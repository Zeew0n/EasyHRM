using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeeHrmClientWeb.Models
{
    public class EmployeeSearchViewModel
    {
        public EmployeeSearchViewModel()
        {
            this.EmpCode = null;
            this.EmpName = null;
            this.BgId = null;
            this.DeptId = null;
            this.DesgId = null;
            this.GroupId = null;
            this.OfficeId = null;
            this.LevelId = null;
            this.RankId = null;
            this.ShiftId = null;
            this.SectionId = null;
            this.TypeId = null;
            this.JobTypeId = null;
            
        }
        public int? EmpCode { get; set; }
        public string EmpName { get; set; }
        public int? OfficeId { get; set; }
        public int? DeptId { get; set; }
        public int? DesgId { get; set; }
        public int? GroupId { get; set; }
        public int? BgId { get; set; }


        public int? LevelId { get; set; }
        public int? RankId { get; set; }
        public int? ShiftId { get; set; }
        public int? SectionId { get; set; }
        public int? TypeId { get; set; }
        public int? JobTypeId { get; set; }
       
    }
}