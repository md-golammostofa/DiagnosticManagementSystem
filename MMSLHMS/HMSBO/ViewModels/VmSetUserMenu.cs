using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMSBO.ViewModels
{
    public class VmSetUserMenu
    {
        public string UserId { get; set; }
        public List<VmUserAuthorize> authorizeData { get; set; }
    }
    public class VmUserAuthorize
    {
        public string ModuleId { get; set; }
        public string MenuId { get; set; }
        public string SubMenuId { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsApproval { get; set; }
        public bool IsReport { get; set; }
    }

    public class VmSetRoleMenu
    {
        public string RoleId { get; set; }
        public string OrgId { get; set; }
        public List<VmUserAuthorize> authorizeData { get; set; }
    }
}