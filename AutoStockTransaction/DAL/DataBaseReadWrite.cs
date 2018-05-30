using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoStockTransaction
{
    public class DataBaseReadWrite
    {
        public List<StockHistoricalPrice> ReadHistoricalPrice(string stkId)
        {
            var startDate = StockTypeSettings.StartDate;
            var endDate = StockTypeSettings.EndDate;
            if (startDate == DateTime.Parse("0001/1/1"))
            {
                startDate = DateTime.Now.AddYears(-1);
            }
            if (endDate == DateTime.Parse("0001/1/1"))
            {
                endDate = DateTime.Now;
            }
            using (StockEntities se = new StockEntities())
            {   //取得listbox被選中的股票的歷史價格
                var oneOfStkHistoryPrice = (from oneOfStkPrice in se.StockHistoricalPrice
                                            where oneOfStkPrice.StkCode == stkId &&
                                            oneOfStkPrice.Date >= startDate && oneOfStkPrice.Date <= endDate
                                            select oneOfStkPrice).ToList();
                return oneOfStkHistoryPrice;
            }
        }

        public List<ListedStock> ReadStkCodeAndNameWithCat(int Category)
        {   //取得category X 股票代碼分類 StockData.StkCategory
            //Stock                     = 1
            //ListedWarrant             = 2
            //TaiwanDepositaryReceipt   = 3
            //RealEstateInvestmentTrust = 4
            //ManageStocks              = 5
            //AssetBasedSecurities      = 6
            //ETF                       = 7
            //SpecialStock              = 8
            using (StockEntities se = new StockEntities())
            { 
                var stkCodeAndNameList = (from stkCodeName in se.ListedStock
                                          where stkCodeName.StkCategory == Category
                                          select stkCodeName).ToList();
                return stkCodeAndNameList;
            }
        }
        public void WriteListedStockToDB(List<ListedStock> listedStock)
        {

        }
    }
}