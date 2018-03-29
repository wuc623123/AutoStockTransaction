using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trady.Core;
using Trady.Analysis;

namespace AutoStockTransaction
{
    class TechAnalysis
    {
        public async Task<List<StkWithCandles>> GetAllStkCandles()
        {
            using (StockEntities se = new StockEntities())
            {
                List<StkWithCandles> listStkWithCandles = new List<StkWithCandles>();
                IQueryable<string> allStkCodeIn﻿StockHistoricalPrice = from shp in se.StockHistoricalPrice
                                                                       select shp.StkCode;
                foreach (string stkCode in allStkCodeInStockHistoricalPrice)
                {
                    var candles = await DBImport(stkCode);

                    listStkWithCandles.Add(new StkWithCandles()
                    {
                        StkCode = stkCode,
                        Candles = candles
                    });

                        
                }
                return listStkWithCandles;
            }


        }

        private async Task<IEnumerable<Candle>> DBImport(string symbol)
        {
            using (StockEntities se = new StockEntities())
            {
                List<Candle> candles = new List<Candle>();
                IEnumerable<Candle> ieCandles = candles;
                IQueryable<StockHistoricalPrice> shps = from shp in se.StockHistoricalPrice
                                                        where shp.StkCode == symbol
                                                        select shp;
                foreach (StockHistoricalPrice shp in shps)
                {
                    candles.Add(new Candle(shp.Date
                        , (decimal)shp.OpenPrice
                        , (decimal)shp.HighPrice
                        , (decimal)shp.LowPrice
                        , (decimal)shp.ClosePrice
                        , (decimal)shp.Volume));
                }
                return ieCandles;
            }
        }
    }
}
