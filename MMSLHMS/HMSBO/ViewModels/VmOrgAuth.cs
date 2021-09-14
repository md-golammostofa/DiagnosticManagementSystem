using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.ViewModels
{
    public class VmOrgAuth
    {
        public int OrgId { get; set; }
        public List<AuthMenus> AuthMenus { get; set; }
    }
    public class AuthMenus
    {
        public int SubmenuId { get; set; }
        public int MainmenuId { get; set; }
        public int ModuleId { get; set; }

    }
}
