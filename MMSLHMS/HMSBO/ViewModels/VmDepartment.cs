using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMSBO.ViewModels
{
    public class VmDepartment
    {
        public int DepartmentId { get; set; }
        [Required]
        [StringLength(70)]
        public string DepartmentName { get; set; }
        [Required]
        [StringLength(20)]
        public string ShortName { get; set; }
        [Required]
        public int OrgId { get; set; }
        public string Name { get; set; }
        public string EntryUser { get; set; }
    }
}