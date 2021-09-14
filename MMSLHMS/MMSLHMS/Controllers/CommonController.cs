using HMSBO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMSBO.ViewModels;
using MMSLHMS.DAL;
using MMSLHMS.DAL.Security;
using MMSLHMS.CustomFiles.Filters;
using HMSBLL;

namespace MMSLHMS.Controllers
{
    [CustomAuthorize]
    public class CommonController : BaseController
    {
        // GET: Common
        DataContext db = new DataContext();
        public ActionResult MenuItem()
        {
            //CustomPrincipalSerializeModel serializeModel = (CustomPrincipalSerializeModel)Session["UserDetail"];
            var mItem = new List<VmMenu>();
            // || (serializeModel.IsRoleActive && serializeModel.RoleName =="Admin")
            if (User.RoleName == UserType.SystemAdmin && User.OrgId == 1) // Here 1 = MM Services Limited 
            {
                var menuData = db.tblMainMenus.ToList();
                foreach (var m in menuData)
                {
                    var menu = new VmMenu();
                    List<VmSubMenu> subMenus = new List<VmSubMenu>();
                    var submenuList = db.tblSubmenus.Where(s => s.MainMenuId == m.MMId && s.IsShow).ToList();
                    foreach (var s in submenuList)
                    {
                        var submenu = new VmSubMenu();
                        submenu.SubMenuName = s.SubMenuName;
                        submenu.ControllerName = s.ControllName;
                        submenu.ActionName = s.ActionName;
                        submenu.Area = s.Area;
                        subMenus.Add(submenu);
                    }
                    if (subMenus.Count > 0)
                    {
                        menu.MenuName = m.MenuName;
                        menu.SubMenus = subMenus;
                        mItem.Add(menu);
                    }
                }
            }
            else
            {
                if (!User.IsRoleActive)
                {
                    var userMenu = db.tblUserAuthorization.Where(u => u.UserId == User.UserId);
                    List<int> menuId = new List<int>();
                    List<int> submenuId = new List<int>();
                    foreach (var m in userMenu)
                    {
                        var menuInDb = db.tblMainMenus.FirstOrDefault(mm => mm.MMId == m.MainmenuId);
                        if (menuInDb != null)
                        {
                            menuId.Add(menuInDb.MMId);
                            // submenu checking
                            var submenuInDb = db.tblSubmenus.FirstOrDefault(sbm => sbm.SubMenuId == m.SubmenuId && sbm.MainMenuId == menuInDb.MMId && sbm.IsShow);
                            if (submenuInDb != null)
                            {
                                submenuId.Add(submenuInDb.SubMenuId);
                            }
                        }
                    }
                    var distinctMenuItem = (from mmItem in db.tblMainMenus
                                            where menuId.Contains(mmItem.MMId)
                                            select mmItem).Distinct();

                    var distinctSubMenuItem = from sbItem in db.tblSubmenus
                                              where submenuId.Contains(sbItem.SubMenuId)
                                              select sbItem;


                    foreach (var mitem in distinctMenuItem)
                    {
                        var submenu = from submenuData in distinctSubMenuItem
                                      where submenuData.MainMenuId == mitem.MMId
                                      select submenuData;
                        var menu = new VmMenu();
                        List<VmSubMenu> subMenus = new List<VmSubMenu>();

                        foreach (var s in submenu)
                        {
                            var vmsubmenu = new VmSubMenu();
                            vmsubmenu.SubMenuName = s.SubMenuName;
                            vmsubmenu.ControllerName = s.ControllName;
                            vmsubmenu.ActionName = s.ActionName;
                            vmsubmenu.Area = s.Area;
                            subMenus.Add(vmsubmenu);
                        }

                        if (subMenus.Count > 0)
                        {
                            menu.MenuName = mitem.MenuName;
                            menu.SubMenus = subMenus;
                            mItem.Add(menu);
                        }
                    }
                }

                else if (User.IsRoleActive)
                {
                    var userMenu = db.tblRoleWiseUserMenu.Where(u => u.RoleId == User.RoleId);
                    List<int> menuId = new List<int>();
                    List<int> submenuId = new List<int>();
                    foreach (var m in userMenu)
                    {
                        var menuInDb = db.tblMainMenus.FirstOrDefault(mm => mm.MMId == m.MainmenuId);
                        if (menuInDb != null)
                        {
                            menuId.Add(menuInDb.MMId);
                            // submenu checking
                            var submenuInDb = db.tblSubmenus.FirstOrDefault(sbm => sbm.SubMenuId == m.SubmenuId && sbm.MainMenuId == menuInDb.MMId && sbm.IsShow);
                            if (submenuInDb != null)
                            {
                                submenuId.Add(submenuInDb.SubMenuId);
                            }
                        }
                    }
                    var distinctMenuItem = (from mmItem in db.tblMainMenus
                                            where menuId.Contains(mmItem.MMId)
                                            select mmItem).Distinct();

                    var distinctSubMenuItem = from sbItem in db.tblSubmenus
                                              where submenuId.Contains(sbItem.SubMenuId)
                                              select sbItem;

                    foreach (var mitem in distinctMenuItem)
                    {
                        var submenu = from submenuData in distinctSubMenuItem
                                      where submenuData.MainMenuId == mitem.MMId
                                      select submenuData;
                        var menu = new VmMenu();
                        List<VmSubMenu> subMenus = new List<VmSubMenu>();

                        foreach (var s in submenu)
                        {
                            var vmsubmenu = new VmSubMenu();
                            vmsubmenu.SubMenuName = s.SubMenuName;
                            vmsubmenu.ControllerName = s.ControllName;
                            vmsubmenu.ActionName = s.ActionName;
                            vmsubmenu.Area = s.Area;
                            subMenus.Add(vmsubmenu);
                        }
                        if (subMenus.Count > 0)
                        {
                            menu.MenuName = mitem.MenuName;
                            menu.SubMenus = subMenus;
                            mItem.Add(menu);
                        }
                    }
                }
            }
            return PartialView("_navbar", mItem);
            //return Json(mItem,JsonRequestBehavior.AllowGet);
        }

