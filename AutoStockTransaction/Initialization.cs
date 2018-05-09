using System.IO;
using System.Windows.Forms;

namespace AutoStockTransaction
{
    internal static class Initialization
    {
        public static string ServerIP { get; set; }
        public static string User { get; set; }
        public static string Password { get; set; }
        public static string[] ListStkUrl { get; set; }

        public Initialization()
        {
            ReadINI();
        }

        public void ReadINI()
        {
            using (CINI oCINI = new CINI(Path.Combine(Application.StartupPath, "Config.ini")))
            {
                ServerIP = oCINI.GetKeyValue("DB_connection_config", "serverIP");
                User = oCINI.GetKeyValue("DB_connection_config", "user");
                Password = oCINI.GetKeyValue("DB_connection_config", "password");
                ListStkUrl = oCINI.GetKeyValue("Stock_TWSE_Declaration", "stkUrl").Split(';');
            }
        }
    }
}