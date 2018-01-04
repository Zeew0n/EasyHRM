using BeeHrmClientWeb.CodeBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BeeHrmClientWeb.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Errors

    
        public ActionResult NotFound()
        {

            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View();

        }

        public ActionResult ExceptionError(string aspxerrorpath)
        {


            return View();
        }


    }
}