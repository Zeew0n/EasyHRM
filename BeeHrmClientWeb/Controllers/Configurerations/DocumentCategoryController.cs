using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.Repository;
using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers.Configurerations
{
    public class DocumentCategoryController : BaseController
    {
        private IDocumentCategoryService _documentCategoryService;
        private IModulServices _moduleService;

        private dbBeeHRMEntities db = new dbBeeHRMEntities();
        public DocumentCategoryController()
        {
            _documentCategoryService = new DocumentCategoryService();
            _moduleService = new ModuleService();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();
        }



        // GET: Skills
        [Route("configurations/documentcategory")]
        public ActionResult Index()
        {
            IEnumerable<DocumentCategoryDTO> list = _documentCategoryService.GetAllDocumentCategory();
            return View (list);
        }

        //// GET: Skills/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Skill skill = db.Skills.Find(id);
        //    if (skill == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(skill);
        //}

        // GET: Skills/Create
        [Route("configurations/documentcategory/create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Skills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("configurations/documentcategory/create")]
        [MultipleButton(Name = "action", Argument = "Create")]
        public ActionResult Create([Bind(Include = "CatId,CatTitle")] DocumentCategory documentCategory)
        {
            if (ModelState.IsValid)
            {
                db.DocumentCategories.Add(documentCategory);
                db.SaveChanges();
                Session["Success"]= "Document Category Created Succesfully";
                return RedirectToAction("Index");
            }

            return View("DocumentCategory/Create");
        }

        // GET: Skills/Edit/5
        [HttpGet]
        [Route("configurations/documentcategory/edit/{id}")]
        public ActionResult Edit(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //DocumentCategory documentCategory = db.DocumentCategories.Find(id);

            //if (documentCategory == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(documentCategory);

            DocumentCategoryDTO Record = new DocumentCategoryDTO();
            Record = _documentCategoryService.GetDocumentCategoryById(id);
            Record.CatId = id;
            return View("../DocumentCategory/Edit", Record);
        }

        // POST: Skills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("configurations/documentcategory/edit/{id}")]
        public ActionResult Edit([Bind(Include = "CatId,CatTitle")] DocumentCategoryDTO Record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _documentCategoryService.UpdateDocumentCategory(Record);
                    Session["Success"] = "Successfully Updated Employee Document";
                    return RedirectToAction("Index", "DocumentCategory");
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
            return View("../DocumentCategory/Edit", Record);
        }

        public ActionResult Delete(int id)
        {
            //ViewBag.SideBar = _moduleService.AdminEmployeeDetailsMenu(id);
            DocumentCategoryDTO Record = new DocumentCategoryDTO();
            try
            {
                //Record.DocumentEmpCode = _employeeDocumentService.GetEmpCode(id);
                _documentCategoryService.DeleteDocumentCategory(id);
                Session["Success"] = "Successfully Deleted Employee Document";

            }
            catch (Exception Exception)
            {

                ViewBag.Error = Exception.Message;
            }
            return RedirectToAction("Index", "DocumentCategory");
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