using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMSBO.ViewModels
{
    public class VmModule
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public List<VmMenu> Menus { get; set; }
    }

    public class VmMenu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public List<VmSubMenu> SubMenus { get; set; }
    }

    public class VmSubMenu
    {
        public int SubmenuId { get; set; }
        public string SubMenuName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Area { get; set; }
    }

    public class VmMenuItems
    {
        public List<VmMenu> Menus { get; set; }
    }
}