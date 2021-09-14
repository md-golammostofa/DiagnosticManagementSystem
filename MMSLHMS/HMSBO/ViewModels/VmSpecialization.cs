using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMSBO.ViewModels
{
    public class VmSpecialization
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string  TypeName { get; set; }
        public string OrgName { get; set; }
        [Required]
        public int OrgId { get; set; }
    }
}