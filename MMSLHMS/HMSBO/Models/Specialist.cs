using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.Models
{
   public class Specialist
    {
        [Key]
        public long SpId { get; set; }
        [StringLength(100)]
        public string Specialization { get; set; }
        public string Remarks { get; set; }
        [StringLength(50)]
        public string EntryUser { get; set; }
        public long? OrgId { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        [StringLength(50)]
        public string UpdateBy { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
    }
}
