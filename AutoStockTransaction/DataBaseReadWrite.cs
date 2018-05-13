using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoStockTransaction
{
    class DataBaseReadWrite
    {
        public List<StockHistoricalPrice> GetHistoricalPrice(string stkId, DateTime startDate,DateTime endDate)
        {
            if(endDate == null)
            {
                endDate = DateTime.Now.Date;
            }
            using (StockEntities se = new StockEntities())
            {   //取得listbox被選中的股票的歷史價格
                var oneOfStkHistoryPrice = (from oneOfStkPrice in se.StockHistoricalPrice
                                            where oneOfStkPrice.StkCode == stkId
                                            where oneOfStkPrice.Date.CompareTo(startDate) > 0
                                            where oneOfStkPrice.Date.CompareTo(endDate) < 0
                                            select oneOfStkPrice).ToList();
                return oneOfStkHistoryPrice;
            }
        }
        public List<ListedStock> GetStkCodeAndNameWithCat(int Category)
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
    }
}
