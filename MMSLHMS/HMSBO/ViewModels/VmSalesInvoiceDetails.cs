using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.ViewModels
{
    public class VmSalesInvoiceDetails
    {
        public long IDetailsId { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public double SellPrice { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
        public string Remarks { get; set; }
        public long? OrgId { get; set; }
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public string UpdateUser { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        //
        public long IInfoId { get; set; }
    }
}
