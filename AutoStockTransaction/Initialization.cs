using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace AutoStockTransaction
{
    internal class Initialization
    {
        public string ServerIP { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public List<string> ListStkUrl { get; set; }

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
            }
            ListStkUrl = new List<string>();
            if (File.Exists(Path.Combine(Application.StartupPath, "stock1.htm")))
            {
                ListStkUrl.Add(Path.Combine(Application.StartupPath, "stock1.htm"));
            }
            if (File.Exists(Path.Combine(Application.StartupPath, "stock2.htm")))
            {
                ListStkUrl.Add(Path.Combine(Application.StartupPath, "stock2.htm"));
            }
        }
    }
}