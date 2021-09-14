using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMSBO.ViewModels
{
    public class VmMainMenu
    {
        public int MMId { get; set; }
        [Required]
        public string MenuName { get; set; }
        [Required]
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
    }
}