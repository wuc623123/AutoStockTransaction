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
        void RptStkCodeUpgradeProgress(int percentOfProgress)
        {
            if (indexOfListboxAdd != -1)
            {
                listBoxControl2.Items[indexOfListboxAdd] = $"股票代碼更新中...{percentOfProgress}%";
            }
        }
        void RptCatchHistoryProgress(RptStructure percentOfProgress)
        {
            if (indexOfListboxAdd != -1)
            {
                listBoxControl1.Items[indexOfListboxAdd] = $"自動更新股票歷史價格中...網路取得價格:{percentOfProgress.GetProgressOnAllPriceLists}%寫入資料庫:{percentOfProgress.GetProgressOnWrittingDB}%";
                if (percentOfProgress.ErrorStkCode != null)
                {
                    listBoxControl2.Items.Add(percentOfProgress.ErrorStkCode);
                    listBoxControl2.SelectedIndex = listBoxControl1.Items.Count - 1;
                }
            }
        }
        void RptProcessTime(string processTime)
        {
            listBoxControl2.Items.Add(processTime);
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
                    listBoxControl1.Items.Add($"{ls.StkCode} {ls.StkName}");
            }
        }

        private async void UpdateStkBasicallyData_BarButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DBUpdate_barSubItem1.Enabled = false;
            CatchStkData cd = new CatchStkData();
            indexOfListboxAdd = listBoxControl2.Items.Add("股票代碼更新中...");
            //取得更薪資料庫進度
            Progress<int> progress = new Progress<int>(RptStkCodeUpgradeProgress);
            //取得資料庫儲存時間
            Progress<string> processTime = new Progress<string>(RptProcessTime);
            try
            {
                await Task.Run(() => cd.MultiCatch(progress, processTime));
                listBoxControl2.Items.Add("股票代碼更新完成!");
            }
            catch (NullReferenceException nullEx)
            {
                listBoxControl2.Items.Add("股票代碼更新失敗!");
                listBoxControl2.Items.Add(nullEx.Message);
            }
            finally
            {
                DBUpdate_barSubItem1.Enabled = true;
            }
        }

        private async void UpdateStkHistoricalPrice_BarButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //取得所有上市上櫃20年股價並塞入DB內
            DBUpdate_barSubItem1.Enabled = false;
            CatchHistoricalPrice CHP = new CatchHistoricalPrice();
            Progress<RptStructure> historyPriceProgress = new Progress<RptStructure>(RptCatchHistoryProgress);
            indexOfListboxAdd = listBoxControl2.Items.Add("自動更新股票歷史價格中...");
            await Task.Run(() => CHP.UpdatePriceToDB(historyPriceProgress));
            listBoxControl1.Items.Add("自動更新股票歷史價格完成!");
            DBUpdate_barSubItem1.Enabled = true;
        }

        private void ListBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            chartControl1.Series.Clear();
            string[] stkCodeName = listBoxControl1.SelectedItem.ToString().Split(' ');
            var stkID = stkCodeName[0];
            using (StockEntities se = new StockEntities())
            {
                //取得listbox被選中的股票的歷史價格
                DateTime startTime = DateTime.Now.AddMonths(-5);
                var oneOfStkHistoryPrice = (from   oneOfStkPrice in se.StockHistoricalPrice
                                            where  oneOfStkPrice.StkCode == stkID
                                            where  oneOfStkPrice.Date.CompareTo(startTime) > 0
                                            select oneOfStkPrice).ToList();
                //設定price candle stick顏色
                CandleStickSeriesView priceSeriesView = (CandleStickSeriesView)stockChart.Series["Price"].View;
                priceSeriesView.Color = Color.Red;
                priceSeriesView.ReductionOptions.Color = Color.Green;
                //加入price資料
                stockChart.Series["Price"].DataSource = oneOfStkHistoryPrice;
                stockChart.Series["Price"].ArgumentDataMember = "Date";
                stockChart.Series["Price"].ValueDataMembers.AddRange(new string[] { "LowPrice", "HighPrice", "OpenPrice", "ClosePrice" });
                //加入volume資料
                stockChart.Series["Volume"].DataSource = oneOfStkHistoryPrice;
                stockChart.Series["Volume"].ArgumentDataMember = "Date";
                stockChart.Series["Volume"].ValueDataMembers.AddRange(new string[] { "Volume" });
            }
        }
    }
}
