using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.Models
{
    [Table("tblSalesInvoiceInfo")]
    public class SalesInvoiceInfo
    {
        [Key]
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
        [StringLength(50)]
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        [StringLength(50)]
        public string UpdateUser { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public List<SalesInvoiceDetails> salesInvoiceDetals { get; set; }
    }
}
