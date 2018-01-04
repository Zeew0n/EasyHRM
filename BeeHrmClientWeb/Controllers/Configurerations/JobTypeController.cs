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

namespace BeeHrmClientWeb.Controllers.Configurerations
{
    public class JobTypeController : BaseController
    {
        // GET: JobType
        #region interfaces

        private IJobTypeService _jobTypeService;
        

        #endregion
        public JobTypeController()
        {
            _jobTypeService = new JobTypeService();
       
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            ViewBag.Empcode = this.Session["EmpCode"];
            ViewBag.RoleId = this.Session["RoleId"];
            GetLoginInfo();
        }
        #region Job Type
        [Route("JobType")]
        public ActionResult JobType()
        {
            //if (!ViewBag.AllowView)
            //{
            //    ViewBag.Error = "You are not Authorize to use this Page";
            //    return PartialView("_partialviewNotFound");
            //}


            IEnumerable<JobTypeDTO> list = _jobTypeService.GetJobTypeList();
            return View(list);
        }
        [Route("JobType/Create")]
        public ActionResult JobTypeCreate()
        {
            return View();
        }
        [HttpPost]
        [Route("JobType/Create")]
        [MultipleButton(Name = "action", Argument = "Create")]
        public ActionResult JobTypeCreate(JobTypeDTO data)
        {
            JobTypeDTO jtd = new JobTypeDTO();
            if (!ModelState.IsValid)
            {
                return View(jtd);
            }
            JobTypeDTO res = new JobTypeDTO();
            res = _jobTypeService.InsertJobType(data);
            ViewBag.Success = "Job type " + res.JobTypeName + " has been created";
            ModelState.Clear();
            return View();
        }
        [HttpPost]
        [Route("JobType/Create")]
        [MultipleButton(Name = "action", Argument = "CreateClose")]
        public ActionResult JobTypeCreateClose(JobTypeDTO data)
        {
            JobTypeDTO jtd = new JobTypeDTO();
            if (!ModelState.IsValid)
            {
                return View(jtd);
            }
            JobTypeDTO res = new JobTypeDTO();
            res = _jobTypeService.InsertJobType(data);
            ViewBag.Success = "Job type " + res.JobTypeName + " has been created";
            return RedirectToAction("JobType");
        }
        [HttpGet]
        [Route("JobType/Edit/{id}")]
        public ActionResult JobTypeEdit(int id)
        {
            JobTypeDTO res = new JobTypeDTO();
            res = _jobTypeService.GetJobTypeId(id);
            return View(res);
        }

        [HttpPost]
        [Route("JobType/Edit/{id}")]
        public ActionResult JobTypeEdit(JobTypeDTO data)
        {
            JobTypeDTO jtd = new JobTypeDTO();
            if (!ModelState.IsValid)
            {
                return RedirectToAction("JobTypeEdit", jtd);
            }
            int res = _jobTypeService.UpdateJobType(data);
            return RedirectToAction("JobType");
        }
        [Route("JobType/Delete/{id}")]
        public RedirectResult JobTypeDelete(int id)
        {
            _jobTypeService.DeleteJobTypeById(id);
            return Redirect("/JobType");
        }
        #endregion
    }
}