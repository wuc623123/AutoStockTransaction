using System;
using System.Windows.Forms;

namespace AutoStockTransaction
{
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            Z.EntityFramework.Extensions.LicenseManager.AddLicense("73; 100 - RUSS", "0F3F1086D99A2D274D6E5FBB4AF9002D");
        }
    }
}