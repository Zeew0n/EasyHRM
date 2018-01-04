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

namespace BeeHrmClientWeb.Controllers.Reports
{
    public class EmployeeReportsController : BaseController
    {

        private IModulServices _moduleService;

        public EmployeeReportsController()
        {
            _moduleService = new ModuleService();


        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            GetLoginInfo();
            GetUserAccess();

        }

        [Route("Reports")]
        public ActionResult Index()
        {
            int roleid = Convert.ToInt32(Session["RoleId"].ToString());


            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();


            int ParentId = _moduleService.GetParentId(controllerName);

            IEnumerable<ModuleDTOs> ChildList = _moduleService.GetChildMenuList(ParentId, roleid);


            return View("../Configurations/index", ChildList);


        }
    }
}