using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.ViewModels
{
    public class VmInvestigationReferrar
    {
        public Int64 Id { get; set; }
        [Required]
        [Display(Name = "Referrar Name")]
        [StringLength(100)]
        public string Name { get; set; }
        [Display(Name = "Address")]
        [Required]
        [StringLength(150)]
        public string Address { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public Int64? OrgId { get; set; }
        public Int64? BranchId { get; set; }
        [StringLength(50)]
        [Display(Name = "Entry User")]
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        [StringLength(50)]
        [Display(Name = "Update By")]
        public string UpdateBy { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }

        // ------------- //
        [Display(Name="Organization")]
        public string OrgName { get; set; }
        public string BranchName { get; set; }
    }
}
