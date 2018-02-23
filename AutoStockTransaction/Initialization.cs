using System.IO;
using System.Windows.Forms;

namespace AutoStockTransaction
{
    internal class Initialization
    {
        private string serverIP;
        private string user;
        private string password;
        private string[] listStkUrl;

        public string ServerIP { get => serverIP; set => serverIP = value; }
        public string User { get => user; set => user = value; }
        public string Password { get => password; set => password = value; }
        public string[] ListStkUrl { get => listStkUrl; set => listStkUrl = value; }

        public Initialization()
        {
            ReadINI();
        }

        public void ReadINI()
        {
            using (CINI oCINI = new CINI(Path.Combine(Application.StartupPath, "Config.ini")))
            {
                serverIP = oCINI.GetKeyValue("DB_connection_config", "serverIP");
                user = oCINI.GetKeyValue("DB_connection_config", "user");
                password = oCINI.GetKeyValue("DB_connection_config", "password");
                listStkUrl = oCINI.GetKeyValue("Stock_TWSE_Declaration", "stkUrl").Split(',');
            }
        }
    }
}