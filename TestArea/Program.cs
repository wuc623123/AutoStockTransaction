using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trady.Core;
using Trady.Analysis;
using Trady.Importer.Google;
using YahooFinanceAPI.Models;
using Trady.Core.Infrastructure;
using YahooFinanceAPI;

namespace TestArea
{
    class Program
    {
        static void Main()
        {
            //first get a valid token from Yahoo Finance
            //while (string.IsNullOrEmpty(Token.Cookie) || string.IsNullOrEmpty(Token.Crumb))
            //{
            //    Token.RefreshAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            //}
            //List<HistoryPrice> NASDAQ = Historical.GetPriceAsync("^IXIC", DateTime.Now.AddYears(-1),DateTime.Now).ConfigureAwait(false).GetAwaiter().GetResult();
            //foreach(HistoryPrice a in NASDAQ)
            //{
            //    Console.WriteLine($"{a.Date.ToString("MM/dd/yyyy")} {a.Open} {a.High} {a.Low} {a.Close} {a.Volume}");
            //}
            //Console.ReadKey();


            string[] s = {              "自動更新股票歷史價格中...",
                                        "網路取得價格:UpdatePriceListsProgress%",
                                        "寫入資料庫:WrittingDBProgress%"};

            int a = 1;
            int b = 2;
            int c = 3;


            for (int i = 0; i < s.Length; i++)
            {
                s[i] = s[i].Replace("UpdatePriceListsProgress", a.ToString());
                s[i] = s[i].Replace("WrittingDBProgress", "2");
            }
            foreach (var item in s)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }

    }

}
