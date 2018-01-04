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

namespace BeeHrmClientWeb.Controllers.Employee
{
   
    public class SkillController : BaseController
    {
        private IEmployeeSkillService _EmployeeSkillService;
        private ILeaveEarnedService _LeaveEarnedService;
        private IModulServices _moduleService;
        public SkillController()
        {
            _EmployeeSkillService = new EmployeeSkillService();
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
            try
            {
                ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
                EmployeeSkillInformation Record = new EmployeeSkillInformation();
                Record.EmployeeSkillList = new List<EmployeeSkillsDTO>();
                Record.EmployeeSkillList = _EmployeeSkillService.GetEmployeeByEmpCode(id);
                Record.EmpId = id;
                return View("../Employee/Skill/Index", Record);
            }
            catch (Exception ex)
            {
                return null;

            }

        }

        public ActionResult Add(int Id)
        {
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(Id);
            EmployeeSkillsDTO Record = new EmployeeSkillsDTO();
            Record.SkillSelectlist= _EmployeeSkillService.GetSkillSelectList();
            Record.EmpId = Id;
            return View("../Employee/Skill/Add", Record);
        }

        [HttpPost]
        public ActionResult Add(EmployeeSkillsDTO Record)
        {
           
            Record.EmpCode = Record.EmpId;
            Record.SkillSelectlist = _EmployeeSkillService.GetSkillSelectList();
            try
            {
                if (ModelState.IsValid)
                {
                    _EmployeeSkillService.AddEmployeeSkills(Record);
                    Session["Success"] = "Successfully Added Employee Skill";
                    return RedirectToAction("Index", "Skill", new { id = Record.EmpId });
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
            return View("../Employee/Skill/Add", Record);
        }

        public ActionResult Update(int id)
        {
           
            EmployeeSkillsDTO Record = new EmployeeSkillsDTO();
            Record = _EmployeeSkillService.GetOneEmployeeSkills(id);
            Record.EmpId = Record.EmpCode;
            Record.SkillSelectlist = _EmployeeSkillService.GetSkillSelectList();


            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(Record.EmpId);

            return View("../Employee/Skill/Update", Record);
        }

        [HttpPost]
        public ActionResult Update(EmployeeSkillsDTO Record)
        {
            Record.SkillSelectlist = _EmployeeSkillService.GetSkillSelectList();
            try
            {
                if (ModelState.IsValid)
                {
                    _EmployeeSkillService.UpdateEmployeeSkill(Record);
                    Session["Success"] = "Successfully Updated Employee Skill";
                    return RedirectToAction("Index", "Skill", new { id = Record.EmpId });
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
            return View("../Employee/Skill/Update", Record);
        }

        public ActionResult Delete(int id)
        {
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
            EmployeeSkillsDTO Record = new EmployeeSkillsDTO();
            try
            {
                Record.EmpId = _EmployeeSkillService.GetEmpCode(id);
                _EmployeeSkillService.Delete(id);
                Session["Success"] = "Successfully Deleted Employee Skill";

            }
            catch (Exception Exception)
            {

                ViewBag.Error = Exception.Message;
            }
            return RedirectToAction("Index", "Skill", new { id = Record.EmpId });
        }
    }
}


