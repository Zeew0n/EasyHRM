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

namespace BeeHrmClientWeb.Controllers
{
    public class HRController : BaseController
    {
        // GET: HR
        private IModulServices _moduleService;

        public HRController()
        {
            _moduleService = new ModuleService();
            

        }

        protected override void Initialize(RequestContext requestContext)
        {

            base.Initialize(requestContext);
            chk();
            GetLoginInfo();
            GetUserAccess();

        }
        [Route("hr")]
        public ActionResult Index()
        {
          
            int roleid = Convert.ToInt32(Session["RoleId"].ToString());

          
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();


            int ParentId = _moduleService.GetParentId(controllerName);

            IEnumerable<ModuleDTOs> ChildList = _moduleService.GetChildMenuList(ParentId, roleid);


            return View("../Configurations/index", ChildList);

           
        }

        public ActionResult chk()
        {
            int roleid = Convert.ToInt32(Session["RoleId"].ToString());
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            int ParentId = _moduleService.GetParentId(controllerName);
            return RedirectToAction("index", "Login");
           
        }

       
    }
}