using System.Collections.Generic;
using System.Linq;
using Trady.Core;
using Trady.Core.Period;

namespace AutoStockTransaction
{
    public class TechnicalAnalysis
    {
        public IEnumerable<Candle> ChangeStockHistoricalToCandle(List<StockHistoricalPrice> priceList)
        {
            List<Candle> after = new List<Candle>();
            IEnumerable<Candle> enumAfter;
            foreach (var price in priceList)
            {
                after.Add(new Candle(price.Date, price.OpenPrice, price.HighPrice, price.LowPrice, price.ClosePrice, price.Volume));
            }
            enumAfter = after;
            return enumAfter;
        }

        public IEnumerable<Candle> TransformInterval(IEnumerable<Candle> candles, string period)
        {
            switch (period)
            {
                case "周":
                    return candles.Transform<Daily, Weekly>();

                case "月":
                    return candles.Transform<Daily, Monthly>();

                default:
                    return candles;
            }
        }

        public List<Candle> GetIntervalPriceList(string stkCode)
        {
            DataBaseReadWrite readWrite = new DataBaseReadWrite();
            var prices = readWrite.ReadHistoricalPrice(stkCode);
            var enumPrices = ChangeStockHistoricalToCandle(prices);
            var periodPrices = TransformInterval(enumPrices, StockTypeSettings.Interval);
            return periodPrices.ToList();
        }

        //public IEnumerable<IOhlcv> GetIndicator(IEnumerable<Candle> candles)
        //{

        //}
    }
}