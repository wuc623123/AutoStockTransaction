using DevExpress.XtraEditors;
using System.Collections.Generic;

namespace AutoStockTransaction
{
    public interface IMsgDisplayable
    {
        ListBoxControl ListBoxObject { get; set; }
        string[] MsgFormat { get; set; }
        List<int> IndexOfListBox { get; set; }
        void DisplayMsgToUserCtrl(object msg);
    }
}