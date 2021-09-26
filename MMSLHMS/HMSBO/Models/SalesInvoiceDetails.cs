using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.Models
{
    [Table("tblSalesInvoiceDetails")]
    public class SalesInvoiceDetails
    {
        [Key]
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
        //For
        [ForeignKey("SalesInvoiceInfo")]
        public long IInfoId { get; set; }
        public SalesInvoiceInfo SalesInvoiceInfo { get; set; }
    }
}
