using System.Collections.Generic;
using System.Web.Mvc;

namespace BeeHrmInterface.GlobalSelectLists
{
    public interface IDynamicSelectList
    {
        IEnumerable<SelectListItem> GetBranchSelectList();
        IEnumerable<SelectListItem> GetDepartmentSelectList();
        IEnumerable<SelectListItem> GetGradeSelectList();
        IEnumerable<SelectListItem> GetLevelSelectList();
        IEnumerable<SelectListItem> GetEmployeeSelectList();
        IEnumerable<SelectListItem> LeaveTypeList();
        IEnumerable<SelectListItem> LeaveYearList();
        IEnumerable<SelectListItem> GetApproverSelectList(int empcode);
        IEnumerable<SelectListItem> GetRecommenderSelectList(int empcode);
        IEnumerable<SelectListItem> GetLeaveApproverSelectList(int empcode, string approverType);
        IEnumerable<SelectListItem> GetLeaveRecommenderSelectList(int empcode, string recommendType);
        List<SelectListItem> GetEmployeeByEmpCode(int empCode);
    }
}

