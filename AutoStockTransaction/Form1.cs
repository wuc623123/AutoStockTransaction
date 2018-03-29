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
            //主要功能：取得所有上市上櫃20年股價並塞入DB內
            自動更新股票歷史價格ToolStripMenuItem.Enabled = false;
            更新股票代碼ToolStripMenuItem.Enabled = false;
            CatchHistoricalPrice CHP = new CatchHistoricalPrice();
            Progress<RptStructure> historyPriceProgress = new Progress<RptStructure>(RptCatchHistoryProgress);
            indexOfListboxAdd = listBox1.Items.Add("自動更新股票歷史價格中...");
            await Task.Run(() => CHP.UpdatePriceToDB(historyPriceProgress));
            listBox1.Items.Add("自動更新股票歷史價格完成!");
            自動更新股票歷史價格ToolStripMenuItem.Enabled = true;
            更新股票代碼ToolStripMenuItem.Enabled = true;
        }
        void RptStkCodeUpgradeProgress(int percentOfProgress)
        {
            if (indexOfListboxAdd != -1)
            {
                listBox1.Items[indexOfListboxAdd] = $"股票代碼更新中...{percentOfProgress}%";
            }
        }
        void RptCatchHistoryProgress(RptStructure percentOfProgress)
        {
            if (indexOfListboxAdd != -1)
            {
                listBox1.Items[indexOfListboxAdd] = $"自動更新股票歷史價格中...網路取得價格:{percentOfProgress.GetProgressOnAllPriceLists}%寫入資料庫:{percentOfProgress.GetProgressOnWrittingDB}%";
                if (percentOfProgress.ErrorStkCode != null)
                {
                    listBox1.Items.Add(percentOfProgress.ErrorStkCode);
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                }
            }
        }
        void RptProcessTime(string processTime)
        {
            listBox1.Items.Add(processTime);
        }

        private async void 技術分析ToolStripMenuItem_ClickAsync(object sender, EventArgs e)
        {
            TechAnalysis ta = new TechAnalysis();
            var result = await ta.GetAllAnalysis();
            foreach (AnalyzableTick<decimal?> at in result)
            {
                if (at.Tick != null)
                {
                    listBox1.Items.Add($"{at.DateTime.Value}  {Math.Round(at.Tick.Value, 2)}");
                }
            }
        }
    }
}
