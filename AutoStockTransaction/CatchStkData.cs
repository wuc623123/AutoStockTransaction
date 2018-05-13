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
        private int RptCounts = 0;
        private List<StockData> stockDataCollection = new List<StockData>();
        RptStructure rs = new RptStructure();
        Stopwatch RST = new Stopwatch();
        Stopwatch AST = new Stopwatch();
        Stopwatch DataTransformTime = new Stopwatch();
        Stopwatch CatchTime = new Stopwatch();
        Stopwatch WrittingDBTime = new Stopwatch();

        public void MultiCatch(IProgress<RptStructure> progress)
        {
            Initialization init = new Initialization();
            CatchTime.Start();
            foreach (string strUrl in init.ListStkUrl)
            {
                Catch(strUrl, progress);
            }
            CatchTime.Stop();
            WrittingDBTime.Start();
            WriteToDB();
            WrittingDBTime.Stop();
            rs.ProcessTime = new List<string>
            {
                $"CatchTime:{CatchTime.ElapsedMilliseconds}ms",
                $"WrittingDBTime:{WrittingDBTime.ElapsedMilliseconds}ms",
                $"DataTransformTime:{DataTransformTime.ElapsedMilliseconds}ms",
                $"BulkSaveChanges(Remove):{RST.ElapsedMilliseconds}ms",
                $"BulkSaveChanges(Add):{AST.ElapsedMilliseconds}ms"
            };
            rs.CompletedMsg = "更新股票代碼完成!";
            progress.Report(rs);

        }

        /// <summary>
        /// 取得指定網頁的所有表格資訊，此方法必須要先執行
        /// </summary>
        public void Catch(string strUrl,IProgress<RptStructure> progress)
        {
            HtmlAgilityPack.HtmlDocument docLoad = new HtmlAgilityPack.HtmlDocument();
            HtmlAgilityPack.HtmlDocument storeParentNode = new HtmlAgilityPack.HtmlDocument();
            //docLoad.Load(@"D:\test.htm");
            docLoad.Load(strUrl);
            try
            {
                storeParentNode.LoadHtml(docLoad.DocumentNode.SelectSingleNode("/html/body/table[2]/tbody").InnerHtml);
            }
            catch (NullReferenceException nullEx)
            {
                //取得網頁資料失敗
                MessageBox.Show($"取得網頁資料遭遇錯誤，可能網頁有更動，造成資料欄位取得失敗。\r\n請聯絡工程師處理\r\n{nullEx.Message}", "Html Data Catch Error!");
                throw nullEx;
            }
            //取得成功後開始擷取
            BuildingDataStructure(storeParentNode, progress);
        }
        /// <summary>
        /// 將資料一一的寫入到StockData內
        /// </summary>
        /// <param name="htmldocParent">The htmldoc parent.</param>
        public void BuildingDataStructure(HtmlAgilityPack.HtmlDocument htmldocParent,IProgress<RptStructure> progress)
        {
            int InputCategory = 0;
            int row = 2;
            float totalColumnCount;
            //取得第一行資料，後續依照這一行一個一個填入列資料
            HtmlNodeCollection columnOfData = htmldocParent.DocumentNode.SelectNodes("./tr[position()>1]/td[1]");
            totalColumnCount = columnOfData.Count;
            foreach (HtmlNode oneOfColumnData in columnOfData)
            {
                //取得第row列資料的集合，列如第1行和2,3,4,5,6行的資料合併
                HtmlNodeCollection rowOfData = htmldocParent.DocumentNode.SelectNodes($"/tr[{row}]/td[position()>1]");
                //取得的資料如果是分類資訊，則將分類記錄下來存在InputCategory，並在之後取得的資料都以記錄下來的分類進行分類。
                string compareData = oneOfColumnData.InnerText.Trim();
                switch (compareData)
                {
                    //上市分類
                    case "股票":
                        InputCategory = (int)StockData.StkCategory.Stock;
                        break;

                    case "上市認購(售)權證":
                        InputCategory = (int)StockData.StkCategory.ListedWarrant;
                        break;

                    case "臺灣存託憑證(TDR)":
                        InputCategory = (int)StockData.StkCategory.TaiwanDepositaryReceipt;
                        break;

                    case "受益證券-不動產投資信託":
                        InputCategory = (int)StockData.StkCategory.RealEstateInvestmentTrust;
                        break;

                    case "特別股":
                        InputCategory = (int)StockData.StkCategory.SpecialStock;
                        break;
                    //上櫃分類
                    case "管理股票":
                        InputCategory = (int)StockData.StkCategory.ManageStocks;
                        break;

                    case "上櫃認購(售)權證":
                        InputCategory = (int)StockData.StkCategory.ListedWarrant;
                        break;

                    case "ETF":
                        InputCategory = (int)StockData.StkCategory.ETF;
                        break;

                    case "臺灣存託憑證":
                        InputCategory = (int)StockData.StkCategory.TaiwanDepositaryReceipt;
                        break;

                    case "受益證券-資產基礎證券":
                        InputCategory = (int)StockData.StkCategory.AssetBasedSecurities;
                        break;

                    default:
                        //如果取得的資料不是分類資訊，就會在這裡以StockData的類別進行資料儲存。
                        string id;
                        string name;
                        int rowNumber = 4;
                        //因第一行包含ID與NAME，在這裡將他們分開
                        string[] text = compareData.Split('　');
                        id = text[0].Trim();
                        name = text[1].Trim();
                        //儲存第一行分開後的資料，ID與NAME
                        StockData sd = new StockData();
                        sd.SetData(InputCategory, (int)StockData.FieldName.StkCategory);
                        sd.SetData(id, (int)StockData.FieldName.StkCode);
                        sd.SetData(name, (int)StockData.FieldName.StkName);
                        //儲存第二行之後的資料
                        foreach (HtmlNode oneOfRowData in rowOfData)
                        {
                            if (rowNumber <= 8)
                            {
                                sd.SetData(oneOfRowData.InnerText, rowNumber);
                                rowNumber++;
                            }
                            else
                            {
                                break;
                            }
                        }
                        //到這裡一列資料已經儲存完畢，並將StockData類別新增到stockDataCollection泛型的類別陣列
                        stockDataCollection.Add(sd);
                        //回報目前處理進度給UI
                        if (rs != null)
                        {
                            int totalPercent = (int)((row - 1) * 100 / totalColumnCount);
                            if (totalPercent != RptCounts)
                            {
                                rs.UpdateStkCodeProgress = totalPercent;
                                RptCounts = totalPercent;
                                progress.Report(rs);
                            }
                        }
                        break;
                }
                //累加到下一列
                row++;
            }
        }

        public void WriteToDB()
        {
            using (StockEntities se = new StockEntities())
            {

                var deleteData = from ls in se.ListedStock
                                 select ls;
                RST.Start();
                se.BulkDelete(deleteData);
                RST.Stop();
                DataTransformTime.Start();
                List<ListedStock> stockList = new List<ListedStock>();
                //將自訂類別轉換成ListedStock類別
                foreach (StockData sd in stockDataCollection)
                {
                    ListedStock stock = new ListedStock
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
                //如果找不到重複的主鍵，便加入到儲存列表中
                List<ListedStock> allStkInDB = (from stk in se.ListedStock
                                                orderby stk.StkCategory, stk.StkCode
                                                select stk).ToList();
                List<ListedStock> neededWrittingStockList = new List<ListedStock>();
                //剔除已經在資料庫內的StkCode
                foreach (ListedStock incomeStk in stockList)
                {
                    Boolean match = false;
                    foreach (ListedStock dbStk in allStkInDB)
                    {
                        if (incomeStk.StkCode == dbStk.StkCode)
                        {
                            match = true;
                        }
                    }
                    if (!match)
                    {
                        neededWrittingStockList.Add(incomeStk);
                    }
                }
                DataTransformTime.Stop();
                AST.Start();
                se.BulkInsert(neededWrittingStockList);
                se.BulkSaveChanges();
                AST.Stop();
            };
        }
    }
}