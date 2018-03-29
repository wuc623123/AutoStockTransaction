using System.IO;
using System.Windows.Forms;

namespace AutoStockTransaction
{
    internal class Initialization
    {
        public string ServerIP { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string[] ListStkUrl { get; set; }

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