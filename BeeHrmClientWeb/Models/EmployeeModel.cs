using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeeHrmClientWeb.Models
{
    public class EmployeeModel: employeeBaseMOdel
    {
        public int EmpCode { get; set; }
        public string Name { get; set; }
        public string UserNme { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Image { get; set; }
        public string Gender { get; set; }
        public bool EmpStatus { get; set; }
        public int MyProperty { get; set; }

        public List<EmployeeModel> MinEmployees{ get; set; }

    }
    public class employeeBaseMOdel
    {

        public int EmpOfficeId { get; set; }
        public int EmpLevelId { get; set; }
        public int EmpRankId { get; set; }
        public int EmpShiftId { get; set; }
        public int EmpDeptId { get; set; }
        public Nullable<int> EmpSectionId { get; set; }
        public int EmpDesgId { get; set; }
        public int EmpTypeId { get; set; }
        public int EmpBgId { get; set; }
        public Nullable<int> EmpJobTypeId { get; set; }
        public Nullable<int> EmpLeaveRuleId { get; set; }

        public int EmpRoleId { get; set; }
        public Nullable<bool> EmpDeptHead { get; set; }
        public Nullable<bool> EmpOfficeHead { get; set; }
        public string EmpName { get; set; }
        public string EmpEmail { get; set; }

        [Required]
        [Display(Name = "UserName")]
        public string EmpUserName { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string EmpPassword { get; set; }
        public string EmpPhoto { get; set; }
        public string EmpGender { get; set; }
        public string EmpMaritalStatus { get; set; }
        public Nullable<bool> EmpIncapacitated { get; set; }
        public Nullable<bool> EmpStatus { get; set; }
        public Nullable<System.DateTime> EmpCreatedDate { get; set; }
        public Nullable<System.DateTime> EmpContractExpiryDate { get; set; }
        public Nullable<bool> EmpAttendanceIgnore { get; set; }
        public Nullable<bool> EmpPayroll { get; set; }
        public Nullable<int> EmpJobHistoryId { get; set; }
        public Nullable<decimal> EmpSalaryScale { get; set; }
        public Nullable<decimal> EmpGradeSalary { get; set; }
        public Nullable<decimal> EmpDearnessSalary { get; set; }
        public Nullable<decimal> EmpTimeScale { get; set; }
        public HttpPostedFileBase EmpProfilePhoto { get; set; }

        public string JoinDate { get; set; }


        public int EmpServiceEventGroupId { get; set; }
        public string ResponseMessage { get; set; }

        public string OfficeName { get; set; }
        public string DeptName { get; set; }
        public string DsgName { get; set; }
        public string GroupName { get; set; }
        public string BgName { get; set; }

        public string Address { get; set; }
        public string Contact { get; set; }
    }

}