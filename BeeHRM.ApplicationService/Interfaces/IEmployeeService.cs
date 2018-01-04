using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.ViewModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IEmployeeService
    {
        EmployeeDTO InsertEmployee(EmployeeDTO newEmployee);
        EmployeeDTO GetLoginData(EmployeeDTO loginModel);
        IEnumerable<EmployeeDTO> GetEmployeeList(int roleId);

        bool EmployeeExists(EmployeeDTO newEmp);
        EmployeeEditViewModel GetEmployeeByID(int id);
        int UpdateEmployee(EmployeeEditViewModel empToEdit);
        EmployeeDetailsViewModel GetEmployeeDetails(int id);
        IEnumerable<EmployeeAppointmentDetailsViewModel> GetEmployeeAppointmentDetails(int id);
        IEnumerable<EmployeeDTO> SearchEmployees(int? code, string name, int? officeId, int? deptId, int? desgId, int? groupId, int? bgId, int roleId);
        IEnumerable<EmployeeDTO> EmployeeSearch(int officeId, int? desgnId = 0);


        IEnumerable<EmployeeDTO> GetApproverByOffice(int officeId);
        IEnumerable<EmployeeDTO> GetAttendanceApprover(int EmpCode);
        IEnumerable<EmployeeDTO> GetAttendanceRecommander(int EmpCode);
        //leave
        IEnumerable<EmployeeDTO> GetLeaveApprover(int EmpCode, string approverType);
        IEnumerable<EmployeeDTO> GetLeaveRecommander(int EmpCode, string recommendType);

        IEnumerable<EmployeeDTO> GetEmployeeByOfficeid(int id);
        IEnumerable<EmployeeDTO> GetEmployeeDetailsByOfficeid(int id);
        IEnumerable<EmployeeDTO> GetEmployeeByDesgination(int id);
        IEnumerable<DistrictViewModel> GetDistrictList();
        int UpdateEmployeeRoles(int Empcode, int RoleId);
        List<EmployeeAddressDTO> GetAddressesofEmployeeById(int id);
        int UpdatePassword(int Empcode, string Password);
        int GetIsProfileViewable(int id, int AdminEmpCode);
        IEnumerable<EmployeeFamilyViewModel> GetEmployeeFamilyByID(int id);
        IEnumerable<EmployeeSkillViewModel> GetEmployeeSkillsByID(int id);
        IEnumerable<EmployeeDocumentViewModel> GetEmployeeDocumentsByID(int id);
        List<SelectListItem> GetDistrictSelectList();
        IEnumerable<EmployeeExperinceViewModel> GetEmployeeExperiencesByID(int id);
        List<SelectListItem> GetZoneSelectList();
        IEnumerable<EmployeeExperinceViewModel> GetEmployeeExperienceDetailsByID(int id);
        List<SelectListItem> GetAddressTypeSelectList();
        IEnumerable<EmployeeDTO> SearchEmployeesByRoleId(int? officId, int? roleId);
        IEnumerable<EmployeeDTO> EmployeeListByRoleId(int RoleId);
        int UpdateEmployeeLeaveRuleId(int Empcode, int LeaveId);
        string CreateRandomPassword(int PasswordLength);
        IEnumerable<EmployeeDTO> GetEmployeeLists(int roleId);
        string GetUpperOfficeName(int id);
        EmployeeAppointmentDetailsViewModel GetEmployeeAppointmentDetail(int id);
        int InsertApprover(int OfficeId, int Empcode);
        void UpdateEncryptPassword();

        IEnumerable<EmployeeDTO> GetEmployeeByOfficeidPayroll(int OfficeId);
        EmpAllCodesVIewmodels EmployeesIds(int empcode);
        List<SelectListItem> GetEmployeeSelectList();
        void CreateEmployeeAddress(EmployeeAddressDTO record);
        List<SelectListItem> GetSponsorshipList();
        EmployeeDTO GetEmployeeDTOById(int Id);
        List<SelectListItem> GetNationInternationalList();
        EmployeeAddressDTO GetAddressById(int Id);
        void UpdateEmployeeAddress(EmployeeAddressDTO record);
        void DeleleEmployeeApprover(int id);
    }
}
