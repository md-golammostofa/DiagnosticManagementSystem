using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMSBO.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string ShortName { get; set; }
        public int OrgId { get; set; }
        public string EntryUser { get; set; }
        [DefaultValue("getutcdate()")]
        public Nullable<DateTime> EntryDate{ get; set; }
        public string UpdateBy { get; set; }
        [DefaultValue("getutcdate()")]
        public Nullable<DateTime> UpdateDate { get; set; }
    }
}