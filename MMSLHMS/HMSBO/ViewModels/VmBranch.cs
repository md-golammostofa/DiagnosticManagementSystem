using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.ViewModels
{
    public class VmBranch
    {
        public int BranchId { get; set; }
        [Required]
        public int? OrgId { get; set; }
        public string OrgName { get; set; }
        [Required]
        [StringLength(50)]
        public string BranchName { get; set; }
        [Required]
        [StringLength(5)]
        public string ShortName { get; set; }
        [Required]
        [StringLength(15)]
        public string BranchCode { get; set; }
        [Required]
        [StringLength(100)]
        public string Address { get; set; }
        [Required]
        [StringLength(50)]
        public string ContactNumber { get; set; }
        [StringLength(50)]
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        [StringLength(50)]
        public string UpdateBy { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
    }
}
