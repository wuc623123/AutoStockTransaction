using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trady.Core;

namespace AutoStockTransaction
{
    class StkWithCandles
    {
        public string StkCode { get; set; }
        public IEnumerable<Candle> Candles { get; set; }
    }
}
