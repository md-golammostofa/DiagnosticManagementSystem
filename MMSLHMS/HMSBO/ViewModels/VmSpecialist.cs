using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.ViewModels
{
   public class VmSpecialist
    {
        public long SpId { get; set; }
        public string Specialization { get; set; }
        public string Remarks { get; set; }
        public long? OrgId { get; set; }
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public string OrgName { get; set; }
    }
}
