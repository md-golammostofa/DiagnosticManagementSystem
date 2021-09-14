using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMSBO.ViewModels
{
    public class VmUserModule
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public List<VmUserMenu> Menus { get; set; }
    }
    public class VmUserMenu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public List<VmUserSubmenu> SubMenus { get; set; }
    }

    public class VmUserSubmenu
    {
        public int SubmenuId { get; set; }
        public string SubMenuName { get; set; }
        public bool HasUser { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Approval { get; set; }
        public bool Report { get; set; }
    }
}