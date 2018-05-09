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
            //entity framework extensions3.12.10破解
            string licenseName = "41;100-DZB";//... PRO license name
            string licenseKey = "60200499171A9676A68AC7DBBDA8C0E2";//... PRO license key
            Z.EntityFramework.Extensions.LicenseManager.AddLicense(licenseName, licenseKey);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}