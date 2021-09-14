using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace HMSBO.Models
{
    [TableName("tblOrganizations")]
    public class Organization
    {
        [Key]
        public int OrgId { get; set; }
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(150)]
        public string ShortName { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(100)]
        public string Telephone { get; set; }
        [StringLength(100)]
        public string MobileNumber { get; set; }
        [StringLength(100)]
        public string Fax { get; set; }
        [StringLength(150)]
        public string  Email { get; set; }
        public bool IsActive { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        [StringLength(50)]
        public string EntryUser { get; set; }
        [StringLength(50)]
        public string ApprovedBy { get; set; }
        public Nullable<DateTime> ApprovedDate { get; set; }
        [StringLength(250)]
        public string OrgLogoPath { get; set; }
        [StringLength(250)]
        public string ReportLogoPath { get; set; }
        //public ICollection<User> Users { get; set; }
    }
}