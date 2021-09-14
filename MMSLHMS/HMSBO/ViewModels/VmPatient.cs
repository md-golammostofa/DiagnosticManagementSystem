using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.ViewModels
{
   public class VmPatient
    {
        public long PatientId { get; set; }
        public string PatiendName { get; set; }
        public string PatientAddress { get; set; }
        public string PatientMobile { get; set; }
        public string PatientSex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PatientEmail { get; set; }
        public string Remarks { get; set; }
        public string PatientWeight { get; set; }
        public string EntryUser { get; set; }
        public long? OrgId { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
    }
}
