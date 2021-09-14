using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.Models
{
    public class Branch
    {
        [Key]
        public int BranchId { get; set; }
        public int? OrgId { get; set; }
        [StringLength(50)]
        public string BranchName { get; set; }
        [StringLength(5)]
        public string ShortName { get; set; }
        [StringLength(15)]
        public string BranchCode { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
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
