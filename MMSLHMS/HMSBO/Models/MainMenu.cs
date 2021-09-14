using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMSBO.Models
{
    public class MainMenu
    {
        [Key]
        public int MMId { get; set; }
        public string MenuName { get; set; }
        public int ModuleID { get; set; }
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public string ApprovedUser { get; set; }
        public Nullable<DateTime> ApprovedDate { get; set; }
    }
}