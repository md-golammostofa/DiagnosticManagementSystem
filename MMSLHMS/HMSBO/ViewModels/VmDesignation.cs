using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMSBO.ViewModels
{
    public class VmDesignation
    {
        public int DesigId { get; set; }
        [Required,StringLength(100)]
        public string DesignationName { get; set; }
        [Required, StringLength(10)]
        public string ShortName { get; set; }
        [Required]
        public int OrgId { get; set; }
        public string OrgName { get; set; }
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
    }
}