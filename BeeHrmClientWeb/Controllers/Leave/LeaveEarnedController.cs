using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Utilities;
using BeeHrmInterface.GlobalSelectLists;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
namespace BeeHrmClientWeb.Controllers.Leave

{
    public class LeaveEarnedController : BaseController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();
        }

        private ILeaveApplicationService _leaveServices;
        private ILeaveEarnedService _LeaveEarnedService;
        private ISpecialLeaveTypeService _SpecialleavetypeService;
        private IEmployeeService _employeeServices;
        private IDynamicSelectList _DynamicSelectList;

        public LeaveEarnedController()
        {
            _leaveServices = new LeaveApplicationService();
            _LeaveEarnedService = new LeaveEarnedService();
            _SpecialleavetypeService = new SpecialLeaveTypeService();
            _employeeServices = new EmployeeService();
            this._DynamicSelectList = new DynamicSelectList();
        }

        public ActionResult Index()
        {
            LeaveEarnedInfomationDTO Record = new LeaveEarnedInfomationDTO();
            Record.LeaveEarnedList = new List<LeaveEarnedDTO>();
            int curentemp = Convert.ToInt32(Session["Empcode"]);
            Record.YearSelectList = _SpecialleavetypeService.GetYearSelectList();
            Record.BranchEmployeeCodeSelectlist = _LeaveEarnedService.GetBrancheEmployeeSelectList(curentemp);
            return View("../Leave/LeaveEarned/Index", Record);
        }
        [HttpPost]
        public ActionResult Index(LeaveEarnedInfomationDTO Record)
        {
            Record.LeaveEarnedList = new List<LeaveEarnedDTO>();
            int curentemp = Convert.ToInt32(Session["Empcode"]);
            Record.YearSelectList = _SpecialleavetypeService.GetYearSelectList();
            Record.BranchEmployeeCodeSelectlist = _LeaveEarnedService.GetBrancheEmployeeSelectList(curentemp);
            try
            {
                if (ModelState.IsValid)
                {
                    Record.LeaveEarnedList = _LeaveEarnedService.GetAllLeaveEarnedListByYearIdAndEmpCode(Record);
                }
                else
                {
                    ViewBag.Error = "Please Select Properly";
                }
            }
            catch (Exception Exception)
            {

                ViewBag.Error = Exception.Message;
            }
            return View("../Leave/LeaveEarned/Index", Record);
        }

        public ActionResult Add()
        {
            LeaveEarnedDTO Record = new LeaveEarnedDTO();
            int curentemp = Convert.ToInt32(Session["Empcode"]);
            Record.YearSelectList = _SpecialleavetypeService.GetYearSelectList();
            Record.LeaveTypeSelectList = _LeaveEarnedService.GetEarnedLeaveTypeList();
            int roleid = Convert.ToInt32(Session["RoleId"]);
            Record.EmployeeCodeSelectlist = _LeaveEarnedService.GetBrancheEmployeeSelectList(curentemp);
            return View("../Leave/LeaveEarned/Add", Record);
        }

        [HttpPost]
        public ActionResult Add(LeaveEarnedDTO Record)
        {
            Record.WorkedStartDate= Convert.ToDateTime(NepEngDate.NepToEng(Record.WorkedStartDateNP));
            Record.WorkedEndDate= Convert.ToDateTime(NepEngDate.NepToEng(Record.WorkedEndDateNP));

            int curentemp = Convert.ToInt32(Session["Empcode"]);
            Record.YearSelectList = _SpecialleavetypeService.GetYearSelectList();
            Record.LeaveTypeSelectList = _LeaveEarnedService.GetEarnedLeaveTypeList();
            int roleid = Convert.ToInt32(Session["RoleId"]);
            Record.EmployeeCodeSelectlist = _LeaveEarnedService.GetBrancheEmployeeSelectList(curentemp);
            Decimal totalDays = _LeaveEarnedService.YearlyEarnedLeave(Record.EmpCode, Record.LeaveYearId, Convert.ToInt32(Record.LeaveTypeId));

            int takingLeave = Convert.ToDateTime(Record.WorkedEndDate).Date.Subtract((Convert.ToDateTime(Record.WorkedStartDate)).Date).Duration().Days + 1;

            if (Record.WorkedEndDate.Date.CompareTo(Record.WorkedStartDate) < 0)
            {
                ViewBag.Error = "Work Start Date should be smaller than Work End Date";
            }
            else if ((takingLeave + totalDays) > 10)
            {
                ViewBag.Error = "One Employee can earn max 10 days in one year. This Employee has already earned " + totalDays + " Days.";
            }
            else
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        _LeaveEarnedService.AddLeaveEarned(Record);
                        Session["Success"] = "Successfully Added Leave Earn";
                        return RedirectToAction("Index", "LeaveEarned");
                    }
                    else
                    {
                        ViewBag.Error = "Form Error";

                    }
                }
                catch (Exception Exception)
                {

                    ViewBag.Error = Exception.Message;

                }
            }


            return View("../Leave/LeaveEarned/Add", Record);
        }
        public ActionResult Update(int id)
        {

            int curentemp = Convert.ToInt32(Session["Empcode"]);
            int roleid = Convert.ToInt32(Session["RoleId"]);
            LeaveEarnedDTO Record = new LeaveEarnedDTO();
            Record = _LeaveEarnedService.GetOneLeaveEarned(id);
            Record.WorkedStartDateNP = NepEngDate.EngToNep(Record.WorkedStartDate);
            Record.WorkedEndDateNP = NepEngDate.EngToNep(Record.WorkedEndDate);
            Record.YearSelectList = _SpecialleavetypeService.GetYearSelectList();
            Record.LeaveTypeSelectList = _LeaveEarnedService.GetEarnedLeaveTypeList();


            //   Record.EmployeeCodeSelectlist = _LeaveEarnedService.GetSelectedBranchEmployeeSelectList(curentemp,Convert.ToInt32(Record.EmpCode));
            Record.ApproveEmployeeCodeSelectlist = _DynamicSelectList.GetLeaveApproverSelectList(Record.EmpCode, "Earning");
            Record.EmpId = Record.EmpCode;
            Record.GetRecommederSelectList = _DynamicSelectList.GetLeaveRecommenderSelectList(Record.EmpCode, "Earning");

            return View("../Leave/LeaveEarned/Update", Record);
        }

        [HttpPost]
        public ActionResult Update(LeaveEarnedDTO Record)
        {

            int curentemp = Convert.ToInt32(Session["Empcode"]);
            Record.WorkedStartDate = Convert.ToDateTime(NepEngDate.NepToEng(Record.WorkedStartDateNP));
            Record.WorkedEndDate = Convert.ToDateTime(NepEngDate.NepToEng(Record.WorkedEndDateNP));
            Record.YearSelectList = _SpecialleavetypeService.GetYearSelectList();
            Record.LeaveTypeSelectList = _LeaveEarnedService.GetEarnedLeaveTypeList();
            int roleid = Convert.ToInt32(Session["RoleId"]);

            Record.ApproveEmployeeCodeSelectlist = _DynamicSelectList.GetLeaveApproverSelectList(Record.EmpCode, "Earning");
            Record.EmpId = Record.EmpCode;
            Record.GetRecommederSelectList = _DynamicSelectList.GetLeaveRecommenderSelectList(Record.EmpCode, "Earning");

            try
            {
                if (ModelState.IsValid)
                {
                    _LeaveEarnedService.UpdateLeaveEarned(Record);

                    Session["Success"] = "Successfully Updated Leave Earn";
                    return RedirectToAction("Index", "LeaveEarned");
                }
                else
                {
                    ViewBag.Error = "Form Error";

                }
            }
            catch (Exception Exception)
            {

                ViewBag.Error = Exception.Message;

            }

            return View("../Leave/LeaveEarned/Update", Record);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                _LeaveEarnedService.Delete(id);
                Session["Success"] = "Successfully Deleted Leave Earn";
            }
            catch (Exception Exception)
            {

                ViewBag.Error = Exception.Message;
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult GetEmloyeeApproverSelectList(int EmpCode)
        {
            string approverType = "Earning";
            LeaveApplicationDTO Record = new LeaveApplicationDTO();
            Record.ApproverSelectList = _SpecialleavetypeService.GetEmployeeApproverSelectList(EmpCode, approverType);
            return Json(Record.ApproverSelectList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetEmloyeeRecommederSelectList(int EmpCode)
        {
            string recommendType = "Earning";
            LeaveApplicationDTO Record = new LeaveApplicationDTO();
            Record.ApproverSelectList = _SpecialleavetypeService.GetEmployeeRecommenderSelectList(EmpCode, recommendType);
            return Json(Record.ApproverSelectList, JsonRequestBehavior.AllowGet);
        }

    }
}