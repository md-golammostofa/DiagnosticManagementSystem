using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HMSBO.Models
{
    
    public class DoctorProfile
    {
        [Key]
        public int Id { get; set; }
        [StringLength(30)]
        public string DoctorId { get; set; }
        public string Prefix { get; set; }
        [StringLength(70)]
        public string FirstName { get; set; }
        [Column("MiddleName")]
        [StringLength(70)]
        public string MiddleName { get; set; }
        [Column("LastName")]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(20)]
        public string LicenseNo { get; set; }
        [StringLength(70)]
        public string FatherName { get; set; }
        [StringLength(70)]
        public string MotherName { get; set; }
        [StringLength(70)]
        public string SpouseName { get; set; }
        [StringLength(50)]
        public string ContactNumber { get; set; }
        [StringLength(50)]
        public string PhoneNumber { get; set; }
        [StringLength(150)]
        public string ParmanentAddress { get; set; }
        [StringLength(150)]
        public string PresentAddress { get; set; }
        [StringLength(10)]
        public string Gender { get; set; }
        [StringLength(30)]
        public string City { get; set; }
        [StringLength(250)]
        public string Degrees { get; set; }
        [StringLength(200)]
        public string Experiences { get; set; }
        [StringLength(100)]
        public string CurrentJobLocation { get; set; }
        public int? YearsOfExperience { get; set; }
        [StringLength(150)]
        public string About { get; set; }
        [DataType(DataType.Date)]
        public Nullable<DateTime> DateOfBirth{ get; set; }
        public int? DepartmentId { get; set; }

        [StringLength(200)]
        public string PhotoUrl { get; set; }
        public bool IsActive { get; set; }
        [StringLength(50)]
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        [StringLength(50)]
        public string ApprovedUser { get; set; }
        public Nullable<DateTime> ApprovedDate { get; set; }
        [StringLength(15)]
        public string InPatientChargeType { get; set; }
        [StringLength(15)]
        public string OutPatientChargeType { get; set; }
        public double? InPatientChargeValue { get; set; }
        public double? OutPatientChargeValue { get; set; }
        public int? DesignationId { get; set; }
        public int? SpecializationTypeId { get; set; }
        public int? BranchId { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        [StringLength(30)]
        public string MaritalStatus { get; set; }
        public bool AllowToLogin { get; set; }
        [StringLength(100)]
        public string EmployeeCode { get; set; }
        public int OrgId { get; set; }
        [StringLength(50)]
        public string Nationality { get; set; }
    }
}