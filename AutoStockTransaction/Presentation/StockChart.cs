using DevExpress.XtraCharts;
using System.Collections.Generic;
using System.Drawing;
using Trady.Core;
using Trady.Core.Infrastructure;

namespace AutoStockTransaction
{
    public class StockChart
    {
        public void DrawingStockChart(ChartControl chartControl, IEnumerable<IOhlcv> oneOfStkHistoryPrice)
        {
            //設定price candle stick顏色
            CandleStickSeriesView priceSeriesView = (CandleStickSeriesView)chartControl.Series["Price"].View;
            priceSeriesView.Color = Color.Red;
            priceSeriesView.ReductionOptions.Color = Color.Green;
            //加入price資料
            chartControl.Series["Price"].DataSource = oneOfStkHistoryPrice;
            chartControl.Series["Price"].ArgumentDataMember = "DateTime.DateTime";
            chartControl.Series["Price"].ValueDataMembers.AddRange(new string[] { "Low", "High", "Open", "Close" });
            //加入volume資料
            chartControl.Series["Volume"].DataSource = oneOfStkHistoryPrice;
            chartControl.Series["Volume"].ArgumentDataMember = "DateTime.DateTime";
            chartControl.Series["Volume"].ValueDataMembers.AddRange(new string[] { "Volume" });
        }
        public void DrawingStockChart(ChartControl chartControl, IReadOnlyList<IAnalyzableTick> AnalyzableList)
        {

        }
    }
}