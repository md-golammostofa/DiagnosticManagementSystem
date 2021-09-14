using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.Models
{
    public class DiagnosticBillInfo
    {
        [Key]
        public Int64 Id { get; set; }
        public Int64? OrgId { get; set; }
        public Int64? BranchId { get; set; }
        public string InvoiceNo { get; set; }
        [StringLength(100)]
        public string PatientId { get; set; }
        [StringLength(100)]
        public string PatientName { get; set; }
        [StringLength(150)]
        public string Address { get; set; }
        [StringLength(50)]
        public string MobileNo { get; set; }
        public short? Year { get; set; }
        public short? Months { get; set; }
        public short? Days { get; set; }

        [StringLength(10)]
        public string Sex { get; set; }
        public long ReferrerId { get; set; }
        [StringLength(50)]
        public string PaymentMode { get; set; }
        public decimal? DiscountPercent { get; set; }
        public Int64? BankId { get; set; }
        public decimal? CashAmount { get; set; }
        public decimal? CardAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? ReceivedAmount { get; set; }
        public decimal? DueAmount { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? NetAmount { get; set; }
        [StringLength(20)]
        public string Status { get; set; }
        [StringLength(50)]
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        [StringLength(50)]
        public string UpdateBy { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public short? InvestigationQty { get; set; }
        public decimal? TotalRefFee { get; set; }

    }

    public class DiagnosticBillDetail
    {
        [Key]
        public Int64 Id { get; set; }
        public Int64? BillInfoId { get; set; }
        public string InvoiceNo { get; set; }
        public Int64? InvestigationId { get; set; }
        public decimal? Fee { get; set; }
        public decimal? Discount { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? NetTotal { get; set; }
        [StringLength(50)]
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        [StringLength(50)]
        public string UpdateBy { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public long ReferrerId { get; set; }
        ///////
        public decimal? RefFee { get; set; }
    }

    public class DiagnosticBill
    {
        public DiagnosticBillInfo BillInfo{ get; set; }
        public List<DiagnosticBillDetail> BillDetails { get; set; }
    }
}
