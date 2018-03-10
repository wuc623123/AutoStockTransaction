using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace AutoStockTransaction
{
    class DBconnection
    {
        string connString;
        SqlConnection conn;

        public DBconnection()
        {
            Initialization init = new Initialization();
            SqlConnectionStringBuilder sqlConBuild = new SqlConnectionStringBuilder
            {
                DataSource = $"{init.ServerIP}",
                UserID = $"{init.User}",
                Password = $"{init.Password}"
            };
            connString = sqlConBuild.ConnectionString;
            conn = new SqlConnection(connString);
        }
        public string Connecting()
        {
            try
            {
                conn.Open();
                return ("成功連線");
            }
            catch (SqlException ex)
            {
                return ($"發現錯誤!代碼:{ex.Number}");
            }
        }
        public SqlConnection GetSqlConnection()
        {
            return conn;
        }
    }
}
