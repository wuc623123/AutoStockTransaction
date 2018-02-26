using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoStockTransaction
{
    public partial class Form1 : Form
    {
        int indexOfListboxAdd;
        public Form1()
        {
            InitializeComponent();
        }

        private async void 更新股票代碼ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            更新股票代碼ToolStripMenuItem.Enabled = false;
            CatchStkData cd = new CatchStkData();
            indexOfListboxAdd = listBox1.Items.Add("股票代碼更新中...");
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
            }
        }

        private void 自動更新股票歷史價格ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (StockEntities test = new StockEntities())
            {
                ListedStock stock = new ListedStock()
                {
                    StkCode = "w8743219",
                    StkCategory = 1,
                    ISIN_code = "64546",
                    CFI_code = "646546",
                    BelongClass = "65465",
                    MarketNo = "6465",
                    StkName = "test",
                    SubmitDate = System.DateTime.Now.ToString()
                };
                test.ListedStock.Add(stock);
                test.SaveChanges();
                listBox1.Items.Add("完成");
            };
        }
        void RptStkCodeUpgradeProgress(int percentOfProgress)
        {
            if (indexOfListboxAdd != -1)
            {
                listBox1.Items[indexOfListboxAdd] = $"股票代碼更新中...{percentOfProgress}%";
            }
        }
        void RptProcessTime(string processTime)
        {
            listBox1.Items.Add(processTime);
        }
    }
}
