using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHrmClientWeb.CodeBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BeeHrmClientWeb.Controllers.Reports
{
    public class AttendanceReportController : BaseController
    {
        // GET: AttendanceReport

        private IAttendanceDailyService _attendanceDailyServices;
        //private IOfficeTypeService _officeTypeServices;
        private IDepartmentService _departmentServices;
        private IGroupService _groupServices;
        private IDesignationService _designationServices;
        private IModulServices _moduleService;
        private IOfficeServices _officeServices;
        private INotification _notifications;
        private IEmployeeService _employeeServices;
        private IReportsServices _reportServices;
        public AttendanceReportController()
        {

            _attendanceDailyServices = new AttendanceDailyService();
            _departmentServices = new DepartmentService();
            _designationServices = new DesignationService();
            _groupServices = new GroupService();
            _officeServices = new OfficeServices();
            _moduleService = new ModuleService();
            _employeeServices = new EmployeeService();
            _notifications = new NotificationService();
            _reportServices = new ReportsServices();
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();

        }
        [Route("attmonthlyreport")]
        [HttpGet]
        public ActionResult AttendanceReport()
        {
            int empcode = Convert.ToInt32(Session["EmpCode"]);
            AttendanceReportsDTO result = new AttendanceReportsDTO();
            IEnumerable<OfficeDTOs> officelist = _officeServices.GetClildOfficeListByEmpCode(empcode);
            List<int> nepaliyears = _reportServices.GetYearList();

            List<SelectListItem> sl = new List<SelectListItem>();
            List<SelectListItem> yrs = new List<SelectListItem>();
            List<SelectListItem> Mth = _reportServices.NepaliMonthList();


            foreach (int str in nepaliyears)
            {
                yrs.Add(new SelectListItem
                {
                    Text = str.ToString(),
                    Value = str.ToString()
                });
            }
            foreach (OfficeDTOs str in officelist)
            {
                sl.Add(new SelectListItem
                {
                    Text = str.OfficeName,
                    Value = str.OfficeId.ToString()
                });
            }

            result.BranchSelectList = sl;
            result.MonthList = Mth;
            result.YearList = yrs;
            return View(result);

        }

        [Route("attmonthlyreport")]
        [HttpPost]
        public ActionResult AttendanceReport(AttendanceReportsDTO attd)
        {
            AttendanceReportsDTO result = new AttendanceReportsDTO();


            string StartDate = null;
            string EndDate = null;
            _reportServices.GetStartAndEndDate(attd.month_code, attd.nepali_year, out StartDate, out EndDate);
            DateTime sdate = DateTime.Parse(StartDate);
            DateTime edate = DateTime.Parse(EndDate);
            int empcode = Convert.ToInt32(Session["EmpCode"]);
            IEnumerable<OfficeDTOs> officelist = _officeServices.GetClildOfficeListByEmpCode(empcode);
            List<int> nepaliyears = _reportServices.GetYearList();

            List<SelectListItem> sl = new List<SelectListItem>();
            List<SelectListItem> yrs = new List<SelectListItem>();
            List<SelectListItem> Mth = _reportServices.NepaliMonthList();

            foreach (int str in nepaliyears)
            {
                yrs.Add(new SelectListItem
                {
                    Text = str.ToString(),
                    Value = str.ToString()
                });
            }
            foreach (OfficeDTOs str in officelist)
            {
                sl.Add(new SelectListItem
                {
                    Text = str.OfficeName,
                    Value = str.OfficeId.ToString()
                });
            }
            result.OfficeId = attd.OfficeId;
            result.ExcelStartDate = sdate;
            result.ExcelEndDate = edate;
            ViewBag.attreport = _attendanceDailyServices.GetMonthlyAttendanceAll(sdate, edate, attd.OfficeId);
            result.BranchSelectList = sl;
            result.MonthList = Mth;
            result.YearList = yrs;
            return View(result);

        }

        [Route("AttendanceReportExcel")]
        [HttpPost]
        public ActionResult AttendanceReportExcel(AttendanceReportsDTO att)
        {
            // Step 1 - get the data from database
            var data = _attendanceDailyServices.GetMonthlyAttendanceAll(att.ExcelStartDate, att.ExcelEndDate, att.OfficeId);
            GridView gridview = new GridView();
            gridview.DataSource = data;
            gridview.DataBind();

            // Clear all the content from the current response
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename = AttendanceReport" + DateTime.Now + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            // create HtmlTextWriter object with StringWriter
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            return View();
        }

        [Route("attendnace/summaryreports")]
        public ActionResult AttendanceSummary()
        {
            AttendanceReportsDTO result = new AttendanceReportsDTO();
            int empcode = Convert.ToInt32(Session["EmpCode"]);
            IEnumerable<OfficeDTOs> officelist = _officeServices.GetClildOfficeListByEmpCode(empcode);
            List<int> nepaliyears = _reportServices.GetYearList();

            List<SelectListItem> sl = new List<SelectListItem>();
            List<SelectListItem> yrs = new List<SelectListItem>();
            List<SelectListItem> Mth = _reportServices.NepaliMonthList();

            foreach (int str in nepaliyears)
            {
                yrs.Add(new SelectListItem
                {
                    Text = str.ToString(),
                    Value = str.ToString()
                });
            }
            foreach (OfficeDTOs str in officelist)
            {
                sl.Add(new SelectListItem
                {
                    Text = str.OfficeName,
                    Value = str.OfficeId.ToString()
                });
            }

            result.BranchSelectList = sl;
            result.MonthList = Mth;
            result.YearList = yrs;
            return View(result);

        }


        [Route("attendnace/summaryreports")]
        [HttpPost]
        public ActionResult AttendanceSummary(AttendanceReportsDTO attd)
        {
            AttendanceReportsDTO result = new AttendanceReportsDTO();
            string StartDate = null;
            string EndDate = null;
            _reportServices.GetStartAndEndDate(attd.month_code, attd.nepali_year, out StartDate, out EndDate);
            DateTime sdate = DateTime.Parse(StartDate);
            DateTime edate = DateTime.Parse(EndDate);
            int empcode = Convert.ToInt32(Session["EmpCode"]);
            IEnumerable<OfficeDTOs> officelist = _officeServices.GetClildOfficeListByEmpCode(empcode);
            List<int> nepaliyears = _reportServices.GetYearList();

            List<SelectListItem> sl = new List<SelectListItem>();
            List<SelectListItem> yrs = new List<SelectListItem>();
            List<SelectListItem> Mth = _reportServices.NepaliMonthList();

            foreach (int str in nepaliyears)
            {
                yrs.Add(new SelectListItem
                {
                    Text = str.ToString(),
                    Value = str.ToString()
                });
            }
            foreach (OfficeDTOs str in officelist)
            {
                sl.Add(new SelectListItem
                {
                    Text = str.OfficeName,
                    Value = str.OfficeId.ToString()
                });
            }
            result.OfficeId = attd.OfficeId;
            result.MonthList = Mth;
            result.ExcelStartDate = sdate;
            result.ExcelEndDate = edate;
            ViewBag.attreport = _reportServices.AttendanceMonthlySummary(sdate, edate, attd.OfficeId);
            result.BranchSelectList = sl;
            result.YearList = yrs;
            return View(result);
        }


        [Route("attendance/summaryreportexcel")]
        [HttpPost]
        public ActionResult AttendnaceSummaryExcel(AttendanceReportsDTO att)
        {
            // Step 1 - get the data from database
            var data = _reportServices.AttendanceMonthlySummary(att.ExcelStartDate, att.ExcelEndDate, att.OfficeId);
            GridView gridview = new GridView();
            gridview.DataSource = data;
            gridview.DataBind();

            // Clear all the content from the current response
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename = AttendanceSummary" + DateTime.Now + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            // create HtmlTextWriter object with StringWriter
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            return View();
        }


        [Route("reports/attendance/leavereport")]
        [HttpGet]
        public ActionResult AttendanceLeaveReport()
        {
            AttendanceReportsDTO result = new AttendanceReportsDTO();
            IEnumerable<OfficeDTOs> officelist = _officeServices.GetOfficeAllData();
            List<int> nepaliyears = _reportServices.GetYearList();

            List<SelectListItem> sl = new List<SelectListItem>();
            List<SelectListItem> yrs = new List<SelectListItem>();
            List<SelectListItem> Mth = _reportServices.NepaliMonthList();

            foreach (int str in nepaliyears)
            {
                yrs.Add(new SelectListItem
                {
                    Text = str.ToString(),
                    Value = str.ToString()
                });
            }
            foreach (OfficeDTOs str in officelist)
            {
                sl.Add(new SelectListItem
                {
                    Text = str.OfficeName,
                    Value = str.OfficeId.ToString()
                });
            }

            result.BranchSelectList = sl;
            result.MonthList = Mth;
            result.YearList = yrs;
            return View(result);

        }


        [Route("reports/attendance/leavereport")]
        [HttpPost]
        public ActionResult AttendanceLeaveReport(AttendanceReportsDTO attd)
        {
            AttendanceReportsDTO result = new AttendanceReportsDTO();
            string StartDate = null;
            string EndDate = null;
            _reportServices.GetStartAndEndDate(attd.month_code, attd.nepali_year, out StartDate, out EndDate);
            DateTime sdate = DateTime.Parse(StartDate);
            DateTime edate = DateTime.Parse(EndDate);
            int empcode = Convert.ToInt32(Session["EmpCode"]);
            IEnumerable<OfficeDTOs> officelist = _officeServices.GetClildOfficeListByEmpCode(empcode);
            List<int> nepaliyears = _reportServices.GetYearList();

            List<SelectListItem> sl = new List<SelectListItem>();
            List<SelectListItem> yrs = new List<SelectListItem>();
            List<SelectListItem> Mth = _reportServices.NepaliMonthList();

            foreach (int str in nepaliyears)
            {
                yrs.Add(new SelectListItem
                {
                    Text = str.ToString(),
                    Value = str.ToString()
                });
            }
            foreach (OfficeDTOs str in officelist)
            {
                sl.Add(new SelectListItem
                {
                    Text = str.OfficeName,
                    Value = str.OfficeId.ToString()
                });
            }
            result.OfficeId = attd.OfficeId;
            result.ExcelStartDate = sdate;
            result.ExcelEndDate = edate;

            ViewBag.attreport = _reportServices.AttendanceLeaveReports(sdate, edate, attd.OfficeId);
            result.BranchSelectList = sl;
            result.MonthList = Mth;
            result.YearList = yrs;
            return View(result);
        }

    }
}