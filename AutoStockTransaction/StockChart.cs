using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoStockTransaction
{
    public class StockChart
    {
        public void DrawingStockChart(ChartControl chartControl, List<StockHistoricalPrice> oneOfStkHistoryPrice)
        {
            //設定price candle stick顏色
            CandleStickSeriesView priceSeriesView = (CandleStickSeriesView)chartControl.Series["Price"].View;
            priceSeriesView.Color = Color.Red;
            priceSeriesView.ReductionOptions.Color = Color.Green;
            //加入price資料
            chartControl.Series["Price"].DataSource = oneOfStkHistoryPrice;
            chartControl.Series["Price"].ArgumentDataMember = "Date";
            chartControl.Series["Price"].ValueDataMembers.AddRange(new string[] { "LowPrice", "HighPrice", "OpenPrice", "ClosePrice" });
            //加入volume資料
            chartControl.Series["Volume"].DataSource = oneOfStkHistoryPrice;
            chartControl.Series["Volume"].ArgumentDataMember = "Date";
            chartControl.Series["Volume"].ValueDataMembers.AddRange(new string[] { "Volume" });
        }
    }
}
