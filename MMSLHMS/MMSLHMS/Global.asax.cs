using MMSLHMS.DAL;
using MMSLHMS.DAL.Security;
using HMSBO.Models;
using HMSBO.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace MMSLHMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //Database.SetInitializer<DataContext>(new);
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            //if (HttpContext.Current.Session != null)
            //{
            //    if (HttpContext.Current.Session["UserName"] != null && HttpContext.Current.Session["UserDetail"] != null)
            //    {
            //        CustomPrincipalSerializeModel serializeModel = (CustomPrincipalSerializeModel)Session["UserDetail"];
            //        CustomPrincipal newUser = new CustomPrincipal(Session["UserName"].ToString());
            //        newUser.UserId = serializeModel.UserId;
            //        newUser.FirstName = serializeModel.FirstName;
            //        newUser.LastName = serializeModel.LastName;
            //        newUser.roles = serializeModel.roles;
            //        HttpContext.Current.User = newUser;
            //    }
            //}

            //--------Block Code------------//
            //HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            //if (authCookie != null)
            //{
            //    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            //    CustomPrincipalSerializeModel serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);
            //    CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
            //    newUser.UserId = serializeModel.UserId;
            //    newUser.FirstName = serializeModel.FirstName;
            //    newUser.LastName = serializeModel.LastName;
            //    newUser.roles = serializeModel.roles;
            //    HttpContext.Current.User = newUser; // Very Important Line
            //}
        }

        protected void Application_AcquireRequestState(Object sender, EventArgs e)
        {
            if (HttpContext.Current.Session != null)
            {
                if (HttpContext.Current.Session["UserName"] != null && HttpContext.Current.Session["UserDetail"] != null)
                {
                    CustomPrincipalSerializeModel serializeModel = (CustomPrincipalSerializeModel)Session["UserDetail"];
                    CustomPrincipal newUser = new CustomPrincipal(Session["UserName"].ToString());
                    newUser.UserId = serializeModel.UserId;
                    newUser.FirstName = serializeModel.FirstName;
                    newUser.LastName = serializeModel.LastName;
                    newUser.OrgName = serializeModel.OrgName;
                    newUser.UserName = serializeModel.UserName;
                    newUser.OrgId = serializeModel.OrgId;
                    newUser.LogInTime = serializeModel.LogInTime;
                    newUser.MacID = serializeModel.MacID;
                    //newUser.roles = serializeModel.roles;
                    newUser.RoleId = serializeModel.RoleId;
                    newUser.RoleName = serializeModel.RoleName;
                    newUser.IsRoleActive = serializeModel.IsRoleActive;
                    newUser.OrgLogo = serializeModel.OrgLogo;
                    newUser.HeaderLogo = serializeModel.HeaderLogo;
                    newUser.LogoPaths = serializeModel.LogoPaths;
                    newUser.Address = serializeModel.Address;
                    newUser.Telephone = serializeModel.Telephone;
                    newUser.MobileNo = serializeModel.MobileNo;
                    newUser.Fax = serializeModel.Fax;
                    HttpContext.Current.User = newUser;
                }
            }

        }

        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }

        //protected void Application_EndRequest()
        //{
        //   Exception [] exceptions = this.Context.AllErrors;
        //    if (exceptions != null )
        //    {
        //        if (exceptions.Length > 0)
        //        {
        //            string s = exceptions[0].Message;
        //        }
        //    }
        //}
    }
}
