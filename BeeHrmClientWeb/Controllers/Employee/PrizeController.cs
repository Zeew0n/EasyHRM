using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Employee
{
    public class PrizeController : BaseController
    {
        private IEmployeePrizeService _empPrizeService;
        private IDesignationService _designationServices;
        private IEmployeeService _employeeService;
        private IModulServices _moduleService;

        public PrizeController()
        {
            _employeeService = new EmployeeService();
            _designationServices = new DesignationService();
            _empPrizeService = new EmployeePrizeService();
            _moduleService = new ModuleService();
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();
        }

        #region Employee Prize
        [Route("prize/{id}")]
        public ActionResult EmployeePrize(int id)
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            IEnumerable<EmployeePrizeDTO> data = _empPrizeService.GetAllPrizeOfEmployee(id);
            ViewBag.EmployeeDetail = _employeeService.GetEmployeeDetails(id);
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
            return View("../Employee/EmployeePrize/Index", data);
        }
        [Route("prize/{id}/create")]
        public ActionResult EmployeePrizeCreate(int id)
        {
            if (!ViewBag.AllowCreate)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            EmployeePrizeDTO Record = new EmployeePrizeDTO();
            Record.PrizeEmpCode = id;
            ViewBag.EmployeeDetail = _employeeService.GetEmployeeDetails(id);
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
            List<SelectListItem> designationList = new List<SelectListItem>();
            foreach (var row in _designationServices.GetDesignationList())
            {
                designationList.Add(new SelectListItem
                {
                    Text = row.DsgName,
                    Value = row.DsgId.ToString()
                });
            }
            ViewBag.DesignationList = designationList;
            return View("../Employee/EmployeePrize/Create",Record);
        }

        [HttpPost]
        [Route("prize/{id}/create")]
        public ActionResult EmployeePrizeCreate(int id, EmployeePrizeDTO data)
        {
            data.PrizeDate = !string.IsNullOrEmpty(data.PrizeDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.PrizeDateNP)) : data.PrizeDate;


            if (!ViewBag.AllowCreate)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            try
            {
                data.PrizeEmpCode = id;
                data.CreatedAt = System.DateTime.Now;
                EmployeePrizeDTO res = _empPrizeService.InsertPrizeOfEmployee(data);
                ViewBag.EmployeeDetail = _employeeService.GetEmployeeDetails(id);
                ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
                ModelState.Clear();
                ViewBag.Success = "Employee prize added";
                return RedirectToAction("EmployeePrize", id);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ModelState.Clear();
                return RedirectToAction("EmployeePrize", id);
            }
        }

        [Route("prize/{empCode}/EmployeePrizeEdit/{id}")]
        public ActionResult EmployeePrizeEdit(int empCode, int id)
        {
            if (!ViewBag.AllowEdit)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            EmployeePrizeDTO res = _empPrizeService.GetPrizeById(id);
            res.PrizeDateNP = !String.IsNullOrEmpty(Convert.ToString(res.PrizeDate)) ? NepEngDate.EngToNep(Convert.ToDateTime(res.PrizeDate)) : null;
            ViewBag.EmployeeDetail = _employeeService.GetEmployeeDetails(empCode);
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(empCode);
            List<SelectListItem> designationList = new List<SelectListItem>();
            foreach (var row in _designationServices.GetDesignationList())
            {
                designationList.Add(new SelectListItem
                {
                    Text = row.DsgName,
                    Value = row.DsgId.ToString()
                });
            }
            ViewBag.DesignationList = designationList;
            return View("../Employee/EmployeePrize/Edit", res);
        }

        [Route("prize/{empCode}/EmployeePrizeEdit/{prizeId}")]
        [HttpPost]
        public ActionResult EmployeePrizeEdit(int empCode, int prizeId, EmployeePrizeDTO data)
        {
            data.PrizeDate = !string.IsNullOrEmpty(data.PrizeDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.PrizeDateNP)) : data.PrizeDate;

            if (!ViewBag.AllowEdit)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            try
            {
                data.CreatedAt = System.DateTime.Now;
                int res = _empPrizeService.UpdateEmployeePrize(data);
                ViewBag.Success = "Employee prize updated successfully";
                ViewBag.EmployeeDetail = _employeeService.GetEmployeeDetails(empCode);
                ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(empCode);

                ModelState.Clear();
                return RedirectToAction("EmployeePrize", new { id = empCode });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ModelState.Clear();
                ViewBag.EmployeeDetail = _employeeService.GetEmployeeDetails(empCode);
                ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(empCode);
                return RedirectToAction("EmployeePrize", new { id = empCode });
            }
        }

        [Route("prize/{empCode}/EmployeeDelete/{prizeId}")]
        public ActionResult EmployeePrizeDelete(int empCode, int prizeId)
        {
            if (!ViewBag.AllowDelete)
            {
                ViewBag.Error = "You are not Authorize to perform this Action";
                return PartialView("_partialviewNotFound");
            }
            _empPrizeService.DeleteEmployeePrize(prizeId);
            return RedirectToAction("EmployeePrize", new { id = empCode });
        }

        #endregion
    }
}