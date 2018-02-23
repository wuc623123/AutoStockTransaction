using System;
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
        //private async Task<int> GetAllStockHistoryPrice()
        //{
        //取得DB中StkCategory = 1所有StkCode的集合

        //並使用GetHistoricalPrice($"{StkCode}.TW")取得迴圈中某一股20年的歷史資料集合

        //將歷史集合寫入到DB內
        //}
        /// <summary>
        /// 取得歷史價格的集合List<HistoryPrice>
        /// </summary>
        /// <param name="symbol">傳入範例2330.TW</param>
        /// <returns></returns>
        private async Task<List<HistoryPrice>> GetHistoricalPrice(string symbol)
        {
            //first get a valid token from Yahoo Finance
            while (string.IsNullOrEmpty(Token.Cookie) || string.IsNullOrEmpty(Token.Crumb))
            {
                await Token.RefreshAsync().ConfigureAwait(false);
            }
            List<HistoryPrice> hps = await Historical.GetPriceAsync(symbol, DateTime.Now.AddYears(-20), DateTime.Now).ConfigureAwait(false);
            return hps;
        }

        private async Task GetRawHistoricalPrice(string symbol)
        {
            //first get a valid token from Yahoo Finance
            while (string.IsNullOrEmpty(Token.Cookie) || string.IsNullOrEmpty(Token.Crumb))
            {
                await Token.RefreshAsync().ConfigureAwait(false);
            }
            string csvdata = await Historical.GetRawAsync(symbol, DateTime.Now.AddMonths(-1), DateTime.Now).ConfigureAwait(false);
            //process further

        }
    }
}
