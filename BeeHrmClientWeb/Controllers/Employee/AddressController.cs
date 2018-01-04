using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHrmClientWeb.CodeBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Employee
{
    public class AddressController : BaseController
    {
        private IEmployeeTrainingService _employeeTrainingService;
        private IEmployeeService _employeeService;
        private ICountryService _countryService;
        private IModulServices _moduleService;
        public AddressController()
        {
            _employeeService = new EmployeeService();
            _employeeTrainingService = new EmployeeTrainingService();
            _countryService = new CountryService();
            _moduleService = new ModuleService();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();

        }
        public ActionResult Index(int Id)
        {
            ViewBag.EmployeeDetail = _employeeService.GetEmployeeDetails(Id);
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(Id);

            List <EmployeeAddressDTO> Record = _employeeService.GetAddressesofEmployeeById(Id);



            return View("../Employee/Address/Index", Record);
        }
        public ActionResult Create(int Id)
        {
            ViewBag.EmployeeDetail = _employeeService.GetEmployeeDetails(Id);
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(Id);
            EmployeeAddressDTO Record = new EmployeeAddressDTO();
            Record.EmployeeCode = Id;
            Record.DistrictSelectList = _employeeService.GetDistrictSelectList();
            Record.ZoneSelectList = _employeeService.GetZoneSelectList();
            Record.CountrySelectList = _countryService.GetCountryList().ToList();
            Record.AddressTypeSelectList = _employeeService.GetAddressTypeSelectList();
            return View("../Employee/Address/Create",Record);
        }
        [HttpPost]
        public ActionResult Create(EmployeeAddressDTO Record)
        {
            ViewBag.EmployeeDetail = _employeeService.GetEmployeeDetails(Record.Id);
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(Record.Id);
            Record.DistrictSelectList = _employeeService.GetDistrictSelectList();
            Record.ZoneSelectList = _employeeService.GetZoneSelectList();
            Record.CountrySelectList = _countryService.GetCountryList().ToList();
            Record.AddressTypeSelectList = _employeeService.GetAddressTypeSelectList();
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeService.CreateEmployeeAddress(Record);
                    TempData["Success"] = "Address created successfully.";
                }
                else
                {
                    TempData["Danger"] = "Please fill in the required field.";
                    return View("../Employee/Address/Create", Record);
                }
            }catch(Exception Exception)
            {
                TempData["Danger"] = Exception.Message;
                return View("../Employee/Address/Create", Record);
            }
            return RedirectToAction("Index",new { Id = Record.EmployeeCode });
        }
        public ActionResult Edit(int Id)
        {
            EmployeeAddressDTO Record = _employeeService.GetAddressById(Id);
            ViewBag.EmployeeDetail = _employeeService.GetEmployeeDetails(Record.EmployeeCode);
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(Record.EmployeeCode);
            Record.DistrictSelectList = _employeeService.GetDistrictSelectList();
            Record.ZoneSelectList = _employeeService.GetZoneSelectList();
            Record.CountrySelectList = _countryService.GetCountryList().ToList();
            Record.AddressTypeSelectList = _employeeService.GetAddressTypeSelectList();
            return View("../Employee/Address/Edit", Record);
        }
        [HttpPost]
        public ActionResult Edit(EmployeeAddressDTO Record)
        {
            ViewBag.EmployeeDetail = _employeeService.GetEmployeeDetails(Record.EmployeeCode);
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(Record.EmployeeCode);
            Record.DistrictSelectList = _employeeService.GetDistrictSelectList();
            Record.ZoneSelectList = _employeeService.GetZoneSelectList();
            Record.CountrySelectList = _countryService.GetCountryList().ToList();
            Record.AddressTypeSelectList = _employeeService.GetAddressTypeSelectList();
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeService.UpdateEmployeeAddress(Record);
                    TempData["Success"] = "Address updated successfully.";
                }
                else
                {
                    TempData["Danger"] = "Please fill in the required field.";
                    return View("../Employee/Address/Edit", Record);
                }
            }
            catch (Exception Exception)
            {
                TempData["Danger"] = Exception.Message;
                return View("../Employee/Address/Edit", Record);
            }
            return RedirectToAction("Index", new { Id = Record.EmployeeCode });
        }
    }
}