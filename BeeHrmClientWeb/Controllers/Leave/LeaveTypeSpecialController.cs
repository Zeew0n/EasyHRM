using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using BeeHrmClientWeb.CodeBase;
using System.Web.Routing;
using Newtonsoft.Json;
using BeeHrmClientWeb.Models;
using BeeHrmClientWeb.Utilities;

namespace BeeHrmClientWeb.Controllers
{
    public class LeaveTypeSpecialController : BaseController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();
        }
        private IEmployeeService _employeeServices;
        private ILeaveApplicationService _leaveServices;
        private ISpecialLeaveTypeService _SpecialleavetypeService;
        private ILeaveEarnedService _LeaveEarnedService;


        public LeaveTypeSpecialController()
        {
            _leaveServices = new LeaveApplicationService();
            _SpecialleavetypeService = new SpecialLeaveTypeService();
            _LeaveEarnedService = new LeaveEarnedService();
            _employeeServices = new EmployeeService();
        }

        public ActionResult FilterBox()
        {
            LeaveApplicationDTO Record = new LeaveApplicationDTO();
            Record.YearSelectList = _SpecialleavetypeService.GetYearSelectList();
            Record.EmployeeCodeSelectlist = _LeaveEarnedService.GetBrancheEmployeeSelectList(Convert.ToInt32(ViewBag.Empcode));
            return View("../Leave/LeaveTypeSpecial/FilterBox", Record);
        }
        public ActionResult Index()
        {
           
            LeaveApplicationDTOInformation Record = new LeaveApplicationDTOInformation();
            Record.LeaveApplicationDTOList = new List<LeaveApplicationDTO>();
            Record.YearSelectList = _SpecialleavetypeService.GetYearSelectList();
            Record.EmployeeCodeSelectlist = _LeaveEarnedService.GetBrancheEmployeeSelectList(Convert.ToInt32(ViewBag.Empcode));
             return View("../Leave/LeaveTypeSpecial/Index",Record);
        }

        [HttpPost]
        public ActionResult Index(LeaveApplicationDTOInformation Record)
        {
            Record.YearSelectList = _SpecialleavetypeService.GetYearSelectList();
            Record.EmployeeCodeSelectlist = _LeaveEarnedService.GetBrancheEmployeeSelectList(Convert.ToInt32(ViewBag.Empcode));
            List<LeaveApplicationDTO> RecordList = new List<LeaveApplicationDTO>();
            try
            {
                if (ModelState.IsValid)
                {
                    int roleid = Convert.ToInt32(Session["RoleId"]);
                    Record.LeaveApplicationDTOList = _SpecialleavetypeService.GetAllSpecialApplicationLeaveListByYearAndMonth(Record);
                }
                else
                {
                    ViewBag.Error = "Please Select Properly";
                }
           
            }
            catch (Exception Exception)
            {

                ViewBag.Error = Exception.Message;
                return View("../Leave/LeaveTypeSpecial/Index", Record);
            }
          
            return View("../Leave/LeaveTypeSpecial/Index", Record);
        }

        public ActionResult Add()
        {
            int roleid = Convert.ToInt32(Session["RoleId"]);
            int empCode = Convert.ToInt32(Session["EmpCode"]);
            LeaveApplicationDTO Record = new LeaveApplicationDTO();
            Record.EmployeeCodeSelectlist = _LeaveEarnedService.GetBrancheEmployeeSelectList(empCode);
            Record.YearSelectList = _SpecialleavetypeService.GetYearSelectList();
            Record.LeaveTypeSelectList = _SpecialleavetypeService.GetSpecialLeaveTypeSelectList();
            Record.ApproverSelectList = new List<SelectListItem>();
            return View("../Leave/LeaveTypeSpecial/Add", Record);
        }

        [HttpPost]
        public ActionResult Add(LeaveApplicationDTO Record)
        {
           // int curentemp = Convert.ToInt32(Session["Empcode"]);
            //int roleid = Convert.ToInt32(Session["RoleId"]);
            int empCode = Convert.ToInt32(Session["EmpCode"]);
            Record.LeaveStartDate = Convert.ToDateTime(NepEngDate.NepToEng(Record.LeaveStartDateNP));
            Record.LeaveEndDate = Convert.ToDateTime(NepEngDate.NepToEng(Record.LeaveEndDateNP));
            Record.RecommededEmpCode = Record.RecommededEmpCode;
            Record.EmployeeCodeSelectlist = _LeaveEarnedService.GetBrancheEmployeeSelectList(empCode);
            Record.YearSelectList = _SpecialleavetypeService.GetYearSelectList();
            Record.LeaveTypeSelectList = _SpecialleavetypeService.GetSpecialLeaveTypeSelectList();

            if (Record.LeaveEmpCode == 0 || Record.RecommededEmpCode == 0 || Record.LeaveApproverEmpCode == 0)
            {
                ViewBag.Error = "* Fields are required";
                return View("../Leave/LeaveTypeSpecial/Add", Record);
            }
            try
            {
                if (ModelState.IsValid)
                {
                    _SpecialleavetypeService.AddSpecialLeaveType(Record);
                    Session["Success"] = "Successfully Added Special Leave ";
                    return RedirectToAction("Index", "LeaveTypeSpecial");
                }
            }
            catch (Exception Exception)
            {
                ViewBag.Error = Exception.Message;
                Session["Error"] = ViewBag.Error = Exception.Message;
                return View("../Leave/LeaveTypeSpecial/Add");
            }
            return View("../Leave/LeaveTypeSpecial/Add");
        }
        
        [HttpGet]
         public ActionResult RejectSpecialLeaveType(int Id)
        {
            try
            {
             _SpecialleavetypeService.Reject(Id);
                Session["Success"] = "Successfully Rejected ";
            }
            catch (Exception Exception)
            {
                ViewBag.Error = Exception.Message;
            }
             return RedirectToAction("Index", "LeaveTypeSpecial");
        }

        public ActionResult LeaveTypeSpecialDetail(int id)
        {
            LeaveApplicationViewModel lavm = _leaveServices.LeaveDetailAdmin(id);
            LeaveApplicationRecommendDetailViewModel lardVM = new LeaveApplicationRecommendDetailViewModel();
            lardVM.LeaveDetail = lavm;
            lardVM.LeaveApplier = _employeeServices.GetEmployeeDetails(lavm.LeaveEmpCode);
            return View("../Leave/LeaveTypeSpecial/LeaveTypeSpecialDetail", lardVM);
        }



        [HttpPost]
        public JsonResult GetEmloyeeApproverSelectList(int EmpCode)
        {
            string recommendType = "Specical";

            LeaveApplicationDTO Record = new LeaveApplicationDTO();
            Record.ApproverSelectList = _SpecialleavetypeService.GetEmployeeApproverSelectList(EmpCode, recommendType);
            return Json(Record.ApproverSelectList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetEmloyeeRecommederSelectList(int EmpCode)
        {
            string recommendType = "Specical";
            LeaveApplicationDTO Record = new LeaveApplicationDTO();
            Record.ApproverSelectList = _SpecialleavetypeService.GetEmployeeRecommenderSelectList(EmpCode, recommendType);
            return Json(Record.ApproverSelectList, JsonRequestBehavior.AllowGet);
        }
    }
   
}