        public ActionResult TopBar()
        {
            return PartialView("_topbar");
        }

        public JsonResult GetUserInfo(int id)
        {
            //var user = db.tblUsers.FirstOrDefault(u => u.UserId == id);
            var user = from u in db.tblUsers
                       join org in db.tblOrganizations
                       on u.OrgId equals org.OrgId
                       where u.UserId == id
                       select new { UserId = u.UserId, UserName = u.UserName, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email, IsActive = u.IsActive, OrgId = org.OrgId, OrgName = org.Name };
            return Json(user, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllMenuItem()
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
            return Json(modulesItem, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllMenuItemForUser(int id)
        {
            List<VmUserModule> modulesItem = new List<VmUserModule>();
            var modules = db.tblModules.ToList();

            foreach (var md in modules)
            {
                VmUserModule module = new VmUserModule();
                module.ModuleId = md.MId;
                module.ModuleName = md.ModuleName;

                List<VmUserMenu> vmMenus = new List<VmUserMenu>();
                var mainmenu = db.tblMainMenus.Where(mm => mm.ModuleID == md.MId).ToList();

                foreach (var mm in mainmenu)
                {
                    VmUserMenu vmUserMenu = new VmUserMenu();
                    vmUserMenu.MenuId = mm.MMId;
                    vmUserMenu.MenuName = mm.MenuName;

                    var submenu = db.tblSubmenus.Where(sm => sm.MainMenuId == mm.MMId).ToList();
                    List<VmUserSubmenu> vmSubMenus = new List<VmUserSubmenu>();

                    foreach (var s in submenu)
                    {
                        VmUserSubmenu vmSubMenu = new VmUserSubmenu();
                        vmSubMenu.SubmenuId = s.SubMenuId;
                        vmSubMenu.SubMenuName = s.SubMenuName;
                        var checkSubmenuItem = db.tblUserAuthorization.FirstOrDefault(ss => ss.SubmenuId == s.SubMenuId && ss.UserId == id);
                        if (checkSubmenuItem == null)
                        {
                            vmSubMenu.HasUser = false;
                            vmSubMenu.Add = false;
                            vmSubMenu.Edit = false;
                            vmSubMenu.Delete = false;
                            vmSubMenu.Approval = false;
                            vmSubMenu.Report = false;
                        }
                        else
                        {
                            vmSubMenu.HasUser = true;
                            vmSubMenu.Add = checkSubmenuItem.Add;
                            vmSubMenu.Edit = checkSubmenuItem.Edit;
                            vmSubMenu.Delete = checkSubmenuItem.Delete;
                            vmSubMenu.Approval = checkSubmenuItem.Approval;
                            vmSubMenu.Report = checkSubmenuItem.Report;

                        }
                        vmSubMenus.Add(vmSubMenu);
                    }
                    vmUserMenu.SubMenus = vmSubMenus;
                    vmMenus.Add(vmUserMenu);
                }
                module.Menus = vmMenus;

                modulesItem.Add(module);
            }
            return Json(modulesItem, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllMenuItemForRole(int roleId)
        {

            List<VmRoleModule> modulesItem = new List<VmRoleModule>();
            var modules = db.tblModules.ToList();

            foreach (var md in modules)
            {
                VmRoleModule module = new VmRoleModule();
                module.ModuleId = md.MId;
                module.ModuleName = md.ModuleName;

                List<VmRoleMenu> vmMenus = new List<VmRoleMenu>();
                var mainmenu = db.tblMainMenus.Where(mm => mm.ModuleID == md.MId).ToList();

                foreach (var mm in mainmenu)
                {
                    VmRoleMenu vmUserMenu = new VmRoleMenu();
                    vmUserMenu.MenuId = mm.MMId;
                    vmUserMenu.MenuName = mm.MenuName;

                    var submenu = db.tblSubmenus.Where(sm => sm.MainMenuId == mm.MMId).ToList();
                    List<VmRoleSubmenu> vmSubMenus = new List<VmRoleSubmenu>();

                    foreach (var s in submenu)
                    {
                        VmRoleSubmenu vmSubMenu = new VmRoleSubmenu();
                        vmSubMenu.SubmenuId = s.SubMenuId;
                        vmSubMenu.SubMenuName = s.SubMenuName;
                        var checkSubmenuItem = db.tblRoleWiseUserMenu.FirstOrDefault(ss => ss.SubmenuId == s.SubMenuId && ss.RoleId == roleId);
                        if (checkSubmenuItem == null)
                        {
                            vmSubMenu.HasRole = false;
                            vmSubMenu.Add = false;
                            vmSubMenu.Edit = false;
                            vmSubMenu.Delete = false;
                            vmSubMenu.Approval = false;
                            vmSubMenu.Report = false;
                        }
                        else
                        {
                            vmSubMenu.HasRole = true;
                            vmSubMenu.Add = checkSubmenuItem.Add;
                            vmSubMenu.Edit = checkSubmenuItem.Edit;
                            vmSubMenu.Delete = checkSubmenuItem.Delete;
                            vmSubMenu.Approval = checkSubmenuItem.Approval;
                            vmSubMenu.Report = checkSubmenuItem.Report;

                        }
                        vmSubMenus.Add(vmSubMenu);
                    }
                    vmUserMenu.SubMenus = vmSubMenus;
                    vmMenus.Add(vmUserMenu);
                }
                module.Menus = vmMenus;

                modulesItem.Add(module);
            }
            return Json(modulesItem, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllMenuItemForUserByOrg(int id)
        {
            var org = (from u in db.tblUsers
                      join o in db.tblOrganizations on u.OrgId equals o.OrgId
                      where u.UserId == id
                      select new { Id = o.OrgId, OrgName = o.Name, u.UserId, u.UserName }).FirstOrDefault();
                     
            
            if (UserType.SystemAdmin == User.RoleName && org.OrgName.Contains("MM Services Limited"))
            {
                List<VmUserModule> modulesItem = new List<VmUserModule>();
                var modules = db.tblModules.ToList();

                foreach (var md in modules)
                {
                    VmUserModule module = new VmUserModule();
                    module.ModuleId = md.MId;
                    module.ModuleName = md.ModuleName;

                    List<VmUserMenu> vmMenus = new List<VmUserMenu>();
                    var mainmenu = db.tblMainMenus.Where(mm => mm.ModuleID == md.MId).ToList();

                    foreach (var mm in mainmenu)
                    {
                        VmUserMenu vmUserMenu = new VmUserMenu();
                        vmUserMenu.MenuId = mm.MMId;
                        vmUserMenu.MenuName = mm.MenuName;

                        var submenu = db.tblSubmenus.Where(sm => sm.MainMenuId == mm.MMId).ToList();
                        List<VmUserSubmenu> vmSubMenus = new List<VmUserSubmenu>();

                        foreach (var s in submenu)
                        {
                            VmUserSubmenu vmSubMenu = new VmUserSubmenu();
                            vmSubMenu.SubmenuId = s.SubMenuId;
                            vmSubMenu.SubMenuName = s.SubMenuName;
                            var checkSubmenuItem = db.tblUserAuthorization.FirstOrDefault(ss => ss.SubmenuId == s.SubMenuId && ss.UserId == org.UserId);
                            if (checkSubmenuItem == null)
                            {
                                vmSubMenu.HasUser = false;
                                vmSubMenu.Add = false;
                                vmSubMenu.Edit = false;
                                vmSubMenu.Delete = false;
                                vmSubMenu.Approval = false;
                                vmSubMenu.Report = false;
                            }
                            else
                            {
                                vmSubMenu.HasUser = true;
                                vmSubMenu.Add = checkSubmenuItem.Add;
                                vmSubMenu.Edit = checkSubmenuItem.Edit;
                                vmSubMenu.Delete = checkSubmenuItem.Delete;
                                vmSubMenu.Approval = checkSubmenuItem.Approval;
                                vmSubMenu.Report = checkSubmenuItem.Report;

                            }
                            vmSubMenus.Add(vmSubMenu);
                        }
                        vmUserMenu.SubMenus = vmSubMenus;
                        vmMenus.Add(vmUserMenu);
                    }
                    module.Menus = vmMenus;

                    modulesItem.Add(module);
                }
                return Json(modulesItem, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var orgAuthDatas = from oa in db.tblOrgAuthorization
                                   where oa.OrgId == org.Id
                                   select oa;
                var moduleIds = (from oa in orgAuthDatas select oa.ModuleId).Distinct().ToList();
                var mainmenuIds = (from oa in orgAuthDatas select oa.MainmenuId).Distinct().ToList();
                var submenuIds = (from oa in orgAuthDatas select oa.SubmenuId).Distinct().ToList();
                List<VmUserModule> modulesItem = new List<VmUserModule>();
                //db.tblModules.ToList();
                var modules = from md in db.tblModules
                              where moduleIds.Contains(md.MId)
                              select md;
                foreach (var md in modules)
                {
                    VmUserModule module = new VmUserModule();
                    module.ModuleId = md.MId;
                    module.ModuleName = md.ModuleName;

                    List<VmUserMenu> vmMenus = new List<VmUserMenu>();

                    var mainmenu = db.tblMainMenus.Where(mm => mm.ModuleID == md.MId && mainmenuIds.Contains(mm.MMId)).ToList();

                    foreach (var mm in mainmenu)
                    {
                        VmUserMenu vmUserMenu = new VmUserMenu();
                        vmUserMenu.MenuId = mm.MMId;
                        vmUserMenu.MenuName = mm.MenuName;

                        var submenu = db.tblSubmenus.Where(sm => sm.MainMenuId == mm.MMId && submenuIds.Contains(sm.SubMenuId)).ToList();
                        List<VmUserSubmenu> vmSubMenus = new List<VmUserSubmenu>();

                        foreach (var s in submenu)
                        {
                            VmUserSubmenu vmSubMenu = new VmUserSubmenu();
                            vmSubMenu.SubmenuId = s.SubMenuId;
                            vmSubMenu.SubMenuName = s.SubMenuName;
                            var checkSubmenuItem = db.tblUserAuthorization.FirstOrDefault(ss => ss.SubmenuId == s.SubMenuId && ss.UserId == org.UserId);
                            if (checkSubmenuItem == null)
                            {
                                vmSubMenu.HasUser = false;
                                vmSubMenu.Add = false;
                                vmSubMenu.Edit = false;
                                vmSubMenu.Delete = false;
                                vmSubMenu.Approval = false;
                                vmSubMenu.Report = false;
                            }
                            else
                            {
                                vmSubMenu.HasUser = true;
                                vmSubMenu.Add = checkSubmenuItem.Add;
                                vmSubMenu.Edit = checkSubmenuItem.Edit;
                                vmSubMenu.Delete = checkSubmenuItem.Delete;
                                vmSubMenu.Approval = checkSubmenuItem.Approval;
                                vmSubMenu.Report = checkSubmenuItem.Report;

                            }
                            vmSubMenus.Add(vmSubMenu);
                        }
                        vmUserMenu.SubMenus = vmSubMenus;
                        vmMenus.Add(vmUserMenu);
                    }
                    module.Menus = vmMenus;

                    modulesItem.Add(module);
                }
                return Json(modulesItem, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetAllMenuItemForRoleByOrg(int roleId,int OrgId)
        {
            var Org = (from or in db.tblOrganizations
                       where or.OrgId == OrgId
                       select or).FirstOrDefault();
            List<VmRoleModule> modulesItem = new List<VmRoleModule>();

            if (Org.Name.Contains("MM Services"))
            {
                var modules = db.tblModules.ToList();
                foreach (var md in modules)
                {
                    VmRoleModule module = new VmRoleModule();
                    module.ModuleId = md.MId;
                    module.ModuleName = md.ModuleName;

                    List<VmRoleMenu> vmMenus = new List<VmRoleMenu>();
                    var mainmenu = db.tblMainMenus.Where(mm => mm.ModuleID == md.MId).ToList();

                    foreach (var mm in mainmenu)
                    {
                        VmRoleMenu vmUserMenu = new VmRoleMenu();
                        vmUserMenu.MenuId = mm.MMId;
                        vmUserMenu.MenuName = mm.MenuName;

                        var submenu = db.tblSubmenus.Where(sm => sm.MainMenuId == mm.MMId).ToList();
                        List<VmRoleSubmenu> vmSubMenus = new List<VmRoleSubmenu>();

                        foreach (var s in submenu)
                        {
                            VmRoleSubmenu vmSubMenu = new VmRoleSubmenu();
                            vmSubMenu.SubmenuId = s.SubMenuId;
                            vmSubMenu.SubMenuName = s.SubMenuName;
                            var checkSubmenuItem = db.tblRoleWiseUserMenu.FirstOrDefault(ss => ss.SubmenuId == s.SubMenuId && ss.RoleId == roleId);
                            if (checkSubmenuItem == null)
                            {
                                vmSubMenu.HasRole = false;
                                vmSubMenu.Add = false;
                                vmSubMenu.Edit = false;
                                vmSubMenu.Delete = false;
                                vmSubMenu.Approval = false;
                                vmSubMenu.Report = false;
                            }
                            else
                            {
                                vmSubMenu.HasRole = true;
                                vmSubMenu.Add = checkSubmenuItem.Add;
                                vmSubMenu.Edit = checkSubmenuItem.Edit;
                                vmSubMenu.Delete = checkSubmenuItem.Delete;
                                vmSubMenu.Approval = checkSubmenuItem.Approval;
                                vmSubMenu.Report = checkSubmenuItem.Report;

                            }
                            vmSubMenus.Add(vmSubMenu);
                        }
                        vmUserMenu.SubMenus = vmSubMenus;
                        vmMenus.Add(vmUserMenu);
                    }
                    module.Menus = vmMenus;

                    modulesItem.Add(module);
                }

            }
            else if (!Org.Name.Contains("MM Services"))
            {
                var orgAuthDatas = from oa in db.tblOrgAuthorization
                                   where oa.OrgId == Org.OrgId
                                   select oa;

                var moduleIds = (from oa in orgAuthDatas select oa.ModuleId).Distinct().ToList();
                var mainmenuIds = (from oa in orgAuthDatas select oa.MainmenuId).Distinct().ToList();
                var submenuIds = (from oa in orgAuthDatas select oa.SubmenuId).Distinct().ToList();

                var modules = db.tblModules.Where(mod=> moduleIds.Contains(mod.MId)).ToList();

                foreach (var md in modules)
                {
                    VmRoleModule module = new VmRoleModule();
                    module.ModuleId = md.MId;
                    module.ModuleName = md.ModuleName;

                    List<VmRoleMenu> vmMenus = new List<VmRoleMenu>();
                    var mainmenu = db.tblMainMenus.Where(mm => mm.ModuleID == md.MId && mainmenuIds.Contains(mm.MMId)).ToList();

                    foreach (var mm in mainmenu)
                    {
                        VmRoleMenu vmUserMenu = new VmRoleMenu();
                        vmUserMenu.MenuId = mm.MMId;
                        vmUserMenu.MenuName = mm.MenuName;

                        var submenu = db.tblSubmenus.Where(sm => sm.MainMenuId == mm.MMId && submenuIds.Contains(sm.SubMenuId)).ToList();
                        List<VmRoleSubmenu> vmSubMenus = new List<VmRoleSubmenu>();

                        foreach (var s in submenu)
                        {
                            VmRoleSubmenu vmSubMenu = new VmRoleSubmenu();
                            vmSubMenu.SubmenuId = s.SubMenuId;
                            vmSubMenu.SubMenuName = s.SubMenuName;
                            var checkSubmenuItem = db.tblRoleWiseUserMenu.FirstOrDefault(ss => ss.SubmenuId == s.SubMenuId && ss.RoleId == roleId && ss.OrgId == Org.OrgId);
                            if (checkSubmenuItem == null)
                            {
                                vmSubMenu.HasRole = false;
                                vmSubMenu.Add = false;
                                vmSubMenu.Edit = false;
                                vmSubMenu.Delete = false;
                                vmSubMenu.Approval = false;
                                vmSubMenu.Report = false;
                            }
                            else
                            {
                                vmSubMenu.HasRole = true;
                                vmSubMenu.Add = checkSubmenuItem.Add;
                                vmSubMenu.Edit = checkSubmenuItem.Edit;
                                vmSubMenu.Delete = checkSubmenuItem.Delete;
                                vmSubMenu.Approval = checkSubmenuItem.Approval;
                                vmSubMenu.Report = checkSubmenuItem.Report;

                            }
                            vmSubMenus.Add(vmSubMenu);
                        }
                        vmUserMenu.SubMenus = vmSubMenus;
                        vmMenus.Add(vmUserMenu);
                    }
                    module.Menus = vmMenus;

                    modulesItem.Add(module);
                }
            }
            return Json(modulesItem, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public JsonResult GetDepartmentItems(string contextKey)
        {
            var deptItems = db.tblDepartment.OrderBy(d => d.DepartmentName).Select(d => new { d.DepartmentName }).Distinct().ToList();

            if (contextKey.Trim() != "")
            {
                deptItems = deptItems.Where(d => d.DepartmentName.ToLower().Contains(contextKey.ToLower())).OrderBy(d => d.DepartmentName).Select(d => new { d.DepartmentName }).Distinct().ToList();
            }
            return Json(deptItems, JsonRequestBehavior.AllowGet);
        }

        #region DropDown - Action Methods

        // DDL
        [HttpPost]
        public JsonResult GetModuleList()
        {
            var data = db.tblModules.ToList();
            var data2 = new List<DropDown>();
            foreach (var d in data)
            {
                var obj = new DropDown();
                obj.value = d.MId.ToString();
                obj.text = d.ModuleName;
                data2.Add(obj);
            }
            return Json(data2);
        }

        //DDL
        [HttpPost]
        public JsonResult GetMainMenuList()
        {
            var mainMenus = db.tblMainMenus.ToList();
            var data = new List<DropDown>();
            foreach (var item in mainMenus)
            {
                var module = db.tblModules.Single(m => m.MId == item.ModuleID);
                if (module != null)
                {
                    var obj = new DropDown();
                    string menuName = item.MenuName + " (" + module.ModuleName + ")";
                    obj.value = item.MMId.ToString();
                    obj.text = menuName;
                    data.Add(obj);
                }
            }
            return Json(data);
        }

        //DDL
        [HttpPost]
        public ActionResult GetOrgList()
        {
            List<Organization> organizations = new List<Organization>();
            if (User.RoleName == UserType.SystemAdmin || User.OrgName.Contains("MM Services"))
            {
                organizations = db.tblOrganizations.OrderBy(o=> o.OrgId).ToList();
            }
            else
            {
                organizations = db.tblOrganizations.Where(o=> o.OrgId == User.OrgId).ToList();
            }
            var data = new List<DropDown>();
            foreach (var i in organizations)
            {
                var item = new DropDown();
                item.value = i.OrgId.ToString();
                string activity = i.IsActive == true ? "Active" : "Inactive";
                item.text = i.Name + " (" + activity + ")";
                data.Add(item);
            }
            return Json(data);
        }

        // DDL
        public JsonResult GetRoleList()
        {
            List<Role> roles = new List<Role>();
            if (User.RoleName == UserType.SystemAdmin)
            {
                roles = db.tblRoles.ToList();
            }
            else
            {
                roles = db.tblRoles.Where(r=> !r.RoleName.Contains(UserType.SystemAdmin)).ToList();
            }
            
            var data = new List<DropDown>();
            foreach (var i in roles)
            {
                var item = new DropDown();
                item.value = i.RoleId.ToString();
                item.text = i.RoleName;
                data.Add(item);
            }
            return Json(data);
        }

        public JsonResult GetInvestiongationByOrg()
        {
            var investigation = db.tblInvestigations.Where(inv => inv.OrgId == User.OrgId).Select(inv => new { inv.Id, inv.Name }).OrderBy(inv=> inv.Name).ToList();
            List<DropDown> ddlInvestigation = new List<DropDown>();
            foreach (var item in investigation)
            {
                DropDown dropDown = new DropDown();
                dropDown.text = item.Name;
                dropDown.value = item.Id.ToString();
                ddlInvestigation.Add(dropDown);
            }
            return Json(ddlInvestigation);
        }

        //GetReferrerByOrg
        [HttpPost]
        public JsonResult GetReferrerByOrg()
        {
            var referrer = db.tblInvestigatinReferrer.Where(inv => inv.OrgId == User.OrgId).Select(inv => new { inv.Id, inv.Name }).OrderBy(inv => inv.Name).ToList();
            List<DropDown> ddlReferrer = new List<DropDown>();
            foreach (var item in referrer)
            {
                DropDown dropDown = new DropDown();
                dropDown.text = item.Name;
                dropDown.value = item.Id.ToString();
                ddlReferrer.Add(dropDown);
            }
            return Json(ddlReferrer);
        }

        #endregion

        #region Boolean Checker - Action Methods

        [HttpPost]
        public ActionResult IsEmailExist(int? id, string email, bool isEdit)
        {
            bool isExist = false;
            if ((id == null || id == 0) && isEdit == false)
            {
                var CheckEmail = db.tblUsers.FirstOrDefault(u => u.Email == email.Trim());
                if (CheckEmail != null)
                {
                    isExist = true;
                }
            }
            else if (id > 0 && isEdit == true)
            {
                var CheckEmail = db.tblUsers.FirstOrDefault(u => u.Email == email.Trim() && u.UserId != id);
                if (CheckEmail != null)
                {
                    isExist = true;
                }
            }
            return Json(isExist);
        }

        [HttpPost]
        public ActionResult IsUserExist(string username, int id, bool isEdit)
        {
            bool isExist = true;
            if (id == 0 && isEdit == false)
            {
                var user = db.tblUsers.FirstOrDefault(u => u.UserName == username);
                if (user == null)
                {
                    isExist = false;
                }
            }
            else if (id > 0 && isEdit == true)
            {
                var user = db.tblUsers.FirstOrDefault(u => u.UserName == username && u.UserId != id);
                if (user == null)
                {
                    isExist = false;
                }
            }
            return Json(isExist);
        }

        public JsonResult isValidUser(int id, string username)
        {
            var isValid = true;
            var userId = Convert.ToInt32(id);
            var user = db.tblUsers.FirstOrDefault(u => u.UserId == userId && u.UserName == username);
            if (user == null)
            {
                isValid = false;
            }
            return Json(isValid, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsUserMainItemExist(int id) // userid
        {
            var isUserMainItemExist = false;
            var menuItem = db.tblUserAuthorization.Where(m => m.UserId == id).ToList();
            if (menuItem.Count > 0)
            {
                isUserMainItemExist = true;
            }
            return Json(isUserMainItemExist, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public JsonResult IsUniqueDeptShortName(string deptName, string shortName, int orgId)
        {
            bool IsValid = false;
            if (shortName.Trim() != "" && orgId > 0)
            {
                var deptShortName = db.tblDepartment.FirstOrDefault(d => d.OrgId == orgId && d.ShortName == shortName.Trim());
                if (deptShortName == null)
                {
                    IsValid = true;
                }
                else
                {
                    IsValid = false;
                }
            }
            else if (deptName.Trim() != "" && orgId > 0)
            {
                var deptNameinDb = db.tblDepartment.FirstOrDefault(d => d.OrgId == orgId && d.DepartmentName == deptName.Trim());
                if (deptNameinDb == null)
                {
                    IsValid = true;
                }
                else
                {
                    IsValid = false;
                }
            }
            else if (deptName.Trim() != "" && orgId > 0 && shortName.Trim() != "")
            {
                var deptNameinDb = db.tblDepartment.FirstOrDefault(d => d.OrgId == orgId && d.DepartmentName == deptName.Trim() && d.ShortName == shortName.Trim());
                if (deptNameinDb == null)
                {
                    IsValid = true;
                }
                else
                {
                    IsValid = false;
                }
            }
            return Json(IsValid);
        }

        // Checking Uniqueness of Specialization Type Name By Org.
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public JsonResult IsUniqueSpecializationTypeByOrg(string TypeName, int OrgId, int Id)
        {
            bool IsValid = false;
            if (TypeName.Trim() != "" && Id > 0)
            {
                var spType = db.tblSpecializationType.FirstOrDefault(sp => sp.TypeName == TypeName && sp.OrgId == OrgId && sp.Id != Id);
                if (spType == null)
                {
                    IsValid = true;
                }
            }
            else if (TypeName.Trim() != "" && Id <= 0)
            {
                var spType = db.tblSpecializationType.FirstOrDefault(sp => sp.TypeName == TypeName && sp.OrgId == OrgId);
                if (spType == null)
                {
                    IsValid = true;
                }
            }
            return Json(IsValid);
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult IsDesignationAndShortUnique(int desigId, string Designation, string ShortName)
        {
            bool IsValid = false;
            if (!string.IsNullOrEmpty(Designation.Trim()) && !string.IsNullOrEmpty(ShortName))
            {
                if (desigId <= 0)
                {
                    var desig = db.tblDesignation.FirstOrDefault(d => d.Name == Designation && d.OrgId == User.OrgId);
                    IsValid = (desig == null ? true : false);

                    if (IsValid)
                    {
                        var shortName = db.tblDesignation.FirstOrDefault(d => d.ShortName == ShortName && d.OrgId == User.OrgId);
                        IsValid = (shortName == null ? true : false);
                    }
                }
                else
                {
                    var desig = db.tblDesignation.FirstOrDefault(d => d.Name == Designation && d.OrgId == User.OrgId && d.DesigId != desigId);
                    IsValid = (desig == null ? true : false);

                    if (IsValid)
                    {
                        var shortName = db.tblDesignation.FirstOrDefault(d => d.ShortName == ShortName && d.OrgId == User.OrgId && d.DesigId != desigId);
                        IsValid = (shortName == null ? true : false);
                    }
                }
            }
            return Json(IsValid);
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult IsDoctorLicenseUnique(string LicenseNo, int doctorId)
        {
            bool isValid = true;
            if (doctorId > 0)
            {
                var doctorLicense = db.tblDoctorProfile.FirstOrDefault(d => d.LicenseNo.Trim().ToLower() == LicenseNo.Trim().ToLower() && d.Id != doctorId);
                if (doctorLicense != null)
                {
                    isValid = false;
                }
            }
            else
            {
                var doctorLicense = db.tblDoctorProfile.FirstOrDefault(d => d.LicenseNo.Trim().ToLower() == LicenseNo.Trim().ToLower());
                if (doctorLicense != null)
                {
                    isValid = false;
                }
            }

            return Json(isValid);
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult IsBranchNameExist(int BranchId,string BranchName)
        {
            var branch = db.tblBranchInfo.FirstOrDefault(b => b.BranchId != BranchId && b.BranchName == BranchName);
            var result = (branch == null) ? true : false;
            return Json(result);
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult IsInvestigationExist(int ChartId)
        {
            //bool IsExist = true;
            //var investigation = db.tblInvestigations.FirstOrDefault(f => f.OrgId == User.OrgId && f.Name == InvestigationName && f.ChartId != InvestigationId);
            //if (investigation != null)
            //{
            //    IsExist = false;
            //}
            //return Json(IsExist);

            bool IsExist = false;
            var investigation = db.tblInvestigations.Where(f => f.OrgId == User.OrgId && f.ChartId == ChartId).Count();
            if (investigation == 1)
            {
                IsExist = true;
            }
            return Json(IsExist);
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult IsInvestigationChartIdExistInList(int ChartId)
        {
            bool IsExist = false;
            var investigation = db.tblInvestigations.Where(f => f.OrgId == User.OrgId &&  f.ChartId == ChartId).Count();
            if (investigation > 0)
            {
              IsExist = true;
            }
            return Json(IsExist);
        }

        [HttpPost, ValidateJsonAntiForgeryToken]
        public ActionResult IsInvestigationChartExist(int InvestigationId, string InvestigationName)
        {
            bool IsExist = true;
            var investigation = db.tblInvestigationChart.FirstOrDefault(f => f.InvestigationName == InvestigationName && f.Id != InvestigationId);
            if (investigation != null)
            {
                IsExist = false;
            }
            return Json(IsExist);
        }
        #endregion

        #region Auto Complete TextBox - Action Methods

        // Auto Complete Extender...
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public JsonResult GetSpecializationItems(int? OrgId, string contextKey)
        {
            var speciaItems = db.tblSpecializationType.Where(s => s.OrgId == OrgId).OrderBy(d => d.TypeName).Select(d => new { d.TypeName }).ToList();

            if (contextKey.Trim() != "")
            {
                speciaItems = speciaItems.Where(d => d.TypeName.ToLower().Contains(contextKey.ToLower())).OrderBy(d => d.TypeName).ToList();
            }
            //, JsonRequestBehavior.AllowGet
            return Json(speciaItems);
        }

        // Auto Complete Extender
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult GetDesignationByNames(int OrgId, string contextKey)
        {
            var desigItems = db.tblDesignation.Where(o => o.OrgId == OrgId && o.Name.ToLower().StartsWith(contextKey.ToLower())).Select(d => new { d.Name }).OrderBy(d => d.Name).ToList();
            return Json(desigItems);
        }

        // Auto Complete Extender
        public JsonResult GetUserList(string contextKey)
        {
            //if(User.RoleName ==)
            if (UserType.SystemAdmin == User.RoleName)
            {
                if (contextKey.Trim() != "")
                {
                    var data = db.tblUsers.Where(u => u.UserName.ToLower().Contains(contextKey.ToLower())).Select(s => new
                    {
                        UserId = s.UserId,
                        UserName = s.UserName
                    }).ToList();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = db.tblUsers.Select(s => new
                    {
                        UserId = s.UserId,
                        UserName = s.UserName
                    }).ToList();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var data = db.tblUsers.Where(u => u.UserName.ToLower().Contains(contextKey.ToLower()) && u.OrgId == User.OrgId).Select(s => new
                {
                    UserId = s.UserId,
                    UserName = s.UserName
                }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetEmployees(string contextKey)
        {
            if (User.RoleName == UserType.SystemAdmin)
            {
                var data = (from emp in db.tblEmployeeInfo
                            where !(from us in db.tblUsers select us.EmpId).Contains(emp.EmployeeCode) && emp.IsActive && emp.AllowToLogin
                            select new
                            {
                                EmpName = (emp.FirstName + " " + emp.MiddleName + " " + emp.LastName +" ("+emp.EmployeeCode+")"),
                                EmpId=emp.EmployeeCode
                            }).ToList().Where(emp => emp.EmpName.ToLower().Contains(contextKey.ToLower().Trim())).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = (from emp in db.tblEmployeeInfo
                            where !(from us in db.tblUsers select us.EmpId).Contains(emp.EmployeeCode)
                            && emp.OrgId == User.OrgId && emp.IsActive && emp.AllowToLogin
                            select new
                            {
                                EmpName = (emp.FirstName + " " + emp.MiddleName + " " + emp.LastName + " (" + emp.EmployeeCode + ")"),
                                EmpId = emp.EmployeeCode
                            }).ToList().Where(emp => emp.EmpName.ToLower().Contains(contextKey.ToLower())).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            
        }

        // Auto Complete Extender
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult GetInvestigationNameByOrg(int OrgId, string contextKey)
        {
            var invesItems = db.tblInvestigations.Where(o => o.OrgId == OrgId && (contextKey == null || contextKey.Trim() == "" || o.Name.ToLower().Contains(contextKey.Trim().ToLower()) )).Select(d => new { d.Id,d.Name,d.Fee,d.RefFee }).OrderBy(d => d.Name).ToList();
            return Json(invesItems);
        }

        // Auto Complete Extender
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult GetReferrerName(int OrgId, string contextKey)
        {
            var invesRefItems = db.tblInvestigatinReferrer.Where(o => o.OrgId == OrgId && (contextKey == null || contextKey.Trim() == "" || o.Name.ToLower().Contains(contextKey.Trim().ToLower()))).Select(d => new { d.Id, d.Name}).OrderBy(d => d.Name).ToList();
            return Json(invesRefItems);
        }

        // Auto Complete Extender
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult GetInvestigationChart(string contextKey="")
        {
            if (!string.IsNullOrEmpty(contextKey.Trim()))
            {
                contextKey = contextKey.Trim();
            }
            var data = db.tblInvestigationChart.Where(c => contextKey == null || contextKey == "" || c.InvestigationName.ToLower().Contains(contextKey.ToLower())).Select(c => new { Id = c.Id, Name = c.InvestigationName }).ToList();
            return Json(data);
        }

        #endregion

        #region -Controller : Organization  
        [HttpGet]
        public ActionResult GetBranchById(int Id)
        {
            var branch = db.tblBranchInfo.Find(Id);
            if (branch != null)
            {
                return Json(branch, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(branch, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region - Base64 Image String
        [HttpPost,ValidateJsonAntiForgeryToken]
        public ActionResult GetImageBase64String(int orgId)
        {
            string orgLogo = string.Empty;
            string reportLogo = string.Empty;
            try
            {
                if(orgId > 0)
                {
                    var org = db.tblOrganizations.FirstOrDefault(o => o.OrgId == orgId);
                    if (org != null)
                    {
                        if (!string.IsNullOrEmpty(org.OrgLogoPath))
                        {
                            orgLogo = Utility.GetImage(string.Format(@"{0}", org.OrgLogoPath));
                        }
                        if (!string.IsNullOrEmpty(org.ReportLogoPath))
                        {
                            reportLogo = Utility.GetImage(string.Format(@"{0}", org.ReportLogoPath));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(new { orgLogo = orgLogo, reportLogo = reportLogo });
        }
        #endregion
    }
}