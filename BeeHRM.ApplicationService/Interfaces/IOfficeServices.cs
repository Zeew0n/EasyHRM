using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IOfficeServices
    {
        IEnumerable<OfficeDTOs> GetOfficeData();
        IEnumerable<OfficeDTOs> GetOfficeListByEmpRole(int roleId);
        IEnumerable<OfficeDTOs> GetClildOfficeListByEmpCode(int Empcode);
        IEnumerable<OfficeDTOs> GetOfficeAllData();
        OfficeDTOs InsertOffice(OfficeDTOs data);
        OfficeDTOs GetOfficeById(int officeId);
        int UpdateDarbandi(OfficeDTOs data);
        string GetOfficeName(int officeId);
        //OfficeDTOs OfficeCreateForm();
        List<SelectListItem> GetEmployeeList();
        List<SelectListItem> GetRemoteList();
        IEnumerable<OfficeDTOs> getBranchOfficeData(string id);
        IEnumerable<OfficeDTOs> GetActiveOfficeList();
        List<int> MyAccessOfficeList();
    }
}
