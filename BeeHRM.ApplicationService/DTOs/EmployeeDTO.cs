using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.DTOs
{
    public class EmployeeDTO : LoginDetail
    {
        [DisplayName("Employee code")]
        [Required(ErrorMessage = "The employee Code is required field.")]
        public int EmpCode { get; set; }
        public string CustomerId { get; set; }
        [Required]
        public int EmpOfficeId { get; set; }
        [Required]
        public int OfficeId { get; set; }

        [Required]
        public int EmpLevelId { get; set; }
        [Required]
        public int EmpRankId { get; set; }
        [Required]
        public int EmpShiftId { get; set; }
        [Required]
        public int EmpDeptId { get; set; }
        public int EmpRoleId { get; set; }

        public int CurrentGrade { get; set; }

        public int EmpSectionId { get; set; }
        [Required]
        public int EmpDesgId { get; set; }
        [Required]
        public int EmpTypeId { get; set; }
        [Required]
        public int EmpBgId { get; set; }
        [Required]
        public int EmpJobTypeId { get; set; }
        [Required]
        public Nullable<int> EmpLeaveRuleId { get; set; }
        public Nullable<bool> EmpDeptHead { get; set; }
        public Nullable<bool> EmpOfficeHead { get; set; }
        [Required(ErrorMessage = "The employee name is a required field.")]
        [DisplayName("Employee Name")]
        public string EmpName { get; set; }
        public int RecommendateEmpCode { get; set; }
        public int ApprovalEmpCode { get; set; }
        public string RecommendateEmpName { get; set; }
        public string ApprovalEmpName { get; set; }

        public string EmpEmail { get; set; }
        public int ApproverID { get; set; }

        public string EmpPhoto { get; set; }
        [Required]
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
        public DateTime? JoinDate { get; set; }
        public int EmpServiceEventGroupId { get; set; }
        public int EmpServiceSubEventGroupId { get; set; }
        public string ResponseMessage { get; set; }
        public string OfficeName { get; set; }
        public string DeptName { get; set; }
        public string DsgName { get; set; }
        public string GroupName { get; set; }
        public string BgName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string RankName { get; set; }
        public string FacultyName { get; set; }
        public string DegreeName { get; set; }
        public string Division { get; set; }
        public string SectionName { get; set; }
        public string DistrictName { get; set; }
        public string EthnicityName { get; set; }
        public string EmpDob { get; set; }
        public Nullable<System.DateTime> EmpAppointmentDate { get; set; }
        public string EmpAppointmentDateNP { get; set; }
        public string EmpDisplayCode { get; set; }
        public string EmpCurrentRankAppDate { get; set; }
        public DateTime? EmpCurrentRankUpgradeDate { get; set; }
        public string OfficeParentId { get; set; }
        public string JobTypeName { get; set; }
        public HttpPostedFileBase EmpImage { get; set; }
        public string EmpTAddress { get; set; }
        public string EmpMobileNumber { get; set; }
        public string EmpRemoteType { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> OfficeList { get; set; }
        public List<SelectListItem> LeaveRuleList { get; set; }
        public List<SelectListItem> LevelList { get; set; }
        public List<SelectListItem> SectionList { get; set; }
        public List<SelectListItem> EmployeeGroup { get; set; }
        public List<SelectListItem> JobTypeList { get; set; }
        public List<SelectListItem> RankList { get; set; }
        public List<SelectListItem> dllShiftList { get; set; }
        public List<SelectListItem> dllEventGroup { get; set; }
        public List<SelectListItem> dllSubEventGroup { get; set; }
        public List<SelectListItem> dllDesginationList { get; set; }
        public List<SelectListItem> ddlBusinessGroupList { get; set; }
        public string RoleName { get; set; }

        public int EmpTaxSetupId { get; set; }
        public IEnumerable<SelectListItem> dllTaxRuleList { get; set; }

    }

    public class LoginDetail
    {

        [Required]
        [Display(Name = "UserName")]
        public string EmpUserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string EmpPassword { get; set; }
    }
}
