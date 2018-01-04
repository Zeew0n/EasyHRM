using System.Collections.Generic;
using System.Web.Mvc;

namespace BeeHRM.ApplicationService.ViewModel
{
    public class DropDownListViewmodelcs
    {
        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> OfficeList { get; set; }
        public List<SelectListItem> LeaveRuleList { get; set; }
        public List<SelectListItem> LevelList { get; set; }
        public List<SelectListItem> SectionList { get; set; }
        public List<SelectListItem> EmployeeGroup { get; set; }
        public List<SelectListItem> JobTypeList { get; set; }
        public List<SelectListItem> RankList { get; set; }
        public List<SelectListItem> dllShiftList { get; set; }

        public List<SelectListItem> dllDesginationList { get; set; }
        public List<SelectListItem> ddlBusinessGroupList { get; set; }
        public List<SelectListItem> dllEventGroup { get; set; }
        public List<SelectListItem> dllSubEventGroupList { get; set; }

        public List<SelectListItem> dllTaxRuleList { get; set; }



    }
}
