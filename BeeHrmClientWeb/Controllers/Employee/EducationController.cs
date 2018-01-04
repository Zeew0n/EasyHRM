using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using BeeHrmClientWeb.CodeBase;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHrmClientWeb.Utilities;
using BeeHrmClientWeb.Utilities.Date;

namespace BeeHrmClientWeb.Controllers.Employee
{
    public class EducationController : BaseController
    {
        
        private IEmpEducationService _empEducationService;
        private IEmployeeService _empDetails;
        private IEducationLevelService _educationLevel;
        private ICountryService _countryService;
        private IModulServices _moduleService;

        //IEmployeeTrainingService _empTraining;


        public EducationController()
        {
            _empEducationService = new EmpEducationService();
            _empDetails = new EmployeeService();
            _educationLevel = new EducationLevelService();
            _countryService = new CountryService();
            _moduleService = new ModuleService();
            // _empTraining = new EmployeeTrainingService();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();

        }
       
        [Route("education/{id}")]
        public ActionResult EducationList(int id)
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            ViewBag.EmployeeDetail = _empDetails.GetEmployeeDetails(id);
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
            // ViewBag.EmployeeTraining = _empTraining.GetAllTrainingById(id);
            IEnumerable<EmpEducationDTO> list = _empEducationService.GetAllEducationById(id);
            return View("../Employee/Education/EducationList", list);
        }
        [Route("education/{id}/create")]
        public ActionResult EducationCreate(int id)
        {
            if (!ViewBag.AllowCreate)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            List<SelectListItem> educationLevel = new List<SelectListItem>();
            foreach (var row in _educationLevel.GetEducationLevel())
            {
                educationLevel.Add(new SelectListItem {
                    Text = row.LevelName,
                    Value = row.LevelId.ToString()
                });
            }
            IEnumerable<SelectListItem> CountryList = _countryService.GetCountryList();
            ViewBag.Countries = CountryList;
            ViewBag.EducationLevel = educationLevel;
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
            ViewBag.EmployeeDetail = _empDetails.GetEmployeeDetails(id);
            return View("../Employee/Education/EducationCreate");
        }

        [HttpPost]
        [Route("education/{id}/create")]
        public ActionResult EducationCreate(int id,EmpEducationDTO data)
        {
            data.PassedDate = !string.IsNullOrEmpty(data.PassedDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.PassedDateNP)) : data.PassedDate;
            List<SelectListItem> educationLevel = new List<SelectListItem>();
            foreach (var row in _educationLevel.GetEducationLevel())
            {
                educationLevel.Add(new SelectListItem
                {
                    Text = row.LevelName,
                    Value = row.LevelId.ToString()
                });
            }
            IEnumerable<SelectListItem> CountryList = _countryService.GetCountryList();
            ViewBag.Countries = CountryList;
            ViewBag.EducationLevel = educationLevel;
            ViewBag.EmployeeDetail = _empDetails.GetEmployeeDetails(id);
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
            try
            {
                if (ModelState.IsValid)
                {
                   
                    ModelState.AddModelError("Error","Please Fill up all required field");
                    return RedirectToAction("EducationList" + id);
                }
                EmpEducationDTO res = new EmpEducationDTO();
                data.EmpCode = id;
                res = _empEducationService.InsertEmpEducation(data);
                ViewBag.Success = "Education added";
                return Redirect("/Education/" + data.EmpCode);
            }
            catch (Exception ex)
            {
                ViewBag.EmployeeDetail = _empDetails.GetEmployeeDetails(id);
                ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
                ViewBag.Error = ex.Message;
                ModelState.AddModelError("Error",ex.Message);
                return View("../Employee/Education/EducationCreate",data);
            }
        }

        [Route("Education/{empId}/Edit/{eduId}")]
        public ActionResult EducationEdit(int empId, int eduId)
        {
            if (!ViewBag.AllowView)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            List<SelectListItem> educationLevel = new List<SelectListItem>();
            foreach (var row in _educationLevel.GetEducationLevel())
            {
                educationLevel.Add(new SelectListItem
                {
                    Text = row.LevelName,
                    Value = row.LevelId.ToString()
                });
            }
            IEnumerable<SelectListItem> countries = _countryService.GetCountryList();
            ViewBag.Countries = countries;
            ViewBag.EducationLevel = educationLevel;
            ViewBag.EmployeeDetail = _empDetails.GetEmployeeDetails(empId);
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(empId);
            EmpEducationDTO data = _empEducationService.GetEducationByEduId(eduId);
            data.PassedDateNP = !String.IsNullOrEmpty(Convert.ToString(data.PassedDate)) ? NepEngDate.EngToNep(Convert.ToDateTime(data.PassedDate)) : null;

            //data.PassedDate = Convert.ToDateTime( NepDateConverter.EngToNep(data.PassedDate).ToString());
            return View("../Employee/Education/EducationEdit",data);
        }


        [HttpPost]
        [Route("Education/{empId}/Edit/{eduId}")]
        public ActionResult EducationEdit(EmpEducationDTO data)
        {
            data.PassedDate = !string.IsNullOrEmpty(data.PassedDateNP) ? Convert.ToDateTime(NepEngDate.NepToEng(data.PassedDateNP)) : data.PassedDate;

            if (!ViewBag.AllowEdit)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            try
            {
                ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(data.EmpCode);
                List<SelectListItem> educationLevel = new List<SelectListItem>();
                foreach (var row in _educationLevel.GetEducationLevel())
                {
                    educationLevel.Add(new SelectListItem
                    {
                        Text = row.LevelName,
                        Value = row.LevelId.ToString()
                    });
                }
                IEnumerable<SelectListItem> countries = _countryService.GetCountryList();
                ViewBag.Countries = countries;
                ViewBag.EducationLevel = educationLevel;              
                ViewBag.EmployeeDetail = _empDetails.GetEmployeeDetails(data.EmpCode);
                if (!ModelState.IsValid)
                {
                    return View("../Employee/Education/EducationEdit", data);
                }
                int res = _empEducationService.UpdateEducation(data);
                if (res > 0)
                {
                    ViewBag.Success = "Education updated successfully";
                }
                else
                {
                    ViewBag.Error = "Couldn't update";
                }
                return Redirect("/education/" + data.EmpCode);
            }
            catch (Exception ex)
            {
                ViewBag.EmployeeDetail = _empDetails.GetEmployeeDetails(data.EmpCode);
                ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(data.EmpCode);
                ViewBag.Error = ex.Message;
                return View("../Employee/Education/EducationEdit",data);
            }
        }
        [Route("Education/{empId}/Delete/{eduId}")]
        public ActionResult EducationDelete(int empId,int eduId)
        {
            if (!ViewBag.AllowEdit)
            {
                ViewBag.Error = "You are not Authorize to use this Page";
                return PartialView("_partialviewNotFound");
            }
            try
            {
                ViewBag.EmployeeDetail = _empDetails.GetEmployeeDetails(empId);
                _empEducationService.DeleteEducation(eduId);
                ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(empId);
                ViewBag.Success = "Education deleted successfully";
                return View("../Employee/Education/EducationList", _empEducationService.GetAllEducationById(empId));
            }
            catch (Exception ex)
            {
                ViewBag.EmployeeDetail = _empDetails.GetEmployeeDetails(empId);
                ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(empId);
                ViewBag.Error = ex.Message;
                return View("../Employee/Education/EducationList", _empEducationService.GetAllEducationById(empId));
            }
        }

        
    }
}