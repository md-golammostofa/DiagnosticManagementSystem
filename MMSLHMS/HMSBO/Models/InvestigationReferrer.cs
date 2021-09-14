using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.Models
{
    public class InvestigationReferrer
    {
        [Key]
        public Int64 Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(150)]
        public string Address { get; set; }
        [StringLength(50)]
        public string PhoneNumber { get; set; }
        public Int64?  OrgId{ get; set; }
        public Int64? BranchId { get; set; }
        [StringLength(50)]
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        [StringLength(50)]
        public string UpdateBy { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
    }
}
