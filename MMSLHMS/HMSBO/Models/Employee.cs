using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMSBO.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public int? BranchId { get; set; }
        public int? DepartmentId { get; set; }
        public int? DesignationId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string MiddleName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(20)]
        public string MaritalStatus { get; set; }
        [StringLength(15)]
        public string Gender { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        [StringLength(15)]
        public string HomeContactNo { get; set; }
        public Nullable<DateTime> DOB { get; set; }
        [StringLength(15)]
        public string MobileNo { get; set; }
        [StringLength(50)]
        public string Nationality { get; set; }
        [StringLength(150)]
        public string PresentAddress { get; set; }
        [StringLength(150)]
        public string ParmanentAddress { get; set; }
        [StringLength(50)]
        public string EmpType { get; set; }
        [StringLength(200)]
        public string PhotoUrl { get; set; }
        [StringLength(20)]
        public string StateStatus { get; set; }
        public bool IsActive { get; set; }
        [StringLength(100)]
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        [StringLength(100)]
        public string UpdateBy { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        [StringLength(100)]
        public string EmployeeCode { get; set; }
        public bool AllowToLogin { get; set; }
        public int? OrgId { get; set; }
    }
}