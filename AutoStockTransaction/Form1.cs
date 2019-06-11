using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Trady.Analysis.Extension;
using Trady;
using Trady.Analysis.Indicator;
using Trady.Core;

namespace AutoStockTransaction
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UserControlItemUpdateAndRemove userControlItemUpdateAndRemove = new UserControlItemUpdateAndRemove();
            userControlItemUpdateAndRemove.UpdateListBoxControlItem(stockListBox, stateListBox, notifyIcon1);
        }

        private async void UpdateStkBasicallyData_BarButtonItem1_ItemClick(
            object sender,
            DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DBUpdate_barSubItem1.Enabled = false;
            CatchStkData cd = new CatchStkData();
            string[] msgFormat = { "更新股票代碼中...UpdateStkCodeProgress%" };
            ShowReportProgressMsg srpm = new ShowReportProgressMsg(stateListBox, msgFormat);
            //取得更新資料庫進度
            Progress<RptStructure> progress = new Progress<RptStructure>(srpm.DisplayMsgToUserCtrl);
            try
            {
                await Task.Run(() => cd.MultiCatch(progress));
            }
            catch (NullReferenceException nullEx)
            {
                stateListBox.Items.Add("股票代碼更新失敗!");
                stateListBox.Items.Add(nullEx.Message);
            }

            UserControlItemUpdateAndRemove userControlItemUpdateAndRemove = new UserControlItemUpdateAndRemove();
            userControlItemUpdateAndRemove.UpdateListBoxControlItem(stockListBox, stateListBox, notifyIcon1);
            DBUpdate_barSubItem1.Enabled = true;
        }

        private async void UpdateStkHistoricalPrice_BarButtonItem2_ItemClick(
            object sender,
            DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //取得所有上市上櫃20年股價並塞入DB內
            DBUpdate_barSubItem1.Enabled = false;
            CatchHistoricalPrice CHP = new CatchHistoricalPrice();
            string[] msgFormat = { "更新股票歷史價格中...", "網路取得價格:UpdatePriceListsProgress%", "寫入資料庫:WrittingDBProgress%" };
            ShowReportProgressMsg srpm = new ShowReportProgressMsg(stateListBox, msgFormat);
            Progress<RptStructure> historyPriceProgress = new Progress<RptStructure>(srpm.DisplayMsgToUserCtrl);
            await Task.Run(() => CHP.UpdatePriceToDB(historyPriceProgress));
            DBUpdate_barSubItem1.Enabled = true;
        }

        private void ListBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (stockListBox.SelectedItem != null)
            {
                //須修改為正規取值
                string[] stkCodeName = stockListBox.SelectedItem.ToString().Split(' ');
                var stkCode = stkCodeName[0];
                //取得listbox被選中的股票的歷史價格
                TechnicalAnalysis technicalAnalysis = new TechnicalAnalysis();
                var oneOfStkHistoryPrice = technicalAnalysis.GetIntervalPriceList(stkCode);
                var oneOfStkIndicator = technicalAnalysis.GetIndicator(
                    oneOfStkHistoryPrice,
                    StockTypeSettings.Indicator);
                StockChart Chart = new StockChart();
                Chart.DrawingStockChart(stockChart, oneOfStkHistoryPrice);
                foreach (var item in oneOfStkIndicator)
                {
                    showIndicator.Items.Add(item);
                }
            }
        }

        private void StateBoxPanel_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            //更新不動作才可以清除狀態欄內的資料
            UserControlItemUpdateAndRemove userControlItemRemove = new UserControlItemUpdateAndRemove();
            userControlItemRemove.RemoveListBoxCotrolItem(stateListBox, DBUpdate_barSubItem1, notifyIcon1);
        }

        private void EditValueChanged(object sender, EventArgs e)
        {
            if (startDateBarEditItem.EditValue != null)
            {
                StockTypeSettings.StartDate = (DateTime)startDateBarEditItem.EditValue;
            }

            if (endDateBarEditItem.EditValue != null)
            {
                StockTypeSettings.EndDate = (DateTime)endDateBarEditItem.EditValue;
            }

            if (intervalBarEditItem.Edit.ToString() != null)
            {
                StockTypeSettings.Interval = (string)intervalBarEditItem.EditValue;
            }

            if (IndicatorBarEditItem.Edit.ToString() != null)
            {
                StockTypeSettings.Indicator = (string)IndicatorBarEditItem.EditValue;
            }

            ListBoxControl1_SelectedIndexChanged(stockListBox, null);
        }
    }
}