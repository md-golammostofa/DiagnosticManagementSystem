using MMSLHMS.DAL;
using MMSLHMS.DAL.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MMSLHMS.CustomFiles.Filters;
using HMSBLL;
using HMSBO.ViewModels;
using HMSBO.Models;

namespace MMSLHMS.Controllers
{
    [CustomAuthorize]
    public class AdminController : BaseController
    {
        // GET: Admin
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            var Modules = db.tblModules.ToList();
            return View(Modules);
        }

        public ActionResult Home()
        {
            return View();   
        }

        [HttpGet]
        [ActionName("ModuleList")]
        public ActionResult ModuleList()
        {
            var modules = db.tblModules.ToList();
            return View(modules);
        }

        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult SaveModule(vmModule data)
        {
            bool IsSuccess = false;
            if (ModelState.IsValid)
            {
                Module module = new Module();
                if (data.ModuleId == 0)
                {
                    module.ModuleName = data.ModuleName;
                    module.IconName = data.IconName;
                    module.IconColor = data.IconColor;
                    module.EntryUser = User.UserName;
                    module.EntryDate = DateTime.Now;
                    module.ApprovedBy = User.UserName;
                    module.ApprovedDate = DateTime.Now;
                    db.tblModules.Add(module);
                    IsSuccess = true;
                }
                else
                {
                    var dbmodule = db.tblModules.SingleOrDefault(s => s.MId == data.ModuleId);
                    if(dbmodule != null)
                    {
                        dbmodule.ModuleName = data.ModuleName;
                        dbmodule.IconName = data.IconName;
                        dbmodule.IconColor = data.IconColor;
                        IsSuccess = true;
                    }
                }
                db.SaveChanges();
            }
            return Json(IsSuccess);
        }

        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult DeleteModule(int? id)
        {
            bool IsSuccess = false;
            if (id != null)
            {
                var module = db.tblModules.FirstOrDefault(m => m.MId == id);
                if (module != null)
                {
                    db.tblModules.Remove(module);
                    db.SaveChanges();
                    IsSuccess = true;
                }
            }
            return Json(IsSuccess);
        }

        [HttpGet]
        [ActionName("MainMenuList")]
        public ActionResult MainMenuList()
        {
            var MainMenuList = db.tblMainMenus.ToList();
            List<VmMainMenu> LstvmMainMenus = new List<VmMainMenu>();
            foreach (var item in MainMenuList)
            {
                VmMainMenu vmMainMenu = new VmMainMenu();
                vmMainMenu.MMId = item.MMId;
                vmMainMenu.MenuName = item.MenuName;
                var module = db.tblModules.FirstOrDefault(m=> m.MId == item.ModuleID);
                vmMainMenu.ModuleId = module.MId;
                vmMainMenu.ModuleName = module.ModuleName;
                LstvmMainMenus.Add(vmMainMenu);
            }
            return View(LstvmMainMenus);
        }

        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult SaveMainMenu(VmMainMenu vmMainMenu)
        {
            bool IsSuccess = false;
            if (ModelState.IsValid)
            {
                if (vmMainMenu.MMId == 0)
                {
                    MainMenu mainMenu = new MainMenu();
                    mainMenu.MenuName = vmMainMenu.MenuName;
                    mainMenu.ModuleID = vmMainMenu.ModuleId;
                    mainMenu.EntryUser = User.UserName;
                    mainMenu.EntryDate = DateTime.Now;
                    mainMenu.ApprovedUser = User.UserName;
                    mainMenu.ApprovedDate = DateTime.Now;
                    db.tblMainMenus.Add(mainMenu);
                    IsSuccess = true;
                }
                else
                {
                    var dbMainMenu = db.tblMainMenus.FirstOrDefault(m => m.MMId == vmMainMenu.MMId);
                    if (dbMainMenu != null)
                    {
                        dbMainMenu.MenuName = vmMainMenu.MenuName;
                        dbMainMenu.ModuleID = vmMainMenu.ModuleId;
                        IsSuccess = true;
                    }
                }
                db.SaveChanges();
            }
            return Json(IsSuccess);
        }

        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult DeleteMainMenu(int? id)
        {
            bool IsSuccess = false;
            if (id != null)
            {
                var menuItem = db.tblMainMenus.FirstOrDefault(m => m.MMId == id);
                if (menuItem != null)
                {
                    db.tblMainMenus.Remove(menuItem);
                    db.SaveChanges();
                    IsSuccess = true;
                }              
            }
            return Json(IsSuccess);
        }

