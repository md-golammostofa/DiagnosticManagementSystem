using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.Models
{
   public class Appoinment
    {
        [Key]
        public long AppId { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public string PatientAddress { get; set; }
        public string PatientMobile { get; set; }
        public string SateStatus { get; set; }
        public string Age { get; set; }
        public string Time { get; set; }
        public string Shift { get; set; }
        public Nullable<DateTime> AppoinmentDate { get; set; }
        public string EntryUser { get; set; }
        public long? OrgId { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
    }
}
