using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using HMSBLL;
using MMSLHMS.DAL;
using HMSBO.Models;
using HMSBO.ViewModels;
using System.Web.Caching;

namespace MMSLHMS.DAL.Security
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public string UserConfigKey { get; set; }
        public string RoleConfigKey { get; set; }

        protected virtual CustomPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as CustomPrincipal; }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["UserName"] != null)
            {
                if (filterContext.HttpContext.Request.IsAuthenticated)
                {
                    var authorizedUsers = ConfigurationManager.AppSettings[UserConfigKey];
                    var authorizedRoles = ConfigurationManager.AppSettings[RoleConfigKey];

                    Users = String.IsNullOrEmpty(Users) ? authorizedUsers : Users;
                    Roles = String.IsNullOrEmpty(Roles) ? authorizedRoles : Roles;

                    DataContext db = new DataContext();
                    CustomPrincipalSerializeModel User = (CustomPrincipalSerializeModel)HttpContext.Current.Session["UserDetail"];
                    string action = filterContext.RouteData.Values["action"].ToString();
                    string controller = filterContext.RouteData.Values["controller"].ToString();
                   
                    if ((action == "Index" && controller == "Admin") || (action == "Index" && controller == "User"))
                    {
                        if (action == "Index" && controller == "Admin")
                        {
                            if (User.RoleName == UserType.Admin || User.RoleName == UserType.SystemAdmin)
                            {
                                //
                            }
                            else
                            {
                                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                            }
                        }
                        else 
                        {
                            
                            if (User.RoleName == UserType.Admin || User.RoleName == UserType.SystemAdmin)
                            {
                                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                            }
                            
                        }
                    }
                    
                    else if (controller == "Common" || User.RoleName == UserType.SystemAdmin)
                    {
                        // Nothing to do...
                    }

                    //else if (User.IsRoleActive == true)
                    //{
                    //    var submenu = db.tblSubmenus.FirstOrDefault(sb => sb.ActionName == action && sb.ControllName == controller);

                    //    if (submenu != null)
                    //    {
                    //        var authSubmenu = db.tblRoleWiseUserMenu.FirstOrDefault(rm => rm.SubmenuId == submenu.SubMenuId && rm.RoleId == User.RoleId);
                    //        if (authSubmenu == null)
                    //        {
                    //            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                    //        }
                    //    }
                    //    //else
                    //    //{
                    //    //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                    //    //}
                    //}

                    else if (User.IsRoleActive == true)
                    {
                        //var submenu = db.tblSubmenus.FirstOrDefault(sb => sb.ActionName == action && sb.ControllName == controller);
                        var submenuList = (List<Submenu>)HttpContext.Current.Session["SubmenuList"];
                         var submenu = submenuList.FirstOrDefault(sb => sb.ActionName == action && sb.ControllName == controller);

                        if (submenu != null)
                        {
                            var roleMenu = (List<RoleWiseUserMenu>) HttpContext.Current.Session["UserRoleMenu"];
                            var authSubmenu = roleMenu.FirstOrDefault(rm => rm.SubmenuId == submenu.SubMenuId && rm.RoleId == User.RoleId);

                            if (authSubmenu == null)
                            {
                                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                            }
                        }
                        //else
                        //{
                        //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                        //}

                    }

                    //else
                    //{
                    //    var submenu = db.tblSubmenus.FirstOrDefault(sb => sb.ActionName == action && sb.ControllName == controller);
                    //    if (submenu != null)
                    //    {
                    //        var authSubmenu = db.tblUserAuthorization.FirstOrDefault(ua => ua.SubmenuId == submenu.SubMenuId && ua.UserId == User.UserId);
                    //        if (authSubmenu == null)
                    //        {
                    //            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                    //        }
                    //    }
                    //    //else
                    //    //{
                    //    //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                    //    //}
                    //}

                    else
                    {
                        var submenuList = (List<Submenu>)HttpContext.Current.Session["SubmenuList"];
                        var submenu = submenuList.FirstOrDefault(sb => sb.ActionName == action && sb.ControllName == controller);
                        if (submenu != null)
                        {
                            var userMenu = (List<UserAuthorization>)HttpContext.Current.Session["UserCustomMenu"];
                            var authSubmenu = userMenu.FirstOrDefault(ua => ua.SubmenuId == submenu.SubMenuId && ua.UserId == User.UserId);
                            if (authSubmenu == null)
                            {
                                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                            }
                        }
                        //else
                        //{
                        //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
                        //}
                    }
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary(new { controller = "Account", action = "LogIn" }));
            }

        }
    }
}