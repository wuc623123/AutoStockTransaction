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
            while (string.IsNullOrEmpty(Token.Cookie) || string.IsNullOrEmpty(Token.Crumb))
            {
                Token.RefreshAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            }
            List<HistoryPrice> NASDAQ = Historical.GetPriceAsync("^IXIC", DateTime.Now.AddYears(-1),DateTime.Now).ConfigureAwait(false).GetAwaiter().GetResult();
            foreach(HistoryPrice a in NASDAQ)
            {
                Console.WriteLine($"{a.Date.ToString("MM/dd/yyyy")} {a.Open} {a.High} {a.Low} {a.Close} {a.Volume}");
            }
            Console.ReadKey();
        }
    }
}
