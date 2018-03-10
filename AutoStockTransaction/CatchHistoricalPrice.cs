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
            using (StockEntities se = new StockEntities())
            {
                //刪除所以價格資料
                se.StockHistoricalPrice.Where(shp => true).DeleteFromQuery();
            }
            //取得stkcode差集列表
            List<string> neededUpdateList = await GetAllNeededAddStkCode();
            //取得所有需要新增的股票價格列表
            ConcurrentStack<List<StockHistoricalPrice>> allStkPrice = await GetAllStkPrice(neededUpdateList, progress);
            using (StockEntities se = new StockEntities())
            {

                //新增所有缺少記錄的價格資料
                int index = 0;
                foreach (List<StockHistoricalPrice> priceOfList in allStkPrice)
                {
                    var RptProgress = new ProgressStoreStructure();
                    se.BulkInsert(priceOfList);
                    se.BulkSaveChanges();
                    RptProgress.GetProgressOnWrittingDB = (float)++index / allStkPrice.Count * 100;
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
        private async Task<ConcurrentStack<List<StockHistoricalPrice>>> GetAllStkPrice(List<string> neededUpdateList, IProgress<ProgressStoreStructure> progress)
        {
            ConcurrentStack<List<StockHistoricalPrice>> allStkPrice = new ConcurrentStack<List<StockHistoricalPrice>>();
            await Task.Run(() =>
            {
                Parallel.ForEach(neededUpdateList, new ParallelOptions { MaxDegreeOfParallelism = 3 }, aStk =>
                {
                    var RptState = new ProgressStoreStructure();
                    List<StockHistoricalPrice> aHistoryPriceList = GetHistoricalPrice(aStk, ".TW", DateTime.Now.AddYears(-20), DateTime.Now).GetAwaiter().GetResult();
                    if (aHistoryPriceList.Count < 1)
                    {
                        List<StockHistoricalPrice> tryCatchHistoryPriceListAgain = GetHistoricalPrice(aStk, ".TWO", DateTime.Now.AddYears(-20), DateTime.Now).GetAwaiter().GetResult();
                        if (tryCatchHistoryPriceListAgain.Count < 1)
                        {
                            errorStkAmount++;
                            RptState.ErrorStkCode = $"{aStk}  {errorStkAmount}";
                        }
                        else
                        {
                            RptState.ErrorStkCode = $"{aStk}  {tryCatchHistoryPriceListAgain.Count}";
                            allStkPrice.Push(tryCatchHistoryPriceListAgain);
                        }
                    }
                    else
                    {
                        RptState.ErrorStkCode = $"{aStk}  {aHistoryPriceList.Count}";
                        allStkPrice.Push(aHistoryPriceList);
                    }
                    RptState.GetProgressOnAllPriceLists = (float)allStkPrice.Count / (totalStkAmount - errorStkAmount) * 100;
                    progress.Report(RptState);
                });
                errorStkAmount = 0;
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
        public async Task<List<StockHistoricalPrice>> GetHistoricalPrice(string symbol, string symbolType, DateTime start, DateTime end)
        {
            //first get a valid token from Yahoo Finance
            while (string.IsNullOrEmpty(Token.Cookie) || string.IsNullOrEmpty(Token.Crumb))
            {
                await Token.RefreshAsync().ConfigureAwait(false);
            }
            List<HistoryPrice> hps = await Historical.GetPriceAsync(symbol + symbolType, start, end).ConfigureAwait(false);
            //Historyprice轉換為HistoryPriceExtStkCode類型, 新增StkCode後傳回
            List<StockHistoricalPrice> hpExtList = new List<StockHistoricalPrice>();
            foreach (HistoryPrice hp in hps)
            {
                hpExtList.Add(new StockHistoricalPrice()
                {
                    StkCode = symbol,
                    Date = hp.Date,
                    OpenPrice = hp.Open,
                    HighPrice = hp.High,
                    LowPrice = hp.Low,
                    ClosePrice = hp.Close,
                    AdjustedClosePrice = hp.AdjClose,
                    Volume = hp.Volume
                });
            }
            return hpExtList;
        }
    }
}
