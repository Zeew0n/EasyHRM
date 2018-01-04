using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeeHRM.ApplicationService;
using BeeHRM.Repository;
using BeeHrmClientWeb.Models;
using BeeHrmClientWeb.CodeBase;

namespace BeeHrmClientWeb.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult Index()
        {
            return View("_partialloginpage");
        }
    }
}