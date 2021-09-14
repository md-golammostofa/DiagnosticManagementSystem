using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMSBO.ViewModels
{
    public class vmModule
    {
        public int ModuleId { get; set; }
        [Required]
        public string ModuleName { get; set; }
        public string IconName { get; set; }
        public string IconColor { get; set; }
    }
}