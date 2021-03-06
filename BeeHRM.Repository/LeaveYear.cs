//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BeeHRM.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class LeaveYear
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LeaveYear()
        {
            this.LeaveApplications = new HashSet<LeaveApplication>();
            this.LeaveAssigneds = new HashSet<LeaveAssigned>();
            this.LeaveAssigneds1 = new HashSet<LeaveAssigned>();
            this.LeaveEarneds = new HashSet<LeaveEarned>();
            this.PayrollLeaveDeductions = new HashSet<PayrollLeaveDeduction>();
        }
    
        public int YearId { get; set; }
        public Nullable<int> YearName { get; set; }
        public System.DateTime YearStartDate { get; set; }
        public System.DateTime YearEndDate { get; set; }
        public string YearStartDateNp { get; set; }
        public string YearEndDateNp { get; set; }
        public Nullable<bool> YearCurrent { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeaveApplication> LeaveApplications { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeaveAssigned> LeaveAssigneds { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeaveAssigned> LeaveAssigneds1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeaveEarned> LeaveEarneds { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PayrollLeaveDeduction> PayrollLeaveDeductions { get; set; }
    }
}
