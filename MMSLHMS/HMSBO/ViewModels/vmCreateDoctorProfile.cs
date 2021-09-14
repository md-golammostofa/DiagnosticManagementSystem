using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMSBO.ViewModels
{
    public class vmCreateDoctorProfile
    {
        public int Id { get; set; }
        [StringLength(30)]
        public string DoctorId { get; set; }
        [Required]
        public string Prefix { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage ="First Name is required")]
        public string FirstName { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "Last Name is required")]
        public string MiddleName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(70)]
        [Required(ErrorMessage = "Father Name is required")]
        public string FatherName { get; set; }
        [StringLength(70)]
        [Required(ErrorMessage = "Mother Name is required")]
        public string MotherName { get; set; }
        [StringLength(70)]
        public string SpouseName { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "Contact Number is required")]
        public string ContactNumber { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "Home Contact Number is required")]
        public string PhoneNumber { get; set; }
        [StringLength(150)]
        [Required(ErrorMessage = "Parmanent address is required")]
        public string ParmanentAddress { get; set; }
        [StringLength(150)]
        [Required(ErrorMessage = "Present address is required")]
        public string PresentAddress { get; set; }
        [StringLength(10)]
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
        [StringLength(30)]
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [StringLength(250)]
        [Required(ErrorMessage = "Degrees is required")]
        public string Degrees { get; set; }
        [StringLength(200)]
        [Required(ErrorMessage = "Experiences is required")]
        public string Experiences { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "Current Job Location is required")]
        public string CurrentJobLocation { get; set; }
        [Required(ErrorMessage = "Years of Experience is required")]
        public int? YearsOfExperience { get; set; }
        [StringLength(150)]
        public string About { get; set; }
        //[DisplayFormat(DataFormatString ="{0:dd-MMM-yyyy}",ApplyFormatInEditMode =true)]
        [Required(ErrorMessage ="Date of birth is required")]
        public Nullable<DateTime> DateOfBirth { get; set; }
        //[StringLength(200)]
        public HttpPostedFileBase Photo { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required(ErrorMessage ="Department is required")]
        public int? DepartmentId { get; set; }
        public string PhotoUrl { get; set; }
        [Required(ErrorMessage = "InPatient Charge Type is required")]
        [StringLength(15)]
        public string InPatientChargeType { get; set; }
        [Required(ErrorMessage = "OutPatient Charge Type is required")]
        [StringLength(15)]
        public string OutPatientChargeType { get; set; }
        [Required(ErrorMessage = "InPatient Charge Type is required")]
        public double? InPatientChargeValue { get; set; }
        [Required(ErrorMessage = "OutPatient Charge Type is required")]
        public double? OutPatientChargeValue { get; set; }
        [Required(ErrorMessage = "License is required")]
        [StringLength(20)]
        public string LicenseNo { get; set; }
        [Required(ErrorMessage = "Designation is required")]
        public int? DesignationId { get; set; }
        [Required(ErrorMessage = "Specialization Type is required")]
        public int? SpecializationTypeId { get; set; }

        [Required(ErrorMessage = "Branch is required")]
        public int? BranchId { get; set; }
        
        [StringLength(150)]
        public string Email { get; set; }
        [StringLength(30)]
        public string MaritalStatus { get; set; }

        // Allow login to user..
        public bool AllowToLogin { get; set; }
        [StringLength(100)]
        public string EmployeeCode { get; set; }
        public int? OrgId { get; set; }
        [StringLength(50)]
        [Required]
        public string Nationality { get; set; }
    }
}