        [HttpGet]
        public ActionResult SubMenuList()
        {
            var subMenuList = db.tblSubmenus.ToList();
            List<VmSubmenu> vmSubmenusList = new List<VmSubmenu>();
            foreach (var submenu in subMenuList)
            {
                var menuItem = db.tblMainMenus.SingleOrDefault(m => m.MMId == submenu.MainMenuId);
                if (menuItem == null)
                { return HttpNotFound(); }
                else
                {
                    var moduleItem = db.tblModules.SingleOrDefault(m => m.MId == menuItem.ModuleID);
                    if (moduleItem == null)
                    { return HttpNotFound(); }
                    else
                    {
                        VmSubmenu vmSubmenu = new VmSubmenu();
                        vmSubmenu.SubMenuId = submenu.SubMenuId;
                        vmSubmenu.SubMenuName = submenu.SubMenuName;
                        vmSubmenu.ControllerName = submenu.ControllName;
                        vmSubmenu.ActionName = submenu.ActionName;
                        vmSubmenu.EntryUser = submenu.EntryUser;
                        vmSubmenu.EntryDate = submenu.EntryDate;
                        vmSubmenu.MainMenuId = menuItem.MMId;
                        vmSubmenu.MainMenuName = menuItem.MenuName;
                        vmSubmenu.ModuleId = moduleItem.MId;
                        vmSubmenu.ModuleName = moduleItem.ModuleName;
                        vmSubmenu.IsShow = submenu.IsShow;
                        vmSubmenusList.Add(vmSubmenu);
                    }
                }
            }
            return View(vmSubmenusList);
        }

        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult SaveSubmenu(VmSubmenu vmSubmenu)
        {
            bool IsSuccess = false;
            if (ModelState.IsValid) {
                Submenu submenu = new Submenu();
                if (vmSubmenu.SubMenuId == 0) {
                    submenu.SubMenuName = vmSubmenu.SubMenuName;
                    submenu.ControllName = vmSubmenu.ControllerName;
                    submenu.ActionName = vmSubmenu.ActionName;
                    submenu.MainMenuId = vmSubmenu.MainMenuId;
                    submenu.EntryUser = User.UserName;
                    submenu.EntryDate = DateTime.Now;
                    submenu.ApprovedUser = User.UserName;
                    submenu.ApprovedDate = DateTime.Now;
                    submenu.IsShow = vmSubmenu.IsShow;
                    db.tblSubmenus.Add(submenu);
                    IsSuccess = true;
                }
                else
                {
                    var submenuItem = db.tblSubmenus.FirstOrDefault(s => s.SubMenuId == vmSubmenu.SubMenuId);
                    if (submenuItem != null)
                    {
                        submenuItem.SubMenuName = vmSubmenu.SubMenuName;
                        submenuItem.ControllName = vmSubmenu.ControllerName;
                        submenuItem.ActionName = vmSubmenu.ActionName;
                        submenuItem.MainMenuId = vmSubmenu.MainMenuId;
                        submenuItem.IsShow = vmSubmenu.IsShow;
                        IsSuccess = true;
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                db.SaveChanges();
            }
            return Json(IsSuccess);
        }

        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult DeleteSubmenu(int? id)
        {
            bool IsSuccess = false;
            if (id != null) {
                var submenuItem = db.tblSubmenus.FirstOrDefault(s => s.SubMenuId == id);
                if (submenuItem != null)
                {
                    db.tblSubmenus.Remove(submenuItem);
                    db.SaveChanges();
                    IsSuccess = true;
                }
                else
                {
                    return HttpNotFound();
                }
            }
            return Json(IsSuccess);
        }

        [HttpGet]
        public ActionResult GetAllMenuItem()
        {
            List<VmModule> modulesItem = new List<VmModule>();
            var modules = db.tblModules.ToList();

            foreach (var md in modules)
            {
                VmModule module = new VmModule();
                module.ModuleId = md.MId;
                module.ModuleName = md.ModuleName;

                List<VmMenu> vmMenus = new List<VmMenu>();
                var mainmenu = db.tblMainMenus.Where(mm => mm.ModuleID == md.MId).ToList();

                foreach (var mm in mainmenu)
                {
                    VmMenu vmMenu = new VmMenu();
                    vmMenu.MenuId = mm.MMId;
                    vmMenu.MenuName = mm.MenuName;

                    var submenu = db.tblSubmenus.Where(sm => sm.MainMenuId == mm.MMId).ToList();
                    List<VmSubMenu> vmSubMenus = new List<VmSubMenu>();

                    foreach (var s in submenu)
                    {
                        VmSubMenu vmSubMenu = new VmSubMenu();
                        vmSubMenu.SubmenuId = s.SubMenuId;
                        vmSubMenu.SubMenuName = s.SubMenuName;
                        vmSubMenu.ControllerName = s.ControllName;
                        vmSubMenu.ActionName = s.ActionName;
                        vmSubMenu.Area = s.Area;
                        vmSubMenus.Add(vmSubMenu);
                    }
                    vmMenu.SubMenus = vmSubMenus;
                    vmMenus.Add(vmMenu);
                }
                module.Menus = vmMenus;

                modulesItem.Add(module);
            }
            return View(modulesItem);
            //return Json(vm);
        }

        [HttpGet]
        public ActionResult OrganizationList()
        {
            var orgList = db.tblOrganizations.ToList();
            //orgList = new List<Organization>();
            return View(orgList);
        }
        
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult SaveOrg(VmOrganization vmOrg)
        {
            bool IsSuccess= false;
            if (ModelState.IsValid)
            {
                if (vmOrg.OrgId == 0)
                {
                    Organization org = new Organization();
                    org.Name = vmOrg.OrgName;
                    org.Address = vmOrg.Address;
                    org.Telephone = vmOrg.Telephone;
                    org.MobileNumber = vmOrg.MobileNumber;
                    org.Fax = vmOrg.Fax;
                    org.Email = vmOrg.Email;
                    org.IsActive = vmOrg.IsActive;
                    org.EntryDate = DateTime.Now;
                    org.EntryUser = User.UserName;
                    org.ApprovedDate = DateTime.Now;
                    org.ApprovedBy = User.UserName;

                    if (vmOrg.OrgImage != null)
                    {
                        org.OrgLogoPath = Utility.SaveImage(vmOrg.OrgImage.InputStream, vmOrg.OrgImage.FileName, Utility.OrgLogoPath, User.OrgId);
                    }
                    if(vmOrg.ReportImage != null)
                    {
                        org.ReportLogoPath = Utility.SaveImage(vmOrg.ReportImage.InputStream, vmOrg.ReportImage.FileName, Utility.ReportLogoPath, User.OrgId);
                    }

                    db.tblOrganizations.Add(org);
                    IsSuccess = true;
                }
                else if (vmOrg.OrgId > 0)
                {
                    var orgDb = db.tblOrganizations.FirstOrDefault(o => o.OrgId == vmOrg.OrgId);
                    if (orgDb != null) {
                        orgDb.Name = vmOrg.OrgName;
                        orgDb.Address = vmOrg.Address;
                        orgDb.Telephone = vmOrg.Telephone;
                        orgDb.MobileNumber = vmOrg.MobileNumber;
                        orgDb.Fax = vmOrg.Fax;
                        orgDb.Email = vmOrg.Email;
                        orgDb.IsActive = vmOrg.IsActive;
                        if (vmOrg.OrgImage != null)
                        {
                            Utility.DeleteImage(orgDb.OrgLogoPath);
                            orgDb.OrgLogoPath = Utility.SaveImage(vmOrg.OrgImage.InputStream, vmOrg.OrgImage.FileName, Utility.OrgLogoPath, User.OrgId);
                        }
                        if (vmOrg.ReportImage != null)
                        {
                            Utility.DeleteImage(orgDb.ReportLogoPath);
                            orgDb.ReportLogoPath = Utility.SaveImage(vmOrg.ReportImage.InputStream, vmOrg.ReportImage.FileName, Utility.ReportLogoPath, User.OrgId);
                        }
                        IsSuccess = true;
                    }
                }
                db.SaveChanges();
            }
            return Json(IsSuccess);
        }

        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult DeleteOrg(int id)
        {
            bool IsSuccess = false;
            if (id > 0)
            {
                var OrgItem = db.tblOrganizations.FirstOrDefault(o => o.OrgId == id);
                if (OrgItem != null)
                {
                    db.tblOrganizations.Remove(OrgItem);
                    db.SaveChanges();
                    IsSuccess = true;
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return HttpNotFound();
            }
            return Json(IsSuccess);
        }

        [HttpGet]
        public ActionResult UserList()
        {
            List<User> users = new List<User>(); 
            if (User.RoleName == UserType.SystemAdmin)
            {
                users = db.tblUsers.ToList();
            }
            else
            {
                users = db.tblUsers.Where(u => u.OrgId == User.OrgId).ToList();
            }

            List<vmUser> VmUsersList = new List<vmUser>();
            foreach (var u in users)
            {
                vmUser vmUser = new vmUser();
                var org = db.tblOrganizations.FirstOrDefault(o => o.OrgId == u.OrgId);
                var role = db.tblRoles.FirstOrDefault(o => o.RoleId == u.RoleId);
                if (org != null) {
                    vmUser.UserId = u.UserId;
                    vmUser.UserName = u.UserName;
                    vmUser.EmpId = u.EmpId;
                    vmUser.EmployeeName = u.FirstName + " " + u.MiddleName + " " + u.LastName;
                    vmUser.Email = u.Email;
                    vmUser.Password =Utility.Decrypt(u.Password);
                    vmUser.IsActive = u.IsActive;
                    vmUser.OrgId = org.OrgId;
                    vmUser.OrgName = org.Name;
                    vmUser.RoleId = (role == null) ?-1:role.RoleId;
                    vmUser.IsRoleActive = u.IsRoleActive;
                    vmUser.RoleName = (role == null) ? "N/A" : role.RoleName;
                    vmUser.ApprovedDate = u.ApprovedDate;
                    VmUsersList.Add(vmUser);
                }
            }
            return View(VmUsersList);
        }

        //[Authorize]
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult SaveUser(vmUser vmUser)
        {
            bool IsSuccess = false;
            if (ModelState.IsValid)
            {
                var userName = vmUser.UserName.Trim();
                if (vmUser.UserId == 0)
                {
                    var userIndb = db.tblUsers.FirstOrDefault(us => us.UserName == userName);
                    if (userIndb == null)
                    {
                        var user = new User();
                        user.UserName = vmUser.UserName;
                        
                        user.Password = Utility.Encrypt(vmUser.Password);
                        user.OrgId = vmUser.OrgId;
                        user.IsActive = vmUser.IsActive;
                        user.EntryUser = User.UserName;
                        user.EntryDate = DateTime.Now;
                        user.ApprovedBy = User.UserName;
                        user.ApprovedDate = DateTime.Now;
                        user.RoleId = vmUser.RoleId;
                        user.IsRoleActive = vmUser.IsRoleActive;
                        user.FirstName = vmUser.EmployeeName;
                        //var empIdDb = db.tblEmployeeInfo.Find(vmUser.EmpId);
                        //if(empIdDb != null)
                        //{
                        //    user.FirstName = empIdDb.FirstName;
                        //    user.MiddleName = empIdDb.MiddleName;
                        //    user.LastName = empIdDb.LastName;
                        //    user.Email = empIdDb.Email;
                        //}

                        db.tblUsers.Add(user);
                        IsSuccess = true;
                    }
                }
                else if (vmUser.UserId > 0)
                {
                    var UserInDb = db.tblUsers.FirstOrDefault(u => u.UserId == vmUser.UserId);
                    UserInDb.OrgId = vmUser.OrgId;
                    UserInDb.IsActive = vmUser.IsActive;
                    UserInDb.Password = Utility.Encrypt(vmUser.Password);
                    UserInDb.OrgId = vmUser.OrgId;
                    UserInDb.IsRoleActive = vmUser.IsRoleActive;
                    UserInDb.RoleId = vmUser.RoleId;
                    IsSuccess = true;
                }
                db.SaveChanges();
                
            }
            return Json(IsSuccess);
        }

        [HttpGet]
        public ActionResult SetUserMenu()
        {
            return View();
        }

        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult SaveUserMenu(VmSetUserMenu vmSetUserMenu)
        {
            bool IsSuccess = false;
            try
            {
                int id = Convert.ToInt32(vmSetUserMenu.UserId);
                if (vmSetUserMenu.authorizeData != null)
                {
                    // Check User
                    var user = db.tblUsers.FirstOrDefault(u => u.UserId == id);
                    if (user != null)
                    {
                        foreach (VmUserAuthorize ua in vmSetUserMenu.authorizeData)
                        {
                            int ModuleId = Convert.ToInt32(ua.ModuleId);
                            int MenuId = Convert.ToInt32(ua.MenuId);
                            int SubMenuId = Convert.ToInt32(ua.SubMenuId);
                            // Check Module //
                            var module = db.tblModules.FirstOrDefault(m => m.MId == ModuleId);
                            if (module != null)
                            {
                                // Check Mainmenu
                                var MainMenu = db.tblMainMenus.FirstOrDefault(mm => mm.MMId == MenuId && mm.ModuleID == ModuleId);
                                if (MainMenu != null)
                                {
                                    // Check Submenu //
                                    var subMenu = db.tblSubmenus.FirstOrDefault(sm => sm.SubMenuId == SubMenuId && sm.MainMenuId == MenuId);
                                    if (subMenu != null)
                                    {
                                        var userMenu = db.tblUserAuthorization.FirstOrDefault(um => um.SubmenuId == SubMenuId && um.UserId == id);
                                        if (userMenu != null)
                                        {
                                            userMenu.Add = ua.IsAdd;
                                            userMenu.Edit = ua.IsEdit;
                                            userMenu.Delete = ua.IsDelete;
                                            userMenu.Approval = ua.IsApproval;
                                            userMenu.Report = ua.IsReport;
                                            IsSuccess = true;
                                           
                                        }
                                        else
                                        {
                                            // New Menu Entry
                                            UserAuthorization userAuthorization = new UserAuthorization();
                                            userAuthorization.ModuleId = ModuleId;
                                            userAuthorization.MainmenuId = MenuId;
                                            userAuthorization.SubmenuId = SubMenuId;
                                            userAuthorization.UserId = id;
                                            userAuthorization.Add = ua.IsAdd;
                                            userAuthorization.Edit = ua.IsEdit;
                                            userAuthorization.Delete = ua.IsDelete;
                                            userAuthorization.Approval = ua.IsApproval;
                                            userAuthorization.Report = ua.IsReport;
                                            userAuthorization.EntryUser = User.UserName;
                                            userAuthorization.EntryDate = DateTime.Now;
                                            userAuthorization.ApprovedBy = User.UserName;
                                            userAuthorization.ApprovedDate = DateTime.Now;
                                            db.tblUserAuthorization.Add(userAuthorization);
                                            IsSuccess = true;
                                        }
                                    }
                                }
                            }
                        }
                        // Delete Others Menu of the user
                        if (vmSetUserMenu.authorizeData.Count > 0) {
                            int[] submenuIds =new int [vmSetUserMenu.authorizeData.Count];
                            int index = 0;
                            foreach (var item in vmSetUserMenu.authorizeData)
                            {
                                submenuIds[index] = Convert.ToInt32(item.SubMenuId);
                                index+=1;
                            }
                            var otherItem = from sb in db.tblUserAuthorization
                                            where sb.UserId == id && !submenuIds.Contains(sb.SubmenuId)
                                            select sb;

                            foreach (var item in otherItem)
                            {
                                db.tblUserAuthorization.Remove(item);
                            }
                        }
                        db.SaveChanges();
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                else if(id > 0)
                {
                    var userMenu = db.tblUserAuthorization.Where(m => m.UserId == id);
                    foreach (var item in userMenu)
                    {
                        db.tblUserAuthorization.Remove(item);
                    }
                    db.SaveChanges();
                    IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                //result = "Something went wrong";
                //result = ex.Message;
                IsSuccess = false;
            }
            return Json(IsSuccess);
        }

        [HttpGet]
        public ActionResult RoleList() {
            var roles = db.tblRoles.ToList();
            return View(roles);
        }

        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult SaveRole(Role role) {
            bool isSuccess = false;
            if (ModelState.IsValid) {
                if (role.RoleId > 0)
                {
                    var CheckRole = db.tblRoles.FirstOrDefault(r => r.RoleName == role.RoleName && r.RoleId != role.RoleId);
                    if (CheckRole == null) {
                        var roleInDb = db.tblRoles.FirstOrDefault(r => r.RoleId == role.RoleId);
                        if (roleInDb != null) {
                            roleInDb.RoleName = role.RoleName;
                            roleInDb.Description = role.Description;
                            isSuccess = true;
                        }
                    }
                }
                else
                {
                    var newRole = new Role();
                    newRole.RoleName = role.RoleName;
                    newRole.Description = role.Description;
                    newRole.EntryUser = User.UserName;
                    newRole.EntryDate = DateTime.Now;
                    newRole.ApprovedBy = User.UserName;
                    newRole.ApprovedDate = DateTime.Now;
                    db.tblRoles.Add(newRole);
                    isSuccess = true;
                }
                db.SaveChanges();
            }
            return Json(isSuccess);
        }

        [HttpGet]
        public ActionResult RoleWiseUserMenu()
        {
            return View();
        }

        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult SaveRoleMenu(VmSetRoleMenu vmSetRoleMenu)
        {
            string result = "Something went wrong";
            try
            {
                if (vmSetRoleMenu != null)
                {
                    // Check User
                    int id = Convert.ToInt32(vmSetRoleMenu.RoleId);
                    int OrgId = Convert.ToInt32(vmSetRoleMenu.OrgId);
                    var user = db.tblRoles.FirstOrDefault(u => u.RoleId == id);
                    if (user != null)
                    {
                        foreach (VmUserAuthorize ua in vmSetRoleMenu.authorizeData)
                        {
                            int ModuleId = Convert.ToInt32(ua.ModuleId);
                            int MenuId = Convert.ToInt32(ua.MenuId);
                            int SubMenuId = Convert.ToInt32(ua.SubMenuId);
                            // Check Module //
                            var module = db.tblModules.FirstOrDefault(m => m.MId == ModuleId);
                            if (module != null)
                            {
                                // Check Mainmenu
                                var MainMenu = db.tblMainMenus.FirstOrDefault(mm => mm.MMId == MenuId && mm.ModuleID == ModuleId);
                                if (MainMenu != null)
                                {
                                    // Check Submenu //
                                    var subMenu = db.tblSubmenus.FirstOrDefault(sm => sm.SubMenuId == SubMenuId && sm.MainMenuId == MenuId);
                                    if (subMenu != null)
                                    {
                                        var roleMenu = db.tblRoleWiseUserMenu.FirstOrDefault(um => um.SubmenuId == SubMenuId && um.RoleId == id && um.OrgId == OrgId);
                                        if (roleMenu != null)
                                        {
                                            roleMenu.OrgId =OrgId ;
                                            roleMenu.Add = ua.IsAdd;
                                            roleMenu.Edit = ua.IsEdit;
                                            roleMenu.Delete = ua.IsDelete;
                                            roleMenu.Approval = ua.IsApproval;
                                            roleMenu.Report = ua.IsReport;
                                        }
                                        else
                                        {
                                            // New Menu Entry
                                            RoleWiseUserMenu roleWiseUserMenu = new RoleWiseUserMenu();
                                            roleWiseUserMenu.ModuleId = ModuleId;
                                            roleWiseUserMenu.MainmenuId = MenuId;
                                            roleWiseUserMenu.SubmenuId = SubMenuId;
                                            roleWiseUserMenu.RoleId = id;
                                            roleWiseUserMenu.OrgId = OrgId;
                                            roleWiseUserMenu.Add = ua.IsAdd;
                                            roleWiseUserMenu.Edit = ua.IsEdit;
                                            roleWiseUserMenu.Delete = ua.IsDelete;
                                            roleWiseUserMenu.Approval = ua.IsApproval;
                                            roleWiseUserMenu.Report = ua.IsReport;
                                            roleWiseUserMenu.EntryUser = User.UserName;
                                            roleWiseUserMenu.EntryDate = DateTime.Now;
                                            roleWiseUserMenu.ApprovedBy = User.UserName;
                                            roleWiseUserMenu.ApprovedDate = DateTime.Now;
                                            db.tblRoleWiseUserMenu.Add(roleWiseUserMenu);
                                        }
                                    }
                                }
                            }
                        }
                        // Delete Others Menu of the user
                        if (vmSetRoleMenu.authorizeData.Count > 0)
                        {
                            int[] submenuIds = new int[vmSetRoleMenu.authorizeData.Count];
                            int index = 0;
                            foreach (var item in vmSetRoleMenu.authorizeData)
                            {
                                submenuIds[index] = Convert.ToInt32(item.SubMenuId);
                                index += 1;
                            }
                            var otherItem = from rm in db.tblRoleWiseUserMenu
                                            where rm.RoleId == id && !submenuIds.Contains(rm.SubmenuId) && rm.OrgId == OrgId
                                            select rm;

                            foreach (var item in otherItem)
                            {
                                db.tblRoleWiseUserMenu.Remove(item);
                            }
                        }
                        db.SaveChanges();
                        result = "Data has been saved successfully..";
                    }
                }
            }
            catch (Exception ex)
            {
                result = "Something went wrong";
                result = ex.Message;
            }
            return Json(result);
        }

        [HttpGet]
        public ActionResult GetAllMenuItemForOrgAuth()
        {
            List<VmModule> modulesItem = new List<VmModule>();
            var modules = db.tblModules.ToList();

            foreach (var md in modules)
            {
                VmModule module = new VmModule();
                module.ModuleId = md.MId;
                module.ModuleName = md.ModuleName;

                List<VmMenu> vmMenus = new List<VmMenu>();
                var mainmenu = db.tblMainMenus.Where(mm => mm.ModuleID == md.MId).ToList();

                foreach (var mm in mainmenu)
                {
                    VmMenu vmMenu = new VmMenu();
                    vmMenu.MenuId = mm.MMId;
                    vmMenu.MenuName = mm.MenuName;

                    var submenu = db.tblSubmenus.Where(sm => sm.MainMenuId == mm.MMId).ToList();
                    List<VmSubMenu> vmSubMenus = new List<VmSubMenu>();

                    foreach (var s in submenu)
                    {
                        VmSubMenu vmSubMenu = new VmSubMenu();
                        vmSubMenu.SubmenuId = s.SubMenuId;
                        vmSubMenu.SubMenuName = s.SubMenuName;
                        vmSubMenu.ControllerName = s.ControllName;
                        vmSubMenu.ActionName = s.ActionName;
                        vmSubMenu.Area = s.Area;
                        vmSubMenus.Add(vmSubMenu);
                    }
                    vmMenu.SubMenus = vmSubMenus;
                    vmMenus.Add(vmMenu);
                }
                module.Menus = vmMenus;

                modulesItem.Add(module);
            }
            return View(modulesItem);
            //return Json(vm);
        }

        [HttpPost,ValidateJsonAntiForgeryToken]
        public ActionResult GetOrgWiseMenuItem(int? OrgId)
        {
            if (OrgId == null ||OrgId <= 0)
            {
                return Json(new { });
            }
            else
            {
                var orgMenus = db.tblOrgAuthorization.Where(om => om.OrgId == OrgId).Select(om=> new {SubmenuId=om.SubmenuId,MainmenuId=om.MainmenuId,ModuleId=om.ModuleId }).ToList();
                return Json(orgMenus);
            }
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult SaveOrgAuthorization(VmOrgAuth vmOrgAuth)
        {
            bool isSuccess = false;
            if (ModelState.IsValid)
            {
                var orgMenus = db.tblOrgAuthorization.Where(o => o.OrgId == vmOrgAuth.OrgId).ToList();
                if (orgMenus == null)
                {
                    foreach (var item in vmOrgAuth.AuthMenus)
                    {
                        OrgAuthorization orgAuthorization = new OrgAuthorization();
                        orgAuthorization.OrgId = vmOrgAuth.OrgId;
                        orgAuthorization.SubmenuId = item.SubmenuId;
                        orgAuthorization.MainmenuId = item.MainmenuId;
                        orgAuthorization.ModuleId = item.ModuleId;
                        orgAuthorization.EntryUser = User.UserName;
                        orgAuthorization.EntryDate = DateTime.Now;
                        db.tblOrgAuthorization.Add(orgAuthorization);
                        isSuccess = true;
                    }
                }
                else
                {
                    foreach (var item in vmOrgAuth.AuthMenus)
                    {
                        var o = (from om in orgMenus
                                where om.SubmenuId == item.SubmenuId
                                select om).FirstOrDefault();

                        if (o == null)
                        {
                            OrgAuthorization orgAuthorization = new OrgAuthorization();
                            orgAuthorization.OrgId = vmOrgAuth.OrgId;
                            orgAuthorization.SubmenuId = item.SubmenuId;
                            orgAuthorization.MainmenuId = item.MainmenuId;
                            orgAuthorization.ModuleId = item.ModuleId;
                            orgAuthorization.EntryUser = User.UserName;
                            orgAuthorization.EntryDate = DateTime.Now;
                            db.tblOrgAuthorization.Add(orgAuthorization);
                            isSuccess = true;
                        }
                    }

                    var deleteOauth = from oauth in orgMenus
                              where !(from oo in vmOrgAuth.AuthMenus select oo.SubmenuId).Contains(oauth.SubmenuId)
                              select oauth;

                    foreach (var item in deleteOauth)
                    {
                        db.tblOrgAuthorization.Remove(item);
                    }
                }
                db.SaveChanges();
            }
            return Json(isSuccess);
        }
    }
}