namespace AutoStockTransaction
{
    public class StockData
    {
        //股票基礎訊息結構
        private int intStkCategory;

        private string strStkCode = null;
        private string strStkName = null;
        private string strISIN_code = null;
        private string strSubmitDate = null;
        private string strMarketNo = null;
        private string strBelongClass = null;
        private string strCFI_code = null;

        public int IntStkCategory { get => intStkCategory; set => intStkCategory = value; }
        public string StrStkCode { get => strStkCode; set => strStkCode = value; }
        public string StrStkName { get => strStkName; set => strStkName = value; }
        public string StrISIN_code { get => strISIN_code; set => strISIN_code = value; }
        public string StrSubmitDate { get => strSubmitDate; set => strSubmitDate = value; }
        public string StrMarketNo { get => strMarketNo; set => strMarketNo = value; }
        public string StrBelongClass { get => strBelongClass; set => strBelongClass = value; }
        public string StrCFI_code { get => strCFI_code; set => strCFI_code = value; }

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
                    this.intStkCategory = (int)data;
                    break;

                case 2:
                    this.strStkCode = (string)data;
                    break;

                case 3:
                    this.strStkName = (string)data;
                    break;

                case 4:
                    this.strISIN_code = (string)data;
                    break;

                case 5:
                    this.strSubmitDate = (string)data;
                    break;

                case 6:
                    this.strMarketNo = (string)data;
                    break;

                case 7:
                    this.strBelongClass = (string)data;
                    break;

                case 8:
                    this.strCFI_code = (string)data;
                    break;
            }
        }
    }
}