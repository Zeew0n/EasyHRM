using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeeHRM.Repository;
using BeeHrmClientWeb.CodeBase;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.Implementations;
using System.Web.Routing;
using BeeHRM.ApplicationService.DTOs;
using BeeHrmClientWeb.Utilities;
using BeeHrmInterface.GlobalSelectLists;

namespace BeeHrmClientWeb.Controllers.Payroll
{
    public class PayrollOvertimesController : BaseController
    {
        private dbBeeHRMEntities db = new dbBeeHRMEntities();
     
        private IPayrollOvertimeSerivce _payrollOvertimeService;
        private IEmployeeService _employeeService;
        private IDynamicSelectList _dynamicSelectList;
        public PayrollOvertimesController()
        {
            _employeeService = new EmployeeService();
            _payrollOvertimeService = new PayrollOvertimeService();
            _dynamicSelectList = new DynamicSelectList();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();

        }
        // GET: PayrollOvertimes
        public ActionResult Index()
        {
            List<PayrollOvertimeDTO> list = _payrollOvertimeService.GetAllPayrollOvertime();
            foreach (var item in list)
            {
                item.ApproverName = _employeeService.GetEmployeeByID(Convert.ToInt32(item.ApprovedById)).Name;
                item.OvertimeDateNP = (NepEngDate.EngToNep(Convert.ToDateTime(item.OvertimeDate)));
                item.ApproveStatusDateNP = (NepEngDate.EngToNep(Convert.ToDateTime(item.ApproveStatusDate)));
                ViewBag.Overtimedate = Convert.ToDateTime(item.ApproveStatusDate).ToString("yyyy-MMM-dd");
                ViewBag.ApproveStatusDate = Convert.ToDateTime(item.ApproveStatusDate).ToString("yyyy-MMM-dd");
            }


            return View(list);
        }

        // GET: PayrollOvertimes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) 
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayrollOvertime payrollOvertime = db.PayrollOvertimes.Find(id);
            if (payrollOvertime == null)
            {
                return HttpNotFound();
            }
            return View(payrollOvertime);
        }

        // GET: PayrollOvertimes/Create
        public ActionResult Create()
        {
            int EmpCode = Convert.ToInt32(Session["Empcode"]);
            PayrollOvertimeDTO data = new PayrollOvertimeDTO();
           // data.EmployeeList = _payrollOvertimeService.GetEmployeeList();
            ViewBag.ddlEmployeeList = _dynamicSelectList.GetEmployeeByEmpCode(EmpCode).ToList();
            //data.ApproverList = _dynamicSelectList.GetLeaveApproverSelectList(EmpCode, "Overtime");
            return View(data);
        }

        // POST: PayrollOvertimes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PayrollOvertimeDTO payrollOvertime)
        {
            int EmpCode = Convert.ToInt32(Session["Empcode"]);
            payrollOvertime.OvertimeDate = Convert.ToDateTime(NepEngDate.NepToEng(payrollOvertime.OvertimeDateNP));
            payrollOvertime.ApproveStatusDate = Convert.ToDateTime(NepEngDate.NepToEng(payrollOvertime.ApproveStatusDateNP));
            payrollOvertime.OvertimeAppliedDate = Convert.ToDateTime(NepEngDate.NepToEng(payrollOvertime.OvertimeAppliedDateNP));
            PayrollOvertimeDTO jtd = new PayrollOvertimeDTO();
            ViewBag.ddlEmployeeList = _dynamicSelectList.GetEmployeeByEmpCode(EmpCode).ToList();
            if (!ModelState.IsValid)
            {
                return View(jtd);
            }
            PayrollOvertimeDTO res = new PayrollOvertimeDTO();
            res = _payrollOvertimeService.InsertPayrollOvertime(payrollOvertime);
            ViewBag.Success = "Overtime of " + res.OvertimeAppliedDate + " has been created";
            ModelState.Clear();
            return View(jtd);
        }

        // GET: PayrollOvertimes/Edit/5  
        public ActionResult Edit(int id)
        {
            int EmpCode = Convert.ToInt32(Session["Empcode"]);
            PayrollOvertimeDTO res = new PayrollOvertimeDTO();
            res = _payrollOvertimeService.GetPayrollOvertimeById(id);
            res.OvertimeDateNP = (NepEngDate.EngToNep(Convert.ToDateTime(res.OvertimeDate)));
            res.ApproveStatusDateNP = (NepEngDate.EngToNep(Convert.ToDateTime(res.ApproveStatusDate)));
            res.OvertimeAppliedDateNP = (NepEngDate.EngToNep(Convert.ToDateTime(res.OvertimeAppliedDate)));
            res.ApproverList = _dynamicSelectList.GetLeaveApproverSelectList(res.EmpCode, "Overtime");
            ViewBag.EmpCode = res.EmpCode;
            ViewBag.ddlEmployeeList = _dynamicSelectList.GetEmployeeByEmpCode(EmpCode).ToList();
            return View(res);
        }

        // POST: PayrollOvertimes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PayrollOvertimeDTO payrollOvertime)
        {
            int EmpCode = Convert.ToInt32(Session["Empcode"]);
            payrollOvertime.OvertimeDate = Convert.ToDateTime(NepEngDate.NepToEng(payrollOvertime.OvertimeDateNP));
            payrollOvertime.ApproveStatusDate = Convert.ToDateTime(NepEngDate.NepToEng(payrollOvertime.ApproveStatusDateNP));
            payrollOvertime.OvertimeAppliedDate = Convert.ToDateTime(NepEngDate.NepToEng(payrollOvertime.OvertimeAppliedDateNP));
            PayrollOvertimeDTO jtd = new PayrollOvertimeDTO();
            if (!ModelState.IsValid)
            {
                return View(jtd);
            }
            int res = _payrollOvertimeService.UpdatePayrollOvertime(payrollOvertime);
            ViewBag.ddlEmployeeList = _dynamicSelectList.GetEmployeeByEmpCode(EmpCode).ToList();
            jtd.ApproverList = _dynamicSelectList.GetLeaveApproverSelectList(jtd.EmpCode, "Overtime");
            ViewBag.Success = "Overtime of " + payrollOvertime.OvertimeAppliedDate + " has been updated";
            ModelState.Clear();
            return View(jtd);
        }

        // GET: PayrollOvertimes/Delete/5
        public ActionResult Delete(int id)
        {
            _payrollOvertimeService.DeletePayrollOvertime(id);
            return View();
        }

        // POST: PayrollOvertimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PayrollOvertime payrollOvertime = db.PayrollOvertimes.Find(id);
            db.PayrollOvertimes.Remove(payrollOvertime);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ApproverList(int empcode)
        {
            IEnumerable<SelectListItem> ApproverList = _dynamicSelectList.GetLeaveApproverSelectList(empcode, "Overtime");
            //jtd.ApproverList = _dynamicSelectList.GetLeaveApproverSelectList(jtd.EmpCode, "Overtime");

            return Json(ApproverList,JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
