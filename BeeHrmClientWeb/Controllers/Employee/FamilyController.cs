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

namespace BeeHrmClientWeb.Controllers.Employee
{
    public class FamilyController : BaseController
    {
       private IEmployeeFamilyService _EmployeeFamilyService;
        private ILeaveEarnedService _LeaveEarnedService;
        private IModulServices _moduleService;
        public FamilyController()
        {
            _EmployeeFamilyService = new EmployeeFamilyService();
            _LeaveEarnedService = new LeaveEarnedService();
            _moduleService = new ModuleService();
        }


        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();

        }
        public ActionResult Index(int id)
        {
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
            EmployeeFamilyInformation Record = new EmployeeFamilyInformation();
            Record.EmployeeFamilyList = new List<EmployeeFamilyDTO>();
           Record.EmployeeFamilyList = _EmployeeFamilyService.GetEmployeeByEmpCode(id);
            Record.EmpId = id;
           return View("../Employee/Family/Index", Record);
        }

        public ActionResult Add(int Id)
        {
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(Id);
            EmployeeFamilyDTO Record = new EmployeeFamilyDTO();
            Record.EmpId = Id;
            return View("../Employee/Family/Add", Record);
        }

        [HttpPost]
        public ActionResult Add(EmployeeFamilyDTO Record)
        {
            Record.FDob = !string.IsNullOrEmpty(Record.FDobNP) ? Convert.ToDateTime(NepEngDate.NepToEng(Record.FDobNP)) : Record.FDob;

            int curentemp = Convert.ToInt32(Session["Empcode"]);
            Record.EmpCode = Record.EmpId;
            Record.EmployeeCodeSelectlist = _LeaveEarnedService.GetBrancheEmployeeSelectList(curentemp);
            try
            {
                if (ModelState.IsValid)
                {
                    _EmployeeFamilyService.AddEmplyeeFamily(Record);
                    Session["Success"] = "Successfully Added Employee Family";
                    return RedirectToAction("Index", "Family",new { id=Record.EmpId });
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
            return View("../Employee/Family/Add", Record);
        }

        public ActionResult Update(int id)
        {
            //ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
            EmployeeFamilyDTO Record = new EmployeeFamilyDTO();
            Record = _EmployeeFamilyService.GetOneEmployeeFamily(id);
            Record.FDobNP = !String.IsNullOrEmpty(Convert.ToString(Record.FDob)) ? NepEngDate.EngToNep(Convert.ToDateTime(Record.FDob)) : null;
            return View("../Employee/Family/Update", Record);
        }

        [HttpPost]
        public ActionResult Update(EmployeeFamilyDTO Record)
        {
            Record.FDob = !string.IsNullOrEmpty(Record.FDobNP) ? Convert.ToDateTime(NepEngDate.NepToEng(Record.FDobNP)) : Record.FDob;

            try
            {
                if (ModelState.IsValid)
                {
                    _EmployeeFamilyService.UpdateEmployeeFamily(Record);
                    Session["Success"] = "Successfully Updated Employee Family";
                    return RedirectToAction("Index", "Family", new { id = Record.EmpId });
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
            return View("../Employee/Family/Update", Record);
        }

    }


    }


