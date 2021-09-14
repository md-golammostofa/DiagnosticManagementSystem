using MMSLHMS.CustomFiles.Filters;
using MMSLHMS.DAL;
using MMSLHMS.DAL.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HMSBO.Models;
using HMSBO.ViewModels;

namespace MMSLHMS.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        DataContext db = new DataContext();
        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult Default()
        {
            return View();
        }

        private string GetMacID()
        {
            string mac = "";
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up && (!nic.Description.Contains("Virtual") && !nic.Description.Contains("Pseudo")))
                {
                    if (nic.GetPhysicalAddress().ToString() != "")
                    {
                        mac = nic.GetPhysicalAddress().ToString();
                    }
                }
            }
            return mac;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LoginViewModel loginViewModel, string ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {
                string password = Utility.Encrypt(loginViewModel.Password);
                var user = db.tblUsers.FirstOrDefault(u => u.UserName == loginViewModel.Username && u.Password == password);

                if (user != null)
                {
                    //var roles = user.Roles.Select(m => m.RoleName).ToArray();
                    string dbRoleName = string.Empty;
                    try
                    {
                        if (user.IsActive == true)
                        {
                            var org = db.tblOrganizations.SingleOrDefault(o => o.OrgId == user.OrgId);

                            if (org != null)
                            {
                                if (org.IsActive == true)
                                {                                    
                                    string[] LogoPaths = new string[2];
                                    CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                                    serializeModel.UserId = user.UserId;
                                    serializeModel.FirstName = user.FirstName;
                                    serializeModel.LastName = user.LastName;
                                    serializeModel.OrgName = org.Name;
                                    serializeModel.OrgId = org.OrgId;
                                    serializeModel.UserName = user.UserName;
                                    serializeModel.LogInTime = DateTime.Now;
                                    serializeModel.MacID = GetMacID();
                                    serializeModel.RoleId = user.RoleId;

                                    serializeModel.Address = org.Address;
                                    serializeModel.Telephone = org.Telephone;
                                    serializeModel.MobileNo = org.MobileNumber;
                                    serializeModel.Fax = org.Fax;

                                    serializeModel.IsRoleActive = user.IsRoleActive;

                                    LogoPaths[0] = org.OrgLogoPath == null ? "": org.OrgLogoPath;
                                    LogoPaths[1] = org.ReportLogoPath == null ?"": org.ReportLogoPath;

                                    serializeModel.LogoPaths = LogoPaths;

                                    serializeModel.OrgLogo = Utility.GetImage(org.OrgLogoPath);
                                    serializeModel.HeaderLogo = Utility.GetImage(org.ReportLogoPath);
                                    string[] roleArray = new string[2] ;
                                    if (user.RoleId != null)
                                    {
                                        var roleName = db.tblRoles.SingleOrDefault(r => r.RoleId == user.RoleId).RoleName.ToString();
                                        dbRoleName = roleName;
                                        if (!string.IsNullOrEmpty(roleName))
                                        {
                                            serializeModel.RoleName = dbRoleName;
                                        }
                                        roleArray[0] = dbRoleName;
                                    }
                                    serializeModel.roles = roleArray;

                                    Session["UserName"] = user.UserName;
                                    Session["UserDetail"] = serializeModel;

                                    Session["SubmenuList"] = db.tblSubmenus.ToList();
                                    if (user.IsRoleActive)
                                    {
                                        if (user.RoleId > 0 && user.RoleId != null)
                                        {
                                            Session["UserRoleMenu"] = db.tblRoleWiseUserMenu.Where(r => r.RoleId == user.RoleId).ToList();
                                        }
                                    }
                                    else
                                    {
                                        Session["UserCustomMenu"] = db.tblUserAuthorization.Where(u => u.UserId == user.UserId).ToList();
                                    }
                                    //string userData = JsonConvert.SerializeObject(serializeModel);
                                    //FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1,user.Email,DateTime.Now,DateTime.Now.AddMinutes(15),false,userData);
                                    //string encTicket = FormsAuthentication.Encrypt(authTicket);
                                    //HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                                    //Response.Cookies.Add(cookie);
                                    //string sId= Session.SessionID;
                                    
                                    if (user.IsActive && org.IsActive && org.Name == "PlayOn24 Limited")
                                    {
                                        return RedirectToAction("Index", "Admin");
                                    }
                                    else if (user.IsActive && org.IsActive && org.Name != "MM Services Limited")
                                    {
                                        return RedirectToAction("Index", "User");
                                    }
                                }
                                else
                                {
                                    ModelState.AddModelError("", "The Organization that you are belongs to is inactive");
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "The Organization that you are belongs to is invalid");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "The User is inactive");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception ( " Db Role= "+ dbRoleName + "; RoleActive= "+ user.IsRoleActive.ToString() +"; RoleId= "+user.RoleId.ToString()+"; Message= " + ex.Message + "; StackTrace= " + ex.StackTrace + "; Source" + ex.Source );
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect Username/Password");
                }
            }
            return View();
        }

        public ActionResult Token()
        {
            return PartialView("_token");
        }

        [CustomAuthorize]
        public ActionResult LogOut()
        {
            Session["UserName"] = null;
            Session["UserDetail"] = null;
            Session["SubmenuList"] = null;
            if (User.IsRoleActive)
            {
                Session["UserRoleMenu"] = null;
            }
            else
            {
                Session["UserCustomMenu"] = null;
            }
            //System.Web.HttpContext.Current.Session.Clear();
            System.Web.HttpContext.Current.Session.Abandon();
            //System.Web.HttpContext.Current.Session.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn", "Account", null);
        }

        //Change user password
        [CustomAuthorize]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            User user = db.tblUsers.FirstOrDefault(u => u.UserId == User.UserId);
            vmChangePassword vwModel = new vmChangePassword() { UserId = user.UserId, UserName = user.UserName };
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(vwModel);
        }


        [HttpPost, CustomAuthorize, ValidateInput(true), ValidateJsonAntiForgeryToken]
        public ActionResult ChangePassword(vmChangePassword vwModel)
        {
            bool IsSuccess = false;
            if (ModelState.IsValid)
            {
                string password = Utility.Encrypt(vwModel.Password);
                var user = db.tblUsers.Where(u => u.UserId == User.UserId && u.UserName == vwModel.UserName && u.Password == password).FirstOrDefault();
                if (user != null)
                {
                    if (vwModel.NewPassword == vwModel.ConfirmPassword)
                    {
                        user.Password = Utility.Encrypt(vwModel.NewPassword);
                        db.SaveChanges();
                        IsSuccess = true;
                    }
                }
            }
            return Json(IsSuccess);
        }
    }

}