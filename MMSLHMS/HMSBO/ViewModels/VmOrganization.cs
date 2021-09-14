using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMSBO.ViewModels
{
    public class VmOrganization
    {
        public int OrgId { get; set; }
        [Required]
        [StringLength(150)]
        public string OrgName { get; set; }
        [Required]
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(100)]
        public string Telephone { get; set; }
        [Required]
        [StringLength(100)]
        public string MobileNumber { get; set; }
        [StringLength(100)]
        public string Fax { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(150)]
        public string Email { get; set; }
        public string ShortName { get; set; }
        [StringLength(50)]
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        [StringLength(50)]
        public string ApprovedBy { get; set; }
        public Nullable<DateTime> ApprovedDate { get; set; }
        public bool IsActive { get; set; }
        [StringLength(250)]
        public string OrgLogoPath { get; set; }
        [StringLength(250)]
        public string ReportLogoPath { get; set; }
        public HttpPostedFileBase OrgImage { get; set; }
        public HttpPostedFileBase ReportImage { get; set; }
    }
}