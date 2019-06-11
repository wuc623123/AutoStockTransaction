using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoStockTransaction
{
    public class CatchStkData
    {
        /// <summary>
        /// The rpt counts.
        /// </summary>
        private int rptCounts = 0;

        /// <summary>
        /// The stock data collection.
        /// </summary>
        private List<StockData> stockDataCollection = new List<StockData>();

        /// <summary>
        /// The Report progress structure.
        /// </summary>
        private RptStructure reportStructure = new RptStructure();

        /// <summary>
        /// The bulk remove stopwatch time.
        /// </summary>
        private Stopwatch bulkRemoveStopwatchTime = new Stopwatch();

        /// <summary>
        /// The bulk add stopwatch time.
        /// </summary>
        private Stopwatch bulkAddStopwatchTime = new Stopwatch();

        /// <summary>
        /// The data transform stopwatch time.
        /// </summary>
        private Stopwatch dataTransformStopwatchTime = new Stopwatch();

        /// <summary>
        /// The catch stopwatch time.
        /// </summary>
        private Stopwatch catchStopwatchTime = new Stopwatch();

        /// <summary>
        /// The writing db stopwatch time.
        /// </summary>
        private Stopwatch writingDbStopwatchTime = new Stopwatch();

        /// <summary>Multis the catch.</summary>
        /// <param name="progress">The progress.</param>
        public void MultiCatch(IProgress<RptStructure> progress)
        {
            var init = new Initialization();
            this.catchStopwatchTime.Start();
            foreach (var strUrl in init.ListStkUrl)
            {
                this.Catch(strUrl, progress);
            }

            this.catchStopwatchTime.Stop();
            this.writingDbStopwatchTime.Start();
            this.WriteToDb();
            this.writingDbStopwatchTime.Stop();
            this.reportStructure.ProcessTime = new List<string>
                                 {
                                     $"CatchTime:{this.catchStopwatchTime.ElapsedMilliseconds}ms",
                                     $"WritingDBTime:{this.writingDbStopwatchTime.ElapsedMilliseconds}ms",
                                     $"DataTransformTime:{this.dataTransformStopwatchTime.ElapsedMilliseconds}ms",
                                     $"BulkSaveChanges(Remove):{this.bulkRemoveStopwatchTime.ElapsedMilliseconds}ms",
                                     $"BulkSaveChanges(Add):{this.bulkAddStopwatchTime.ElapsedMilliseconds}ms"
                                 };
            this.reportStructure.CompletedMsg = "更新股票代碼完成!";
            progress.Report(this.reportStructure);
        }

        /// <summary>
        /// 取得指定網頁的所有表格資訊，此方法必須要先執行
        /// </summary>
        /// <param name="strUrl">
        /// The str Url.
        /// </param>
        /// <param name="progress">
        /// The progress.
        /// </param>
        public void Catch(string strUrl, IProgress<RptStructure> progress)
        {
            var docLoad = new HtmlAgilityPack.HtmlDocument();
            var storeParentNode = new HtmlAgilityPack.HtmlDocument();

            // docLoad.Load(@"D:\test.htm");
            docLoad.Load(strUrl);
            try
            {
                storeParentNode.LoadHtml(docLoad.DocumentNode.SelectSingleNode("/html/body/table[2]/tbody").InnerHtml);
            }
            catch (NullReferenceException nullEx)
            {
                // 取得網頁資料失敗
                MessageBox.Show(
                    $"取得網頁資料遭遇錯誤，可能網頁有更動，造成資料欄位取得失敗。\r\n請與工程師聯繫\r\n{nullEx.Message}",
                    "Html Data Catch Error!");
                throw;
            }

            // 取得成功後開始擷取
            this.BuildingDataStructure(storeParentNode, progress);
        }

        /// <summary>
        /// 將資料一一的寫入到StockData內
        /// </summary>
        /// <param name="htmldocParent">
        /// The htmldoc parent.
        /// </param>
        /// <param name="progress">
        /// The progress.
        /// </param>
        public void BuildingDataStructure(HtmlAgilityPack.HtmlDocument htmldocParent, IProgress<RptStructure> progress)
        {
            var currentCategory = 0;
            var row = 2;

            // 取得第一行資料，後續依照這一行一個一個填入列資料
            var columnOfData = htmldocParent.DocumentNode.SelectNodes("./tr[position()>1]/td[1]");
            var totalColumnCount = columnOfData.Count;
            foreach (var oneOfColumnData in columnOfData)
            {
                // 取得第row列資料的集合，列如第1行和2,3,4,5,6行的資料合併
                var rowOfData = htmldocParent.DocumentNode.SelectNodes($"/tr[{row}]/td[position()>1]");

                // 取得的資料如果是分類資訊，則將分類記錄下來存在InputCategory，並在之後取得的資料都以記錄下來的分類進行分類。
                var compareData = oneOfColumnData.InnerText.Trim();
                switch (compareData)
                {
                    // 上市分類
                    case "股票":
                        currentCategory = (int)StockData.StkCategory.Stock;
                        break;

                    case "上市認購(售)權證":
                        currentCategory = (int)StockData.StkCategory.ListedWarrant;
                        break;

                    case "臺灣存託憑證(TDR)":
                        currentCategory = (int)StockData.StkCategory.TaiwanDepositaryReceipt;
                        break;

                    case "受益證券-不動產投資信託":
                        currentCategory = (int)StockData.StkCategory.RealEstateInvestmentTrust;
                        break;

                    case "特別股":
                        currentCategory = (int)StockData.StkCategory.SpecialStock;
                        break;

                    // 上櫃分類
                    case "管理股票":
                        currentCategory = (int)StockData.StkCategory.ManageStocks;
                        break;

                    case "上櫃認購(售)權證":
                        currentCategory = (int)StockData.StkCategory.ListedWarrant;
                        break;

                    case "ETF":
                        currentCategory = (int)StockData.StkCategory.ETF;
                        break;

                    case "臺灣存託憑證":
                        currentCategory = (int)StockData.StkCategory.TaiwanDepositaryReceipt;
                        break;

                    case "受益證券-資產基礎證券":
                        currentCategory = (int)StockData.StkCategory.AssetBasedSecurities;
                        break;

                    default:
                        // 如果取得的資料不是分類資訊，就會在這裡以StockData的類別進行資料儲存。
                        var rowNumber = 4;

                        // 因第一欄包含ID與NAME，在這裡將他們分開
                        var text = compareData.Split('　');
                        var id = text[0].Trim();
                        var name = text[1].Trim();

                        // 儲存第一欄分開後的資料，ID與NAME
                        var stockData = new StockData();
                        stockData.SetData(currentCategory, (int)StockData.FieldName.StkCategory);
                        stockData.SetData(id, (int)StockData.FieldName.StkCode);
                        stockData.SetData(name, (int)StockData.FieldName.StkName);

                        // 儲存第二欄之後的資料
                        foreach (var oneOfRowData in rowOfData)
                        {
                            if (rowNumber <= 8)
                            {
                                stockData.SetData(oneOfRowData.InnerText, rowNumber);
                                rowNumber++;
                            }
                            else
                            {
                                break;
                            }
                        }

                        // 到這裡一列資料已經儲存完畢，並將StockData類別新增到stockDataCollection泛型的類別陣列
                        this.stockDataCollection.Add(stockData);

                        // 回報目前處理進度給UI
                        if (this.reportStructure != null)
                        {
                            var totalPercent = (int)((row - 1) * 100 / totalColumnCount);
                            if (totalPercent != this.rptCounts)
                            {
                                this.reportStructure.UpdateStkCodeProgress = totalPercent;
                                this.rptCounts = totalPercent;
                                progress.Report(this.reportStructure);
                            }
                        }

                        break;
                }

                // 累加到下一列
                row++;
            }
        }

        /// <summary>Writes to database.</summary>
        public void WriteToDb()
        {
            using (var se = new StockEntities())
            {
                var deleteData = from ls in se.ListedStock select ls;
                bulkRemoveStopwatchTime.Start();
                se.BulkDelete(deleteData);
                bulkRemoveStopwatchTime.Stop();
                dataTransformStopwatchTime.Start();
                var stockList = new List<ListedStock>();

                // 將自訂類別轉換成ListedStock類別
                foreach (var sd in this.stockDataCollection)
                {
                    var stock = new ListedStock
                                            {
                                                StkCategory = sd.IntStkCategory,
                                                StkCode = sd.StrStkCode,
                                                StkName = sd.StrStkName,
                                                ISIN_code = sd.StrISIN_code,
                                                SubmitDate = sd.StrSubmitDate,
                                                MarketNo = sd.StrMarketNo,
                                                BelongClass = sd.StrBelongClass,
                                                CFI_code = sd.StrCFI_code
                                            };
                    stockList.Add(stock);
                }

                // 如果找不到重複的主鍵，便加入到儲存列表中
                var allStkInDb = (from stk in se.ListedStock
                                                orderby stk.StkCategory, stk.StkCode
                                                select stk).ToList();
                var neededWritingStockList = new List<ListedStock>();

                // 剔除已經在資料庫內的StkCode
                foreach (var incomeStk in stockList)
                {
                    var match = false;
                    foreach (var dbStk in allStkInDb)
                    {
                        if (incomeStk.StkCode == dbStk.StkCode)
                        {
                            match = true;
                        }
                    }

                    if (!match)
                    {
                        neededWritingStockList.Add(incomeStk);
                    }
                }

                dataTransformStopwatchTime.Stop();
                bulkAddStopwatchTime.Start();
                se.BulkInsert(neededWritingStockList);
                se.BulkSaveChanges();
                bulkAddStopwatchTime.Stop();
            }

            ;
        }
    }
}