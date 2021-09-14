using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.Models
{
    public class Investigation
    {
        [Key]
        public int Id { get; set; }
        [StringLength(150)]
        public string Name { get; set; }
        public double? Fee { get; set; }

        public int? OrgId { get; set; }
        public int? BranchId { get; set; }
        public int? ChartId { get; set; }
        [StringLength(100)]
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        [StringLength(100)]
        public string UpdateBy { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        //////
        public double? RefFee { get; set; }
    }
}
