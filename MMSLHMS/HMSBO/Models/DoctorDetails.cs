using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.Models
{
   public class DoctorDetails
    {
        [Key]
        public long TDId { get; set; }
        public string Day { get; set; }
        public string DayOfTime { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string EntryUser { get; set; }
        public long? OrgId { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        //
        public string DocName { get; set; }
        public string DocSpecialist { get; set; }
        public double? GenerelFee { get; set; }
        public string DocDegree { get; set; }
        public string DocMobile { get; set; }

        [ForeignKey("Doctor")]
        public long DocId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
