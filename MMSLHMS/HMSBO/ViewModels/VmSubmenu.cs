using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMSBO.ViewModels
{
    public class VmSubmenu
    {
        public int SubMenuId { get; set; }
        [Required]
        public string SubMenuName { get; set; }
        [Required]
        public int MainMenuId { get; set; }
        public string MainMenuName { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        [Required]
        public string ControllerName { get; set; }
        [Required]
        public string ActionName { get; set; }
        public string Area { get; set; }
        public bool IsShow { get; set; }
        public string EntryUser { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> EntryDate { get; set; }
    }
}