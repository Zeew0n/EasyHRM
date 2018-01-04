using BeeHrmClientWeb.CodeBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeeHrmClientWeb.Controllers
{
    public class ParentModuleController : BaseController
    {
        // GET: ParentModule
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
       // [Route("Configurations/GetModelByParentModel/{Id}")]
        public ActionResult GetModelByParentModel(int Id)
        {

            return null;
        }



    }
}