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

namespace BeeHrmClientWeb.Controllers.Leave
{
    public class PayrollLeaveDeductionController : BaseController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();
        }
        private IPayrollLeaveDeductionService _payrollLeaveDeductionService;
        private ILeaveEarnedService _LeaveEarnedService;
        private ISpecialLeaveTypeService _SpecialleavetypeService;
        public PayrollLeaveDeductionController()
        {
            _payrollLeaveDeductionService = new PayrollLeaveDeductionService();
            _LeaveEarnedService = new LeaveEarnedService();
            _SpecialleavetypeService = new SpecialLeaveTypeService();
        }
        public ActionResult Index()
        {
            PayrollLeaveDeductionInformation Record = new PayrollLeaveDeductionInformation();
            Record.PayrollLeaveDeductionList = new List<PayrollLeaveDeductionDTO>();
            Record.YearSelectList = _SpecialleavetypeService.GetYearSelectList();
            Record.EmployeeCodeSelectlist = _LeaveEarnedService.GetBrancheEmployeeSelectList(Convert.ToInt32(ViewBag.Empcode));
            List<PayrollLeaveDeductionDTO> RecordList = new List<PayrollLeaveDeductionDTO>();
            return View("../Leave/PayrollLeaveDeduction/Index", Record);
        }



        [HttpPost]
        public ActionResult Index(PayrollLeaveDeductionInformation Record)
        {
            Record.PayrollLeaveDeductionList = new List<PayrollLeaveDeductionDTO>();
            Record.YearSelectList = _SpecialleavetypeService.GetYearSelectList();
            Record.EmployeeCodeSelectlist = _LeaveEarnedService.GetBrancheEmployeeSelectList(Convert.ToInt32(ViewBag.Empcode));
            try
            {
                if (ModelState.IsValid)
                {
                    Record.PayrollLeaveDeductionList = _payrollLeaveDeductionService.GetAllPayrollLeaveDeductionListByYearIdAndEmpCode(Record);
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

            return View("../Leave/PayrollLeaveDeduction/Index", Record);
        }

        public ActionResult Add()
        {
            int roleid = Convert.ToInt32(Session["RoleId"]);
            PayrollLeaveDeductionDTO Record = new PayrollLeaveDeductionDTO();
            Record.YearSelectList = _SpecialleavetypeService.GetYearSelectList();
            Record.LeaveTypeSelectList = _payrollLeaveDeductionService.GetPayrollLeaveDeductionLeaveTypeSelectList();
            Record.LeaveYearId = _payrollLeaveDeductionService.GetCurrentYear();
            if (Record.LeaveTypeId == null)
            {
                ViewBag.Error = "No Current Leave Year. Please Add Current Leave Year.";
                return View("../Leave/PayrollLeaveDeduction/Add", Record);
            }
            Record.EmployeeCodeSelectlist = _LeaveEarnedService.GetBrancheEmployeeSelectList(Convert.ToInt32(ViewBag.Empcode));
            return View("../Leave/PayrollLeaveDeduction/Add", Record);
        }

        [HttpPost]
        public ActionResult Add(PayrollLeaveDeductionDTO Record)
        {
            Record.EmployeeCodeSelectlist = _LeaveEarnedService.GetBrancheEmployeeSelectList(Convert.ToInt32(ViewBag.Empcode));
            Record.YearSelectList = _SpecialleavetypeService.GetYearSelectList();
            Record.LeaveTypeSelectList = _payrollLeaveDeductionService.GetPayrollLeaveDeductionLeaveTypeSelectList();
            Record.LeaveDate = Convert.ToDateTime(NepEngDate.NepToEng(Record.LeaveDateNepali));
            try
            {
                if (ModelState.IsValid)
                {
                    _payrollLeaveDeductionService.AddPayrollLeaveDeduction(Record);

                    Session["Success"] = "Successfully Added Payroll Leave Deduction";
                    return RedirectToAction("Index", "PayrollLeaveDeduction");
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
            return View("../Leave/PayrollLeaveDeduction/Add", Record);
        }
        public ActionResult Update(int id)
        {
            PayrollLeaveDeductionDTO Record = _payrollLeaveDeductionService.GetOnePayrollLeaveDeduction(id);
            Record.EmployeeCodeSelectlist = _LeaveEarnedService.GetBrancheEmployeeSelectList(Convert.ToInt32(ViewBag.Empcode));
            Record.YearSelectList = _SpecialleavetypeService.GetYearSelectList();
            Record.LeaveDateNepali =(NepEngDate.EngToNep(Record.LeaveDate));
            Record.LeaveTypeSelectList = _payrollLeaveDeductionService.GetPayrollLeaveDeductionLeaveTypeSelectList();
            Record.EmpId = Record.EmpCode;
            return View("../Leave/PayrollLeaveDeduction/Update", Record);
        }

        [HttpPost]
        public ActionResult Update(PayrollLeaveDeductionDTO Record)
        {
            int roleid = Convert.ToInt32(Session["RoleId"]);
            Record.EmployeeCodeSelectlist = _LeaveEarnedService.GetBrancheEmployeeSelectList(Convert.ToInt32(ViewBag.Empcode));
            Record.YearSelectList = _SpecialleavetypeService.GetYearSelectList();
            Record.LeaveTypeSelectList = _payrollLeaveDeductionService.GetPayrollLeaveDeductionLeaveTypeSelectList();
            try
            {
                if (ModelState.IsValid)
                {
                    _payrollLeaveDeductionService.UpdatePayrollLeaveDeduction(Record);

                    Session["Success"] = "Successfully Updated Payroll Leave Deduction";
                    return RedirectToAction("Index", "PayrollLeaveDeduction");
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
            return View("../Leave/PayrollLeaveDeduction/Update", Record);
        }


        public ActionResult Delete(int id)
        {
            try
            {
                _payrollLeaveDeductionService.Delete(id);
                Session["Success"] = "Successfully Deleted ";
            }
            catch (Exception Exception)
            {
                ViewBag.Error = Exception.Message;
            }
            return RedirectToAction("Index", "PayrollLeaveDeduction");

        }

        [HttpPost]
        public JsonResult PayrollLeaveBalance(int LeaveTypeId, int LeaveYearId, int EmpCode)
        {
            PayrollLeaveDeductionDTO Record = new PayrollLeaveDeductionDTO();
            Record.leavebalance = _payrollLeaveDeductionService.GetPayrollLeaveBalance(LeaveTypeId, LeaveYearId, EmpCode);
            return Json(Record.leavebalance, JsonRequestBehavior.AllowGet);
        }
    }
}