using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMSBO.Models
{
    public class Submenu
    {
        [Key]
        public int SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public int MainMenuId { get; set; }
        public string ControllName { get; set; }
        public string ActionName { get; set; }
        public string Area { get; set; }
        public bool IsShow { get; set; }
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public string ApprovedUser { get; set; }
        public Nullable<DateTime> ApprovedDate { get; set; }
    }
}