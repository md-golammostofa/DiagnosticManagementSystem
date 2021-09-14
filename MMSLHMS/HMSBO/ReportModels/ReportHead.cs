using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.ReportModels
{
    public class ReportHead
    {
        public string OrganizationName { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string MobileNo { get; set; }
        public string Fax { get; set; }
        public byte[] OrgLogo { get; set; }
    }
}
