using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoStockTransaction
{
    class RptStructure
    {
        public double GetProgressOnAllPriceLists { get; set; }
        public double GetProgressOnWrittingDB { get; set; }
        public string ErrorStkCode { get; set; }
        public string AnalysisData { get; set; }
    }
}
