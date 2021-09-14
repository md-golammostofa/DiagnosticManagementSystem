using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.ViewModels
{
   public class VmTimes
    {
        public long TimeId { get; set; }
        public string Time { get; set; }
        public string ScendTimeF { get; set; }
        public string EntryUser { get; set; }
        public long? OrgId { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
    }
}
