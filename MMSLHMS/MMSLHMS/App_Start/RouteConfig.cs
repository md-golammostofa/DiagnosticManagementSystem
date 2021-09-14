using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MMSLHMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //var queryString= HttpContext.Current.Request.QueryString;
            //routes.MapRoute(name: "Default",
            //   url: "{controller}/{action}/{OrgId}/{DeptName}",
            //   defaults: new
            //   {
            //       controller = "Organization",
            //       action = "GetDepartmentList"
            //   });
            //routes.MapRoute(
            //   name: "Default",
            //   url: "{controller}/{action}/{OrgId}/{DeptName}",
            //   defaults: new { controller = "Organization", action = "GetDepartmentList", id = UrlParameter.Optional }
               
            //   );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "LogIn", id = UrlParameter.Optional });
        }
    }
}
