using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Leave_Module.DTOs
{
    public class EmployeeDTOs
    {
        public int EmpCode { get; set; }
        public string EmpDisplayCode { get; set; }
        public int EmpOfficeId { get; set; }
        public int EmpLevelId { get; set; }
        public int EmpRankId { get; set; }
        public int EmpShiftId { get; set; }
        public int EmpDeptId { get; set; }
        public int EmpSectionId { get; set; }
        public int EmpDesgId { get; set; }
        public int EmpTypeId { get; set; }
        public int EmpBgId { get; set; }
        public int EmpJobTypeId { get; set; }
        public Nullable<int> EmpLeaveRuleId { get; set; }
        public int EmpRoleId { get; set; }
        public Nullable<int> EmpGradeStartNumber { get; set; }
        public Nullable<int> CurrentGrade { get; set; }
        public Nullable<int> CurrentGrade_Test { get; set; }
        public Nullable<bool> EmpDeptHead { get; set; }
        public Nullable<bool> EmpOfficeHead { get; set; }
        public string EmpName { get; set; }
        public string EmpEmail { get; set; }
        public string EmpUserName { get; set; }
        public string EmpPassword { get; set; }
        public string EmpPhoto { get; set; }
        public string EmpGender { get; set; }
        public string EmpMaritalStatus { get; set; }
        public Nullable<bool> EmpIncapacitated { get; set; }
        public Nullable<bool> EmpStatus { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]       
        public Nullable<System.DateTime> EmpAppointmentDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> EmpCurrentRankAppDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> EmpCreatedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> EmpContractExpiryDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> EmpResignedDate { get; set; }
        public Nullable<bool> EmpAttendanceIgnore { get; set; }
        public Nullable<bool> EmpPayroll { get; set; }
        public Nullable<int> EmpJobHistoryId { get; set; }
        public string EmpRemoteType { get; set; }
        public Nullable<int> EmpTaxSetupId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Department Department1 { get; set; }
        public virtual Designation Designation { get; set; }
        public virtual Employee Employee1 { get; set; }
        public virtual JobType JobType { get; set; }
        public virtual Level Level { get; set; }
        public virtual Office Office { get; set; }

    }
}
