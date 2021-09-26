using HMSBO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.ViewModels
{
    public class VmSalesInvoiceInfo
    {
        public long IInfoId { get; set; }
        public string InvoiceNo { get; set; }
        public string CustomerType { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerAddress { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public double SubTotalAmount { get; set; }
        public double VATAmount { get; set; }
        public double TAXAmount { get; set; }
        public double DisCountAmount { get; set; }
        public double NetAmount { get; set; }
        public double ReceiveAmount { get; set; }
        public double DueAmount { get; set; }
        public string Remarks { get; set; }
        public long? OrgId { get; set; }
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public string UpdateUser { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public List<SalesInvoiceDetails> salesInvoiceDetals { get; set; }
        //
        public long DealerName { get; set; }
    }
}
