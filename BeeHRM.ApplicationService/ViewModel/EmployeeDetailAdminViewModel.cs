using BeeHRM.ApplicationService.DTOs;
using System.Collections.Generic;

namespace BeeHRM.ApplicationService.ViewModel
{
    public class EmployeeDetailAdminViewModel
    {
        public EmployeeDetailsViewModel EmpDetails { get; set; }
        public EmployeeEditViewModel OtherDetails { get; set; }
        public IEnumerable<EmployeeFamilyViewModel> Familydetails { get; set; }
        public IEnumerable<EmployeePrizeDTO> Prize { get; set; }
        public IEnumerable<EmployeeSkillViewModel> Skill { get; set; }
        public IEnumerable<EmployeeDocumentViewModel> Documents { get; set; }
        public EmployeeJobHistoryDTO EmployeeCurrentJobHistory { get; set; }
        public EmployeeJobHistoryDTO EmployeeAppointJobHistory { get; set; }
    }
}
