using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMSBO.Models
{
    public class UserAuthorization
    {
        [Key]
        public Int64 TaskId { get; set; }
        public int UserId { get; set; }
        public int? RoleId { get; set; }
        public int ModuleId { get; set; }
        public int MainmenuId { get; set; }
        public int SubmenuId { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool  Delete { get; set; }
        public bool Approval { get; set; }
        public bool Report { get; set; }
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public string  ApprovedBy { get; set; }
        public Nullable<DateTime> ApprovedDate { get; set; }
    }
}