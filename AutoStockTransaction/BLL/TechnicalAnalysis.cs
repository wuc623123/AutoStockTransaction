using System.Collections.Generic;
using System.Linq;
using Trady.Core;
using Trady.Core.Infrastructure;
using Trady.Core.Period;
using Trady.Analysis.Extension;
using Trady.Analysis.Indicator;
using Trady.Analysis;

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

        public IEnumerable<IOhlcv> TransformInterval(IEnumerable<IOhlcv> candles, string period)
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

        public IEnumerable<IOhlcv> GetIntervalPriceList(string stkCode)
        {
            DataBaseReadWrite readWrite = new DataBaseReadWrite();
            var prices = readWrite.ReadHistoricalPrice(stkCode);
            var enumPrices = ChangeStockHistoricalToCandle(prices);
            var periodPrices = TransformInterval(enumPrices, StockTypeSettings.Interval);
            return periodPrices;
        }

        public List<decimal?> GetIndicator(IEnumerable<IOhlcv> candles, string indicatorType)
        {
            var transformedCandles = TransformInterval(candles, StockTypeSettings.Interval);
            if (indicatorType == null)
            {
                indicatorType = "MACD";
            }

            switch (indicatorType)
            {
                case "ADX":
                    // return transformedCandles.Adx();
                case "MACD":
                    IReadOnlyList<AnalyzableTick<(decimal? MacdLine, decimal? SignalLine, decimal? MacdHistogram)>> resultMacd = transformedCandles.Macd(12, 26, 9);
                    return TickSplit(resultMacd,indicatorType);
                default:
                    return null;
            }
        }
        public List<decimal?> TickSplit(IReadOnlyList<IAnalyzableTick> candleIn, string type)
        {
            List<decimal?> splitedCandle = new List<decimal?>();
            switch (type)
            {
                case "MACD":
                    IReadOnlyList<AnalyzableTick<(decimal? MacdLine, decimal? SignalLine, decimal? MacdHistogram)>> candleTemp = (IReadOnlyList<AnalyzableTick<(decimal? MacdLine, decimal? SignalLine, decimal? MacdHistogram)>>)candleIn;
                    foreach (var item in candleTemp)
                    {
                        splitedCandle.Add(item.Tick.MacdLine);
                    }
                    break;
            }
            return splitedCandle;
        }
    }
}