using System.Collections.Generic;
using System.Web.Mvc;

namespace BeeHrmInterface.GlobalSelectLists
{
    public class StaticSelectList
    {
        public static IEnumerable<SelectListItem> GetGenderList()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="All", Value = "B" },
                new SelectListItem{ Text="Male", Value = "M" },
                new SelectListItem{ Text="Female", Value = "F" },
                new SelectListItem{ Text="Third Gender", Value = "T" },
            };
            return list;
        }
        public static IEnumerable<SelectListItem> GetMaritialStatusList()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="Married", Value = "M" },
                new SelectListItem{ Text="Unmarried", Value = "U" },
                //new SelectListItem{ Text="Divorced", Value = "D" },
                //new SelectListItem{ Text="Widowed", Value = "W" }, 
                new SelectListItem{ Text="Both", Value = "B" },
            };
            return list;
        }

        public static IEnumerable<SelectListItem> GetBloodGroupList()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="A-Positive", Value = "1" },
                new SelectListItem{ Text="A-Negative", Value = "2" },
                new SelectListItem{ Text="B-Positive", Value = "3" },
                new SelectListItem{ Text="B-Negative", Value = "4" },
                new SelectListItem{ Text="O-Positive", Value = "5" },
                new SelectListItem{ Text="O-Negative", Value = "6" },
            };
            return list;
        }

        public static IEnumerable<SelectListItem> GetReligionList()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="Buddhist", Value = "1" },
                new SelectListItem{ Text="Christian", Value = "2" },
                new SelectListItem{ Text="Hindu", Value = "3" },
                new SelectListItem{ Text="Muslims", Value = "4" },
                new SelectListItem{ Text="Shikh", Value = "5" },
                new SelectListItem{ Text="Others", Value = "5" },

            };
            return list;
        }

        public static IEnumerable<SelectListItem> GetNationalityList()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="Nepali", Value = "1" },
                new SelectListItem{ Text="Indian", Value = "2" },
            };
            return list;
        }

        public static IEnumerable<SelectListItem> GetLicenseTypeList()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="LMV", Value = "1" },
                new SelectListItem{ Text="HMV", Value = "2" },
            };
            return list;
        }

        public static IEnumerable<SelectListItem> GetWorkingStatusList()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="Working", Value = "1" },
                new SelectListItem{ Text="Suspended", Value = "2" },
                new SelectListItem{ Text="Resigned", Value = "3" },
            };
            return list;
        }

        public static IEnumerable<SelectListItem> GetJobTypesList()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="Permanent", Value = "1" },
                new SelectListItem{ Text="Contract", Value = "2" },
            };
            return list;
        }

        public static IEnumerable<SelectListItem> GetCRUDList()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="Please select", Value = null },
                new SelectListItem{ Text="Create", Value = "C" },
                new SelectListItem{ Text="Read", Value = "R" },
                new SelectListItem{ Text="Update", Value = "U" },
                new SelectListItem{ Text="Delete", Value = "D" },
            };
            return list;
        }
        public static IEnumerable<SelectListItem> GetHalfdayTypeSelectList()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="Morning", Value = "M" },
                new SelectListItem{ Text="Afternoon", Value = "A" },
            };
            return list;
        }
        public static IEnumerable<SelectListItem> GetLeaveTransferSelectList()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="Transferable", Value = "true" },
                new SelectListItem{ Text="Non-Transferable", Value = "false" },
            };
            return list;
        }
        public static IEnumerable<SelectListItem> GetLeaveCashSelectList()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="Cashable", Value = "true" },
                new SelectListItem{ Text="Non-Cashable", Value = "false" },
            };
            return list;
        }
        public static IEnumerable<SelectListItem> GetLeaveTypeAssignmentSelectList()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="Assigned", Value = "Assigned" },
                new SelectListItem{ Text="UnAssigned", Value = "UnAssigned" },
            };
            return list;
        }
        public static IEnumerable<SelectListItem> GetLeaveAllowFrequencySelectList()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="One Times", Value = "1" },
                new SelectListItem{ Text="Two Times", Value = "2" },
                new SelectListItem{ Text="Many Times", Value = "0" },
            };
            return list;
        }
        public static IEnumerable<SelectListItem> GetYesNoSelectList()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="Yes", Value = "true" },
                new SelectListItem{ Text="No", Value = "false" },
            };
            return list;
        }

        public static IEnumerable<SelectListItem> GetLeavePayableSelectList()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="Payable", Value = "true" },
                new SelectListItem{ Text="Non-Payable", Value = "false" },
            };
            return list;
        }
        public static IEnumerable<SelectListItem> GetLeaveLeaveApprovestatusList()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="Select Please", Value = "0" },
                new SelectListItem{ Text="Pending", Value = "1" },
                new SelectListItem{ Text="Approved", Value = "2" },
                new SelectListItem{ Text="Rejected", Value = "3" },
            };
            return list;
        }
        public static IEnumerable<SelectListItem> GetnepaliMonth()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="Select Month", Value = "0" },
                new SelectListItem{ Text="बैशाख", Value = "01" },
                new SelectListItem{ Text="जेठ", Value = "02" },
                new SelectListItem{ Text="आषाढ", Value = "03" },
                new SelectListItem{ Text="साउन", Value = "04" },
                new SelectListItem{ Text="भाद्र", Value = "05" },
                new SelectListItem{ Text="आश्विन", Value = "06" },
                new SelectListItem{ Text="कार्तिक", Value = "07" },
                new SelectListItem{ Text="मंसिर", Value = "08" },
                new SelectListItem{ Text="पौष", Value = "09" },
                new SelectListItem{ Text="माघ", Value = "10" },
                new SelectListItem{ Text="फाल्गुण", Value = "11" },
                new SelectListItem{ Text="चैत्र", Value = "12" },
            };
            return list;
        }
        public static IEnumerable<SelectListItem> AmountTypeSelectList()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="-- Select Amount Type --", Value = "" },
                new SelectListItem{ Text="Annual", Value = "A" },
                new SelectListItem{ Text="Half Yearly", Value = "H" },
                new SelectListItem{ Text="Quarterly", Value = "Q" },
                new SelectListItem{ Text="Monthly", Value = "M" },
            };
            return list;
        }
        public static IEnumerable<SelectListItem> RemoteAreaTypeList()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="-- Select Remote Type --", Value = "" },
                new SelectListItem{ Text="Local", Value = "L" },
                new SelectListItem{ Text="Remote", Value = "O" },
            };
            return list;
        }
    }
}
