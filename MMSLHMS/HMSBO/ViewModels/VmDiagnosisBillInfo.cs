using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.ViewModels
{
    public class VmDiagnosisBillInfo
    {
        [Key]
        public Int64 Id { get; set; }
        public Int64? OrgId { get; set; }
        public Int64? BranchId { get; set; }
        public string InvoiceNo { get; set; }
        [StringLength(100)]
        public string PatientId { get; set; }
        [Required(ErrorMessage ="Name is required")]
        [StringLength(100)]
        public string PatientName { get; set; }
        //[Required(ErrorMessage = "Address is required")]
        [StringLength(150)]
        public string Address { get; set; }
        [Required(ErrorMessage ="Mobile No is required")]
        [StringLength(50)]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Age is required")]
        [Range(1,150)]
        public short? Year { get; set; }
        [Range(0, 12)]
        public short? Months { get; set; }
        [Range(0, 31)]
        public short? Days { get; set; }
        [Required(ErrorMessage = "Sex is required")]
        [StringLength(10)]
        public string Sex { get; set; }
        public long ReferrerId { get; set; }
        [Required(ErrorMessage = "Payment Mode is required")]
        [StringLength(50)]
        public string PaymentMode { get; set; }
        public Int64? BankId { get; set; }
        public decimal? CashAmount { get; set; }
        public decimal? CardAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        [Required(ErrorMessage = "Receive amt. is required")]
        public decimal? ReceivedAmount { get; set; }
        public decimal? DueAmount { get; set; }
        [Required(ErrorMessage = "Sub total is required")]
        public decimal? SubTotal { get; set; }
        [Required(ErrorMessage = "Net amt. is required")]
        public decimal? NetAmount { get; set; }
        [StringLength(20)]
        public string Status { get; set; }
        [StringLength(50)]
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        [StringLength(50)]
        public string UpdateBy { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public decimal? DiscountPercent { get; set; }
        public string OrgName { get; set; }
        public string BranchName { get; set; }
        public string ReferrerName { get; set; }
        [Required(ErrorMessage = "Item Qty is required")]
        public short? InvestigationQty { get; set; }
        /////
        public decimal? TotalRefFee { get; set; }
    }

    public class VmDiagnosticBillDetail
    {
        [Key]
        public Int64 Id { get; set; }
        public Int64? BillInfoId { get; set; }
        public string InvoiceNo { get; set; }
        [Required(ErrorMessage = "Investigation is required")]
        public string InvestigationName { get; set; }
        [Required(ErrorMessage = "Investigation is required")]
        public Int64? InvestigationId { get; set; }
        [Required(ErrorMessage = "Fee is required")]
        public decimal? Fee { get; set; }
        public decimal? Discount { get; set; }
        //[Required]
        public decimal? ItemSubTotal { get; set; }
        //[Required]
        public decimal? ItemNetTotal { get; set; }
        [StringLength(50)]
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        [StringLength(50)]
        public string UpdateBy { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public decimal? RefFee { get; set; }
    }

    public class VmDiagnosticBill
    {
        public VmDiagnosisBillInfo BillInfo { get; set; }
        [Required(ErrorMessage ="No Bill Details Found")]
        public VmDiagnosticBillDetail[] BillDetails { get; set; }
    }

    public class vmDianosticBillDueAdjustment
    {
        [Required]
        public Int64 Id { get; set; }
        [Required]
        public string InvoiceNo { get; set; }
        public short? InvestigationQty { get; set; }
        public string PatientName { get; set; }
        public string MobileNo { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? ReceiveAmount { get; set; }
        public decimal? DueAmount { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        [DisplayFormat(DataFormatString ="{0:dd-MMM-yyyy hh:mm tt}")]
        public Nullable<DateTime> EntryDate { get; set; }
        public string Status { get; set; }
        public long RefId { get; set; }
        public string RefName { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DiscountPercent { get; set; }
        //// <summary>
        public decimal? TotalRefFee { get; set; }
    }

    public class vmMonthAndInvestigationReport
    {
        public string MonthAndYear { get; set; }
        public Int64 InvestigationId { get; set; }
        public string InvestigationName { get; set; }
        public Int32 TotalQty { get; set; }
        public decimal TotalAmount { get; set; }
        public Int32 OrgId { get; set; }
    }

    public class vmReferrerWiseBillInfo
    {
        public long RefId { get; set; }
        public string RefName { get; set; }
        public string RefPhone { get; set; }
        public long TotalRefData { get; set; }
        public long InvestId { get; set; }
        public string InvestName { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalRefFeeAmount { get; set; }
    }
}
