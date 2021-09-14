using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMSBO.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public int? OrgId { get; set; }
        [StringLength(50)]
        public string EntryUser { get; set; }
        [DisplayFormat(DataFormatString ="{0:dd-MMM-yyyy}")]
        public Nullable<DateTime> EntryDate { get; set; }
        [StringLength(50)]
        public string ApprovedBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public Nullable<DateTime> ApprovedDate{ get; set; }
    }
}