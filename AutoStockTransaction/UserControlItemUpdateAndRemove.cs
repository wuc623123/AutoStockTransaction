using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AutoStockTransaction
{
    public class UserControlItemUpdateAndRemove
    {
        public void RemoveListBoxCotrolItem(object listBox, object buttonItem, NotifyIcon notifyIcon)
        {
            if (((BarSubItem)buttonItem).Enabled == true)
            {   //須先將items轉換乘list,避免修改正在執行的items迴圈
                var selectedItems = ((ListBoxControl)listBox).SelectedItems.ToList();
                foreach (var item in selectedItems)
                {
                    ((ListBoxControl)listBox).Items.Remove(item);
                }
            }
            else
            {
                notifyIcon.Icon = SystemIcons.Warning;
                notifyIcon.BalloonTipText = "不可在更新執行中刪除狀態列表的訊息,請等待更新執行完成.";
                notifyIcon.ShowBalloonTip(2000);
            }
        }
        public void UpdateListBoxControlItem(object listBox,object stateBox, NotifyIcon notifyIcon)
        {
            //因為股票代碼更新會影響資料與資料庫的批配性,所以此更新stocklistbox功能必須移出成獨立方法,並隨時調用
            DataBaseReadWrite stockDataType = new DataBaseReadWrite();
            List<ListedStock> stkCodeAndNameList = stockDataType.GetStkCodeAndNameWithCat(1);
            ((ListBoxControl)listBox).Items.Clear();
            foreach (ListedStock ls in stkCodeAndNameList)
                ((ListBoxControl)listBox).Items.Add($"{ls.StkCode} {ls.StkName}");
            ((ListBoxControl)stateBox).Items.Add("股票代碼列表與資料庫同步完成!");
        }
    }
}