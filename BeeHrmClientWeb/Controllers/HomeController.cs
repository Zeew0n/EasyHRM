using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Implementations;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.ApplicationService.ViewModel;
using BeeHrmClientWeb.CodeBase;

namespace BeeHrmClientWeb.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        private IModulServices _moduleService;

        public HomeController()
        {
        }
        public ActionResult Index()

        {
            _moduleService = new ModuleService();
            //ViewBag.TopMenuList = _moduleService.GetModuleParents(0);
            //ViewBag.SideBar = _moduleService.GetModuleParents(2);
            return View();
        }
   
        [HttpGet]
        public ActionResult AddEmployee()
        {                  
                    return View();
               
         }
    }
}