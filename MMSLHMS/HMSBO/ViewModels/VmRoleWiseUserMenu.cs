using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMSBO.ViewModels
{
    public class VmRoleModule
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public List<VmRoleMenu> Menus { get; set; }
    }
    public class VmRoleMenu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public List<VmRoleSubmenu> SubMenus { get; set; }
    }
    public class VmRoleSubmenu
    {
        public int SubmenuId { get; set; }
        public string SubMenuName { get; set; }
        public bool HasRole { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Approval { get; set; }
        public bool Report { get; set; }
    }
}