using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMSBO.ViewModels
{
    public class vmUser
    {
        public int UserId { get; set; }
        [StringLength(30)]
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        //[Required]
        //public string FirstName { get; set; }
        //[Required]
        //public string LastName { get; set; }
        //[StringLength(30)]
        //[Required]
        public string EmpId { get; set; }
        public Boolean IsActive { get; set; }
        [StringLength(30)]
        public string EntryUser { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> EntryDate { get; set; }
        [StringLength(30)]
        public string ApprovedBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> ApprovedDate { get; set; }
        [Required]
        [Range(minimum:1,maximum:10000)]
        public int OrgId { get; set; }
        [StringLength(150)]
        public string OrgName { get; set; }
        public int? RoleId { get; set; }
        [StringLength(50)]
        public string RoleName { get; set; }
        public bool IsRoleActive { get; set; }

        public string EmployeeName { get; set; }
        public string Email { get; set; }

    }
}