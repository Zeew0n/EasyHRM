using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.Repository;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Utilities;
using BeeHrmClientWeb.Utilities.Date;
using BeeHrmInterface.GlobalSelectLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Payroll
{
    public class PayrollInsurancePremiumController : BaseController
    {
        private IPayrollInsurancePremiumService _PayrollInsurancePremiumService;
        private IInsuranceCompanyService _insuranceCompanyService;
        private IEmployeeService _empDetails;
        private IModulServices _moduleService;
        private IDynamicSelectList _dynamicSelectList;

        private dbBeeHRMEntities db = new dbBeeHRMEntities();

        public PayrollInsurancePremiumController()
        {
            _PayrollInsurancePremiumService = new PayrollInsurancePremiumService();
            _insuranceCompanyService = new InsuranceCompanyService();
            _empDetails = new EmployeeService();
            _moduleService = new ModuleService();
            _dynamicSelectList = new DynamicSelectList();

        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();
        }

        public ActionResult Index()
        {
            try
            {
                //ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
                PayrollInsuranceInformation Record = new PayrollInsuranceInformation();
                Record.PayrollInsurancePremiumList = new List<PayrollInsurancePremiumDTO>();
                Record.PayrollInsurancePremiumList = _PayrollInsurancePremiumService.GetAllPayrollInsurancePremium().ToList();
                foreach (var item in Record.PayrollInsurancePremiumList) {
                    item.StartDateNP = NepEngDate.EngToNep(Convert.ToDateTime(item.StartDate)).ToString().Replace('/', '-');
                    item.EndDateNP = NepEngDate.EngToNep(Convert.ToDateTime(item.EndDate)).ToString().Replace('/', '-');
                    if (item.AmountType == "A") {
                        item.AmountType = "Annual";
                    }
                    else if (item.AmountType == "H") {
                        item.AmountType = "Half Yearly";
                    }
                    else if (item.AmountType == "Q") {
                        item.AmountType = "Quarterly";
                    }
                    else if (item.AmountType == "M") {
                        item.AmountType = "Monthly";
                    }
                }
                //Record.EmpId = id;
                //IEnumerable<PayrollInsurancePremiumDTO> list = _PayrollInsurancePremiumService.GetAllPayrollInsurancePremium();
                return View(Record);
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public ActionResult Create()
        {
            //ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu();
            int EmpCode = Convert.ToInt32(Session["Empcode"]);
            ViewBag.EmpCode = "";
            PayrollInsurancePremiumDTO Record = new PayrollInsurancePremiumDTO();
            Record.InsuranceCompanySelectlist = _PayrollInsurancePremiumService.GetInsuranceCompanySelectList();
            ViewBag.ddlEmployeeList = _dynamicSelectList.GetEmployeeByEmpCode(EmpCode).ToList();
            Record.AmountTypeSelectList = StaticSelectList.AmountTypeSelectList();
            //Record.EmpCode = Id;
            return View("../PayrollInsurancePremium/Create",Record);
        }

        [HttpPost]
        public ActionResult Create(PayrollInsurancePremiumDTO data)
        {
            data.StartDate = Convert.ToDateTime(NepEngDate.NepToEng(data.StartDateNP));
            data.EndDate = Convert.ToDateTime(NepEngDate.NepToEng(data.EndDateNP));
            int EmpCode = Convert.ToInt32(Session["Empcode"]);
            data.InsuranceCompanySelectlist = _PayrollInsurancePremiumService.GetInsuranceCompanySelectList();
            data.AmountTypeSelectList = StaticSelectList.AmountTypeSelectList();
            ViewBag.ddlEmployeeList = _dynamicSelectList.GetEmployeeByEmpCode(EmpCode).ToList();

            ModelState.Clear();
            try
            {

                if (ModelState.IsValid)
                {
                    _PayrollInsurancePremiumService.InsertPayrollInsurancePremium(data);
                    Session["Success"] = "Successfully Added Employee Insurance Information";
                    return RedirectToAction("Index/");
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
            return View("../PayrollInsurancePremium/Create", data);
        }

        public ActionResult Details(int id)
        {
            PayrollInsurancePremiumDTO Record = new PayrollInsurancePremiumDTO();
            Record = _PayrollInsurancePremiumService.GetOnePayrollInsurancePremium(id);
            ViewBag.EmpCode = Record.EmpCode;
            Record.StartDateNP = NepEngDate.EngToNep(Convert.ToDateTime(Record.StartDate)).ToString().Replace('/', '-');
            Record.EndDateNP = NepEngDate.EngToNep(Convert.ToDateTime(Record.EndDate)).ToString().Replace('/', '-');
            if (Record.AmountType == "A")
            {
                Record.AmountType = "Annual";
            }
            else if (Record.AmountType == "H")
            {
                Record.AmountType = "Half Yearly";
            }
            else if (Record.AmountType == "Q")
            {
                Record.AmountType = "Quarterly";
            }
            else if (Record.AmountType == "M")
            {
                Record.AmountType = "Monthly";
            }
            return View("../PayrollInsurancePremium/Details",Record);
        }

        public ActionResult Update(int id)
        {
            int EmpCode = Convert.ToInt32(Session["Empcode"]);
            PayrollInsurancePremiumDTO Record = new PayrollInsurancePremiumDTO();
            Record = _PayrollInsurancePremiumService.GetOnePayrollInsurancePremium(id);
            Record.InsuranceCompanySelectlist = _PayrollInsurancePremiumService.GetInsuranceCompanySelectList();
            Record.AmountTypeSelectList = StaticSelectList.AmountTypeSelectList();
            ViewBag.ddlEmployeeList = _dynamicSelectList.GetEmployeeByEmpCode(EmpCode).ToList();
            ViewBag.EmpCode = Record.EmpCode;
            Record.StartDateNP = NepEngDate.EngToNep(Convert.ToDateTime(Record.StartDate)).ToString().Replace('/', '-');
            Record.EndDateNP = NepEngDate.EngToNep(Convert.ToDateTime(Record.EndDate)).ToString().Replace('/', '-');
            return View("../PayrollInsurancePremium/Update", Record);
        }

        [HttpPost]
        public ActionResult Update(int id, PayrollInsurancePremiumDTO Record)
        {
            int EmpCode = Convert.ToInt32(Session["Empcode"]);
            Record.IsuranceClaimId = id;
            Record.StartDate = Convert.ToDateTime(NepEngDate.NepToEng(Record.StartDateNP));
            Record.EndDate = Convert.ToDateTime(NepEngDate.NepToEng(Record.EndDateNP));
            Record.InsuranceCompanySelectlist = _PayrollInsurancePremiumService.GetInsuranceCompanySelectList();
            Record.AmountTypeSelectList = StaticSelectList.AmountTypeSelectList();
            ViewBag.ddlEmployeeList = _dynamicSelectList.GetEmployeeByEmpCode(EmpCode).ToList();
            ViewBag.EmpCode = Record.EmpCode;
            try
            {
                if (ModelState.IsValid)
                {
                    _PayrollInsurancePremiumService.UpdatePayrollInsurancePremium(Record);
                    Session["Success"] = "Successfully Updated Insurance Information";
                    return RedirectToAction("Index", "PayrollInsurancePremium", new { id = Record.EmpCode });
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
            return View("../PayrollInsurancePremium/Update", Record);
        }

        public ActionResult Delete(int id)
        {
            PayrollInsurancePremiumDTO Record = new PayrollInsurancePremiumDTO();
            try
            {
                //Record.EmpCode = _PayrollInsurancePremiumService.GetEmpCode(id);
                _PayrollInsurancePremiumService.DeletePayrollInsurancePremium(id);
                Session["Success"] = "Successfully Deleted Insurance Information";

            }
            catch (Exception Exception)
            {

                ViewBag.Error = Exception.Message;
            }
            return RedirectToAction("Index", "PayrollInsurancePremium");
        }
    }
}