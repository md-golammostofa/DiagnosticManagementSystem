using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.ReportModels
{
    public class DateRageWiseBillInfo
    {
        public long SLNo { get; set; }
        public long Id { get; set; }
        public string InvoiceNo { get; set; }
        public int InvestigationQty { get; set; }
        public string PatientName { get; set; }
        public string MobileNo { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? ReceivedAmount { get; set; }
        public decimal? DueAmount { get; set; }
        public long RefId { get; set; }
        public string RefName { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        public string Status { get; set; }

    }
}
