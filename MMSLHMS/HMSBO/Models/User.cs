using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace HMSBO.Models
{
    [TableName("tblUsers")]
    public class User
    {
        public int UserId { get; set; }
        [Required]
        [StringLength(30)]
        public string UserName { get; set; }
        [StringLength(150)]
        //[Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string MiddleName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        public int? RoleId { get; set; }
        public  bool IsRoleActive { get; set; }
        public Boolean IsActive { get; set; }
        [StringLength(30)]
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        [StringLength(30)]
        public string ApprovedBy { get; set; }
        public Nullable<DateTime> ApprovedDate { get; set; }
        [StringLength(30)]
        public string UpdateBy { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public int OrgId { get; set; }
        public int? BranchId { get; set; }
        [StringLength(30)]
        public string EmpId { get; set; }
        //public ICollection<Role> Roles { get; set; }
    }
}