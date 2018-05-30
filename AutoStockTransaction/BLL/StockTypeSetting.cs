using System;
using System.Collections;

namespace AutoStockTransaction
{
    public static class StockTypeSettings
    {
        public static DateTime StartDate { get; set; }
        public static DateTime EndDate { get; set; }
        public static string Interval { get; set; }
        public static string Indicator { get; set; }

        public enum Indicators
        {
            
        }
    }
}