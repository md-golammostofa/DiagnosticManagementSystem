using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSBO.ReportModels
{
    public class MonthWiseInvestigationReport
    {
        public string MonthAndYear { get; set; }
        public int InvestigationId { get; set; }
        public string InvestigationName { get; set; }
        public int TestQty { get; set; }
        public decimal Amount { get; set; }
    }
}
