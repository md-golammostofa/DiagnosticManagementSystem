using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.ReportModels
{
    public class InvoiceBill
    {
        public string InvoiceNo { get; set; }
        public string OrgName { get; set; }
        public int OrgId { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string Sex { get; set; }
        public short? Year { get; set; }
        public short? Months { get; set; }
        public short? Days { get; set; }
        public long ReferrerId { get; set; }
        public string ReferrerName { get; set; }
        public string PaymentMode { get; set; }
        public long? BankId { get; set; }
        public string BankName { get; set; }
        public decimal? CashAmount { get; set; }
        public decimal? CardAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? ReceivedAmount { get; set; }
        public decimal? DueAmount { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? NetAmount { get; set; }
        public string Status { get; set; }
        public short? InvestigationQty { get; set; }
        public long? InvestigationId { get; set; }
        public string InvestigationName { get; set; }
        public decimal? Fee { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public byte[] OrgLogo { get; set; }
        public byte[] ReportLogo { get; set; }

    }
}
