using BeeHrmClientWeb.CodeBase;
using BeeHrmClientWeb.Controllers;
using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BeeHrmClientWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            MiniProfilerEF6.Initialize();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
        protected void Application_BeginRequest()
        {
            if (Request.IsLocal)
            {
                MiniProfiler.Start();
            }
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }


        protected void Application_EndRequest()
        {
            if (Context.Response.StatusCode == 404)
            {
                Response.Clear();

                var rd = new RouteData();
                rd.DataTokens["area"] = "AreaName"; // In case controller is in another area
                rd.Values["controller"] = "Errors";
                rd.Values["action"] = "NotFound";

                IController c = new ErrorsController();
                c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Response.Clear();

            var rd = new RouteData();
            rd.DataTokens["area"] = "AreaName"; // In case controller is in another area
            rd.Values["controller"] = "Errors";
            rd.Values["action"] = "ExceptionError";
            IController c = new ErrorsController();
            c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));

        }
        }
}
