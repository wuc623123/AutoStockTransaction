//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutoStockTransaction
{
    using System;
    using System.Collections.Generic;
    
    public partial class ListedStock
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ListedStock()
        {
            this.StockHistoricalPrice = new HashSet<StockHistoricalPrice>();
        }
    
        public int StkCategory { get; set; }
        public string StkCode { get; set; }
        public string StkName { get; set; }
        public string ISIN_code { get; set; }
        public string SubmitDate { get; set; }
        public string MarketNo { get; set; }
        public string BelongClass { get; set; }
        public string CFI_code { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockHistoricalPrice> StockHistoricalPrice { get; set; }
    }
}
