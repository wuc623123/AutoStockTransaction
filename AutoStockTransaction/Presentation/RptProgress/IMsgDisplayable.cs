using DevExpress.XtraEditors;
using System.Collections.Generic;

namespace AutoStockTransaction
{
    interface IMsgDisplayable
    {
        ListBoxControl ListBoxObject { get; set; }
        string[] MsgFormat { get; set; }
        List<int> IndexOfListbox { get; set; }
        void DispMsgToUserCtrl(object msg);
    }
}