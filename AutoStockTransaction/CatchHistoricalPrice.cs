using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YahooFinanceAPI;
using YahooFinanceAPI.Models;



namespace AutoStockTransaction
{
    class CatchHistoricalPrice
    {
        int totalStkAmount = 0;
        int errorStkAmount = 0;
        public async Task UpdatePriceToDB(IProgress<ProgressStoreStructure> progress)
        {
            //取得stkcode差集列表
            List<string> neededUpdateList = await GetAllNeededAddStkCode();
            //取得所有需要新增的股票價格列表
            ConcurrentStack<List<HistoryPriceExtStkCode>> allStkPrice = await GetAllStkPrice(neededUpdateList, progress);
            using (StockEntities se = new StockEntities())
            {
                se.Configuration.AutoDetectChangesEnabled = false;
                int index = 0;
                foreach (List<HistoryPriceExtStkCode> priceOfList in allStkPrice)
                {
                    var RptProgress = new ProgressStoreStructure();

                    List<StockHistoricalPrice> transToSHP = new List<StockHistoricalPrice>();
                    foreach (HistoryPriceExtStkCode aHistoryPrice in priceOfList)
                    {
                        StockHistoricalPrice SHP = new StockHistoricalPrice()
                        {
                            StkCode = aHistoryPrice.Stkcode,
                            Date = aHistoryPrice.Date,
                            OpenPrice = aHistoryPrice.Open,
                            HighPrice = aHistoryPrice.High,
                            LowPrice = aHistoryPrice.Low,
                            ClosePrice = aHistoryPrice.Close,
                            AdjustedClosePrice = aHistoryPrice.AdjClose,
                            Volume = aHistoryPrice.Volume
                        };
                        transToSHP.Add(SHP);
                    }
                    se.BulkInsert<StockHistoricalPrice>(transToSHP);
                    RptProgress.GetProgressOnWrittingDB = (float) index++ / allStkPrice.Count;
                    progress.Report(RptProgress);
                }
            }
        }
        /// <summary>
        /// Gets all needed add STK code.
        /// </summary>
        /// <returns></returns>
        private async Task<List<string>> GetAllNeededAddStkCode()
        {
            using (StockEntities SE = new StockEntities())
            {
                //SE.Configuration.ProxyCreationEnabled = true;
                //SE.Configuration.LazyLoadingEnabled = true;
                //取得category = 1所有項目的列表
                IQueryable<string> stkCodeListInCat1 = from s in SE.ListedStock

                                                       where s.StkCategory == 1
                                                       select s.StkCode;
                ////取得股票價格所有項目的列表
                IQueryable<string> stockPriceList = from a in SE.StockHistoricalPrice
                                                    select a.StkCode;
                IQueryable<string> exceptList = stkCodeListInCat1.Except(stockPriceList.Distinct());
                //取得stkCodeListInCat1除了stockPriceList的差集, 結果為沒有任何價格紀錄需要被新增的集合
                totalStkAmount = exceptList.Count();
                return exceptList.ToList();
            }
        }
        /// <summary>
        /// Gets all STK price.
        /// </summary>
        /// <param name="neededUpdateList">The needed update list.</param>
        /// <returns></returns>
        private async Task<ConcurrentStack<List<HistoryPriceExtStkCode>>> GetAllStkPrice(List<string> neededUpdateList, IProgress<ProgressStoreStructure> progress)
        {
            ConcurrentStack<List<HistoryPriceExtStkCode>> allStkPrice = new ConcurrentStack<List<HistoryPriceExtStkCode>>();
            await Task.Run(() =>
            {

                Parallel.ForEach(neededUpdateList, new ParallelOptions { MaxDegreeOfParallelism = 3 }, aStk =>
                {
                    var RptState = new ProgressStoreStructure();
                    List<HistoryPriceExtStkCode> aHistoryPriceList = GetHistoricalPrice(aStk + ".TW", DateTime.Now.AddYears(-20), DateTime.Now).GetAwaiter().GetResult();
                    if (aHistoryPriceList.Count < 1)
                    {
                        List<HistoryPriceExtStkCode> tryCatchHistoryPriceListAgain = GetHistoricalPrice(aStk + ".TWO", DateTime.Now.AddYears(-20), DateTime.Now).GetAwaiter().GetResult();
                        if (tryCatchHistoryPriceListAgain.Count < 1)
                        {
                            errorStkAmount++;
                            RptState.ErrorStkCode = $"{aStk}   {errorStkAmount}";
                        }
                        else
                        {
                            allStkPrice.Push(tryCatchHistoryPriceListAgain);
                        }
                    }
                    else
                    {
                        allStkPrice.Push(aHistoryPriceList);
                    }
                    RptState.GetProgressOnAllPriceLists = (float)(allStkPrice.Count - errorStkAmount) / totalStkAmount * 100;
                    progress.Report(RptState);

                });
            });

            return allStkPrice;
        }
        /// <summary>
        /// Gets the historical price.
        /// </summary>
        /// <param name="symbol">The symbol seen like 2330.TW, but don't need .TW to add.</param>
        /// <param name="start">The start Datetime.</param>
        /// <param name="end">The end Datetime.</param>
        /// <returns></returns>
        public async Task<List<HistoryPriceExtStkCode>> GetHistoricalPrice(string symbol, DateTime start, DateTime end)
        {
            //first get a valid token from Yahoo Finance
            while (string.IsNullOrEmpty(Token.Cookie) || string.IsNullOrEmpty(Token.Crumb))
            {
                await Token.RefreshAsync().ConfigureAwait(false);
            }
            List<HistoryPrice> hps = await Historical.GetPriceAsync(symbol, start, end).ConfigureAwait(false);
            //Historyprice轉換為HistoryPriceExtStkCode類型, 新增StkCode後傳回
            List<HistoryPriceExtStkCode> hpExtList = new List<HistoryPriceExtStkCode>();
            foreach (HistoryPrice hp in hps)
            {
                hpExtList.Add(new HistoryPriceExtStkCode()
                {
                    Stkcode = symbol,
                    Date = hp.Date,
                    Open = hp.Open,
                    High = hp.High,
                    Low = hp.Low,
                    Close = hp.Close,
                    AdjClose = hp.AdjClose,
                    Volume = hp.Volume
                });
            }
            return hpExtList;
        }
    }
    /// <summary>
    /// 擴展StkCode屬性至HistroyPrice類別
    /// </summary>
    /// <seealso cref="YahooFinanceAPI.Models.HistoryPrice" />
    public class HistoryPriceExtStkCode : HistoryPrice
    {
        public string Stkcode;
    }
}
