using BeeHRM.ApplicationService.DTOs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
namespace BeeHRM.ApplicationService.ViewModel
{
    public sealed class EmployeeEditViewModel
    {
        public string Code { get; set; }
        public string CustomerId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Username { get; set; }
        public string AppointedDate { get; set; }
        public string AppointedDateNP { get; set; }
        public string PhotoName { get; set; }
        [Required]
        public string Gender { get; set; }
        public string Status { get; set; }
        public string MaritalStatus { get; set; }
        public string IsIncapacitated { get; set; }
        public string IsDepartmentHead { get; set; }
        public string IsOfficeHead { get; set; }
        public string AttenfanceIgnore { get; set; }
        public string Payroll { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Password { get; set; }
        public string AdvNumber { get; set; }
        public string HiringMethodId { get; set; }
        public string PermanentAddress { get; set; }
        public string TempAddress { get; set; }
        public string HomePhone { get; set; }
        public string MobileNumber { get; set; }
        public string OfficeEmail { get; set; }
        public string OfficePhone { get; set; }
        public string CitizenNumber { get; set; }
        public string CitIssDate { get; set; }
        public string CitIssDateNP { get; set; }
        public string CitIssDistrictId { get; set; }
        public string Huliya { get; set; }
        public string DateOfBirth { get; set; }
        public string DateOfBirthNP { get; set; }
        public string MarriageAnniversary { get; set; }
        public string MarriageAnniversaryNP { get; set; }
        public string AgeRetireDate { get; set; }
        public string AgeRetireDateNP { get; set; }
        public string EthnicityId { get; set; }
        public string ReligionInd { get; set; }
        public string BloodGroup { get; set; }
        public string PANNumber { get; set; }
        public string CitNumber { get; set; }
        public string PFNumber { get; set; }
        public string DrivingLicenseNumber { get; set; }
        public string PassportNumber { get; set; }
        public string FBLink { get; set; }
        public string TwitterLink { get; set; }
        public string LinkedInLink { get; set; }
        public string EmerContactName { get; set; }
        public string EmerContactRelation { get; set; }
        public string EmerContact { get; set; }
        public string EmerAddress { get; set; }
        public string NomineeName { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public string NomineeDob { get; set; }
        public string NomineeDobNP { get; set; }
        public string NomineeAddress { get; set; }
        public string NomineeRelation { get; set; }
        public string NomCitNo { get; set; }
        public string NomCitIssueDate { get; set; }
        public string NomCitIssueDateNP { get; set; }
        public string NomCitIssDistrictId { get; set; }
        public string NomineePhoto { get; set; }
        public string Nationality { get; set; }

        public string SpouseName { get; set; }
        public string EmployeeHeight { get; set; }
        public string PfEffectiveDate { get; set; }
        public string PfEffectiveDateNP { get; set; }
        public string GratuityEffectiveDate { get; set; }
        public string GratuityEffectiveDateNP { get; set; }
        public string EmployeeBankAccountNumber { get; set; }
        [Required]
        public int EmpShiftId { get; set; }
        [Required]
        public int EmpBgId { get; set; }
        [Required]
        public int EmpTypeId { get; set; }
        [Required]
        public int CurrentGrade { get; set; }
        public int EmpTaxSetupId { get; set; }

        //Photos
        public HttpPostedFileBase EmpImage { get; set; }
        public HttpPostedFileBase NomImage { get; set; }
        public IEnumerable<SelectListItem> LeaveRuleList { get; set; }
        public IEnumerable<SelectListItem> EmployeeGroup { get; set; }
        public IEnumerable<SelectListItem> dllShiftList { get; set; }

        public IEnumerable<SelectListItem> dllTaxRuleList { get; set; }

        public IEnumerable<SelectListItem> ddlBusinessGroupList { get; set; }

        public IEnumerable<DistrictViewModel> Districts { get; set; }
        public IEnumerable<EthnicityDTO> Ethnicities { get; set; }
        public IEnumerable<ReligionDTO> Religions { get; set; }


    }
}
