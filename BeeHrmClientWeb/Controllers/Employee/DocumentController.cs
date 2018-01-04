using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.Repository;
using BeeHrmClientWeb.CodeBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Employee
{
    public class DocumentController : BaseController
    {
        private IEmployeeDocumentService _employeeDocumentService;
        private IDocumentCategoryService _documentCategoryService;
        private IEmployeeService _empDetails;
        private IModulServices _moduleService;
        private dbBeeHRMEntities db = new dbBeeHRMEntities();

        public DocumentController()
        {
            _employeeDocumentService = new EmployeeDocumentService();
            _documentCategoryService = new DocumentCategoryService();
            _empDetails = new EmployeeService();
            _moduleService = new ModuleService();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();
        }

        //[Route("employees/document/{empcode}")]
        public ActionResult Index(int id)
        {
            try
            {
                ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
                EmployeeDocumentInformation Record = new EmployeeDocumentInformation();
                Record.EmployeeDocumentList = new List<EmployeeDocumentDTO>();
                Record.EmployeeDocumentList = _employeeDocumentService.GetEmployeeByEmpCode(id);
                Record.EmpId = id;
                return View("../Employee/Document/Index", Record);
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        //[Route("Document/Add/{id}")]
        public ActionResult Add(int Id)
        {
            ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(Id);
            EmployeeDocumentDTO Record = new EmployeeDocumentDTO();
            Record.DocumentCategorySelectlist = _employeeDocumentService.GetDocumentCategorySelectList();
            Record.DocumentEmpCode = Id;
            return View("../Employee/Document/Add",Record);
        }

        [HttpPost]
        public ActionResult Add(int id, EmployeeDocumentDTO data, HttpPostedFileBase file)
        {
            data.DocumentEmpCode = id;
            data.DocumentCategorySelectlist = _employeeDocumentService.GetDocumentCategorySelectList();
            ModelState.Clear();
            try
            {
                
                if (ModelState.IsValid)
                {
                    if (file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~\\Uploads\\") + data.DocumentEmpCode);
                        var savepath = Path.Combine(Server.MapPath("~\\Uploads\\")+ data.DocumentEmpCode, fileName);
                        Directory.CreateDirectory(path);
                        file.SaveAs(savepath);
                        data.DocumentTitle = fileName;
                    }
                    data.DocumentCreatedAt = DateTime.Now;
                    data.DocumentOnlyAdmin = true;
                    data.DocumentVerified = true;
                    _employeeDocumentService.InsertEmployeeDocument(data);
                    Session["Success"] = "Successfully Added Employee Document";
                    return RedirectToAction("Index/"+ id);
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
            return View("../Employee/Document/Add", data);
        }

        public ActionResult ViewDocument(int id) {
            var data = db.EmployeeDocuments.Find(id);
            var path = Path.Combine(Server.MapPath("~\\Uploads\\"+ data.DocumentEmpCode+"\\") + data.DocumentTitle);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = Path.GetFileName(data.DocumentTitle);
            string Extension = Path.GetExtension(data.DocumentTitle);
            fileName = fileName + Extension;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public ActionResult Update(int id)
        {

            EmployeeDocumentDTO Record = new EmployeeDocumentDTO();
            Record = _employeeDocumentService.GetOneEmployeeDocument(id);
            Record.DocumentCategorySelectlist = _employeeDocumentService.GetDocumentCategorySelectList();
            return View("../Employee/Document/Update", Record);
        }

        [HttpPost]
        public ActionResult Update(int id, EmployeeDocumentDTO Record)
        {
            Record.DocumentCategorySelectlist = _employeeDocumentService.GetDocumentCategorySelectList();
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeDocumentService.UpdateEmployeeDocument(Record);
                    Session["Success"] = "Successfully Updated Employee Document";
                    return RedirectToAction("Index", "Document", new { id = Record.DocumentEmpCode });
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
            return View("../Employee/Document/Update", Record);
        }

        public ActionResult Delete(int id)
        {
            //ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
            EmployeeDocumentDTO Record = new EmployeeDocumentDTO();
            try
            {
                Record.DocumentEmpCode = _employeeDocumentService.GetEmpCode(id);
                _employeeDocumentService.DeleteDocument(id);
                Session["Success"] = "Successfully Deleted Employee Document";

            }
            catch (Exception Exception)
            {

                ViewBag.Error = Exception.Message;
            }
            return RedirectToAction("Index", "Document", new { id = Record.DocumentEmpCode });
        }
    }
}