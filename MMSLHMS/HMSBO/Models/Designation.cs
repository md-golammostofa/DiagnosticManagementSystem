using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMSBO.Models
{
    public class Designation
    {
        [Key]
        public int DesigId { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(10)]
        public string ShortName { get; set; }
        public int? OrgId { get; set; }
        [StringLength(80)]
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
    }
}