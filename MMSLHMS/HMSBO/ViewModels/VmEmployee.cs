using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMSBO.ViewModels
{
    public class VmEmployee
    {
        [Required(ErrorMessage ="Branch is required")]
        public int? BranchId { get; set; }
        [Required(ErrorMessage = "Department is required")]
        public int? DepartmentId { get; set; }
        [Required(ErrorMessage = "Designation is required")]
        public int? DesignationId { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Middle name is required")]
        [StringLength(50)]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Marital status is required")]
        [StringLength(20)]
        public string MaritalStatus { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        [StringLength(15)]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [StringLength(150)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Home contact no is required")]
        [StringLength(15)]
        public string HomeContactNo { get; set; }
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        [Required(ErrorMessage = "Date of birth is required")]
        public Nullable<DateTime> DOB { get; set; }
        [Required(ErrorMessage = "Mobile no is required")]
        [StringLength(15)]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Nationality is required")]
        [StringLength(50)]
        public string Nationality { get; set; }
        [Required(ErrorMessage = "Present address is required")]
        [StringLength(150)]
        public string PresentAddress { get; set; }
        [Required(ErrorMessage = "Parmanent address is required")]
        [StringLength(150)]
        public string ParmanentAddress { get; set; }
        public HttpPostedFileBase Photo { get; set; }

        [StringLength(200)]
        public string PhotoUrl { get; set; }
        [StringLength(20)]
        public string StateStatus { get; set; }
        public bool IsActive { get; set; }
        [StringLength(100)]
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public bool AllowToLogin { get; set; }

        // Use only for list
        public int? EmployeeId { get; set; }
        public string EmployeeCode { get; set; } //--
        public string EmployeeName { get; set; }
        public string DepertmentName { get; set; }
        public string Designation { get; set; }
        public string BranchName { get; set; }
        public string ActiveStatus { get; set; }
        public int? OrgId { get; set; }
        //
    }
}