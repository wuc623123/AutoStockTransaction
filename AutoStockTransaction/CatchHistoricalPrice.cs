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
            List<ListedStock> neededUpdateList = await GetAllNeededAddStkCode();
            //取得所有需要新增的股票價格列表
            ConcurrentStack<List<HistoryPriceExtStkCode>> allStkPrice = await GetAllStkPrice(neededUpdateList, progress);
            using (StockEntities se = new StockEntities())
            {
                foreach (List<HistoryPriceExtStkCode> priceOfList in allStkPrice)
                {
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
                        se.StockHistoricalPrice.Add(SHP);
                    }
                }
                se.SaveChanges();
            }
        }
        /// <summary>
        /// Gets all needed add STK code.
        /// </summary>
        /// <returns></returns>
        private async Task<List<ListedStock>> GetAllNeededAddStkCode()
        {
            using (StockEntities SE = new StockEntities())
            {
                SE.Configuration.ProxyCreationEnabled = true;
                SE.Configuration.LazyLoadingEnabled = true;
                //取得category = 1所有項目的列表
                //IQueryable<string> stkCodeListInCat1 = from s in SE.ListedStock
                //                                       where s.StkCategory == 1
                //                                       orderby s.StkCode
                //                                       select s.StkCode;
                ////取得股票價格所有項目的列表
                //IQueryable<string> stockPriceList = from a in SE.StockHistoricalPrice
                //                                    select a.StkCode;
                IQueryable<ListedStock> exceptList = from ls in SE.ListedStock
                                                     from shp in SE.StockHistoricalPrice
                                                     where ls.StkCategory == 1
                                                     where ls.StkCode != shp.StkCode
                                                     orderby ls.StkCode
                                                     select ls;
                //取得stkCodeListInCat1除了stockPriceList的差集, 結果為沒有任何價格紀錄需要被新增的集合
                //List<string> exceptList = stkCodeListInCat1.Except(stockPriceList).ToList();
                totalStkAmount = exceptList.Count();
                return exceptList.ToList();
            }
        }
        /// <summary>
        /// Gets all STK price.
        /// </summary>
        /// <param name="neededUpdateList">The needed update list.</param>
        /// <returns></returns>
        private async Task<ConcurrentStack<List<HistoryPriceExtStkCode>>> GetAllStkPrice(List<ListedStock> neededUpdateList, IProgress<ProgressStoreStructure> progress)
        {
            ConcurrentStack<List<HistoryPriceExtStkCode>> allStkPrice = new ConcurrentStack<List<HistoryPriceExtStkCode>>();
            await Task.Run(() =>
            {
                Parallel.ForEach(neededUpdateList, new ParallelOptions { MaxDegreeOfParallelism = 1 }, aStk =>
                {
                    List<HistoryPriceExtStkCode> aHistoryPriceList = GetHistoricalPrice(aStk.StkCode, DateTime.Now.AddMonths(-1), DateTime.Now).GetAwaiter().GetResult();
                    allStkPrice.Push(aHistoryPriceList);
                    var p = new ProgressStoreStructure()
                    {
                        GetProgressOnAllPriceLists = (float)allStkPrice.Count / totalStkAmount * 100
                    };
                    if(aHistoryPriceList.Count < 2)
                    {
                        errorStkAmount += 1;
                        p.ErrorStkCode = $"{aStk.StkCode}   {errorStkAmount}";
                    }
                    progress.Report(p);
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
            List<HistoryPrice> hps = await Historical.GetPriceAsync((symbol + ".TW"), start, end).ConfigureAwait(false);
            //Historyprice轉換成HistoryPriceExtStkCode類型, 新增StkCode後傳回
            List<HistoryPriceExtStkCode> hpExtList = new List<HistoryPriceExtStkCode>();
            foreach (HistoryPrice hp in hps)
            {
                HistoryPriceExtStkCode hpExt = new HistoryPriceExtStkCode()
                {
                    Stkcode = symbol,
                    Date = hp.Date,
                    Open = hp.Open,
                    High = hp.High,
                    Low = hp.Low,
                    Close = hp.Close,
                    AdjClose = hp.AdjClose,
                    Volume = hp.Volume
                };
                hpExtList.Add(hpExt);
            }
            return hpExtList;
        }
    }
    public class HistoryPriceExtStkCode : HistoryPrice
    {
        public string Stkcode;
    }
}
