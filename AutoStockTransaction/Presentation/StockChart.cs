using DevExpress.XtraCharts;
using System.Collections.Generic;
using System.Drawing;
using Trady.Core;

namespace AutoStockTransaction
{
    public class StockChart
    {
        public void DrawingStockChart(ChartControl chartControl, List<Candle> oneOfStkHistoryPrice)
        {
            //設定price candle stick顏色
            CandleStickSeriesView priceSeriesView = (CandleStickSeriesView)chartControl.Series["Price"].View;
            priceSeriesView.Color = Color.Red;
            priceSeriesView.ReductionOptions.Color = Color.Green;
            //加入price資料
            chartControl.Series["Price"].DataSource = oneOfStkHistoryPrice;
            chartControl.Series["Price"].ArgumentDataMember = "DateTime";
            chartControl.Series["Price"].ValueDataMembers.AddRange(new string[] { "Low", "High", "Open", "Close" });
            //加入volume資料
            chartControl.Series["Volume"].DataSource = oneOfStkHistoryPrice;
            chartControl.Series["Volume"].ArgumentDataMember = "DateTime";
            chartControl.Series["Volume"].ValueDataMembers.AddRange(new string[] { "Volume" });
        }
    }
}