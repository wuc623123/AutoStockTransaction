namespace AutoStockTransaction
{
    public class StockData
    {
        //股票基礎訊息結構
        public int IntStkCategory { get; set; }
        public string StrStkCode { get; set; }
        public string StrStkName { get; set; }
        public string StrISIN_code { get; set; }
        public string StrSubmitDate { get; set; }
        public string StrMarketNo { get; set; }
        public string StrBelongClass { get; set; }
        public string StrCFI_code { get; set; }

        public enum StkCategory
        {
            Stock = 1,
            ListedWarrant = 2,
            TaiwanDepositaryReceipt = 3,
            RealEstateInvestmentTrust = 4,
            ManageStocks = 5,
            AssetBasedSecurities = 6,
            ETF = 7,
            SpecialStock = 8
        }

        public enum FieldName
        {
            StkCategory = 1,
            StkCode = 2,
            StkName = 3,
            ISIN_code = 4,
            SubmitDate = 5,
            MarketNo = 6,
            BelongClass = 7,
            CFI_code = 8
        }

        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="fieldName">Name of the field.</param>
        public void SetData(object data, int fieldName)
        {
            switch (fieldName)
            {
                case 1:
                    this.IntStkCategory = (int)data;
                    break;

                case 2:
                    this.StrStkCode = (string)data;
                    break;

                case 3:
                    this.StrStkName = (string)data;
                    break;

                case 4:
                    this.StrISIN_code = (string)data;
                    break;

                case 5:
                    this.StrSubmitDate = (string)data;
                    break;

                case 6:
                    this.StrMarketNo = (string)data;
                    break;

                case 7:
                    this.StrBelongClass = (string)data;
                    break;

                case 8:
                    this.StrCFI_code = (string)data;
                    break;
            }
        }
    }
}