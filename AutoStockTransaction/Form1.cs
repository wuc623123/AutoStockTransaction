using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trady.Core;
using Trady.Analysis;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using DevExpress.XtraScheduler;
using DevExpress.XtraCharts;

namespace AutoStockTransaction
{

    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        private int indexOfListboxAdd = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private async void 更新股票代碼ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            更新股票代碼ToolStripMenuItem.Enabled = false;
            自動更新股票歷史價格ToolStripMenuItem.Enabled = false;
            CatchStkData cd = new CatchStkData();
            indexOfListboxAdd = listBoxControl1.Items.Add("股票代碼更新中...");
            //取得更薪資料庫進度
            Progress<int> progress = new Progress<int>(RptStkCodeUpgradeProgress);
            //取得資料庫儲存時間
            Progress<string> processTime = new Progress<string>(RptProcessTime);
            try
            {
                await Task.Run(() => cd.MultiCatch(progress, processTime));
                listBoxControl1.Items.Add("股票代碼更新完成!");
            }
            catch (NullReferenceException nullEx)
            {
                listBoxControl1.Items.Add("股票代碼更新失敗!");
                listBoxControl1.Items.Add(nullEx.Message);
            }
            finally
            {
                更新股票代碼ToolStripMenuItem.Enabled = true;
                自動更新股票歷史價格ToolStripMenuItem.Enabled = true;
            }
        }

        private async void 自動更新股票歷史價格ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //主要功能：取得所有上市上櫃20年股價並塞入DB內
            自動更新股票歷史價格ToolStripMenuItem.Enabled = false;
            更新股票代碼ToolStripMenuItem.Enabled = false;
            CatchHistoricalPrice CHP = new CatchHistoricalPrice();
            Progress<RptStructure> historyPriceProgress = new Progress<RptStructure>(RptCatchHistoryProgress);
            indexOfListboxAdd = listBoxControl1.Items.Add("自動更新股票歷史價格中...");
            await Task.Run(() => CHP.UpdatePriceToDB(historyPriceProgress));
            listBoxControl1.Items.Add("自動更新股票歷史價格完成!");
            自動更新股票歷史價格ToolStripMenuItem.Enabled = true;
            更新股票代碼ToolStripMenuItem.Enabled = true;
        }
        void RptStkCodeUpgradeProgress(int percentOfProgress)
        {
            if (indexOfListboxAdd != -1)
            {
                listBoxControl1.Items[indexOfListboxAdd] = $"股票代碼更新中...{percentOfProgress}%";
            }
        }
        void RptCatchHistoryProgress(RptStructure percentOfProgress)
        {
            if (indexOfListboxAdd != -1)
            {
                listBoxControl1.Items[indexOfListboxAdd] = $"自動更新股票歷史價格中...網路取得價格:{percentOfProgress.GetProgressOnAllPriceLists}%寫入資料庫:{percentOfProgress.GetProgressOnWrittingDB}%";
                if (percentOfProgress.ErrorStkCode != null)
                {
                    listBoxControl1.Items.Add(percentOfProgress.ErrorStkCode);
                    listBoxControl1.SelectedIndex = listBoxControl1.Items.Count - 1;
                }
            }
        }
        void RptProcessTime(string processTime)
        {
            listBoxControl1.Items.Add(processTime);
        }

        private async void 技術分析ToolStripMenuItem_ClickAsync(object sender, EventArgs e)
        {
            TechAnalysis ta = new TechAnalysis();
            //var result = await ta.GetAllAnalysis();
            //foreach (AnalyzableTick<decimal?> at in result)
            //{
            //    if (at.Tick != null)
            //    {
            //        listBox1.Items.Add($"{at.DateTime.Value}  {Math.Round(at.Tick.Value, 2)}");
            //    }
            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (StockEntities se = new StockEntities())
            {
                var stkCodeAndNameList = (from stkCodeName in se.ListedStock
                                          where stkCodeName.StkCategory == 1
                                          select stkCodeName).ToList();
                foreach (ListedStock ls in stkCodeAndNameList)
                    listBoxControl2.Items.Add($"{ls.StkCode} {ls.StkName}");
            }
        }

        private void listBoxControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            chartControl1.Series.Clear();
            string[] stkCodeName = listBoxControl2.SelectedItem.ToString().Split(' ');
            var stk1 = stkCodeName[0];
            using (StockEntities se = new StockEntities())
            {
                //取得listbox被選中的股票的歷史價格
                var oneOfStkHistoryPrice = (from oneOfstkPrice in se.StockHistoricalPrice
                                            where oneOfstkPrice.StkCode == stk1
                                            select oneOfstkPrice).ToList();
                //初始價格線圖與成交量線圖
                Series priceSeries = new Series($"{stkCodeName[0]} {stkCodeName[1]}", ViewType.CandleStick);
                Series volumeSeries = new Series("Volume", ViewType.Bar);

                priceSeries.ArgumentScaleType = ScaleType.DateTime;
                foreach (StockHistoricalPrice shp in oneOfStkHistoryPrice)
                {
                    priceSeries.Points.Add(new SeriesPoint(shp.Date, new Object[] { shp.HighPrice, shp.LowPrice, shp.OpenPrice, shp.ClosePrice }));
                    volumeSeries.Points.Add(new SeriesPoint(shp.Date, new object[] { shp.Volume }));
                }
                //priceSeries新增至上半部與設定
                chartControl1.Series.Add(priceSeries);
                CandleStickSeriesView series1View = (CandleStickSeriesView)priceSeries.View;
                series1View.LineThickness = 2;
                series1View.LevelLineLength = 0.25;
                ((XYDiagram)chartControl1.Diagram).EnableAxisXScrolling = true;
                ((XYDiagram)chartControl1.Diagram).EnableAxisXZooming = true;
                //pane新增至下半部與設定
                XYDiagramPane pane1 = new XYDiagramPane("Volume");
                pane1.Weight = 0.3;
                ((XYDiagram)chartControl1.Diagram).Panes.Clear();
                ((XYDiagram)chartControl1.Diagram).Panes.Add(pane1);
            }
        }
    }
}
