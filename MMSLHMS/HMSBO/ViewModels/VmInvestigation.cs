using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.ViewModels
{
    public class VmInvestigation
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Investigation Name is required")]
        [StringLength(150)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Fee is required.")]
        public double? Fee { get; set; }
        public int? OrgId { get; set; }
        public int? BranchId { get; set; }
        [Required]
        public int? ChartId { get; set; }
        [StringLength(100)]
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        [StringLength(100)]
        public string UpdateBy { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }

        // Using by List Only //
        public string OrgName { get; set; }
        public double? RefFee { get; set; }
    }
}
