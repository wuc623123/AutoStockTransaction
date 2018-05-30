using System.Collections.Generic;

namespace AutoStockTransaction
{
    public class RptStructure
    {
        public List<string> ProcessTime { get; set; }
        public double UpdateStkCodeProgress { get; set; }
        public double UpdatePriceListsProgress { get; set; }
        public double WrittingDBProgress { get; set; }
        public string ErrorStkCode { get; set; }//單獨被輸出
        public string CompletedMsg { get; set; }
    }
}