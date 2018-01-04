using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface ISpecialLeaveTypeService
    {
        List<LeaveApplicationDTO> GetAllSpecialApplicationLeaveList(int EmpCode);
        List<LeaveApplicationDTO> GetAllSpecialApplicationLeaveListByYearAndMonth(LeaveApplicationDTOInformation Record);
        IEnumerable<LeaveTypeDTO> GetSpecialLeaveType();
        void AddSpecialLeaveType(LeaveApplicationDTO newLeave);
        IEnumerable<SelectListItem> GetYearSelectList();
        IEnumerable<SelectListItem> GetSpecialLeaveTypeSelectList();
        IEnumerable<SelectListItem> EmployeeSelectList(int roleid);

        IEnumerable<SelectListItem> GetLeaveTypeSelectList();
        LeaveApplicationDTO GetLeaveApplicationsbyId(int id);
        void Reject(int id);
        IEnumerable<SelectListItem> GetEmployeeApproverSelectList(int EmpCode, string approverType);
        IEnumerable<SelectListItem> GetEmployeeRecommenderSelectList(int EmpCode,string recommendType);

        



    }
}
