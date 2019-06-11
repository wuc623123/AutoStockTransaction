using DevExpress.XtraEditors;

using System.Collections.Generic;

namespace AutoStockTransaction
{
    using System.Globalization;

    internal class ShowReportProgressMsg : IMsgDisplayable
    {
        /// <summary>
        /// Gets or sets the list box object.
        /// </summary>
        public ListBoxControl ListBoxObject { get; set; }

        public string[] MsgFormat { get; set; }

        public List<int> IndexOfListBox { get; set; }

        public ShowReportProgressMsg(ListBoxControl ListBoxObject, string[] msgFormat)
        {
            this.ListBoxObject = ListBoxObject;
            this.IndexOfListBox = new List<int>();
            this.MsgFormat = msgFormat;
        }

        public void DisplayMsgToUserCtrl(object msg)
        {
            var rs = msg as RptStructure;
            var completeOutputMsg = this.EmbedMsgInFormat(msg, this.MsgFormat);

            // 在這個progress中,沒資料時先新增一次訊息
            if (this.IndexOfListBox.Count == 0)
            {
                // 將N陣列塞入進listbox2中,並取回N個索引值
                foreach (var oneStringOfMsg in completeOutputMsg)
                {
                    this.IndexOfListBox.Add(this.ListBoxObject.Items.Add(oneStringOfMsg));
                }
            }
            else
            {
                // 當有資料時將覆蓋N個listbox的N個items
                var indexOfCompleteOutput = 0;
                foreach (var index in this.IndexOfListBox)
                {
                    this.ListBoxObject.Items[index] = completeOutputMsg[indexOfCompleteOutput];
                    indexOfCompleteOutput++;
                }
            }

            if (rs?.ErrorStkCode != null)
            {
                // ListBoxObject.SelectedIndex = ListBoxObject.Items.Count - 1;
                this.ListBoxObject.Items.Add(rs.ErrorStkCode);
            }

            if (rs?.ProcessTime != null)
            {
                foreach (var time in rs.ProcessTime)
                {
                    // ListBoxObject.SelectedIndex = ListBoxObject.Items.Count - 1;
                    this.ListBoxObject.Items.Add(time);
                }
            }

            if (rs?.CompletedMsg != null)
            {
                this.ListBoxObject.Items.Add(rs.CompletedMsg);
            }
        }

        public string[] EmbedMsgInFormat(object msg, string[] msgFormat)
        {
            var rs = msg as RptStructure;
            var tempMsgFormat = new List<string>();
            foreach (var oneOfMsg in msgFormat)
            {
                if (oneOfMsg == null || rs == null) continue;

                if (oneOfMsg.Contains("UpdateStkCodeProgress"))
                {
                    tempMsgFormat.Add(
                        oneOfMsg.Replace("UpdateStkCodeProgress", rs.UpdateStkCodeProgress.ToString(CultureInfo.InvariantCulture)));
                }
                else if (oneOfMsg.Contains("UpdatePriceListsProgress"))
                {
                    tempMsgFormat.Add(
                        oneOfMsg.Replace("UpdatePriceListsProgress", rs.UpdatePriceListsProgress.ToString(CultureInfo.InvariantCulture)));
                }
                else if (oneOfMsg.Contains("WritingDBProgress"))
                {
                    tempMsgFormat.Add(oneOfMsg.Replace("WritingDBProgress", rs.WrittingDBProgress.ToString(CultureInfo.InvariantCulture)));
                }
                else if (oneOfMsg.Contains("ProcessTime"))
                {
                    tempMsgFormat.Add(oneOfMsg.Replace("ProcessTime", rs.ProcessTime.ToString()));
                }
                else
                {
                    tempMsgFormat.Add(oneOfMsg);
                }
            }

            return tempMsgFormat.ToArray();
        }
    }
}