using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMSBO.Models
{
    public class RoleWiseUserMenu
    {
        [Key]
        public int RoleWiseMenuId { get; set; }
        public int RoleId { get; set; }
        public int OrgId { get; set; }
        public int ModuleId { get; set; }
        public int MainmenuId { get; set; }
        public int SubmenuId { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Approval { get; set; }
        public bool Report { get; set; }
        [StringLength(50)]
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        [StringLength(50)]
        public string ApprovedBy { get; set; }
        public Nullable<DateTime> ApprovedDate { get; set; }
    }
}