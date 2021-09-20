using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.Models
{
    [Table("tblProductStockInfo")]
    public class ProductStockInfo
    {
        [Key]
        public long PSInfoId { get; set; }
        public string Category { get; set; }
        public string ProductName { get; set; }
        public int StockInQty { get; set; }
        public int StockOutQty { get; set; }
        public double CostPrice { get; set; }
        public double SellPrice { get; set; }
        public string Remarks { get; set; }
        public long? OrgId { get; set; }
        [StringLength(50)]
        public string EntryUser { get; set; }
        public Nullable<DateTime> EntryDate { get; set; }
        [StringLength(50)]
        public string UpdateUser { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
    }
}
