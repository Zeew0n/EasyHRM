using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.Repository;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Configurerations
{
    public class InsuranceCompanyController : BaseController
    {
        // GET: InsuranceCompany
        private IInsuranceCompanyService _InsuranceCompanyService;
        private IModulServices _moduleService;

        private dbBeeHRMEntities db = new dbBeeHRMEntities();
        public InsuranceCompanyController()
        {
            _InsuranceCompanyService = new InsuranceCompanyService();
            _moduleService = new ModuleService();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();
        }

        // GET: InsuranceCompany
        public ActionResult Index()
        {
            IEnumerable<InsuranceCompanyDTO> list = _InsuranceCompanyService.GetAllInsuranceCompany();
            return View(list);
        }

        
        // GET: InsuranceCompany/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InsuranceCompany/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InsuranceCompany InsuranceCompany)
        {
            if (ModelState.IsValid)
            {
                db.InsuranceCompanies.Add(InsuranceCompany);
                db.SaveChanges();
                Session["Success"] = "Insurance Company Created Succesfully";
                return RedirectToAction("Index");
            }

            return View("InsuranceCompany/Create");
        }

        // GET: InsuranceCompany/Edit/5
        [HttpGet]
        //[Route("configurations/InsuranceCompany/edit/{id}")]
        public ActionResult Edit(int id)
        {
            InsuranceCompanyDTO Record = new InsuranceCompanyDTO();
            Record = _InsuranceCompanyService.GetInsuranceCompanyById(id);
            Record.Id = id;
            return View("../InsuranceCompany/Edit", Record);
        }

        // POST: InsuranceCompany/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InsuranceCompanyDTO Record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _InsuranceCompanyService.UpdateInsuranceCompany(Record);
                    Session["Success"] = "Successfully Updated Insurance Company";
                    return RedirectToAction("Index", "InsuranceCompany");
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
            return View("../InsuranceCompany/Edit", Record);
        }

        //public ActionResult Delete(int id)
        //{
        //    //ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
        //    InsuranceCompanyDTO Record = new InsuranceCompanyDTO();
        //    try
        //    {
        //        //Record.DocumentEmpCode = _employeeDocumentService.GetEmpCode(id);
        //        _InsuranceCompanyService.DeleteInsuranceCompany(id);
        //        Session["Success"] = "Successfully Deleted Insurance Company";

        //    }
        //    catch (Exception Exception)
        //    {

        //        ViewBag.Error = Exception.Message;
        //    }
        //    return RedirectToAction("Index", "InsuranceCompany");
        //}

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