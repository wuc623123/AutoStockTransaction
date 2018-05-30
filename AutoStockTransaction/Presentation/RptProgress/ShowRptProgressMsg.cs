using DevExpress.XtraEditors;
using System.Collections.Generic;

namespace AutoStockTransaction
{
    internal class ShowRptProgressMsg : IMsgDisplayable
    {
        public ListBoxControl ListBoxObject { get; set; }
        public string[] MsgFormat { get; set; }
        
        public List<int> IndexOfListbox { get; set; }

        public ShowRptProgressMsg(ListBoxControl ListBoxObject, string[] msgFormat)
        {
            this.ListBoxObject = ListBoxObject;
            IndexOfListbox = new List<int>();
            this.MsgFormat = msgFormat;
        }
        public void DispMsgToUserCtrl(object msg)
        {
            RptStructure rs = msg as RptStructure;
            string[] completeOutputMsg = EmbedMsgInFormat(msg, MsgFormat);
            //在這個progress中,沒資料時先新增一次訊息
            if (IndexOfListbox.Count == 0)
            {   //將N陣列塞入進listbox2中,並取回N個索引值
                foreach (var oneStringOfMsg in completeOutputMsg)
                {
                    IndexOfListbox.Add(ListBoxObject.Items.Add(oneStringOfMsg));
                }
            }
            else//當有資料時將覆蓋N個listbox的N個items
            {
                int indexOfCompleteOutput = 0;
                foreach (int index in IndexOfListbox)
                {
                    ListBoxObject.Items[index] = completeOutputMsg[indexOfCompleteOutput];
                    indexOfCompleteOutput++;
                }
            }
            if (rs.ErrorStkCode != null)
            {
                ListBoxObject.Items.Add(rs.ErrorStkCode);
                //ListBoxObject.SelectedIndex = ListBoxObject.Items.Count - 1;
            }
            if (rs.ProcessTime != null)
            {
                foreach (var time in rs.ProcessTime)
                {
                    ListBoxObject.Items.Add(time);
                    //ListBoxObject.SelectedIndex = ListBoxObject.Items.Count - 1;
                }
            }
            if (rs.CompletedMsg != null)
            {
                ListBoxObject.Items.Add(rs.CompletedMsg);
            }
        }
        public string[] EmbedMsgInFormat(object msg, string[] msgFormat)
        {
            RptStructure rs = msg as RptStructure;
            List<string> tempMsgFormat = new List<string>();
            for (int i = 0; i < msgFormat.Length; i++)
            {
                if (msgFormat[i].Contains("UpdateStkCodeProgress"))
                {
                    tempMsgFormat.Add(msgFormat[i].Replace("UpdateStkCodeProgress", rs.UpdateStkCodeProgress.ToString()));
                }
                else if (msgFormat[i].Contains("UpdatePriceListsProgress"))
                {
                    tempMsgFormat.Add(msgFormat[i].Replace("UpdatePriceListsProgress", rs.UpdatePriceListsProgress.ToString()));
                }
                else if (msgFormat[i].Contains("WrittingDBProgress"))
                {
                    tempMsgFormat.Add(msgFormat[i].Replace("WrittingDBProgress", rs.WrittingDBProgress.ToString()));
                }
                else if (msgFormat[i].Contains("ProcessTime"))
                {
                    tempMsgFormat.Add(msgFormat[i].Replace("ProcessTime", rs.ProcessTime.ToString()));
                }
                else
                {
                    tempMsgFormat.Add(msgFormat[i]);
                }
            }
            return tempMsgFormat.ToArray();
        }
    }
}