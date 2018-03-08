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

namespace AutoStockTransaction
{
    
    public partial class Form1 : Form
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
            indexOfListboxAdd = listBox1.Items.Add("股票代碼更新中...");
            //取得更薪資料庫進度
            Progress<int> progress = new Progress<int>(RptStkCodeUpgradeProgress);
            //取得資料庫儲存時間
            Progress<string> processTime = new Progress<string>(RptProcessTime);
            try
            {
                await Task.Run(() => cd.MultiCatch(progress, processTime));
                listBox1.Items.Add("股票代碼更新完成!");
            }
            catch (NullReferenceException nullEx)
            {
                listBox1.Items.Add("股票代碼更新失敗!");
                listBox1.Items.Add(nullEx.Message);
            }
            finally
            {
                更新股票代碼ToolStripMenuItem.Enabled = true;
                自動更新股票歷史價格ToolStripMenuItem.Enabled = true;
            }
        }

        private async void 自動更新股票歷史價格ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //CatchHistoricalPrice CHP = new CatchHistoricalPrice();
            //var l = await CHP.GetHistoricalPrice("1256.TW", DateTime.Now.AddMonths(-5), DateTime.Now.Date);
            //foreach (HistoryPriceExtStkCode a in l)
            //{
            //    listBox1.Items.Add($"{a.Stkcode} + 張：{(int)a.Volume / 1000} 股：{a.Volume % 1000} + {a.Date}");
            //}

            自動更新股票歷史價格ToolStripMenuItem.Enabled = false;
            更新股票代碼ToolStripMenuItem.Enabled = false;
            CatchHistoricalPrice CHP = new CatchHistoricalPrice();
            Progress<ProgressStoreStructure> historyPriceProgress = new Progress<ProgressStoreStructure>(RptCatchHistoryProgress);
            indexOfListboxAdd = listBox1.Items.Add("自動更新股票歷史價格中...");
            await Task.Run(() => CHP.UpdatePriceToDB(historyPriceProgress));
            listBox1.Items.Add("自動更新股票歷史價格完成!");
            自動更新股票歷史價格ToolStripMenuItem.Enabled = true;
            更新股票代碼ToolStripMenuItem.Enabled = true;

            //var iii = GetAllNeededAddStkCode();
            //foreach(ListedStock a in iii)
            //{
            //    listBox1.Items.Add(a.StkCode);
            //}
        }
        void RptStkCodeUpgradeProgress(int percentOfProgress)
        {
            if (indexOfListboxAdd != -1)
            {
                listBox1.Items[indexOfListboxAdd] = $"股票代碼更新中...{percentOfProgress}%";
            }
        }
        void RptCatchHistoryProgress(ProgressStoreStructure percentOfProgress)
        {
            if (indexOfListboxAdd != -1)
            {
                listBox1.Items[indexOfListboxAdd] = $"自動更新股票歷史價格中...網路取得價格:{percentOfProgress.GetProgressOnAllPriceLists}%寫入資料庫:{percentOfProgress.GetProgressOnWrittingDB}%";
                if(percentOfProgress.ErrorStkCode != null)
                {
                    listBox1.Items.Add(percentOfProgress.ErrorStkCode);
                }
            }
        }
        void RptProcessTime(string processTime)
        {
            listBox1.Items.Add(processTime);
        }
        //private List<ListedStock> GetAllNeededAddStkCode()
        //{
        //    using (StockEntities SE = new StockEntities())
        //    {
        //        var uuu = from a in SE.StockHistoricalPrice
        //                  join b in SE.ListedStock
        //                  on new { a.StkCode } equals new { b.StkCode } into ab
        //                  from abcom in ab.DefaultIfEmpty()
        //                  select abcom;
        //        //取得stkCodeListInCat1除了stockPriceList的差集, 結果為沒有任何價格紀錄需要被新增的集合
        //        return uuu.ToList();
        //    }
        //}
    }
}
